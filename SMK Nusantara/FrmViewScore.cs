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
    public partial class FrmViewScore : Form
    {
        private Form close;
        private Student student;
        public FrmViewScore(Form close, Student student)
        {
            InitializeComponent();
            this.close = close;
            this.student = student;
        }

        private void tampil()
        {

            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                dataGridView1.DataSource = from v in db.View_1s
                                           where v.StudentID == student.StudentID
                                           orderby v.SubjectID ascending
                                           select new
                                           {
                                               v.SubjectID,
                                               v.Expr1,
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
            dataGridView1.Columns[1].HeaderText = "Subject ID";
            dataGridView1.Columns[2].HeaderText = "Subject Name";
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

        private void FrmViewScore_Load(object sender, EventArgs e)
        {
            tampil();
        }
    }
}
