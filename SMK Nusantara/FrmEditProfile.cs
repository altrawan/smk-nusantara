using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMK_Nusantara
{
    public partial class FrmEditProfile : Form
    {
        private Form close;
        private User user;
        public string id;
        private string photo, name, filePath;

        public FrmEditProfile(Form close, User user)
        {
            InitializeComponent();
            this.close = close;
            this.user = user;
        }

        private void bersih()
        {
            string photo = Path.GetDirectoryName(Application.ExecutablePath) + @"\Images\back.png";
            pictureBox1.BackgroundImage = Image.FromFile(photo);
            txtID.Clear();
            txtName.Clear();
            txtPhoneNumber.Clear();
            txtAddress.Clear();
            txtName.Focus();
        }

        private void awal()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                if (user.Role == "Teacher")
                {
                    Teacher teacher = db.Teachers.Where(s => s.TeacherID == txtID.Text).FirstOrDefault();
                    if (teacher != null)
                    {
                        txtName.Text = teacher.Name;
                        txtPhoneNumber.Text = teacher.PhoneNumber;
                        txtAddress.Text = teacher.Address;


                        if (teacher.Photo == null || teacher.Photo == "-")
                        {
                            string photo = Path.GetDirectoryName(Application.ExecutablePath) + @"\Images\back.png";
                            pictureBox1.BackgroundImage = Image.FromFile(photo);
                        }
                        else
                        {
                            string photo = Path.GetDirectoryName(Application.ExecutablePath) + @"\Images\" + teacher.Photo.ToString();
                            pictureBox1.BackgroundImage = Image.FromFile(photo);
                        }
                    }
                }
                else if (user.Role == "Student")
                {
                    Student student = db.Students.Where(s => s.StudentID == txtID.Text).FirstOrDefault();
                    if (student != null)
                    {
                        txtName.Text = student.Name;
                        txtPhoneNumber.Text = student.PhoneNumber;
                        txtAddress.Text = student.Address;


                        if (student.Photo == null || student.Photo == "-")
                        {
                            string photo = Path.GetDirectoryName(Application.ExecutablePath) + @"\Images\back.png";
                            pictureBox1.BackgroundImage = Image.FromFile(photo);
                        }
                        else
                        {
                            string photo = Path.GetDirectoryName(Application.ExecutablePath) + @"\Images\" + student.Photo.ToString();
                            pictureBox1.BackgroundImage = Image.FromFile(photo);
                        }
                    }
                }
            }
        }

        private void FrmEditProfile_Load(object sender, EventArgs e)
        {
            txtID.Text = id;
            txtID.Enabled = false;
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void FrmEditProfile_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (MessageBox.Show("Do You Want To Exit?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                close.Show();
                this.Hide();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            OpenFileDialog opFile = new OpenFileDialog();
            opFile.Title = "Select a Image";
            opFile.FilterIndex = 1;
            opFile.Filter = "Image Files(*.jpg; *.png)|*.jpg; *.png";
            photo = Path.GetDirectoryName(Application.ExecutablePath) + @"\Images\";
            if (Directory.Exists(photo) == false)
            {
                Directory.CreateDirectory(photo);
            }
            if (opFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox1.BackgroundImage = Image.FromFile(opFile.FileName);
                    filePath = opFile.FileName;
                    name = DateTime.Now.ToString("yyMMddHHmmss") + ".png";
                }
                catch (Exception exp)
                {
                    MessageBox.Show("Unable to open file " + exp.Message);
                }
            }
            else
            {
                opFile.Dispose();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            (new FrmChangePassword(this, user)).Show();
        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {
            awal();
        }

        private void txtName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && (int)e.KeyChar != (int)Keys.Back && (int)e.KeyChar != (int)Keys.Space)
            {
                e.Handled = true;
            }
        }

        private void txtName_Validating(object sender, CancelEventArgs e)
        {
            if (txtName.Text.Length < 3 || txtName.Text.Length > 20)
            {
                MessageBox.Show("Ensure name have between 3 and 20 character", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnSave.Enabled = false;
            }
            else
            {
                btnSave.Enabled = true;
            }
        }

        private void txtPhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && (int)e.KeyChar != (int)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void txtPhoneNumber_Validating(object sender, CancelEventArgs e)
        {
            if (txtPhoneNumber.Text.Length < 11 || txtPhoneNumber.Text.Length > 12)
            {
                MessageBox.Show("Ensure phone number must be 11 – 12 Digit", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnSave.Enabled = false;
            }
            else if (!txtPhoneNumber.Text.StartsWith("08"))
            {
                MessageBox.Show("Ensure phone number start with 08", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnSave.Enabled = false;
            }
            else
            {
                btnSave.Enabled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                if (txtName.Text == "" || txtPhoneNumber.Text == "" || txtAddress.Text == "")
                {
                    MessageBox.Show("Data Cant Be Empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    bersih();
                }
                else
                {
                    if (user.Role == "Teacher")
                    {
                        if (name == null)
                        {
                            Teacher t = db.Teachers.Where(s => s.TeacherID == txtID.Text).FirstOrDefault();
                            t.Name = txtName.Text;
                            t.PhoneNumber = txtPhoneNumber.Text;
                            t.Address = txtAddress.Text;
                            db.SubmitChanges();
                        }
                        else
                        {
                            Teacher t = db.Teachers.Where(s => s.TeacherID == txtID.Text).FirstOrDefault();
                            t.Name = txtName.Text;
                            t.PhoneNumber = txtPhoneNumber.Text;
                            t.Address = txtAddress.Text;
                            t.Photo = name;
                            File.Copy(filePath, photo + name);
                            db.SubmitChanges();
                        }
                        awal();
                        MessageBox.Show("Successfully Saved Data", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (user.Role == "Student")
                    {
                        if (name == null)
                        {
                            Student s = db.Students.Where(t => t.StudentID == txtID.Text).FirstOrDefault();
                            s.Name = txtName.Text;
                            s.PhoneNumber = txtPhoneNumber.Text;
                            s.Address = txtAddress.Text;
                            db.SubmitChanges();
                        }
                        else
                        {
                            Student s = db.Students.Where(t => t.StudentID == txtID.Text).FirstOrDefault();
                            s.Name = txtName.Text;
                            s.PhoneNumber = txtPhoneNumber.Text;
                            s.Address = txtAddress.Text;
                            s.Photo = name;
                            File.Copy(filePath, photo + name);
                            db.SubmitChanges();
                        }
                        awal();
                        MessageBox.Show("Successfully Saved Data", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do You Want To Cancel?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                awal();
            }
        }
    }
}
