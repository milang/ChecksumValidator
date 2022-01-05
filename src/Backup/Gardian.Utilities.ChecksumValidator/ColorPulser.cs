using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Gardian.Utilities.ChecksumValidator
{

    /// <summary>
    /// </summary>
    internal sealed class ColorPulser
    {

        //**************************************************
        //* Construction & destruction
        //**************************************************

        //-------------------------------------------------
        /// <summary>
        /// Create new instance of color pulser, initializing
        /// the color-transitions table.
        /// </summary>
        public ColorPulser(Timer timer, List<object> ongoingPulses, Color startColor, Color endColor, TimeSpan pulseDuration)
        {
            if (timer == null) { throw new ArgumentNullException("timer"); }
            this._timer = timer;
            if (ongoingPulses == null) { throw new ArgumentNullException("ongoingPulses"); }
            this._pulses = ongoingPulses;

            var change = timer.Interval;
            if (change < 10) { throw new ArgumentException("Timer interval should be longer than 10 milliseconds", "timer");}

            var pulse = (int)pulseDuration.TotalMilliseconds;
            if (pulse <= change) { throw new ArgumentException("Pulse duration should be longer than change period", "pulseDuration");}

            var count = pulse / change;
            this._colors = new List<Color>(count * 2);
            var redStep = (decimal)(endColor.R - startColor.R) / count;
            var greenStep = (decimal)(endColor.G - startColor.G) / count;
            var blueStep = (decimal)(endColor.B - startColor.B) / count;
            /*
            var currentRed = (decimal)startColor.R;
            var currentGreen = (decimal)startColor.G;
            var currentBlue = (decimal)startColor.B;
            for (var i = 0; i < count - 1; ++i)
            {
                currentRed += redStep;
                currentGreen += greenStep;
                currentBlue += blueStep;
                this._colors.Add(Color.FromArgb((int)currentRed, (int)currentGreen, (int)currentBlue));
            }
            */
            this._colors.Add(endColor);
            decimal currentRed = endColor.R;
            decimal currentGreen = endColor.G;
            decimal currentBlue = endColor.B;
            for (var i = 0; i < count - 1; ++i)
            {
                currentRed -= redStep;
                currentGreen -= greenStep;
                currentBlue -= blueStep;
                this._colors.Add(Color.FromArgb((int)currentRed, (int)currentGreen, (int)currentBlue));
            }
            this._colors.Add(startColor);

            this._timer.Tick += this.OnTimer;
        }




        //**************************************************
        //* Public interface
        //**************************************************

        //-------------------------------------------------
        /// <summary>
        /// </summary>
        public void Pulse(Control control)
        {
            if (control != null)
            {
                this._pulses.RemoveAll(p => object.ReferenceEquals(((PulseProgress)p).Control, control));
                this._pulses.Add(new PulseProgress { Pulser = this, Control = control, NextColorIndex = 0 });
                this.OnTimer(null, null); // immediately set color[0]
            }
        }




        //**************************************************
        //* Private
        //**************************************************

        //-------------------------------------------------
        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTimer(object sender, EventArgs e)
        {
            if (this._pulses.Count > 0)
            {
                if (!this._timer.Enabled)
                {
                    this._timer.Enabled = true;
                }

                var i = 0;
                while (i < this._pulses.Count)
                {
                    var pulseProgress = (PulseProgress)this._pulses[i];
                    if (object.ReferenceEquals(this, pulseProgress.Pulser))
                    {
                        pulseProgress.Control.BackColor = this._colors[pulseProgress.NextColorIndex++];
                        if (pulseProgress.NextColorIndex >= this._colors.Count)
                        {
                            this._pulses.RemoveAt(i--);
                        }
                    }
                    ++i;
                }
            }
            else
            {
                this._timer.Enabled = false;
            }
        }


        private sealed class PulseProgress
        {
            public ColorPulser Pulser;
            public Control Control;
            public int NextColorIndex;
        }


        private readonly List<Color> _colors;
        private readonly List<object> _pulses;
        private readonly Timer _timer;

    }

}