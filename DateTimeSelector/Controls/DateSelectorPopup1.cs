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
    public partial class DateSelectorPopup1 : UserControl
    {   
        #region Private variables
        private PictureBox dateSelectorPictureBox;
        private int pintMonth = DateTime.Now.Month;
        private DateTime selectedDateTime = DateTime.Now;
        private bool weekendsDarker = false;
        private Color headerColor = System.Drawing.SystemColors.Control;

        private Color activeMonthColor = Color.Gainsboro;
        private Color inactiveMonthColor = SystemColors.ControlLight;

        private Color selectedDayFontColor = Color.Black;
        private Color selectedObjectColor = Color.SpringGreen;
        private Color nonselectedDayFontColor = Color.Gray;

        private Color weekendColor = Color.Red;

        private Font dayFont = new Font("Microsoft Sans Serif", 8F, FontStyle.Regular);
        private Font headerFont = new Font("Microsoft Sans Serif", 9, FontStyle.Regular);

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
        //private Button addButton;
        private Button cancelButton;
        private Label currentDateLabel;
        private ImageList imageList1;
        private GroupBox groupBox1;
        private DateTime[,] arrDates;

        #endregion Private variables

        #region Custom events
        public Action<DateTime> SelectedDateChangedPopup = null;
        protected virtual void OnSelectedDateChanged(DateTime dateTime)
        {
            if (this.SelectedDateChangedPopup != null)
            {
                this.SelectedDateChangedPopup(dateTime);
            }
        }
        #endregion Custom events

        #region Properties

        [Description("The selected date.")]
        [Category("DateSelector")]
        public DateTime SelectedDateTime
        {
            get
            {
                return this.selectedDateTime;
            }
            set
            {
                this.selectedDateTime = value;
                this.currentDateLabel.Text = $"{this.selectedDateTime.DayOfWeek} {this.selectedDateTime.Day}, {this.strMonths[this.selectedDateTime.Month - 1]} {this.selectedDateTime.Year}";
            }
        }

        [Description("Determines whether or not the grid is drawn.")]
        [Category("DateSelector")]
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
        [Description("Weekends darker")]
        [Category("DateSelector")]
        private bool WeekendsDarker
        {
            get
            {
                return weekendsDarker;
            }
            set
            {
                weekendsDarker = value;
            }
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

        #endregion Properties

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public DateSelectorPopup1()
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
            //this.decButton.DoubleClick += this.DCButton_DoubleClick;

            //this.firstYearButton.DoubleClick += this.DCButton_DoubleClick;
            //this.secondYearButton.DoubleClick += this.DCButton_DoubleClick;
            //this.thirdButtonYear.DoubleClick += this.DCButton_DoubleClick;
            //this.fourthButtonYear.DoubleClick += this.DCButton_DoubleClick;

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
        }

        private void DCButton_DoubleClick(object sender, EventArgs e)
        {
            this.addButton.PerformClick();
        }
        #endregion Constructor

        #region Control events
        private void YearButton_Click(object sender, EventArgs e)
        {
            Button priorSelectedButton = this.yearGroupBox.Controls.OfType<Button>()
                                                     .Where(b => b.Text.Trim() != string.Empty && Convert.ToInt32(b.Text) == this.SelectedDateTime.Year)
                                                     .FirstOrDefault();           

            Button selectedButton = (Button)sender;

            int day = this.SelectedDateTime.Day;
            int month = this.SelectedDateTime.Month;
            int year = Convert.ToInt32(selectedButton.Text);

            DateTime tempDateTime = new DateTime(year, month,
                                         DateTime.DaysInMonth(year, month));
            if (day > tempDateTime.Day)
            {
                day = tempDateTime.Day;
            }

            DateTime selectedDate = new DateTime(year, month, day);
            if (selectedDate <= MaxDate && selectedDate >= MinDate)
            {
                if (priorSelectedButton != null)
                {
                    priorSelectedButton.BackColor = Color.Gainsboro;
                }
                selectedButton.BackColor = SelectedObjectColor;

                this.SelectedDateTime = new DateTime(year, month, day);
                this.dateSelectorPictureBox.Invalidate();               
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
            DateTime selectedDate = new DateTime(year, month, day);
            if (selectedDate <= MaxDate && selectedDate >= MinDate)
            {
                priorSelectedButton.BackColor = Color.Gainsboro;
                selectedButton.BackColor = this.SelectedObjectColor;

                this.SelectedDateTime = new DateTime(year, month, day);
                this.GetWeekNumber(this.SelectedDateTime);
                this.dateSelectorPictureBox.Invalidate();
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
                                              .OfType<Button>()
                                              .Where(b => b.Text.Trim() != string.Empty && Convert.ToInt32(b.Text) == this.SelectedDateTime.Year);
            foreach (Button button in buttons)
            {
                button.BackColor = Color.Gainsboro;
            }

            firstYearButton.Text = Convert.ToString(year);
            secondYearButton.Text = Convert.ToString(year + 1);
            thirdButtonYear.Text = Convert.ToString(year + 2);
            fourthButtonYear.Text = Convert.ToString(year + 3);

            Button selectedButton = this.yearGroupBox.Controls
                                                   .OfType<Button>()
                                                   .Where(b => b.Text.Trim() != string.Empty && Convert.ToInt32(b.Text) == this.SelectedDateTime.Year)
                                                   .FirstOrDefault();
            if (selectedButton != null)
            {
                selectedButton.BackColor = SelectedObjectColor;
            }
        }

        private void prevButton_Click(object sender, EventArgs e)
        {
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

        private void dateSelector_SizeChanged(object sender, System.EventArgs e)
        {
            this.rects = this.CreateGrid(dateSelectorPictureBox.Width, dateSelectorPictureBox.Height);
            dateSelectorPictureBox.Invalidate();
        }

        private void dateSelector_MouseDown(object sender, MouseEventArgs e)
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
                                this.SelectedDateTime = this.arrDates[column, line];
                                this.GetWeekNumber(this.SelectedDateTime);
                                this.InitializeMonthButtons();
                                this.InitializeYearButtons();                               
                            }
                        }
                    }
                }
            }
            this.dateSelectorPictureBox.Invalidate();
        }

        private void dateSelectorPictureBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.addButton.PerformClick();
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
            this.headerBrush = new SolidBrush(this.headerColor);
            this.activeMonthBrush = new SolidBrush(this.activeMonthColor);

            this.inactiveMonthBrush = new SolidBrush(this.inactiveMonthColor);

            this.selectedDayBrush = new SolidBrush(this.SelectedObjectColor);
            this.selectedMonthBrush = new SolidBrush(this.SelectedObjectColor);
            this.selectedDayFontBrush = new SolidBrush(this.selectedDayFontColor);
            this.nonselectedDayFontBrush = new SolidBrush(this.nonselectedDayFontColor);

            this.weekendBrush = new SolidBrush(this.weekendColor);

            this.stringFormat = new StringFormat();
            this.stringFormat.Alignment = StringAlignment.Center;
            this.stringFormat.LineAlignment = StringAlignment.Center;
        }

        private Rectangle[,] CreateGrid(int intW, int intH)
        {
            Rectangle[,] rectTemp = new Rectangle[7, 6];

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
                datesCurr[i] = new DateTime(currentDateTime.Year, currentDateTime.Month, i + 1);
            }

            for (int i = 0; i < intPrevDays; i++)
            {
                datesPrev[i] = new DateTime(datPrevMonth.Year, datPrevMonth.Month, i + 1);
            }

            for (int i = 0; i < intNextDays; i++)
            {
                datesNext[i] = new DateTime(datNextMonth.Year, datNextMonth.Month, i + 1);
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
            }

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
            rects = CreateGrid(dateSelectorPictureBox.Width, dateSelectorPictureBox.Height);
            CreateGraphicObjects();
            bDesign = false;

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

        private void addButton_Click(object sender, EventArgs e)
        {
            this.OnSelectedDateChanged(this.SelectedDateTime);
            if (this.okButtonClick != null)
            {
                this.okButtonClick(sender, e);
            }
        }

        private void addButton_KeyUp(object sender, KeyEventArgs e)
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
    }
}
