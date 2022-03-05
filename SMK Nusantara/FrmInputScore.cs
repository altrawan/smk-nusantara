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
    public partial class FrmInputScore : Form
    {
        private Form close;
        private Teacher teacher;
        private int grade;
        private string subjek;
        public FrmInputScore(Form close, Teacher teacher)
        {
            InitializeComponent();
            this.close = close;
            this.teacher = teacher;
        }

        private void isiSubject()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                var dataSource = (from s in db.Subjects
                                 join d in db.DetailSchedules
                                 on s.SubjectID equals d.SubjectID
                                 where s.Grade == grade
                                 && d.TeacherID == teacher.TeacherID
                                 select new
                                 {
                                     Display = d.SubjectID + " - " + s.Name,
                                     Value = d.SubjectID
                                 }).Distinct();

                cbSubject.DisplayMember = "Display";
                cbSubject.ValueMember = "Value";
                cbSubject.DataSource = dataSource;
            }
        }

        private void tampil()
        {
            
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                dataGridView1.DataSource = from v in db.View_1s
                                           where v.ClassName == cbClass.SelectedItem.ToString()
                                           && v.SubjectID == subjek
                                           select new
                                           {
                                               v.StudentID,
                                               v.Name,
                                               v.Assignment,
                                               v.MidExam,
                                               v.FinalExam
                                           };
                dataGridView1.Columns["Final"].DisplayIndex = 5;
                buatHeader();
                final();
                percentage();
            }
        }

        private void buatHeader()
        {
            dataGridView1.Columns[0].HeaderText = "Final";
            dataGridView1.Columns[1].HeaderText = "Student ID";
            dataGridView1.Columns[2].HeaderText = "Student Name";
            dataGridView1.Columns[3].HeaderText = "Assignment";
            dataGridView1.Columns[4].HeaderText = "Mid Exam";
            dataGridView1.Columns[5].HeaderText = "Final Exam";

            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].Width = 60;
            dataGridView1.Columns[2].Width = 200;
            dataGridView1.Columns[3].Width = 100;
            dataGridView1.Columns[4].Width = 100;
            dataGridView1.Columns[5].Width = 100;
        }

        /*
        private void test()
        {
            int a = dataGridView1.Rows.Cast<DataGridViewRow>().AsEnumerable().Sum(x => int.Parse(x.Cells["Assignment"].Value.ToString()));
            int z = dataGridView1.RowCount;
            double y = ((double)a / (double)z) * 100.0;
            int b = dataGridView1.Rows.Cast<DataGridViewRow>().AsEnumerable().Sum(x => int.Parse(x.Cells["MidExam"].Value.ToString()));
            int c = dataGridView1.Rows.Cast<DataGridViewRow>().AsEnumerable().Sum(x => int.Parse(x.Cells["FinalExam"].Value.ToString()));
            label4.Text = "Assignment" + y;
        }*/

        private void isiClass()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                cbClass.DataSource = db.Classes.Select(s => s.ClassName);
            }
        }

        private void percentage()
        {
            double a = dataGridView1.RowCount * 100;
            double b = dataGridView1.Rows.Cast<DataGridViewRow>().Sum(s => Convert.ToInt32(s.Cells[3].Value));
            double c = (b / a) * 100;
            //==================================================================================================
            double d = dataGridView1.Rows.Cast<DataGridViewRow>().Sum(s => Convert.ToInt32(s.Cells[4].Value));
            double e = (d / a) * 100;
            //==================================================================================================
            double f = dataGridView1.Rows.Cast<DataGridViewRow>().Sum(s => Convert.ToInt32(s.Cells[5].Value));
            double g = (f / a) * 100;
            label4.Text = "Assignment : " + c.ToString("00") + "%, Mid Exam : " + e.ToString("00") + "%, Final Exam : " + g.ToString("00") + "%";
        }

        private void final()
        {
            double sum = 0;
            double avg = 0;
            
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                int n = item.Index;

                double a = Double.Parse(dataGridView1.Rows[n].Cells[3].Value.ToString());
                double b = Double.Parse(dataGridView1.Rows[n].Cells[4].Value.ToString());
                double c = Double.Parse(dataGridView1.Rows[n].Cells[5].Value.ToString());
                sum = a + b + c;
                avg = sum / 3;
                dataGridView1.Rows[n].Cells[0].Value = avg.ToString("00.00");
            }
        }
        
        private void FrmInputScore_Load(object sender, EventArgs e)
        {
            isiClass();
            isiSubject();
            subjek = cbSubject.Text.Substring(0, 5);
            tampil();
            button1.Enabled = false;
        }

        private void cbSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            tampil();
        }

        private void cbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            tampil();
            grade = 0;
            if (cbClass.Text == "XA" || cbClass.Text == "XB")
            {
                grade = 1;
            }
            else if (cbClass.Text == "XIA" || cbClass.Text == "XIB")
            {
                grade = 2;
            }
            else if (cbClass.Text == "XIIA" || cbClass.Text == "XIIB")
            {
                grade = 3;
            }
            isiSubject();
            //tampil();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataGridViewRow r = dataGridView1.CurrentRow;
            FrmEntryScore entryScore = new FrmEntryScore(this);
            entryScore.assignment = r.Cells["Assignment"].Value.ToString();
            entryScore.mid = r.Cells["MidExam"].Value.ToString();
            entryScore.final = r.Cells["FinalExam"].Value.ToString();
            entryScore.student = r.Cells["StudentID"].Value.ToString() + "-" + r.Cells["Name"].Value.ToString();
            entryScore.subjectid = cbSubject.Text.Substring(0, 5);
            entryScore.id = r.Cells["StudentID"].Value.ToString();
            entryScore.Show();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            button1.Enabled = true;
        }
    }
}
