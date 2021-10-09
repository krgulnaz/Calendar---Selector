using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dateTimeSelector11.SelectedDate = DateTime.Now.AddDays(-10);
            //dateTimePicker1.Value = DateTime.MaxValue;
            //dateTimePicker1.MinDate = DateTimePicker.MinDateTime;
            //this.dateTimeSelector11.SelectedDate = null;
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            
           // dateTimeSelector11.IsDefaultDate = false;
            dateTimeSelector11.SelectedDate = null;
        }
    }
}
