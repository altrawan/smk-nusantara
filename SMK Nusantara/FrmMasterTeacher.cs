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
    public partial class FrmMasterTeacher : Form
    {
        private Form close;
        public bool databaru;
        public string id, name, address, gender, phonenumber;
        public DateTime dateofbirth;
        public FrmMasterTeacher(Form close)
        {
            InitializeComponent();
            this.close = close;
        }

        private void autoNumber()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                Teacher t = db.Teachers.OrderByDescending(s => s.TeacherID).FirstOrDefault();
                if (t != null)
                {
                    string a = t.TeacherID.ToString().Substring(1, 4);
                    int n = Convert.ToInt32(a) + 1;
                    txtID.Text = "T" + n.ToString("d4");
                }
                else
                {
                    txtID.Text = "T0001";
                }
            }
        }

        private string iduser;
        private void idUser()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                User u = db.Users.OrderByDescending(s => s.UserID).FirstOrDefault();
                if (u != null)
                {
                    string a = u.UserID.ToString();
                    int n = Convert.ToInt32(a) + 1;
                    iduser = n.ToString("d1");
                }
                else
                {
                    iduser = "1";
                }
            }
        }

        private void bersih()
        {
            txtName.Text = "";
            txtAddress.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            dateTimePicker1.Value = DateTime.Now;
            txtPhoneNumber.Text = "";
            txtName.Focus();
        }

        private void awal()
        {
            if (databaru == true)
            {
                autoNumber();
                bersih();
            }
            else
            {
                txtID.Text = id;
                txtName.Text = name;
                txtAddress.Text = address;
                if (gender.Equals("Male"))
                {
                    radioButton1.Checked = true;
                }
                else
                {
                    radioButton2.Checked = true;
                }
                dateTimePicker1.Value = dateofbirth;
                txtPhoneNumber.Text = phonenumber;
            }
        }

        private string pass;
        private void buatPassword()
        {
            string start, end, date;
            start = txtName.Text.Substring(0, 1);
            end = txtName.Text.Substring(txtName.Text.Length - 1);
            date = DateTime.Now.ToString("yyyy");
            pass = start + end + date;
        }

        private void FrmMasterTeacher_Load(object sender, EventArgs e)
        {
            awal();
            txtID.Enabled = false;
        }

        private void FrmMasterTeacher_FormClosed(object sender, FormClosedEventArgs e)
        {
            close.Show();
            this.Hide();
        }

        private void txtName_KeyUp(object sender, KeyEventArgs e)
        {
            TextBox toUpperCase = ((TextBox)sender);
            int selectionStart = toUpperCase.SelectionStart;

            txtName.Text = toUpperCase.Text.ToUpper();
            txtName.SelectionStart = selectionStart++;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    int a = int.Parse(DateTime.Now.ToString("yyyy"));
                    int b = int.Parse(dateTimePicker1.Value.ToString("yyyy"));
                    int c = a - b;

                    if (txtID.Text == "" || txtName.Text == "" || txtAddress.Text == "" || radioButton1.Checked == false && radioButton2.Checked == false || txtPhoneNumber.Text == "")
                    {
                        MessageBox.Show("Data Cant Be Empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (txtName.Text.Length < 3 || txtName.Text.Length > 20)
                    {
                        MessageBox.Show("Ensure name have between 3 and 20 character", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (c < 25 || c > 50)
                    {
                        MessageBox.Show("Ensure the age student must be between 25 and 50 years", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (txtPhoneNumber.Text.Length < 11 || txtPhoneNumber.Text.Length > 13 && !txtPhoneNumber.Text.StartsWith("08"))
                    {
                        MessageBox.Show("Ensure phone number must be 11 – 12 Digit and start with 08", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        string gender;
                        if (radioButton1.Checked == true)
                        {
                            gender = "Male";
                        }
                        else
                        {
                            gender = "Female";
                        }
                        if (databaru == true)
                        {
                            db.Teachers.InsertOnSubmit(new Teacher()
                            {
                                TeacherID = txtID.Text,
                                Name = txtName.Text,
                                Address = txtAddress.Text,
                                Gender = gender,
                                DateofBirth = dateTimePicker1.Value,
                                PhoneNumber = txtPhoneNumber.Text
                            });
                            db.SubmitChanges();

                            idUser();
                            buatPassword();
                            db.Users.InsertOnSubmit(new User()
                            {
                                UserID = Convert.ToInt32(iduser),
                                Username = txtID.Text,
                                Password = pass,
                                Role = "Student"
                            });
                            db.SubmitChanges();
                            awal();
                            MessageBox.Show("Successfully Added Data", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            MessageBox.Show("Password : " + pass, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            Teacher t = db.Teachers.Where(s => s.TeacherID == txtID.Text).FirstOrDefault();
                            t.TeacherID = txtID.Text;
                            t.Name = txtName.Text;
                            t.Address = txtAddress.Text;
                            t.Gender = gender;
                            t.DateofBirth = dateTimePicker1.Value;
                            t.PhoneNumber = txtPhoneNumber.Text;
                            db.SubmitChanges();
                            awal();

                            MessageBox.Show("Successfully Saved Data", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Try Again, Please Contact Admin" + "\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do You Want To Cancel?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                awal();
            }
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && (int)e.KeyChar != (int)Keys.Back && (int)e.KeyChar != (int)Keys.Space)
            {
                e.Handled = true;
            }
        }

        private void txtPhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && (int)e.KeyChar != (int)Keys.Back)
            {
                e.Handled = true;
            }
        }
    }
}
