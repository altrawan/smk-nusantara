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
    public partial class FrmClass : Form
    {
        private Form close;
        private string student_id, student_name, student_class;
        public FrmClass(Form close)
        {
            InitializeComponent();
            this.close = close;
        }

        private void isiCombo()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                comboBox1.DataSource = db.Classes.Select(s => s.ClassName);
            }
        }

        private void studentList()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                /* Query SQL
                select *
                from Student s
                where not exists(
                select 1
                from DetailClass d
                where s.StudentID = d.StudentID
                )*/

                dataGridView1.DataSource = from s in db.Students
                                           where !db.DetailClasses.Any(dc => (dc.StudentID == s.StudentID))
                                           select new
                                           {
                                               s.StudentID,
                                               s.Name
                                           };
                buatHeader1();
            }
        }

        private void participateStudent()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                dataGridView2.DataSource = from u in db.Students
                                           join s in db.DetailClasses
                                           on u.StudentID equals s.StudentID
                                           where s.ClassName == comboBox1.SelectedItem.ToString()
                                           && s.StudentID == u.StudentID
                                           select new
                                           {
                                               s.StudentID,
                                               u.Name
                                           };
                buatHeader2();
            }
        }

        
        private void buatHeader1()
        {
            dataGridView1.Columns[0].HeaderText = "Student ID";
            dataGridView1.Columns[1].HeaderText = "Name";
            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].Width = 200;
        }

        private void buatHeader2()
        {
            dataGridView2.Columns[0].HeaderText = "Student ID";
            dataGridView2.Columns[1].HeaderText = "Name";
            dataGridView2.Columns[0].Width = 100;
            dataGridView2.Columns[1].Width = 200;
        }

        private void add()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                db.DetailClasses.InsertOnSubmit(new DetailClass()
                {
                    ClassName = student_class,
                    StudentID = student_id
                });
                db.SubmitChanges();
                studentList();
                participateStudent();
            }
        }

        private void drop()
        {
            if (MessageBox.Show("Do You Want Drop Student with Name " + student_name + " ?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    DetailClass dc = db.DetailClasses.Where(s => s.StudentID == student_id).FirstOrDefault();
                    db.DetailClasses.DeleteOnSubmit(dc);
                    db.SubmitChanges();
                    studentList();
                    participateStudent();
                }
            }
        }

        private void dataParticipate(int x)
        {
            student_id = dataGridView2.Rows[x].Cells[0].Value.ToString();
            student_name = dataGridView2.Rows[x].Cells[1].Value.ToString();
            student_class = comboBox1.Text;
        }

        private void dataStudent(int x)
        {
            student_id = dataGridView1.Rows[x].Cells[0].Value.ToString();
            student_class = comboBox1.Text;
        }

        private void FrmClass_Load(object sender, EventArgs e)
        {
            isiCombo();
            studentList();
            button1.Enabled = false;
            button2.Enabled = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataStudent(e.RowIndex);
            button1.Enabled = true;
            button2.Enabled = false;
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataParticipate(e.RowIndex);
            button1.Enabled = false;
            button2.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            add();
            button2.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            drop();
            button2.Enabled = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            participateStudent();
        }
    }
}
