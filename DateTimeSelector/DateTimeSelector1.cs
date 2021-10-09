using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
//using NLog;

namespace NaitonControls
{
    [DefaultBindingProperty("SelectedDate")]
    [Designer(typeof(FixedHeightUserControlDesigner))]
    public partial class DateTimeSelector1 : UserControl, INotifyPropertyChanged
    {
        //private readonly static Logger logger = LogManager.GetCurrentClassLogger();
        private const int FIXED_HEIGHT = 20;
        //private string _notSetText = Properties.Resources.Common_NotSet;
        private PopupControl.Popup popup;
        public DateTimeSelector1()
        {
            InitializeComponent();
            this.Height = FIXED_HEIGHT;
            this.MouseClick += this.ControlOnMouseClick;
            if (this.HasChildren == true)
            {
                this.AddOnMouseClickHandlerRecursive(Controls);
            }
        }
        protected override void OnLoad(EventArgs e)
        {            
            if (CurrentControlType == ControlType.Date)
            {
                if (IsVisibleCheckBox && !Checked)
                {
                    this.selectedDateLabel.Text = "Not selected";
                }
                else
                {
                    if (this.IsDefaultDate && this.SelectedDate != null || this.IsDefaultDate == false && this.SelectedDate != null)
                    {
                        this.selectedDateLabel.Text = this.DisplayDate.ToString(this.DateStringFormat);
                    }
                    else
                    {
                        this.selectedDateLabel.Text = "Select Date";
                    }
                }
            }
            else
            {
                if (this.IsDefaultDate && this.SelectedDate != null || this.IsDefaultDate == false && this.SelectedDate != null)
                {
                    this.selectedDateLabel.Text = this.DisplayDate.ToString(this.DateStringFormat);
                }
                else
                {
                    this.selectedDateLabel.Text = "Select Date";
                }
            }
            base.OnLoad(e);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)

                PropertyChanged(this, new PropertyChangedEventArgs(info));
        }

        private DateTime datePartOfDateTime = DateTime.Now;
        private DateTime displayDate = DateTime.Now;

        private DateTime DisplayDate
        {
            get
            {
                if (this.CurrentControlType == ControlType.Time)
                {
                    this.displayDate = this.datePartOfDateTime.Date.AddHours(this.displayDate.Hour).AddMinutes(this.displayDate.Minute).AddSeconds(this.displayDate.Second);
                }
                return this.displayDate;
            }
            set
            {
                if (this.CurrentControlType == ControlType.Time)
                {
                    this.displayDate = value;
                    this.datePartOfDateTime = this.displayDate;
                    value = DateTime.Now.Date.AddHours(this.datePartOfDateTime.Hour).AddMinutes(this.datePartOfDateTime.Minute).AddSeconds(this.datePartOfDateTime.Second);
                }

                this.displayDate = value;
                this.selectedDateLabel.Text =
                    CurrentControlType == ControlType.Date && IsVisibleCheckBox && !Checked ? "Not selected" : this.displayDate.ToString(this.DateStringFormat);
                if (CurrentControlType == ControlType.Date)
                {
                    if (IsVisibleCheckBox && !Checked)
                    {
                        this.selectedDateLabel.Text = "Not selected";
                    }
                    else
                    {
                        if (this.IsDefaultDate && this.SelectedDate != null || this.IsDefaultDate == false && this.SelectedDate != null)
                        {
                            this.selectedDateLabel.Text = this.DisplayDate.ToString(this.DateStringFormat);
                        }
                        else
                        {
                            this.selectedDateLabel.Text = "Select Date";
                        }
                    }
                }
                else
                {
                    if (this.IsDefaultDate && this.SelectedDate != null || this.IsDefaultDate == false && this.SelectedDate != null)
                    {
                        this.selectedDateLabel.Text = this.DisplayDate.ToString(this.DateStringFormat);
                    }
                    else
                    {
                        this.selectedDateLabel.Text = "Select Date";
                    }
                }

                NotifyPropertyChanged("SelectedDate");
            }
        }

