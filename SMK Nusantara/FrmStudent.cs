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
    public partial class FrmStudent : Form
    {
        private Form close;
        public FrmStudent(Form close)
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
                dataGridView1.DataSource = from u in db.Students
                                           select new
                                           {
                                               u.StudentID,
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
                dataGridView1.DataSource = (from u in db.Students
                                           where u.StudentID.Contains(searchValue)
                                           || u.Name.Contains(searchValue)
                                           || u.Address.Contains(searchValue)
                                           || u.PhoneNumber.Contains(searchValue)
                                           select new
                                           {
                                               u.StudentID,
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
            dataGridView1.Columns[2].HeaderText = "Student ID";
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

        private void FrmStudent_Load(object sender, EventArgs e)
        {
            tampil();
        }

        private void FrmStudent_FormClosed(object sender, FormClosedEventArgs e)
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
            FrmMasterStudent ms = new FrmMasterStudent(this);
            ms.databaru = true;
            ms.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (e.RowIndex > -1)
                {
                    DataGridViewRow r = dataGridView1.Rows[e.RowIndex];
                    FrmMasterStudent ms = new FrmMasterStudent(this);
                    ms.databaru = false;
                    ms.id = r.Cells["StudentID"].Value.ToString();
                    ms.name = r.Cells["Name"].Value.ToString();
                    ms.address = r.Cells["Address"].Value.ToString();
                    ms.gender = r.Cells["Gender"].Value.ToString();
                    ms.dateofbirth = DateTime.Parse(r.Cells["DateofBirth"].Value.ToString());
                    ms.phonenumber = r.Cells["PhoneNumber"].Value.ToString();
                    ms.Show();
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
                            Student s = db.Students.Where(u => u.StudentID == r.Cells["StudentID"].Value.ToString()).FirstOrDefault();
                            db.Students.DeleteOnSubmit(s);
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
