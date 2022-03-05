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
    public partial class FrmTeacherNavigation : Form
    {
        private Form close;
        private Teacher teacher;
        private User user;
        public FrmTeacherNavigation(Form close, Teacher teacher, User user)
        {
            InitializeComponent();
            this.close = close;
            this.teacher = teacher;
            this.user = user;
        }

        private void FrmTeacherNavigation_Load(object sender, EventArgs e)
        {
            lblName.Text = "Welcome, " + teacher.Name;
        }

        private void FrmTeacherNavigation_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (MessageBox.Show("Do You Want To Logout?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                close.Show();
                this.Hide();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmEditProfile editProfile = new FrmEditProfile(this, user);
            editProfile.id = teacher.TeacherID;
            editProfile.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            (new FrmViewScheduleTeacher(this, teacher)).Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            (new FrmInputScore(this, teacher)).Show();
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
    }
}
