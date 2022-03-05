using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMK_Nusantara
{
    public partial class FrmAdminNavigation : Form
    {
        public Form close;
        public String name;
        public FrmAdminNavigation(Form close, String name)
        {
            InitializeComponent();
            this.close = close;
            this.name = name;
        }

        private void FrmAdminNavigation_Load(object sender, EventArgs e)
        {
            lblName.Text = "Welcome, " + name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            (new FrmStudent(this)).Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            (new FrmTeacher(this)).Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            (new FrmClass(this)).Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            (new FrmSchedule(this)).Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            (new FrmFinalize(this)).Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            (new FrmReportScore(this)).Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do You Want To Logout?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                close.Show();
                this.Hide();
            }
        }

        private void FrmAdminNavigation_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (MessageBox.Show("Do You Want To Logout?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                close.Show();
                this.Hide();
            }
        }
    }
}
