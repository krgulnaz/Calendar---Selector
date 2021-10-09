namespace TestForm
{
  partial class Form1
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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.button1 = new System.Windows.Forms.Button();
            this.dateTimeSelector11 = new NaitonControls.DateTimeSelector1();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(748, 280);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // dateTimeSelector11
            // 
            this.dateTimeSelector11.BackColor = System.Drawing.Color.White;
            this.dateTimeSelector11.Checked = false;
            this.dateTimeSelector11.CurrentControlType = NaitonControls.DateTimeSelector1.ControlType.Date;
            this.dateTimeSelector11.IsDefaultDate = false;
            this.dateTimeSelector11.IsVisibleCheckBox = false;
            this.dateTimeSelector11.Location = new System.Drawing.Point(375, 82);
            this.dateTimeSelector11.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dateTimeSelector11.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dateTimeSelector11.MinimumSize = new System.Drawing.Size(100, 20);
            this.dateTimeSelector11.Name = "dateTimeSelector11";
            this.dateTimeSelector11.SelectedDate = null;
            this.dateTimeSelector11.ShowHourMin = false;
            this.dateTimeSelector11.Size = new System.Drawing.Size(140, 20);
            this.dateTimeSelector11.TabIndex = 0;
            this.dateTimeSelector11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1083, 594);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dateTimeSelector11);
            this.Name = "Form1";
            this.Text = "Test form";
            this.ResumeLayout(false);

    }


        #endregion

        private NaitonControls.DateTimeSelector1 dateTimeSelector11;
        private System.Windows.Forms.Button button1;
    }
}

