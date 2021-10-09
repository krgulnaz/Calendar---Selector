using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;

namespace NaitonControls
{
    [ToolboxItem(false)]
    public partial class TimeSelectorPopup1 : UserControl
    {
        #region Private variables
        private Color selectedObjectColor = Color.SpringGreen;
        private DateTime selectedDateTime = DateTime.Now;    
        private Button okButton;
        private Button cancelButton;
        private GroupBox minutesGroupBox;
        private DCButton button13;
        private DCButton button14;
        private DCButton button15;
        private DCButton button16;
        private DCButton button17;
        private DCButton button18;
        private DCButton button19;
        private DCButton button20;
        private DCButton button21;
        private DCButton button22;
        private DCButton button23;
        private DCButton button24;
        private GroupBox hoursGroupBox;
        private DCButton button3;
        private DCButton button4;
        private DCButton button5;
        private DCButton button6;
        private DCButton button7;
        private DCButton button8;
        private DCButton button9;
        private DCButton button10;
        private DCButton button11;
        private DCButton button12;
        private DCButton button25;
        private DCButton button26;
        private DCButton button27;
        private DCButton button28;
        private DCButton button29;
        private DCButton button30;
        private DCButton button31;
        private DCButton button32;
        private DCButton button33;
        private DCButton button34;
        private DCButton button35;
        private DCButton button36;
        private DCButton button37;
        private DCButton button38;
        private TextBox hourTextBox;
        private TextBox minuteTextBox;
        private Label currentDateLabel;
        #endregion Private variables

        #region Custom events
        public Action<DateTime> SelectedDateChanged = null;
        protected virtual void OnSelectedDateChanged(DateTime dateTime)
        {
            if (this.SelectedDateChanged != null)
            {
                this.SelectedDateChanged(dateTime);
            }
        }
        #endregion Custom events

        #region Properties

        /// <summary>
        /// The selected date.
        /// </summary>
        [Description("Color of the selected all.")]
        [Category("TimeSelector")]

        public Color SelectedObjectColor
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

        /// <summary>
        /// Property - Color of the text for selected day.
        /// </summary>

        [Description("Selected date time")]
        [Category("TimeSelector")]
        public DateTime SelectedDateTime
        {
            get
            {
                return this.selectedDateTime;
            }
            set
            {
                this.selectedDateTime = value;

                this.ShowSelectedDate();
            }
        }
        #endregion Properties

        private void ShowSelectedDate()
        {
            string hour = $"{this.SelectedDateTime.Hour}";
            if (this.SelectedDateTime.Hour / 10 == 0)
            {
                hour = $"0{this.SelectedDateTime.Hour}";
            }

            string minutes = $"{this.SelectedDateTime.Minute}";
            if (this.SelectedDateTime.Minute / 10 == 0)
            {
                minutes = $"0{SelectedDateTime.Minute}";
            }
            this.currentDateLabel.Text = $"{hour}:{minutes}";
        }

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public TimeSelectorPopup1()
        {
            InitializeComponent();
            this.AttachEventToDCButtons();
        }

