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
    public partial class FrmLogin : Form
    {
        public Object id, name, role;
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void bersih()
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtUsername.Focus();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            txtUsername.Focus();
        }

        private void FrmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (MessageBox.Show("Do You Want To Exit This Application?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUsername.Text == "" || txtPassword.Text == "")
                {
                    MessageBox.Show("Data Cant Be Empty", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    bersih();
                }
                else if (txtUsername.Text == "admin" && txtPassword.Text == "admin")
                {
                    string name = "Nur Muhammad Alif Putra Setiawan";
                    (new FrmAdminNavigation(this, name)).Show();
                    Hide();
                    bersih();
                }
                else
                {
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        User user = db.Users.Where(s => s.Username == txtUsername.Text && s.Password == txtPassword.Text).FirstOrDefault();
                        if (user != null)
                        {
                            if (user.Role == "Teacher")
                            {
                                Teacher teacher = db.Teachers.Where(s => s.TeacherID == txtUsername.Text).FirstOrDefault();
                                if (teacher != null)
                                {
                                    (new FrmTeacherNavigation(this, teacher, user)).Show();
                                    this.Hide();
                                    bersih();
                                }
                            }
                            else if (user.Role == "Student")
                            {
                                Student student = db.Students.Where(s => s.StudentID == txtUsername.Text).FirstOrDefault();
                                if (student != null)
                                {
                                    (new FrmStudentNavigation(this, student, user)).Show();
                                    this.Hide();
                                    bersih();
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("No Such User", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            bersih();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Try Again, Please Contact Admin" + "\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPassword.Focus();
            }

        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin.PerformClick();
            }
        }
    }
}