        private DateTime? selectedDate = null;
        [Category(nameof(DateTimeSelector1))]
        public DateTime? SelectedDate
        {
            get
            {
                return this.selectedDate;
            }
            set
            {
                if (value.HasValue)
                {
                    this.selectedDate = this.DisplayDate;
                    this.DisplayDate = value.Value;                    
                }
                this.selectedDate = value;
                this.selectedDateLabel.Text = value.HasValue ? this.DisplayDate.ToString(DateStringFormat) : "Selected date";
                NotifyPropertyChanged("SelectedDate");
            }
        }

        [Category(nameof(DateTimeSelector1))]
        public bool Checked
        {
            get
            {
                return this.selectedDateCheckBox.Checked;
            }
            set
            {
                this.selectedDateCheckBox.Checked = value;
                this.selectedDateLabel.Enabled = this.selectedDateCheckBox.Checked;
            }
        }

        [Category(nameof(DateTimeSelector1))]
        public bool IsVisibleCheckBox
        {
            get
            {
                return this.selectedDateCheckBox.Visible;
            }
            set
            {
                int x = 4;
                if (value)
                {
                    x = 14;
                }
                this.selectedDateLabel.Location = new Point(x, this.selectedDateLabel.Location.Y);
                this.selectedDateCheckBox.Visible = value;
                ResumeLayout();
            }
        }
        private bool defaultDate = true;

