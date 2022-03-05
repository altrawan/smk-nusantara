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
    public partial class FrmStudentNavigation : Form
    {
        private Form close;
        private Student student;
        private User user;
        public FrmStudentNavigation(Form close, Student student, User user)
        {
            InitializeComponent();
            this.close = close;
            this.student = student;
            this.user = user;
        }

        private void FrmStudentNavigation_Load(object sender, EventArgs e)
        {
            lblName.Text = "Welcome, " + student.Name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmEditProfile editProfile = new FrmEditProfile(this, user);
            editProfile.id = student.StudentID;
            editProfile.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            (new FrmViewSchedule(this, student)).Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            (new FrmViewScore(this, student)).Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            (new FrmInbox(this, user)).Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do You Want To Logout?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                close.Show();
                this.Hide();
            }
        }

        private void FrmStudentNavigation_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (MessageBox.Show("Do You Want To Logout?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                close.Show();
                this.Hide();
            }
        }
    }
}
