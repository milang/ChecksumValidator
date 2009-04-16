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
            this._fileLabel = new System.Windows.Forms.Label();
            this._file = new System.Windows.Forms.TextBox();
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
            layout.SuspendLayout();
            this.SuspendLayout();
            // 
            // layout
            // 
            layout.AutoSize = true;
            layout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            layout.ColumnCount = 2;
            layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            layout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            layout.Controls.Add(this._fileLabel, 0, 0);
            layout.Controls.Add(this._file, 0, 1);
            layout.Controls.Add(this._fileBrowse, 1, 1);
            layout.Controls.Add(this._checksumLabel, 0, 2);
            layout.Controls.Add(this._checksum, 0, 3);
            layout.Controls.Add(this._checksumPaste, 1, 3);
            layout.Controls.Add(this._resultLabel, 0, 4);
            layout.Controls.Add(this._result, 0, 5);
            layout.Controls.Add(this._resultCompute, 1, 5);
            layout.Location = new System.Drawing.Point(0, 0);
            layout.Margin = new System.Windows.Forms.Padding(0);
            layout.Name = "layout";
            layout.Padding = new System.Windows.Forms.Padding(3, 9, 3, 9);
            layout.RowCount = 6;
            layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            layout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            layout.Size = new System.Drawing.Size(439, 168);
            layout.TabIndex = 0;
            // 
            // _fileLabel
            // 
            this._fileLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._fileLabel.AutoSize = true;
            layout.SetColumnSpan(this._fileLabel, 2);
            this._fileLabel.Location = new System.Drawing.Point(3, 9);
            this._fileLabel.Margin = new System.Windows.Forms.Padding(0, 0, 3, 0);
            this._fileLabel.Name = "_fileLabel";
            this._fileLabel.Size = new System.Drawing.Size(66, 13);
            this._fileLabel.TabIndex = 0;
            this._fileLabel.Text = "File to verify:";
            // 
            // _file
            // 
            this._file.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this._file.Location = new System.Drawing.Point(6, 27);
            this._file.Name = "_file";
            this._file.Size = new System.Drawing.Size(340, 20);
            this._file.TabIndex = 0;
            this._file.TextChanged += new System.EventHandler(this.CheckComputationAvailability);
            // 
            // _fileBrowse
            // 
            this._fileBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this._fileBrowse.AutoSize = true;
            this._fileBrowse.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._fileBrowse.Image = global::Gardian.Utilities.ChecksumValidator.Properties.Resources.Open;
            this._fileBrowse.Location = new System.Drawing.Point(352, 25);
            this._fileBrowse.Name = "_fileBrowse";
            this._fileBrowse.Padding = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this._fileBrowse.Size = new System.Drawing.Size(81, 25);
            this._fileBrowse.TabIndex = 0;
            this._fileBrowse.Text = " Browse";
            this._fileBrowse.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._fileBrowse.UseVisualStyleBackColor = true;
            this._fileBrowse.Click += new System.EventHandler(this.OnBrowse);
            // 
            // _checksumLabel
            // 
            this._checksumLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._checksumLabel.AutoSize = true;
            this._checksumLabel.Location = new System.Drawing.Point(3, 62);
            this._checksumLabel.Margin = new System.Windows.Forms.Padding(0, 9, 3, 0);
            this._checksumLabel.Name = "_checksumLabel";
            this._checksumLabel.Size = new System.Drawing.Size(108, 13);
            this._checksumLabel.TabIndex = 0;
            this._checksumLabel.Text = "Expected Checksum:";
            // 
            // _checksum
            // 
            this._checksum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this._checksum.Location = new System.Drawing.Point(6, 80);
            this._checksum.Name = "_checksum";
            this._checksum.Size = new System.Drawing.Size(340, 20);
            this._checksum.TabIndex = 0;
            this._checksum.TextChanged += new System.EventHandler(this.CheckComputationAvailability);
            // 
            // _checksumPaste
            // 
            this._checksumPaste.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this._checksumPaste.AutoSize = true;
            this._checksumPaste.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._checksumPaste.Image = global::Gardian.Utilities.ChecksumValidator.Properties.Resources.Paste;
            this._checksumPaste.Location = new System.Drawing.Point(352, 78);
            this._checksumPaste.Name = "_checksumPaste";
            this._checksumPaste.Padding = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this._checksumPaste.Size = new System.Drawing.Size(81, 25);
            this._checksumPaste.TabIndex = 0;
            this._checksumPaste.Text = " Paste";
            this._checksumPaste.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._checksumPaste.UseVisualStyleBackColor = true;
            this._checksumPaste.Click += new System.EventHandler(this.OnPaste);
            // 
            // _resultLabel
            // 
            this._resultLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this._resultLabel.AutoSize = true;
            this._resultLabel.Enabled = false;
            this._resultLabel.Location = new System.Drawing.Point(3, 115);
            this._resultLabel.Margin = new System.Windows.Forms.Padding(0, 9, 3, 0);
            this._resultLabel.Name = "_resultLabel";
            this._resultLabel.Size = new System.Drawing.Size(111, 13);
            this._resultLabel.TabIndex = 0;
            this._resultLabel.Text = "Computed Checksum:";
            // 
            // _result
            // 
            this._result.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this._result.BackColor = System.Drawing.SystemColors.Window;
            this._result.Enabled = false;
            this._result.Location = new System.Drawing.Point(6, 133);
            this._result.Name = "_result";
            this._result.ReadOnly = true;
            this._result.Size = new System.Drawing.Size(340, 20);
            this._result.TabIndex = 0;
            // 
            // _resultCompute
            // 
            this._resultCompute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this._resultCompute.AutoSize = true;
            this._resultCompute.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this._resultCompute.Enabled = false;
            this._resultCompute.Image = global::Gardian.Utilities.ChecksumValidator.Properties.Resources.Run;
            this._resultCompute.Location = new System.Drawing.Point(352, 131);
            this._resultCompute.Name = "_resultCompute";
            this._resultCompute.Padding = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this._resultCompute.Size = new System.Drawing.Size(81, 25);
            this._resultCompute.TabIndex = 0;
            this._resultCompute.Text = "Compute";
            this._resultCompute.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
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
            this.ClientSize = new System.Drawing.Size(458, 211);
            this.Controls.Add(layout);
            this.Controls.Add(cancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Checksum verifier";
            layout.ResumeLayout(false);
            layout.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _fileLabel;
        private System.Windows.Forms.TextBox _file;
        private System.Windows.Forms.Label _checksumLabel;
        private System.Windows.Forms.TextBox _checksum;
        private System.Windows.Forms.Button _fileBrowse;
        private System.Windows.Forms.Button _checksumPaste;
        private System.Windows.Forms.Label _resultLabel;
        private System.Windows.Forms.Button _resultCompute;
        private System.Windows.Forms.TextBox _result;
        private System.Windows.Forms.Timer _timer;
    }
}