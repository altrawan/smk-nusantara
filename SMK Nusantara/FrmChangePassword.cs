using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMK_Nusantara
{
    public partial class FrmChangePassword : Form
    {
        private Form close;
        private User user;
        public FrmChangePassword(Form close, User user)
        {
            InitializeComponent();
            this.close = close;
            this.user = user;
        }

        private void awal()
        {
            txtOld.Clear();
            txtNew.Clear();
            txtConfirm.Clear();
            txtOld.Focus();
            txtNew.Enabled = false;
            txtConfirm.Enabled = false;
            checkBox1.Checked = false;
            checkBox2.Enabled = false;
            checkBox2.Checked = false;
            checkBox3.Enabled = false;
            checkBox3.Checked = false;
        }

        private bool IsValidPassword(string password)
        {
            string strRegex = "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{6,9}$";
            Regex r = new Regex(strRegex);
            if (r.IsMatch(password))
                return (true);
            else
                return (false);
        }

        private void FrmChangePassword_Load(object sender, EventArgs e)
        {
            awal();
        }

        private void FrmChangePassword_FormClosed(object sender, FormClosedEventArgs e)
        {

            close.Show();
            this.Hide();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                User u = db.Users.Where(s => s.UserID == user.UserID).FirstOrDefault();
                if (txtOld.Text == "" || txtNew.Text == "" || txtConfirm.Text == "")
                {
                    MessageBox.Show("Data Cant Be Empty", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    awal();
                }
                else if (IsValidPassword(txtNew.Text) == false || IsValidPassword(txtConfirm.Text) == false)
                {
                    MessageBox.Show("Password Must Contains Uppercase, Lowercase, Number with Total 6 - 9 Character", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (txtNew.Text != txtConfirm.Text)
                {
                    MessageBox.Show("Ensure confirm password must be same with new password", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (txtOld.Text == txtConfirm.Text)
                {
                    MessageBox.Show("Ensure old password must not be same with new password", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    u.Password = txtConfirm.Text;
                    db.SubmitChanges();
                    MessageBox.Show("Successfully Change Password", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    awal();
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                txtOld.UseSystemPasswordChar = false;
            }
            else
            {
                txtOld.UseSystemPasswordChar = true;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                txtNew.UseSystemPasswordChar = false;
            }
            else
            {
                txtNew.UseSystemPasswordChar = true;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked == true)
            {
                txtConfirm.UseSystemPasswordChar = false;
            }
            else
            {
                txtConfirm.UseSystemPasswordChar = true;
            }
        }

        private void txtOld_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    User u = db.Users.Where(s => s.UserID == user.UserID).FirstOrDefault();
                    if ((txtOld.Text) == u.Password)
                    {
                        txtNew.Enabled = true;
                        checkBox2.Enabled = true;
                        txtNew.Focus();
                    }
                    else
                    {
                        txtNew.Enabled = false;
                        checkBox2.Enabled = false;
                        txtOld.Focus();
                        MessageBox.Show("Your Password Doesn't Match", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void txtNew_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (IsValidPassword(txtNew.Text) == false)
                {
                    txtConfirm.Enabled = false;
                    checkBox3.Enabled = false;
                    txtNew.Focus();
                    MessageBox.Show("Password Must Contains Uppercase, Lowercase, Number with Total 6 - 9 Character", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    txtConfirm.Enabled = true;
                    checkBox3.Enabled = true;
                    txtConfirm.Focus();
                }
            }
        }

        private void txtConfirm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                if (txtNew.Text != txtConfirm.Text)
                {
                    btnSave.Enabled = false;
                    txtConfirm.Focus();
                    MessageBox.Show("Ensure confirm password must be same with new password", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    btnSave.Enabled = true;
                    btnSave.PerformClick();
                }
            }
        }

        private void FrmChangePassword_FormClosing(object sender, FormClosingEventArgs e)
        {
            close.Show();
        }
    }
}
