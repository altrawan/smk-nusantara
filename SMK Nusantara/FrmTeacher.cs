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
    public partial class FrmTeacher : Form
    {
        private Form close;
        public FrmTeacher(Form close)
        {
            InitializeComponent();
            this.close = close;
        }

        private void total()
        {
            lblTotal.Text = "Total Data : " + dataGridView1.RowCount;
        }

        public void tampil()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                dataGridView1.DataSource = from u in db.Teachers
                                           select new
                                           {
                                               u.TeacherID,
                                               u.Name,
                                               u.Address,
                                               u.Gender,
                                               u.DateofBirth,
                                               u.PhoneNumber
                                           };
                buatHeader();
                total();
                dataGridView1.Columns["Update"].DisplayIndex = 7;
                dataGridView1.Columns["Delete"].DisplayIndex = 7;
            }
        }

        private void search()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                var searchValue = txtSearch.Text.Trim();
                dataGridView1.DataSource = (from u in db.Teachers
                                            where u.TeacherID.Contains(searchValue)
                                            || u.Name.Contains(searchValue)
                                            || u.Address.Contains(searchValue)
                                            || u.PhoneNumber.Contains(searchValue)
                                            select new
                                            {
                                                u.TeacherID,
                                                u.Name,
                                                u.Address,
                                                u.Gender,
                                                u.DateofBirth,
                                                u.PhoneNumber
                                            }).ToList();
                buatHeader();
                total();
            }
        }

        private void buatHeader()
        {
            dataGridView1.Columns[0].HeaderText = "Update";
            dataGridView1.Columns[1].HeaderText = "Delete";
            dataGridView1.Columns[2].HeaderText = "Teacher ID";
            dataGridView1.Columns[3].HeaderText = "Name";
            dataGridView1.Columns[4].HeaderText = "Address";
            dataGridView1.Columns[5].HeaderText = "Gender";
            dataGridView1.Columns[6].HeaderText = "Date of Birth";
            dataGridView1.Columns[7].HeaderText = "Phone Number";

            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].Width = 60;
            dataGridView1.Columns[3].Width = 200;
            dataGridView1.Columns[4].Width = 150;
            dataGridView1.Columns[5].Width = 50;
            dataGridView1.Columns[6].Width = 150;
            dataGridView1.Columns[7].Width = 100;
        }

        private void FrmTeacher_Load(object sender, EventArgs e)
        {
            tampil();
        }

        private void FrmTeacher_FormClosed(object sender, FormClosedEventArgs e)
        {
            close.Show();
            this.Hide();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            search();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            FrmMasterTeacher mt = new FrmMasterTeacher(this);
            mt.databaru = true;
            mt.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (e.RowIndex > -1)
                {
                    DataGridViewRow r = dataGridView1.Rows[e.RowIndex];
                    FrmMasterTeacher mt = new FrmMasterTeacher(this);
                    mt.databaru = false;
                    mt.id = r.Cells["TeacherID"].Value.ToString();
                    mt.name = r.Cells["Name"].Value.ToString();
                    mt.address = r.Cells["Address"].Value.ToString();
                    mt.gender = r.Cells["Gender"].Value.ToString();
                    mt.dateofbirth = DateTime.Parse(r.Cells["DateofBirth"].Value.ToString());
                    mt.phonenumber = r.Cells["PhoneNumber"].Value.ToString();
                    mt.Show();
                }
            }
            else if (e.ColumnIndex == 1)
            {
                if (e.RowIndex > -1)
                {
                    DataGridViewRow r = dataGridView1.Rows[e.RowIndex];
                    if (MessageBox.Show("Do You Want Delete Data with Name " + r.Cells["Name"].Value.ToString() + " ?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        using (DataClasses1DataContext db = new DataClasses1DataContext())
                        {
                            Teacher t = db.Teachers.Where(u => u.TeacherID == r.Cells["TeacherID"].Value.ToString()).FirstOrDefault();
                            db.Teachers.DeleteOnSubmit(t);
                            db.SubmitChanges();
                            tampil();
                            MessageBox.Show("Successfully Deleted Data", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
        }
    }
}
