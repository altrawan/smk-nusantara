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
    public partial class FrmEntryScore : Form
    {
        private Form close;
        public string subjectid, student, assignment, mid, final, id;


        public FrmEntryScore(Form close)
        {
            InitializeComponent();
            this.close = close;
        }

        private void awal()
        {
            txtAssignment.Text = assignment;
            txtMid.Text = mid;
            txtFinal.Text = final;
            lblStudent.Text = student;
        }

        private void update()
        {
            
        }

        private void FrmEntryScore_Load(object sender, EventArgs e)
        {
            awal();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                int a = Convert.ToInt32(txtAssignment.Text);
                int b = Convert.ToInt32(txtMid.Text);
                int c = Convert.ToInt32(txtFinal.Text);
            
                if (txtAssignment.Text == "" || txtMid.Text == "" || txtFinal.Text == "")
                {
                    MessageBox.Show("Data Cant Be Empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (a > 100 || b > 100 || c > 100)
                {
                    MessageBox.Show("every score between 0 and 100", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    DetailScore d = db.DetailScores.Where(s => s.StudentID == id).FirstOrDefault();
                    d.Assignment = int.Parse(txtAssignment.Text);
                    d.MidExam = int.Parse(txtMid.Text);
                    d.FinalExam = int.Parse(txtFinal.Text);
                    db.SubmitChanges();
                    awal();
                    MessageBox.Show("Successfully Saved Data", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    /*
                    var query = from k in db.DetailSchedules
                                 join l in db.DetailScores on k.DetailID equals l.DetailID
                                 join m in db.Subjects on k.SubjectID equals m.SubjectID
                                 join n in db.Students on l.StudentID equals n.StudentID
                                 join o in db.HeaderSchedules on k.ScheduleID equals o.ScheduleID
                                 join p in db.Teachers on k.TeacherID equals p.TeacherID
                                 where k.SubjectID == subjectid
                                 select new
                                 { a = l.Assignment, db1 = db.DetailScores };
                foreach (var join in query)
                {
                    join.a.Value = join.db1;
                }
               int t = (int)query.Assignment;
                int u = (int)query.MidExam;
                int v = (int)query.FinalExam;

                    t = int.Parse(txtAssignment.Text);
                u = int.Parse(txtMid.Text);
                v = int.Parse(txtFinal.Text);

                    db.SubmitChanges();*/


                }
            }
        }

        private void txtAssignment_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && (int)e.KeyChar != (int)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void txtMid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && (int)e.KeyChar != (int)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void txtFinal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && (int)e.KeyChar != (int)Keys.Back)
            {
                e.Handled = true;
            }
        }
    }
}
