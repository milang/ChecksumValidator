namespace Gardian.Utilities.ChecksumValidator
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.TableLayoutPanel layout;
            System.Windows.Forms.Button cancel;
            System.Windows.Forms.ToolTip tooltop;
            this._methodsContainer = new System.Windows.Forms.FlowLayoutPanel();
            this._methodLabel = new System.Windows.Forms.Label();
            this._methodSha1 = new System.Windows.Forms.RadioButton();
            this._methodMd5 = new System.Windows.Forms.RadioButton();
            this._methodCrc32 = new System.Windows.Forms.RadioButton();
            this._fileLabel = new System.Windows.Forms.Label();
            this._file = new System.Windows.Forms.TextBox();
            this._filePaste = new System.Windows.Forms.Button();
            this._fileBrowse = new System.Windows.Forms.Button();
            this._checksumLabel = new System.Windows.Forms.Label();
            this._checksum = new System.Windows.Forms.TextBox();
            this._checksumPaste = new System.Windows.Forms.Button();
            this._resultLabel = new System.Windows.Forms.Label();
            this._result = new System.Windows.Forms.TextBox();
            this._resultCompute = new System.Windows.Forms.Button();
            this._timer = new System.Windows.Forms.Timer(this.components);
            layout = new System.Windows.Forms.TableLayoutPanel();
            cancel = new System.Windows.Forms.Button();
            tooltop = new System.Windows.Forms.ToolTip(this.components);
            layout.SuspendLayout();
            this._methodsContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // layout
            // 
            layout.AutoSize = true;
            layout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            layout.ColumnCount = 3;
            layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            layout.Controls.Add(this._methodsContainer, 0, 0);
            layout.Controls.Add(this._fileLabel, 0, 1);
            layout.Controls.Add(this._file, 0, 2);
            layout.Controls.Add(this._filePaste, 1, 2);
            layout.Controls.Add(this._fileBrowse, 2, 2);
            layout.Controls.Add(this._checksumLabel, 0, 3);
            layout.Controls.Add(this._checksum, 0, 4);
            layout.Controls.Add(this._checksumPaste, 1, 4);
            layout.Controls.Add(this._resultLabel, 0, 5);
            layout.Controls.Add(this._result, 0, 6);
            layout.Controls.Add(this._resultCompute, 1, 6);
            layout.Location = new System.Drawing.Point(0, 0);
            layout.Margin = new System.Windows.Forms.Padding(0);
            layout.Name = "layout";
            layout.Padding = new System.Windows.Forms.Padding(6, 9, 6, 9);
            layout.RowCount = 7;
            layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            layout.Size = new System.Drawing.Size(478, 200);
            layout.TabIndex = 0;
            // 
            // _methodsContainer
            // 
            this._methodsContainer.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._methodsContainer.AutoSize = true;
            this._methodsContainer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            layout.SetColumnSpan(this._methodsContainer, 2);
            this._methodsContainer.Controls.Add(this._methodLabel);
            this._methodsContainer.Controls.Add(this._methodSha1);
            this._methodsContainer.Controls.Add(this._methodMd5);
            this._methodsContainer.Controls.Add(this._methodCrc32);
            this._methodsContainer.Location = new System.Drawing.Point(6, 9);
            this._methodsContainer.Margin = new System.Windows.Forms.Padding(0);
            this._methodsContainer.Name = "_methodsContainer";
            this._methodsContainer.Size = new System.Drawing.Size(279, 23);
            this._methodsContainer.TabIndex = 0;
            // 
            // _methodLabel
            // 
            this._methodLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._methodLabel.AutoSize = true;
            this._methodLabel.Location = new System.Drawing.Point(0, 5);
            this._methodLabel.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this._methodLabel.Name = "_methodLabel";
            this._methodLabel.Size = new System.Drawing.Size(98, 13);
            this._methodLabel.TabIndex = 0;
            this._methodLabel.Text = "Checksum method:";
            // 
            // _methodSha1
            // 
            this._methodSha1.AutoSize = true;
            this._methodSha1.Checked = true;
            this._methodSha1.Location = new System.Drawing.Point(104, 3);
            this._methodSha1.Name = "_methodSha1";
            this._methodSha1.Size = new System.Drawing.Size(53, 17);
            this._methodSha1.TabIndex = 0;
            this._methodSha1.TabStop = true;
            this._methodSha1.Text = "SHA1";
            this._methodSha1.UseVisualStyleBackColor = true;
            // 
            // _methodMd5
            // 
            this._methodMd5.AutoSize = true;
            this._methodMd5.Location = new System.Drawing.Point(163, 3);
            this._methodMd5.Name = "_methodMd5";
            this._methodMd5.Size = new System.Drawing.Size(48, 17);
            this._methodMd5.TabIndex = 0;
            this._methodMd5.Text = "MD5";
            this._methodMd5.UseVisualStyleBackColor = true;
            // 
            // _methodCrc32
            // 
            this._methodCrc32.AutoSize = true;
            this._methodCrc32.Location = new System.Drawing.Point(217, 3);
            this._methodCrc32.Name = "_methodCrc32";
            this._methodCrc32.Size = new System.Drawing.Size(59, 17);
            this._methodCrc32.TabIndex = 0;
            this._methodCrc32.Text = "CRC32";
            this._methodCrc32.UseVisualStyleBackColor = true;
            // 
            // _fileLabel
            // 
            this._fileLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._fileLabel.AutoSize = true;
            this._fileLabel.Location = new System.Drawing.Point(6, 41);
            this._fileLabel.Margin = new System.Windows.Forms.Padding(0, 9, 3, 0);
            this._fileLabel.Name = "_fileLabel";
            this._fileLabel.Size = new System.Drawing.Size(66, 13);
            this._fileLabel.TabIndex = 0;
            this._fileLabel.Text = "File to verify:";
            // 
            // _file
            // 
            this._file.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this._file.Location = new System.Drawing.Point(9, 59);
            this._file.Name = "_file";
            this._file.Size = new System.Drawing.Size(340, 20);
            this._file.TabIndex = 0;
            this._file.TextChanged += new System.EventHandler(this.CheckComputationAvailability);
            // 
            // _filePaste
            // 
            this._filePaste.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._filePaste.AutoSize = true;
            this._filePaste.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._filePaste.Image = global::Gardian.Utilities.ChecksumValidator.Properties.Resources.Paste;
            this._filePaste.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._filePaste.Location = new System.Drawing.Point(355, 57);
            this._filePaste.Name = "_filePaste";
            this._filePaste.Padding = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this._filePaste.Size = new System.Drawing.Size(28, 25);
            this._filePaste.TabIndex = 0;
            tooltop.SetToolTip(this._filePaste, "Paste clipboard as \"File to verify\"");
            this._filePaste.UseVisualStyleBackColor = true;
            this._filePaste.Click += new System.EventHandler(this.OnFilePaste);
            // 
            // _fileBrowse
            // 
            this._fileBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._fileBrowse.AutoSize = true;
            this._fileBrowse.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._fileBrowse.Image = global::Gardian.Utilities.ChecksumValidator.Properties.Resources.Open;
            this._fileBrowse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._fileBrowse.Location = new System.Drawing.Point(386, 57);
            this._fileBrowse.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this._fileBrowse.Name = "_fileBrowse";
            this._fileBrowse.Padding = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this._fileBrowse.Size = new System.Drawing.Size(83, 25);
            this._fileBrowse.TabIndex = 0;
            this._fileBrowse.Text = " Browse";
            this._fileBrowse.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            tooltop.SetToolTip(this._fileBrowse, "Browse for \"File to verify\"");
            this._fileBrowse.UseVisualStyleBackColor = true;
            this._fileBrowse.Click += new System.EventHandler(this.OnBrowse);
            // 
            // _checksumLabel
            // 
            this._checksumLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._checksumLabel.AutoSize = true;
            this._checksumLabel.Location = new System.Drawing.Point(6, 94);
            this._checksumLabel.Margin = new System.Windows.Forms.Padding(0, 9, 3, 0);
            this._checksumLabel.Name = "_checksumLabel";
            this._checksumLabel.Size = new System.Drawing.Size(107, 13);
            this._checksumLabel.TabIndex = 0;
            this._checksumLabel.Text = "Expected checksum:";
            // 
            // _checksum
            // 
            this._checksum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this._checksum.Location = new System.Drawing.Point(9, 112);
            this._checksum.Name = "_checksum";
            this._checksum.Size = new System.Drawing.Size(340, 20);
            this._checksum.TabIndex = 0;
            // 
            // _checksumPaste
            // 
            this._checksumPaste.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this._checksumPaste.AutoSize = true;
            this._checksumPaste.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            layout.SetColumnSpan(this._checksumPaste, 2);
            this._checksumPaste.Image = global::Gardian.Utilities.ChecksumValidator.Properties.Resources.Paste;
            this._checksumPaste.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._checksumPaste.Location = new System.Drawing.Point(355, 110);
            this._checksumPaste.Name = "_checksumPaste";
            this._checksumPaste.Padding = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this._checksumPaste.Size = new System.Drawing.Size(114, 25);
            this._checksumPaste.TabIndex = 0;
            this._checksumPaste.Text = "Paste";
            this._checksumPaste.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            tooltop.SetToolTip(this._checksumPaste, "Paste clipboard as \"Expected checksum\"");
            this._checksumPaste.UseVisualStyleBackColor = true;
            this._checksumPaste.Click += new System.EventHandler(this.OnChecksumPaste);
            // 
            // _resultLabel
            // 
            this._resultLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._resultLabel.AutoSize = true;
            this._resultLabel.Enabled = false;
            this._resultLabel.Location = new System.Drawing.Point(6, 147);
            this._resultLabel.Margin = new System.Windows.Forms.Padding(0, 9, 3, 0);
            this._resultLabel.Name = "_resultLabel";
            this._resultLabel.Size = new System.Drawing.Size(110, 13);
            this._resultLabel.TabIndex = 0;
            this._resultLabel.Text = "Computed checksum:";
            // 
            // _result
            // 
            this._result.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this._result.BackColor = System.Drawing.SystemColors.Window;
            this._result.Enabled = false;
            this._result.Location = new System.Drawing.Point(9, 165);
            this._result.Name = "_result";
            this._result.ReadOnly = true;
            this._result.Size = new System.Drawing.Size(340, 20);
            this._result.TabIndex = 0;
            // 
            // _resultCompute
            // 
            this._resultCompute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this._resultCompute.AutoSize = true;
            layout.SetColumnSpan(this._resultCompute, 2);
            this._resultCompute.Enabled = false;
            this._resultCompute.Image = global::Gardian.Utilities.ChecksumValidator.Properties.Resources.Run;
            this._resultCompute.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._resultCompute.Location = new System.Drawing.Point(355, 163);
            this._resultCompute.Name = "_resultCompute";
            this._resultCompute.Padding = new System.Windows.Forms.Padding(6, 1, 3, 1);
            this._resultCompute.Size = new System.Drawing.Size(114, 25);
            this._resultCompute.TabIndex = 0;
            this._resultCompute.Text = "Compute";
            this._resultCompute.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            tooltop.SetToolTip(this._resultCompute, "Compute checksum and verify it against the specified expected value");
            this._resultCompute.UseVisualStyleBackColor = true;
            this._resultCompute.Click += new System.EventHandler(this.OnCompute);
            // 
            // cancel
            // 
            cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            cancel.Location = new System.Drawing.Point(0, 0);
            cancel.Name = "cancel";
            cancel.Size = new System.Drawing.Size(75, 23);
            cancel.TabIndex = 0;
            cancel.TabStop = false;
            cancel.Text = "Cancel";
            cancel.UseVisualStyleBackColor = true;
            cancel.Click += new System.EventHandler(this.OnCancel);
            // 
            // _timer
            // 
            this._timer.Interval = 20;
            // 
            // MainForm
            // 
            this.AcceptButton = this._resultCompute;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = cancel;
            this.ClientSize = new System.Drawing.Size(594, 274);
            this.Controls.Add(layout);
            this.Controls.Add(cancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Main dialog";
            layout.ResumeLayout(false);
            layout.PerformLayout();
            this._methodsContainer.ResumeLayout(false);
            this._methodsContainer.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _file;
        private System.Windows.Forms.Label _checksumLabel;
        private System.Windows.Forms.TextBox _checksum;
        private System.Windows.Forms.Button _fileBrowse;
        private System.Windows.Forms.Button _checksumPaste;
        private System.Windows.Forms.Label _resultLabel;
        private System.Windows.Forms.Button _resultCompute;
        private System.Windows.Forms.TextBox _result;
        private System.Windows.Forms.Timer _timer;
        private System.Windows.Forms.Label _fileLabel;
        private System.Windows.Forms.RadioButton _methodSha1;
        private System.Windows.Forms.RadioButton _methodMd5;
        private System.Windows.Forms.FlowLayoutPanel _methodsContainer;
        private System.Windows.Forms.Label _methodLabel;
        private System.Windows.Forms.RadioButton _methodCrc32;
        private System.Windows.Forms.Button _filePaste;
    }
}