        [Category(nameof(DateTimeSelector1))]
        public bool IsDefaultDate
        {
            get
            {
                return this.defaultDate;
            }
            set
            {
                this.defaultDate = value;
                this.selectedDateLabel.Text = value ? this.DisplayDate.ToString(DateStringFormat) : "Selected date";
                
            }
        }

        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
                this.selectedDateLabel.BackColor = base.BackColor;
                this.selectedDateCheckBox.BackColor = base.BackColor;
            }
        }

        [Category(nameof(DateTimeSelector1))]
        public ContentAlignment TextAlign
        {
            get
            {
                return this.selectedDateLabel.TextAlign;
            }
            set
            {

                this.selectedDateLabel.TextAlign = value;
                ResumeLayout();
            }
        }

        private Color selectedObjectColor = Color.SpringGreen;

        private Color SelectedObjectColor
        {
            get
            {
                return this.selectedObjectColor;
            }
            set
            {
                this.selectedObjectColor = value;
            }
        }

        private string dateStringFormat = "dd MMM yy";

        private string DateStringFormat
        {
            get
            {
                return this.dateStringFormat;
            }
            set
            {
                this.dateStringFormat = value;
                this.selectedDateLabel.Text = CurrentControlType == ControlType.Date && IsVisibleCheckBox && !Checked ? "Not selected" : this.DisplayDate.ToString(this.dateStringFormat);
            }
        }

        private static readonly DateTime defaultMaxDate = new DateTime(9998, 12, 31);
        private DateTime maxDate = defaultMaxDate;

        [Category(nameof(DateTimeSelector1))]
        public DateTime MaxDate
        {
            get
            {
                return this.maxDate;
            }
            set
            {
                if (value > defaultMaxDate)
                {
                    return;
                }
                this.maxDate = value;
            }
        }

        private static readonly DateTime defaultMinDate = new DateTime(1753, 1, 1);
        private DateTime minDate = defaultMinDate;

        [Category(nameof(DateTimeSelector1))]

        public DateTime MinDate
        {
            get
            {
                return this.minDate;
            }
            set
            {
                if (value < defaultMinDate)
                {
                    return;
                }
                this.minDate = value;
            }
        }
        private bool showHourMin = false;

        [Category(nameof(DateTimeSelector1))]
        public bool ShowHourMin
        {
            get
            {
                return this.showHourMin;
            }
            set
            {
                if (this.controlType == ControlType.Time || this.controlType == ControlType.Date)
                {
                    value = false;
                }
                else
                {
                    this.showHourMin = value;
                    this.dateStringFormat = value ? "dd MMM yy   HH:mm" : "dd MMM yy";
                }
            }
        }

        public enum ControlType { Date, DateTime, Time };

        private ControlType controlType;

        [Category(nameof(DateTimeSelector1))]
        public ControlType CurrentControlType
        {
            get
            {
                return this.controlType;
            }
            set
            {
                this.controlType = value;
                if (this.controlType == ControlType.Time)
                {
                    this.dateStringFormat = "HH:mm";
                    this.ShowHourMin = false;
                }
                else
                {
                    this.dateStringFormat = "dd MMM yy";
                }
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.popup.Hide();
            this.count--;
        }

        public void cancelButton_Click(object sender, EventArgs e)
        {
            this.popup.Hide();
            this.count--;
        }

        public Action<DateTime> SelectedDateChanged = null;
        protected void dateSelectorButton_SelectedDateChanged(DateTime dateTime)
        {
            this.SelectedDate = dateTime;
            this.popup.Hide();
            if (this.SelectedDateChanged != null)
            {
                this.SelectedDateChanged(dateTime);
            }
        }
        int count = 0;
        private bool CreateDateSelector(bool isEnterKeyInStack = false)
        {
            this.count++;
            if (this.DisplayDate < MinDate || this.DisplayDate > MaxDate)
            {
                string message = $"The selected date {this.DisplayDate} out of range from 'MinDate' to 'MaxDate'. Would you like to replace given date time with new one within acceptable range?";
                DialogResult dialogResult = MessageBox.Show(this, message, "Info", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                if (dialogResult == DialogResult.OK)
                {
                    this.DisplayDate = DateTime.Now;
                }
                else
                {
                    return false;
                }
            }

            DateSelectorPopup1 dateSelector = new DateSelectorPopup1();
            dateSelector.okButtonClick = this.okButton_Click;
            dateSelector.cancelButtonClick = this.cancelButton_Click;
            dateSelector.SelectedDateTime = this.DisplayDate;
            dateSelector.SelectedObjectColor = this.SelectedObjectColor;
            dateSelector.MaxDate = this.MaxDate;
            dateSelector.MinDate = this.MinDate;
            dateSelector.SelectedDateChangedPopup += dateSelectorButton_SelectedDateChanged;

            this.popup = new PopupControl.Popup(dateSelector);
            if (SystemInformation.IsComboBoxAnimationEnabled)
            {
                this.popup.ShowingAnimation = PopupControl.PopupAnimations.Slide | PopupControl.PopupAnimations.TopToBottom;
                //this.popup.HidingAnimation = PopupControlNext.PopupAnimations.Slide | PopupControlNext.PopupAnimations.BottomToTop;
            }
            else
            {
                this.popup.ShowingAnimation = popup.HidingAnimation = PopupControl.PopupAnimations.None;
            }
            return true;
        }

        private bool CreateDateTimeSelector(bool isEnterKeyInStack = false)
        {
            this.count++;
            if (this.DisplayDate < MinDate || this.DisplayDate > MaxDate)
            {
                string message = $"The selected date {this.DisplayDate} out of range from 'MinDate' to 'MaxDate'. Would you like to replace given date time with new one within acceptable range?";
                DialogResult dialogResult = MessageBox.Show(this, message, "Info", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                if (dialogResult == DialogResult.OK)
                {
                    this.DisplayDate = DateTime.Now;
                }
                else
                {
                    return false;
                }
            }
            DateTimeSelectorPopup1 dateTimeSelector = new DateTimeSelectorPopup1();
            dateTimeSelector.okButtonClick = this.okButton_Click;
            dateTimeSelector.cancelButtonClick = this.cancelButton_Click;
            dateTimeSelector.SelectedDateTime = this.DisplayDate;
            dateTimeSelector.SelectedObjectColor = this.SelectedObjectColor;
            dateTimeSelector.MaxDate = this.MaxDate;
            dateTimeSelector.MinDate = this.MinDate;
            dateTimeSelector.SelectedDateChanged += dateSelectorButton_SelectedDateChanged;

            this.popup = new PopupControl.Popup(dateTimeSelector);
            if (SystemInformation.IsComboBoxAnimationEnabled)
            {
                this.popup.ShowingAnimation = PopupControl.PopupAnimations.Slide | PopupControl.PopupAnimations.TopToBottom;
                this.popup.HidingAnimation = PopupControl.PopupAnimations.Slide | PopupControl.PopupAnimations.BottomToTop;
            }
            else
            {
                this.popup.ShowingAnimation = popup.HidingAnimation = PopupControl.PopupAnimations.None;
            }
            return true;
        }
        private void CreateTimeSelector()
        {
            this.count++;
            TimeSelectorPopup1 timeSelector = new TimeSelectorPopup1();
            timeSelector.okButtonClick = this.okButton_Click;
            timeSelector.cancelButtonClick = this.cancelButton_Click;
            timeSelector.SelectedDateTime = this.DisplayDate;
            timeSelector.SelectedObjectColor = this.SelectedObjectColor;
            timeSelector.SelectedDateChanged += dateSelectorButton_SelectedDateChanged;
            this.popup = new PopupControl.Popup(timeSelector);
            if (SystemInformation.IsComboBoxAnimationEnabled)
            {
                this.popup.ShowingAnimation = PopupControl.PopupAnimations.Slide | PopupControl.PopupAnimations.TopToBottom;
                this.popup.HidingAnimation = PopupControl.PopupAnimations.Slide | PopupControl.PopupAnimations.BottomToTop;
            }
            else
            {
                this.popup.ShowingAnimation = popup.HidingAnimation = PopupControl.PopupAnimations.None;
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if (this.Size.Height != FIXED_HEIGHT)
                this.Size = new Size(this.Size.Width, FIXED_HEIGHT);
            this.selectedDateLabel.Location = new Point((this.Size.Width - this.selectedDateLabel.Width) / 2, this.selectedDateLabel.Location.Y);
            base.OnSizeChanged(e);

            this.selectedDateLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            ResumeLayout();
        }

        private void ControlOnMouseClick(object sender, MouseEventArgs args)
        {
            if (args.Button == MouseButtons.Right)
                return;
            this.CreateAndShow();
        }

        private void CreateAndShow(bool isEnterKeyInStack = false)
        {
            if (this.CurrentControlType == ControlType.DateTime)
            {
                this.CreateDateTimeSelector(isEnterKeyInStack);
            }
            else if (this.CurrentControlType == ControlType.Date)
            {
                this.CreateDateSelector(isEnterKeyInStack);
            }
            else
            {
                this.CreateTimeSelector();
            }

            if (this.IsVisibleCheckBox == true)
            {
                if (this.Checked == false)
                {
                    this.Checked = true;
                    selectedDateLabel.Enabled = true;
                }
            }
            if (this.count % 2 == 1)
            {
                this.popup.Show(this);
            }
        }

        private void AddOnMouseClickHandlerRecursive(IEnumerable controls)
        {
            foreach (Control control in controls)
            {
                if (control is CheckBox)
                {
                    continue;
                }

                control.MouseClick += ControlOnMouseClick;

                if (control.HasChildren)
                    this.AddOnMouseClickHandlerRecursive(control.Controls);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.selectedDateLabel.Enabled = this.selectedDateCheckBox.Checked;
            this.selectedDateLabel.Text =
                CurrentControlType == ControlType.Date && IsVisibleCheckBox && !Checked ? "Not selected" : this.DisplayDate.ToString(this.DateStringFormat); if (CurrentControlType == ControlType.Date) ;
        }

        Color defaultBackColor = SystemColors.Control;
        //BorderStyle defaultBorderStyle = BorderStyle.FixedSingle;
        private void DateTimeSelector1_Enter(object sender, EventArgs e)
        {
            this.defaultBackColor = this.BackColor;
            this.BackColor = SystemColors.ControlLight;
            //this.defaultBorderStyle = this.BorderStyle;
            //this.BorderStyle = BorderStyle.FixedSingle;
        }

        private void DateTimeSelector1_Leave(object sender, EventArgs e)
        {
            this.BackColor = this.defaultBackColor;
            //this.BorderStyle = this.defaultBorderStyle;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                this.CreateAndShow(true);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        public void HidePopup()
        {
            if (this.popup != null)
            {
                this.popup.Hide();
                this.count--;
            }
        }

        private void selectedDateLabel_MouseHover(object sender, EventArgs e)
        {
            this.defaultBackColor = this.BackColor;
            this.BackColor = Color.Gainsboro;
        }

        private void selectedDateLabel_MouseLeave(object sender, EventArgs e)
        {
            this.BackColor = this.defaultBackColor;
        }
    }
}