        private void AttachEventToDCButtons()
        {
            //this.button3.DoubleClick += this.DCButton_DoubleClick;
            //this.button4.DoubleClick += this.DCButton_DoubleClick;
            //this.button5.DoubleClick += this.DCButton_DoubleClick;
            //this.button6.DoubleClick += this.DCButton_DoubleClick;
            //this.button7.DoubleClick += this.DCButton_DoubleClick;
            //this.button8.DoubleClick += this.DCButton_DoubleClick;
            //this.button9.DoubleClick += this.DCButton_DoubleClick;
            //this.button10.DoubleClick += this.DCButton_DoubleClick;
            //this.button11.DoubleClick += this.DCButton_DoubleClick;
            //this.button12.DoubleClick += this.DCButton_DoubleClick;
            //this.button13.DoubleClick += this.DCButton_DoubleClick;
            //this.button14.DoubleClick += this.DCButton_DoubleClick;
            //this.button15.DoubleClick += this.DCButton_DoubleClick;
            //this.button16.DoubleClick += this.DCButton_DoubleClick;
            //this.button17.DoubleClick += this.DCButton_DoubleClick;
            //this.button18.DoubleClick += this.DCButton_DoubleClick;
            //this.button19.DoubleClick += this.DCButton_DoubleClick;
            //this.button20.DoubleClick += this.DCButton_DoubleClick;
            //this.button21.DoubleClick += this.DCButton_DoubleClick;
            //this.button22.DoubleClick += this.DCButton_DoubleClick;
            //this.button23.DoubleClick += this.DCButton_DoubleClick;
            //this.button24.DoubleClick += this.DCButton_DoubleClick;
            //this.button25.DoubleClick += this.DCButton_DoubleClick;
            //this.button26.DoubleClick += this.DCButton_DoubleClick;
            //this.button27.DoubleClick += this.DCButton_DoubleClick;
            //this.button28.DoubleClick += this.DCButton_DoubleClick;
            //this.button29.DoubleClick += this.DCButton_DoubleClick;
            //this.button30.DoubleClick += this.DCButton_DoubleClick;
            //this.button31.DoubleClick += this.DCButton_DoubleClick;
            //this.button32.DoubleClick += this.DCButton_DoubleClick;
            //this.button33.DoubleClick += this.DCButton_DoubleClick;
            //this.button34.DoubleClick += this.DCButton_DoubleClick;
            //this.button35.DoubleClick += this.DCButton_DoubleClick;
            //this.button36.DoubleClick += this.DCButton_DoubleClick;
            //this.button37.DoubleClick += this.DCButton_DoubleClick;
            //this.button38.DoubleClick += this.DCButton_DoubleClick;
            IEnumerable<Button> buttonsHour = this.hoursGroupBox.Controls.OfType<Button>();
            foreach (Button button in buttonsHour)
            {
                button.DoubleClick += this.DCButton_DoubleClick;
            }
            IEnumerable<Button> buttonsMinute = this.minutesGroupBox.Controls.OfType<Button>();
            foreach (Button button in buttonsMinute)
            {
                button.DoubleClick += this.DCButton_DoubleClick;
            }
        }

        private void DCButton_DoubleClick(object sender, EventArgs e)
        {
            this.okButton.PerformClick();
        }

        #endregion Constructor

        #region Control events
        private void HoursButton_Click(object sender, EventArgs e)
        {

            Button selectedButton = (Button)sender;
            if (Convert.ToInt32(selectedButton.Text) / 10 == 0)
            {
                this.hourTextBox.Text = "0" + selectedButton.Text;
            }
            else
            {
                this.hourTextBox.Text = selectedButton.Text;
            }
        }

        private void MinutesButton_Click(object sender, EventArgs e)
        {
            Button selectedButton = (Button)sender;
            this.minuteTextBox.Text = selectedButton.Text;
        }

        private string previousText = string.Empty;
        private void hourTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            previousText = this.hourTextBox.Text;
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void hourTextBox_TextChanged(object sender, EventArgs e)
        {
            #region paint prior selected hour button with default color
            Button priorSelectedHourButton = this.hoursGroupBox.Controls.OfType<Button>()
                                                               .Where(b => Convert.ToInt32(b.Text) == this.SelectedDateTime.Hour)
                                                               .FirstOrDefault();

            if (priorSelectedHourButton != null)
            {
                priorSelectedHourButton.BackColor = Color.Gainsboro;
            }
            #endregion paint prior selected hour button with default color

            string stringHour = this.hourTextBox.Text;
            if (stringHour.Trim().Length == 0)
            {
                stringHour = "0";
            }
            int hour = Convert.ToInt32(stringHour);

            if (hour > 23)
            {
                this.hourTextBox.Text = this.previousText;
                this.hourTextBox.Select(this.hourTextBox.Text.Length, 0);

                hour = Convert.ToInt32(this.hourTextBox.Text);
            }

            if (this.hourTextBox.Text.Trim().Length > 2)
            {
                int h = Convert.ToInt32(this.hourTextBox.Text);
                if (h >= 0 && h < 10)
                {
                    this.hourTextBox.Text = "0" + h;
                }
                else
                {
                    this.hourTextBox.Text = h.ToString();
                }
                this.hourTextBox.Select(this.hourTextBox.Text.Length, 0);
            }

            Button currentSelectedHourButton = this.hoursGroupBox.Controls.OfType<Button>()
                                                           .Where(b => Convert.ToInt32(b.Text) == hour)
                                                           .FirstOrDefault();

            if (currentSelectedHourButton != null)
            {
                currentSelectedHourButton.BackColor = this.SelectedObjectColor;
            }

            this.SelectedDateTime = new DateTime(this.SelectedDateTime.Year,
                                             this.SelectedDateTime.Month,
                                             this.SelectedDateTime.Day,
                                             hour,
                                             this.SelectedDateTime.Minute,
                                             this.SelectedDateTime.Second);
        }

