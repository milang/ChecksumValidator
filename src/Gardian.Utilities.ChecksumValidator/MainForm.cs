﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

using Pandell.Common;

namespace Sha1Check
{

    /// <summary>
    /// </summary>
    public sealed partial class MainForm : Form
    {

        //**************************************************
        //* Construction & destruction
        //**************************************************

        //-------------------------------------------------
        /// <summary>
        /// </summary>
        public MainForm()
        {
            this.InitializeComponent();
            this.Font = SystemFonts.MessageBoxFont;

            var ongoingPulses = new List<object>();
            this._greenPulser = new ColorPulser(
                this._timer,
                ongoingPulses,
                SystemColors.Window,
                Color.PaleGreen, //SystemColors.Info,
                TimeSpan.FromMilliseconds(250));
            this._redPulser = new ColorPulser(
                this._timer,
                ongoingPulses,
                SystemColors.Window,
                Color.MistyRose,
                TimeSpan.FromMilliseconds(250));
            this._result.HelpRequested += this.OnErrorDetails;
            this._result.MouseClick += this.OnErrorDetails;
        }




        //**************************************************
        //* Private
        //**************************************************

        //-------------------------------------------------
        /// <summary>
        /// </summary>
        private void CheckComputationAvailability(object sender, EventArgs e)
        {
            var enabled = !string.IsNullOrEmpty(this._file.Text) && !string.IsNullOrEmpty(this._checksum.Text);
            this._resultCompute.Enabled = enabled;
        }


        //-------------------------------------------------
        /// <summary>
        /// </summary>
        private void OnBrowse(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog
            {
                CheckFileExists = false,
                CheckPathExists = false,
                DereferenceLinks = true,
                Multiselect = false,
                Title = "Select file to compute checksum for",
                Filter = "Executable files (*.exe;*.msi;*.msu)|*.exe;*.msi;*.msu|All files (*.*)|*.*",
            })
            {
                if (!string.IsNullOrEmpty(this._file.Text))
                {
                    try
                    {
                        dlg.InitialDirectory = Path.GetDirectoryName(this._file.Text);
                    }
                    catch (ArgumentException) {}
                }
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    this._file.Text = dlg.FileName;
                    this._greenPulser.Pulse(this._file);
                    this.CheckComputationAvailability(null, null);
                }
            }
        }


        //-------------------------------------------------
        /// <summary>
        /// </summary>
        private void OnCancel(object sender, EventArgs e)
        {
            this.Close();
        }


        //-------------------------------------------------
        /// <summary>
        /// </summary>
        private void OnCompute(object sender, EventArgs e)
        {
            this._result.Enabled = true;

            FileInfo fileInfo = null;
            try { fileInfo = new FileInfo(this._file.Text); } catch (ArgumentException) {}
            if (fileInfo == null || !fileInfo.Exists)
            {
                this._file.Focus();
                //this._file.Select(0, this._file.Text.Length);
                this._result.ForeColor = Color.Red;
                this._result.Text = "Specified file does not exist or is not readable";

                this._redPulser.Pulse(this._file);
                this._redPulser.Pulse(this._result);
                return;
            }

            this._checksumLabel.Enabled = false;
            this._checksum.Enabled = false;
            this._checksumPaste.Enabled = false;
            this._fileLabel.Enabled = false;
            this._file.Enabled = false;
            this._fileBrowse.Enabled = false;
            this._resultLabel.Enabled = true;
            this._result.Focus();
            this._result.ForeColor = SystemColors.ControlText;
            this.OnComputeProgress(0m);
            this._greenPulser.Pulse(this._result);
            this._resultCompute.Enabled = false;

            this._error = null;
            Func<string, Action<decimal>, string> computeFunction = Checksum.ComputeSha1;
            computeFunction.BeginInvoke(fileInfo.FullName, this.OnComputeProgress, this.OnComputeDone, computeFunction);
        }


        //-------------------------------------------------
        /// <summary>
        /// </summary>
        private void OnComputeDone(IAsyncResult result)
        {
            string checksum = null;
            string error = null;
            try
            {
                var test = (Func<string, Action<decimal>, string>)result.AsyncState;
                checksum = test.EndInvoke(result);
            }
            catch (Exception ex)
            {
                error = Utilities.BuildExceptionReport(
                    "Checksum computation failed",
                    string.Empty, null, ex, null).ToString();
                Utilities.TraceMultilineText(error, "Checksum Error");
            }

            this.BeginInvoke(new Action<string, string>(this.OnComputeReportResults), checksum, error); // marshall to the UI thread
        }


        //-------------------------------------------------
        /// <summary>
        /// Computation is still going on, 
        /// </summary>
        private void OnComputeProgress(decimal percentage)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<decimal>(this.OnComputeProgress), percentage); // marshall to the UI thread
            }
            else
            {
                this._result.Text = string.Format(
                    CultureInfo.InvariantCulture,
                    "Please wait... ({0:0.0%} complete)",
                    percentage);
            }
        }


        //-------------------------------------------------
        /// <summary>
        /// </summary>
        private void OnComputeReportResults(string checksum, string error)
        {
            this._checksumLabel.Enabled = true;
            this._checksum.Enabled = true;
            this._checksumPaste.Enabled = true;
            this._fileLabel.Enabled = true;
            this._file.Enabled = true;
            this._fileBrowse.Enabled = true;
            this._resultCompute.Enabled = true;

            if (!string.IsNullOrEmpty(error))
            {
                this._error = error;
                this._result.Text = "Failed (click here or press F1 for error details)";
                this._result.ForeColor = Color.Red;

                this._redPulser.Pulse(this._result);
            }
            else
            {
                var identical = StringComparer.OrdinalIgnoreCase.Equals(checksum, this._checksum.Text.Trim());
                this._result.Text = string.Concat(
                        checksum ?? "(no results)",
                        " - ",
                        identical ? "OK" : "INVALID");
                this._result.ForeColor = identical ? Color.Green : Color.Red;
                if (identical)
                {
                    this._greenPulser.Pulse(this._checksum);
                    this._greenPulser.Pulse(this._result);
                }
                else
                {
                    this._redPulser.Pulse(this._checksum);
                    this._redPulser.Pulse(this._result);
                }
            }
        }


        //-------------------------------------------------
        /// <summary>
        /// </summary>
        private void OnErrorDetails(object sender, EventArgs e)
        {
            if (this._error != null)
            {
                MessageBox.Show(
                    this,
                    this._error,
                    "SHA1 Computation Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }


        //-------------------------------------------------
        /// <summary>
        /// </summary>
        private void OnPaste(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText())
            {
                this._checksum.Text = Clipboard.GetText();
                this._greenPulser.Pulse(this._checksum);
                this.CheckComputationAvailability(null, null);
            }
        }


        private string _error;
        private readonly ColorPulser _greenPulser;
        private readonly ColorPulser _redPulser;

    }

}