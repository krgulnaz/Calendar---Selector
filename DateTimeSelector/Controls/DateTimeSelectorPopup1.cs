using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;

namespace NaitonControls
{
    [ToolboxItem(false)]
    public partial class DateTimeSelectorPopup1 : UserControl
    {
        #region Private DS variables
        private PictureBox dateSelectorPictureBox;
        private DateTime selectedDateTime = DateTime.Now;
        private bool weekendsDarker = false;
        private bool displayWeekendsDarker = false;

        private Color gridColor = SystemColors.Control;
        private Color headerColor = SystemColors.Control;

        private Color activeMonthColor = Color.Gainsboro;
        private Color inactiveMonthColor = SystemColors.ControlLight;

        private Color selectedDayFontColor = Color.Black;
        private Color selectedObjectColor = Color.SpringGreen;
        private Color nonselectedDayFontColor = Color.Gray;

        private Color weekendColor = Color.Red;

        private Font dayFont = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular);
        private Font headerFont = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular);

        private Rectangle[,] rects;
        private Rectangle[] rectDays;

        private string[] strDays = new string[7] { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };
        private string[] strAbbrDays = new string[7] { "mo", "tu", "we", "th", "fr", "sa", "su" };
        private string[] strMonths = new string[12] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

        private bool bDesign = true;

        private SolidBrush headerBrush;
        private SolidBrush activeMonthBrush;
        private SolidBrush inactiveMonthBrush;
        private SolidBrush selectedDayBrush;
        private SolidBrush selectedDayFontBrush;
        private SolidBrush selectedMonthBrush;
        private SolidBrush nonselectedDayFontBrush;
        private SolidBrush weekendBrush;
        private StringFormat stringFormat;
        private GroupBox monthGroupBox;
        private DCButton decButton;
        private DCButton junButton;
        private DCButton novButton;
        private DCButton mayButton;
        private DCButton octButton;
        private DCButton aprButton;
        private DCButton sepButton;
        private DCButton marButton;
        private DCButton augButton;
        private DCButton febButton;
        private DCButton julButton;
        private DCButton janButton;
        private GroupBox yearGroupBox;
        private Button prevButton;
        private Button nextButton;
        private DCButton fourthButtonYear;
        private DCButton thirdButtonYear;
        private DCButton secondYearButton;
        private DCButton firstYearButton;
        private Button okButton;
        private Button cancelButton;
        private Label currentDateLabel;
        private ImageList imageList1;
        private GroupBox groupBox1;
        private GroupBox hoursGroupBox;
        private TextBox hourTextBox;
        private DCButton hourButton23;
        private DCButton hourButton22;
        private DCButton hourButton19;
        private DCButton hourButton18;
        private DCButton hourButton15;
        private DCButton hourButton14;
        private DCButton hourButton11;
        private DCButton hourButton10;
        private DCButton hourButton7;
        private DCButton hourButton6;
        private DCButton hourButton3;
        private DCButton hourButton2;
        private DCButton hourButton21;
        private DCButton hourButton20;
        private DCButton hourButton17;
        private DCButton hourButton16;
        private DCButton hourButton13;
        private DCButton hourButton12;
        private DCButton hourButton9;
        private DCButton hourButton8;
        private DCButton hourButton5;
        private DCButton hourButton4;
        private DCButton hourButton1;
        private DCButton hourButton0;
        private GroupBox minutesGroupBox;
        private TextBox minutesTextBox;
        private DCButton minuteButton55;
        private DCButton minuteButton50;
        private DCButton minuteButton45;
        private DCButton minuteButton40;
        private DCButton minuteButton35;
        private DCButton minuteButton30;
        private DCButton minuteButton25;
        private DCButton minuteButton20;
        private DCButton minuteButton15;
        private DCButton minuteButton10;
        private DCButton minuteButton05;
        private DCButton minuteButton00;

        private DateTime[,] arrDates;

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

        [Description("The selected date.")]
        [Category("DateTimeSelector")]
        public DateTime SelectedDateTime
        {
            get
            {
                return selectedDateTime;
            }
            set
            {
                selectedDateTime = value;
                this.ShowSelectedDate();
            }
        }

        [Description("Determines whether or not the grid is drawn.")]
        [Category("DateTimeSelector")]
        public Color SelectedObjectColor
        {
            get
            {
                return selectedObjectColor;
            }
            set
            {
                selectedObjectColor = value;
            }
        }

        /// <summary>
        /// Property - Color of the text for selected day.
        /// </summary>
        [Description("Weekends darker")]
        [Category("DateTimeSelector")]
        private bool WeekendsDarker
        {
            get
            {
                return this.displayWeekendsDarker;
            }
            set
            {
                this.displayWeekendsDarker = value;
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
                minutes = $"0{this.SelectedDateTime.Minute}";
            }

            this.currentDateLabel.Text = $"{this.SelectedDateTime.DayOfWeek} {this.SelectedDateTime.Day}, {this.strMonths[this.SelectedDateTime.Month - 1]} {this.SelectedDateTime.Year}  {hour}:{minutes}";
        }

        private DateTime maxDate = new DateTime(9998, 12, 31);

        public DateTime MaxDate
        {
            get
            {
                return this.maxDate;
            }
            set
            {
                this.maxDate = value;
            }
        }

        private DateTime minDate = new DateTime(1753, 1, 1);

        public DateTime MinDate
        {
            get
            {
                return this.minDate;
            }
            set
            {
                this.minDate = value;
            }
        }
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public DateTimeSelectorPopup1()
        {
            // This call is required by the Windows.Forms Form Designer.
            InitializeComponent();
            this.AttachEventToDCButtons();
        }

        private void AttachEventToDCButtons()
        {
            //this.decButton.DoubleClick += this.DCButton_DoubleClick;

            //this.novButton.DoubleClick += this.DCButton_DoubleClick;
            //this.octButton.DoubleClick += this.DCButton_DoubleClick;
            //this.sepButton.DoubleClick += this.DCButton_DoubleClick;
            //this.augButton.DoubleClick += this.DCButton_DoubleClick;
            //this.julButton.DoubleClick += this.DCButton_DoubleClick;
            //this.junButton.DoubleClick += this.DCButton_DoubleClick;
            //this.mayButton.DoubleClick += this.DCButton_DoubleClick;
            //this.aprButton.DoubleClick += this.DCButton_DoubleClick;
            //this.marButton.DoubleClick += this.DCButton_DoubleClick;
            //this.febButton.DoubleClick += this.DCButton_DoubleClick;
            //this.janButton.DoubleClick += this.DCButton_DoubleClick;

            //this.firstYearButton.DoubleClick += this.DCButton_DoubleClick;
            //this.secondYearButton.DoubleClick += this.DCButton_DoubleClick;
            //this.thirdButtonYear.DoubleClick += this.DCButton_DoubleClick;
            //this.fourthButtonYear.DoubleClick += this.DCButton_DoubleClick;

            //this.hourButton0.DoubleClick += this.DCButton_DoubleClick;
            //this.hourButton1.DoubleClick += this.DCButton_DoubleClick;
            //this.hourButton2.DoubleClick += this.DCButton_DoubleClick;
            //this.hourButton3.DoubleClick += this.DCButton_DoubleClick;
            //this.hourButton4.DoubleClick += this.DCButton_DoubleClick;
            //this.hourButton5.DoubleClick += this.DCButton_DoubleClick;
            //this.hourButton6.DoubleClick += this.DCButton_DoubleClick;
            //this.hourButton7.DoubleClick += this.DCButton_DoubleClick;
            //this.hourButton8.DoubleClick += this.DCButton_DoubleClick;
            //this.hourButton9.DoubleClick += this.DCButton_DoubleClick;
            //this.hourButton10.DoubleClick += this.DCButton_DoubleClick;
            //this.hourButton11.DoubleClick += this.DCButton_DoubleClick;
            //this.hourButton12.DoubleClick += this.DCButton_DoubleClick;
            //this.hourButton13.DoubleClick += this.DCButton_DoubleClick;
            //this.hourButton14.DoubleClick += this.DCButton_DoubleClick;
            //this.hourButton15.DoubleClick += this.DCButton_DoubleClick;
            //this.hourButton16.DoubleClick += this.DCButton_DoubleClick;
            //this.hourButton17.DoubleClick += this.DCButton_DoubleClick;
            //this.hourButton18.DoubleClick += this.DCButton_DoubleClick;
            //this.hourButton19.DoubleClick += this.DCButton_DoubleClick;
            //this.hourButton20.DoubleClick += this.DCButton_DoubleClick;
            //this.hourButton21.DoubleClick += this.DCButton_DoubleClick;
            //this.hourButton22.DoubleClick += this.DCButton_DoubleClick;
            //this.hourButton23.DoubleClick += this.DCButton_DoubleClick;


            //this.minuteButton00.DoubleClick += this.DCButton_DoubleClick;
            //this.minuteButton05.DoubleClick += this.DCButton_DoubleClick;
            //this.minuteButton10.DoubleClick += this.DCButton_DoubleClick;
            //this.minuteButton15.DoubleClick += this.DCButton_DoubleClick;
            //this.minuteButton20.DoubleClick += this.DCButton_DoubleClick;
            //this.minuteButton25.DoubleClick += this.DCButton_DoubleClick;
            //this.minuteButton30.DoubleClick += this.DCButton_DoubleClick;
            //this.minuteButton35.DoubleClick += this.DCButton_DoubleClick;
            //this.minuteButton40.DoubleClick += this.DCButton_DoubleClick;
            //this.minuteButton45.DoubleClick += this.DCButton_DoubleClick;
            //this.minuteButton50.DoubleClick += this.DCButton_DoubleClick;
            //this.minuteButton55.DoubleClick += this.DCButton_DoubleClick;

            IEnumerable<Button> buttonsYear = this.yearGroupBox.Controls.OfType<Button>();
            foreach (Button button in buttonsYear)
            {
                button.DoubleClick += this.DCButton_DoubleClick;
            }
            IEnumerable<Button> buttonsMonth = this.monthGroupBox.Controls.OfType<Button>();
            foreach (Button button in buttonsMonth)
            {
                button.DoubleClick += this.DCButton_DoubleClick;
            }
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
        private void YearButton_Click(object sender, EventArgs e)
        {
            Button priorSelectedButton = this.yearGroupBox.Controls.OfType<Button>()
                                                     .Where(b => b.Text.Trim() != string.Empty &&
                                                                 Convert.ToInt32(b.Text) == this.SelectedDateTime.Year)
                                                     .FirstOrDefault();           
            Button selectedButton = (Button)sender;

            int day = this.SelectedDateTime.Day;
            int month = this.SelectedDateTime.Month;
            int year = Convert.ToInt32(selectedButton.Text);

            DateTime tempDateTime = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            if (day > tempDateTime.Day)
            {
                day = tempDateTime.Day;
            }
            DateTime selectedDate = new DateTime(year, month, day, this.SelectedDateTime.Hour, SelectedDateTime.Minute, this.SelectedDateTime.Second);
            if (selectedDate <= MaxDate && selectedDate >= MinDate)
            {
                if (priorSelectedButton != null)
                {
                    priorSelectedButton.BackColor = Color.Gainsboro;
                }
                selectedButton.BackColor = SelectedObjectColor;

                this.SelectedDateTime = new DateTime(year, month, day, this.SelectedDateTime.Hour, SelectedDateTime.Minute, this.SelectedDateTime.Second);
                this.dateSelectorPictureBox.Invalidate();
                this.GetWeekNumber(this.SelectedDateTime);
            }
        }

        private void monthsButton_Click(object sender, EventArgs e)
        {
            Button priorSelectedButton = this.monthGroupBox.Controls.OfType<Button>()
                                                           .Where(b => Convert.ToInt32(b.Tag) == this.SelectedDateTime.Month)
                                                           .FirstOrDefault();

            Button selectedButton = (Button)sender;

            int day = this.SelectedDateTime.Day;
            int month = Convert.ToInt32(selectedButton.Tag);
            int year = this.SelectedDateTime.Year;

            DateTime tempDateTime = new DateTime(year, month,
                                    DateTime.DaysInMonth(year, month));
            if (day > tempDateTime.Day)
            {
                day = tempDateTime.Day;
            }
            DateTime selectedDate = new DateTime(year,
                                       month,
                                       day,
                                       this.SelectedDateTime.Hour,
                                       this.SelectedDateTime.Minute,
                                       this.SelectedDateTime.Second); ;
            if (selectedDate <= MaxDate && selectedDate >= MinDate)
            {
                priorSelectedButton.BackColor = Color.Gainsboro;
                selectedButton.BackColor = this.SelectedObjectColor;

                this.SelectedDateTime = new DateTime(year,
                                       month,
                                       day,
                                       this.SelectedDateTime.Hour,
                                       this.SelectedDateTime.Minute,
                                       this.SelectedDateTime.Second);
                this.dateSelectorPictureBox.Invalidate();
                this.GetWeekNumber(this.SelectedDateTime);               
            }
        }

        private void nextButton_Click(object sender, EventArgs e)
        {
            int year = Convert.ToInt32(firstYearButton.Text) + 4;
            if (year >= MaxDate.Year)
            {
                return;
            }
            IEnumerable<Button> buttons = this.yearGroupBox.Controls
                                        .OfType<Button>();
            foreach (Button button in buttons)
            {
                button.BackColor = Color.Gainsboro;
            }

            this.firstYearButton.Text = Convert.ToString(year);
            this.secondYearButton.Text = Convert.ToString(year + 1);
            this.thirdButtonYear.Text = Convert.ToString(year + 2);
            this.fourthButtonYear.Text = Convert.ToString(year + 3);

            Button selectedButton = this.yearGroupBox.Controls
                                                   .OfType<Button>()
                                                   .Where(b => b.Text.Trim() != string.Empty &&
                                                               Convert.ToInt32(b.Text) == this.SelectedDateTime.Year)
                                                   .FirstOrDefault();
            if (selectedButton != null)
            {
                selectedButton.BackColor = this.SelectedObjectColor;
            }
        }

        private void prevButton_Click(object sender, EventArgs e)
        {
            //IEnumerable<Button> buttons = this.yearGroupBox.Controls
            //                                  .OfType<Button>();
            //foreach (Button button in buttons)
            //{
            //  button.BackColor = Color.Gainsboro;
            //}

            //int year = Convert.ToInt32(this.firstYearButton.Text) - 4;
            //this.firstYearButton.Text = Convert.ToString(year);
            //this.secondYearButton.Text = Convert.ToString(year + 1);
            //this.thirdButtonYear.Text = Convert.ToString(year + 2);
            //this.fourthButtonYear.Text = Convert.ToString(year + 3);

            //Button selectedButton = this.yearGroupBox.Controls
            //                                         .OfType<Button>()
            //                                         .Where(b => b.Text.Trim() != string.Empty &&
            //                                                     Convert.ToInt32(b.Text) == this.SelectedDateTime.Year)
            //                                         .FirstOrDefault();
            //if (selectedButton != null)
            //{
            //  selectedButton.BackColor = this.SelectedObjectColor;
            //}
            int year = Convert.ToInt32(firstYearButton.Text) - 4;

            if (year < MinDate.Year)
            {
                return;
            }

            if (year >= MinDate.Year - 2)
            {
                IEnumerable<Button> buttons = this.yearGroupBox.Controls
                                          .OfType<Button>()
                                          .Where(b => b.Text.Trim() != string.Empty && Convert.ToInt32(b.Text) == this.SelectedDateTime.Year);
                foreach (Button button in buttons)
                {
                    button.BackColor = Color.Gainsboro;
                }
                if (year == MinDate.Year - 2 || year == MinDate.Year - 1)
                {
                    firstYearButton.Text = Convert.ToString(MinDate.Year);
                    secondYearButton.Text = Convert.ToString(MinDate.Year + 1);
                    thirdButtonYear.Text = Convert.ToString(MinDate.Year + 2);
                    fourthButtonYear.Text = Convert.ToString(MinDate.Year + 3);

                }

                else
                {
                    firstYearButton.Text = Convert.ToString(year);
                    secondYearButton.Text = Convert.ToString(year + 1);
                    thirdButtonYear.Text = Convert.ToString(year + 2);
                    fourthButtonYear.Text = Convert.ToString(year + 3);
                }
                Button selectedButton = this.yearGroupBox.Controls
                                                       .OfType<Button>()
                                                       .Where(b => b.Text.Trim() != string.Empty && Convert.ToInt32(b.Text) == this.SelectedDateTime.Year)
                                                       .FirstOrDefault();
                if (selectedButton != null)
                {
                    selectedButton.BackColor = SelectedObjectColor;
                }
            }

        }

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
            dateSelectorPictureBox.Invalidate();
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

        private void MinutesButton_Click(object sender, EventArgs e)
        {
            Button selectedButton = (Button)sender;
            this.minutesTextBox.Text = selectedButton.Text;
            dateSelectorPictureBox.Invalidate();
        }

        private void minutesTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            previousText = this.minutesTextBox.Text;
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

            string stringMinutes = this.minutesTextBox.Text;
            if (stringMinutes.Trim().Length == 0)
            {
                stringMinutes = "0";
            }

            int minutes = Convert.ToInt32(stringMinutes);

            if (minutes > 59)
            {
                this.minutesTextBox.Text = this.previousText;
                this.minutesTextBox.Select(this.minutesTextBox.Text.Length, 0);

                minutes = Convert.ToInt32(this.minutesTextBox.Text);
            }

            if (this.minutesTextBox.Text.Trim().Length > 2)
            {
                int min = Convert.ToInt32(this.minutesTextBox.Text);
                if (min >= 0 && min < 10)
                {
                    this.minutesTextBox.Text = "0" + min;
                }
                else
                {
                    this.minutesTextBox.Text = min.ToString();
                }
                this.minutesTextBox.Select(this.minutesTextBox.Text.Length, 0);
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

        private void dateSelector_SizeChanged(object sender, System.EventArgs e)
        {
            rects = CreateGrid(dateSelectorPictureBox.Width, dateSelectorPictureBox.Height);
            dateSelectorPictureBox.Invalidate();
        }

        private void dateSelector_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            for (int line = 0; line < 6; line++)
            {
                for (int column = 0; column < 7; column++)
                {
                    if (this.rects[column, line].Contains(e.X, e.Y))
                    {
                        if (this.arrDates[column, line] != DateTime.MinValue && this.arrDates[column, line] <= MaxDate)
                        {
                            if (this.arrDates[column, line] != DateTime.MinValue && this.arrDates[column, line] >= MinDate)
                            {
                                this.SelectedDateTime = new DateTime(this.arrDates[column, line].Year,
                                                                     this.arrDates[column, line].Month,
                                                                     this.arrDates[column, line].Day,
                                                                     this.SelectedDateTime.Hour,
                                                                     this.SelectedDateTime.Minute,
                                                                     this.SelectedDateTime.Second);
                                this.GetWeekNumber(this.SelectedDateTime);
                                this.InitializeMonthButtons();
                                this.InitializeYearButtons();
                                this.ShowSelectedDate();
                            }
                        }
                    }
                }
            }
            this.dateSelectorPictureBox.Invalidate();
        }

        private void dateSelector_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            if (!bDesign)
            {
                //the calendar is created as a 7 x 6 grid drawn onto the picture box
                //the data to be displayed in the calendar is stored in a 7 x 6 array of arrays
                //update
                this.FillDates2(this.SelectedDateTime);
                this.CreateGraphicObjects();

                Graphics g = e.Graphics;
                this.DrawHeader(g);
                this.DrawCalendar(g);
            }
        }

        private void DrawHeader(Graphics g)
        {
            for (int column = 0; column < 7; column++)
            {
                //draw weekday header rectangle
                g.FillRectangle(this.headerBrush, this.rectDays[column]);
                g.DrawString(this.strAbbrDays[column], this.headerFont, Brushes.Black, this.rectDays[column], this.stringFormat);
            }
        }

        private void DrawCalendar(Graphics g)
        {
            bool active_month = false;
            string str = string.Empty;
            //actual calendar 
            for (int line = 0; line < 6; line++)
            {
                for (int column = 0; column < 7; column++)
                {
                    Rectangle layoutRectangle = new Rectangle(this.rects[column, line].X, this.rects[column, line].Y, this.rects[column, line].Width, (int)(this.rects[column, line].Height * 1));

                    str = string.Empty;
                    if (this.arrDates[column, line] != DateTime.MinValue)
                    {
                        str = this.arrDates[column, line].Day.ToString();
                    }
                    if (this.arrDates[column, line].Month == this.SelectedDateTime.Month)
                    {
                        active_month = true;
                    }
                    else
                    {
                        active_month = false;
                    }
                    if (this.arrDates[column, line].Date == this.SelectedDateTime.Date)//selected date
                    {
                        g.FillRectangle(this.selectedDayBrush, this.rects[column, line]);
                        g.DrawString(str, this.dayFont, this.selectedDayFontBrush, layoutRectangle, this.stringFormat);
                        active_month = true;
                    }
                    else if (active_month)
                    {
                        if (str != string.Empty)
                        {
                            if (((column == 0) || (column == 6)) && this.weekendsDarker) //weekend
                            {
                                g.FillRectangle(this.weekendBrush, this.rects[column, line]);
                            }
                            else //weekday
                            {
                                g.FillRectangle(this.activeMonthBrush, this.rects[column, line]);
                            }
                            g.DrawString(str, this.dayFont, this.selectedDayFontBrush, layoutRectangle, this.stringFormat);
                        }
                    }
                    else
                    {
                        if (str != string.Empty)
                        {
                            if (((column == 0) || (column == 6)) && this.weekendsDarker) //weekend
                            {
                                g.FillRectangle(this.weekendBrush, this.rects[column, line]);
                            }
                            else //weekday
                            {
                                g.FillRectangle(this.inactiveMonthBrush, this.rects[column, line]);
                            }
                            g.DrawString(str, this.dayFont, this.nonselectedDayFontBrush, layoutRectangle, this.stringFormat);
                        }
                    }
                }
            }
        }
        #endregion Control events

        #region Private functions

        private void CreateGraphicObjects()
        {
            //brushes
            this.headerBrush = new SolidBrush(this.headerColor);
            this.activeMonthBrush = new SolidBrush(this.activeMonthColor);
            this.inactiveMonthBrush = new SolidBrush(this.inactiveMonthColor);
            this.selectedDayBrush = new SolidBrush(this.SelectedObjectColor);
            this.selectedMonthBrush = new SolidBrush(this.SelectedObjectColor);
            this.selectedDayFontBrush = new SolidBrush(this.selectedDayFontColor);
            this.nonselectedDayFontBrush = new SolidBrush(this.nonselectedDayFontColor);

            this.weekendBrush = new SolidBrush(this.weekendColor);

            //the text is centered vertically and horizontally
            this.stringFormat = new StringFormat();
            this.stringFormat.Alignment = StringAlignment.Center;
            this.stringFormat.LineAlignment = StringAlignment.Center;
        }

        private Rectangle[,] CreateGrid(int intW, int intH)
        {
            //Array of rectangles representing the calendar
            Rectangle[,] rectTemp = new Rectangle[7, 6];

            //header rectangles
            //
            rectDays = new Rectangle[7];
       
            int deltaWidth = 3;
            int rectWidth = (int)Math.Floor((double)intW / 7) - deltaWidth;
            int deltaHeight = 3;
            int rectHeight = ((int)Math.Floor((double)(intH - 20) / 6)) - deltaHeight;

            int intXX = 3;
            int intYY = 0;

            for (int i = 0; i < 7; i++)
            {
                Rectangle r1 = new Rectangle(intXX, intYY, rectWidth, 20);
                intXX += (rectWidth + deltaWidth);
                rectDays[i] = r1;

            }

            intYY = 20;
            for (int j = 0; j < 6; j++)
            {
                intXX = 3;
                for (int i = 0; i < 7; i++)
                {
                    Rectangle r1 = new Rectangle(intXX, intYY, rectWidth, rectHeight);
                    intXX += (rectWidth + deltaWidth);
                    rectTemp[i, j] = r1;
                }
                intYY += (rectHeight + deltaHeight);
            }
            return rectTemp;
        }
        public void FillDates2(DateTime currentDateTime)
        {           
            //grid column
            int intDayofWeek = 0;
            //grid row
            int intWeek = 0;

            //total day counter
            int intTotalDays = -1;

            //this is where the first day of the month falls in the grid
            int intFirstDay = 0;

            DateTime datPrevMonth = currentDateTime.AddMonths(-1);
            DateTime datNextMonth = currentDateTime.AddMonths(1);

            //number of days in active month
            int intCurrDays = DateTime.DaysInMonth(currentDateTime.Year, currentDateTime.Month);

            //number of days in prev month
            int intPrevDays = DateTime.DaysInMonth(datPrevMonth.Year, datPrevMonth.Month);

            //number of days in active month
            int intNextDays = DateTime.DaysInMonth(datNextMonth.Year, datNextMonth.Month);

            DateTime[] datesCurr = new DateTime[intCurrDays];
            DateTime[] datesPrev = new DateTime[intPrevDays];
            DateTime[] datesNext = new DateTime[intNextDays];

            for (int i = 0; i < intCurrDays; i++)
            {
                datesCurr[i] = new DateTime(currentDateTime.Year, currentDateTime.Month, i + 1, currentDateTime.Hour, currentDateTime.Minute, currentDateTime.Hour);
            }

            for (int i = 0; i < intPrevDays; i++)
            {
                datesPrev[i] = new DateTime(datPrevMonth.Year, datPrevMonth.Month, i + 1, currentDateTime.Hour, currentDateTime.Minute, currentDateTime.Hour);
            }

            for (int i = 0; i < intNextDays; i++)
            {
                datesNext[i] = new DateTime(datNextMonth.Year, datNextMonth.Month, i + 1, currentDateTime.Hour, currentDateTime.Minute, currentDateTime.Hour);
            }

            //array to hold dates corresponding to grid
            arrDates = new DateTime[7, 6];//dates ahead of current date

            //where does the first day of the week land?
            intDayofWeek = Array.IndexOf(strDays, datesCurr[0].DayOfWeek.ToString());


            for (int intDay = 0; intDay < intCurrDays; intDay++)
            {
                //populate array of dates for active month, this is used to tell what day of the week each day is

                intDayofWeek = Array.IndexOf(strDays, datesCurr[intDay].DayOfWeek.ToString());


                //fill the array with the day numbers
                arrDates[intDayofWeek, intWeek] = datesCurr[intDay];
                if (intDayofWeek == 6)
                {
                    intWeek++;
                }

                //Back fill any days from the previous month
                //this is does here because I needed to know where the first day of the active month fell in the grid
                if (intDay == 0)
                {
                    intFirstDay = intDayofWeek;
                    //Days in previous month
                    int intDaysPrev = DateTime.DaysInMonth(datPrevMonth.Year, datPrevMonth.Month);

                    //if the first day of the active month is not sunday, count backwards and fill in day number
                    if (intDayofWeek > 0)
                    {
                        for (int i = intDayofWeek - 1; i >= 0; i--)
                        {
                            arrDates[i, 0] = datesPrev[intDaysPrev - 1];
                            intDaysPrev--;
                            intTotalDays++;
                        }
                    }
                }
                intTotalDays++;
            }//for

            //fill in the remaining days of the grid with the beginning of the next month

            intTotalDays++;
            //what row did we leave off in for active month?
            int intRow = intTotalDays / 7;

            int intCol;

            int intDayNext = 0;

            for (int i = intRow; i < 6; i++)
            {
                intCol = intTotalDays - (intRow * 7);
                for (int j = intCol; j < 7; j++)
                {
                    arrDates[j, i] = datesNext[intDayNext];
                    intDayNext++;
                    intTotalDays++;
                }
                intRow++;
            }
        }
        #endregion Private functions

        #region Form events
        private void dateSelector_Load(object sender, System.EventArgs e)
        {
            this.rects = this.CreateGrid(dateSelectorPictureBox.Width, dateSelectorPictureBox.Height);
            this.CreateGraphicObjects();
            this.bDesign = false;

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
                this.minutesTextBox.Text = "0" + this.SelectedDateTime.Minute.ToString();
            }
            else
            {
                this.minutesTextBox.Text = this.SelectedDateTime.Minute.ToString();
            }

            this.InitializeMonthButtons();
            this.InitializeYearButtons();           
            this.GetWeekNumber(this.SelectedDateTime);
        }
        #endregion Form events

        private void InitializeYearButtons()
        {
            IEnumerable<Button> buttons = this.yearGroupBox.Controls
                                             .OfType<Button>();

            foreach (Button button in buttons)
            {
                if (button.Text != "")
                {
                    button.BackColor = Color.Gainsboro;
                }

            }

            this.firstYearButton.Text = Convert.ToString(this.SelectedDateTime.Year - 2);
            this.secondYearButton.Text = Convert.ToString(this.SelectedDateTime.Year - 1);
            this.thirdButtonYear.Text = Convert.ToString(this.SelectedDateTime.Year);
            this.fourthButtonYear.Text = Convert.ToString(this.SelectedDateTime.Year + 1);

            this.thirdButtonYear.BackColor = this.SelectedObjectColor;
        }
        private void InitializeMonthButtons()
        {
            IEnumerable<Button> buttons = this.monthGroupBox.Controls
                                             .OfType<Button>();
            foreach (Button button in buttons)
            {
                button.BackColor = Color.Gainsboro;
            }

            Button selectedMonthButton = this.monthGroupBox.Controls.OfType<Button>()
                                                   .Where(b => Convert.ToInt32(b.Tag) == this.SelectedDateTime.Month)
                                                   .FirstOrDefault();
            selectedMonthButton.BackColor = this.SelectedObjectColor;
        }
      
        public Action<object, EventArgs> okButtonClick = null;
        private void okButton_Click(object sender, EventArgs e)
        {
            OnSelectedDateChanged(this.SelectedDateTime);
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

        private void dateSelectorPictureBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.okButton.PerformClick();
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

        public void GetWeekNumber(DateTime selectedDateTime)
        {           
            CultureInfo ciCurr = CultureInfo.CurrentCulture;
            var lastDayOfYear = new DateTime(this.SelectedDateTime.Year, 12, 31);
            var date = new DateTime(this.SelectedDateTime.Year, this.SelectedDateTime.Month, 1);
            int lastWeekNum = ciCurr.Calendar.GetWeekOfYear(lastDayOfYear, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            int weekNum = ciCurr.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            IEnumerable<Label> labels = this.weekGroupBox.Controls
                                             .OfType<Label>();
            int i = 0;
            int j = 1;
            foreach (Label label in labels)
            {

                if (lastWeekNum >= weekNum + i)
                {

                    label.Text = Convert.ToString(weekNum + i);
                    i++;

                }
                else
                {
                    label.Text = Convert.ToString(j);
                    j++;
                }
            }
        }                           
    }
}