        private void minuteTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            previousText = this.minuteTextBox.Text;
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void minutesTextBox_TextChanged(object sender, EventArgs e)
        {
            #region paint prior selected minute button with default color
            Button priorSelectedMinuteButton = this.minutesGroupBox.Controls.OfType<Button>()
                                                                   .Where(b => Convert.ToInt32(b.Text) == this.SelectedDateTime.Minute)
                                                                   .FirstOrDefault();
            if (priorSelectedMinuteButton != null)
            {
                priorSelectedMinuteButton.BackColor = Color.Gainsboro;
            }
            #endregion paint prior selected minute button with default color

            string stringMinutes = this.minuteTextBox.Text;
            if (stringMinutes.Trim().Length == 0)
            {
                stringMinutes = "0";
            }

            int minutes = Convert.ToInt32(stringMinutes);

            if (minutes > 59)
            {
                this.minuteTextBox.Text = this.previousText;
                this.minuteTextBox.Select(this.minuteTextBox.Text.Length, 0);

                minutes = Convert.ToInt32(this.minuteTextBox.Text);
            }

            if (this.minuteTextBox.Text.Trim().Length > 2)
            {
                int min = Convert.ToInt32(this.minuteTextBox.Text);
                if (min >= 0 && min < 10)
                {
                    this.minuteTextBox.Text = "0" + min;
                }
                else
                {
                    this.minuteTextBox.Text = min.ToString();
                }
                this.minuteTextBox.Select(this.minuteTextBox.Text.Length, 0);
            }

            Button currentSelectedMinuteButton = this.minutesGroupBox.Controls.OfType<Button>()
                                                                     .Where(b => Convert.ToInt32(b.Text) == minutes)
                                                                     .FirstOrDefault();
            if (currentSelectedMinuteButton != null)
            {
                currentSelectedMinuteButton.BackColor = this.SelectedObjectColor;
            }

            this.SelectedDateTime = new DateTime(this.SelectedDateTime.Year,
                                             this.SelectedDateTime.Month,
                                             this.SelectedDateTime.Day,
                                             this.SelectedDateTime.Hour,
                                             minutes,
                                             this.SelectedDateTime.Second);
        }
        #endregion Control events

        private void TimeSelector_Load(object sender, System.EventArgs e)
        {
            if (this.SelectedDateTime.Hour / 10 == 0)
            {
                this.hourTextBox.Text = "0" + this.SelectedDateTime.Hour.ToString();
            }
            else
            {
                this.hourTextBox.Text = this.SelectedDateTime.Hour.ToString();
            }

            if (this.SelectedDateTime.Minute / 10 == 0)
            {
                this.minuteTextBox.Text = "0" + this.SelectedDateTime.Minute.ToString();
            }
            else
            {
                this.minuteTextBox.Text = this.SelectedDateTime.Minute.ToString();
            }
        }

        public Action<object, EventArgs> okButtonClick = null;
        private void okButton_Click(object sender, EventArgs e)
        {
            this.OnSelectedDateChanged(this.SelectedDateTime);
            if (this.okButtonClick != null)
            {
                this.okButtonClick(sender, e);
            }
        }

        public Action<object, EventArgs> cancelButtonClick = null;
        private void cancelButton_Click(object sender, EventArgs e)
        {
            if (this.cancelButtonClick != null)
            {
                this.cancelButtonClick(sender, e);
            }
        }

        private void okButton_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.OnSelectedDateChanged(this.SelectedDateTime);
                if (this.okButtonClick != null)
                {
                    this.okButtonClick(sender, e);
                }
            }
        }

        private void cancelButton_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.cancelButtonClick != null)
                {
                    this.cancelButtonClick(sender, e);
                }
            }
        }
    }
}
