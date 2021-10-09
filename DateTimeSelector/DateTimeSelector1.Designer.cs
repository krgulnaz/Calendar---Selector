namespace NaitonControls
{
    partial class DateTimeSelector1
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.selectedDateLabel = new System.Windows.Forms.Label();
            this.selectedDateCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // selectedDateLabel
            // 
            this.selectedDateLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.selectedDateLabel.BackColor = System.Drawing.Color.White;
            this.selectedDateLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.selectedDateLabel.Location = new System.Drawing.Point(18, 3);
            this.selectedDateLabel.Name = "selectedDateLabel";
            this.selectedDateLabel.Size = new System.Drawing.Size(119, 14);
            this.selectedDateLabel.TabIndex = 1;
            this.selectedDateLabel.Text = "11 Sep 2019";
            this.selectedDateLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // selectedDateCheckBox
            // 
            this.selectedDateCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.selectedDateCheckBox.AutoSize = true;
            this.selectedDateCheckBox.BackColor = System.Drawing.Color.White;
            this.selectedDateCheckBox.FlatAppearance.BorderColor = System.Drawing.SystemColors.MenuHighlight;
            this.selectedDateCheckBox.Location = new System.Drawing.Point(3, 3);
            this.selectedDateCheckBox.Name = "selectedDateCheckBox";
            this.selectedDateCheckBox.Size = new System.Drawing.Size(15, 14);
            this.selectedDateCheckBox.TabIndex = 2;
            this.selectedDateCheckBox.UseVisualStyleBackColor = false;
            this.selectedDateCheckBox.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // DateTimeSelector1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.selectedDateCheckBox);
            this.Controls.Add(this.selectedDateLabel);
            this.MinimumSize = new System.Drawing.Size(100, 20);
            this.Name = "DateTimeSelector1";
            this.Size = new System.Drawing.Size(140, 20);
            this.Enter += new System.EventHandler(this.DateTimeSelector1_Enter);
            this.Leave += new System.EventHandler(this.DateTimeSelector1_Leave);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label selectedDateLabel;
        public System.Windows.Forms.CheckBox selectedDateCheckBox;
    }
}
