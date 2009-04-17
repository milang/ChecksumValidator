using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Gardian.Utilities.ChecksumValidator
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
        /// Create new instance of main form, initializing
        /// controls (designer), fonts, icon and color pulsers
        /// (pulsers are used to temporarily highlight input
        /// controls when something interesting happens).
        /// </summary>
        public MainForm()
        {
            this.InitializeComponent();
            this.Font = SystemFonts.MessageBoxFont;
            this.Icon = Properties.Resources.App;
            var boldFont = new Font(this.Font, FontStyle.Bold);
            this._methodLabel.Font = this._fileLabel.Font = this._checksumLabel.Font = this._resultLabel.Font = boldFont;

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
            this._result.MouseClick += this.OnErrorDetails;
            this.HelpRequested += this.OnErrorDetails;
        }


        //-------------------------------------------------
        /// <summary>
        /// Set initial form focus.
        /// </summary>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this._file.Focus();
        }




        //**************************************************
        //* Private
        //**************************************************

        //-------------------------------------------------
        /// <summary>
        /// </summary>
        private void CheckComputationAvailability(object sender, EventArgs e)
        {
            this._resultCompute.Enabled = !string.IsNullOrEmpty(this._file.Text);
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
                Filter = "Executable files (*.exe;*.msi;*.msu;*.iso)|*.exe;*.msi;*.msu;*.iso|All files (*.*)|*.*",
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
                    this._checksum.Focus();
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
            if (this._resultCompute.Tag != null)
            {
                Checksum.Cancel = true;
                this._resultCompute.Text = "Cancelling";
            }
            else
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

                this._methodsContainer.Enabled = false;
                this._checksumLabel.Enabled = false;
                this._checksum.Enabled = false;
                this._checksumPaste.Enabled = false;
                this._fileLabel.Enabled = false;
                this._file.Enabled = false;
                this._fileBrowse.Enabled = false;
                this._resultLabel.Enabled = true;
                this._result.ForeColor = SystemColors.ControlText;
                this.OnComputeProgress(0m);
                //this._greenPulser.Pulse(this._result);
                this._resultCompute.Text = "Cancel";
                this._resultCompute.Image = Properties.Resources.Stop;
                this._resultCompute.Tag = string.Empty; // make Tag non-null
                this._resultCompute.Focus();

                Checksum.Cancel = false;
                this._error = null;
                Func<string, ChecksumMethod, Action<decimal>, string> computeFunction = Checksum.ComputeChecksum;
                computeFunction.BeginInvoke(
                    fileInfo.FullName,
                    this._methodMd5.Checked
                        ? ChecksumMethod.MD5
                        : this._methodCrc32.Checked
                            ? ChecksumMethod.CRC32
                            : ChecksumMethod.SHA1,
                    this.OnComputeProgress,
                    this.OnComputeDone,
                    computeFunction);
            }
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
                var test = (Func<string, ChecksumMethod, Action<decimal>, string>)result.AsyncState;
                checksum = test.EndInvoke(result);
            }
            catch (Exception ex)
            {
                error = (ex is ThreadInterruptedException
                    ? string.Empty
                    : TraceUtilities.BuildExceptionReport(
                        "Checksum computation failed",
                        string.Empty, null, ex, null).ToString());
                if (error.Length > 0)
                {
                    TraceUtilities.TraceMultilineText(error, "Checksum Error");
                }
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
            this._methodsContainer.Enabled = true;
            this._checksumLabel.Enabled = true;
            this._checksum.Enabled = true;
            this._checksumPaste.Enabled = true;
            this._fileLabel.Enabled = true;
            this._file.Enabled = true;
            this._fileBrowse.Enabled = true;
            this._resultCompute.Text = "Compute";
            this._resultCompute.Image = Properties.Resources.Run;
            this._resultCompute.Tag = null;

            if (error != null)
            {
                if (error.Length == 0)
                {
                    this._result.Text = "Cancelled";
                }
                else
                {
                    this._error = error;
                    this._result.Text = "Failed (click here or press F1 for error details)";
                }
                this._redPulser.Pulse(this._result);
                this._result.ForeColor = Color.Red;
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
                    this._checksum.Focus();
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
                if (this._resultCompute.Enabled)
                {
                    this._resultCompute.Focus();
                }
            }
        }


        private string _error;
        private readonly ColorPulser _greenPulser;
        private readonly ColorPulser _redPulser;

    }

}