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
    public partial class FrmViewScheduleTeacher : Form
    {
        private Form close;
        private Teacher teacher;
        private string subjectid, day, time;
        public FrmViewScheduleTeacher(Form close, Teacher teacher)
        {
            InitializeComponent();
            this.close = close;
            this.teacher = teacher;
        }

        private void tampilGuru()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                dataGridView1.DataSource = from a in db.DetailSchedules
                                           join b in db.Subjects on a.SubjectID equals b.SubjectID
                                           join c in db.Shifts on a.ShiftID equals c.ShiftID
                                           join d in db.HeaderSchedules on a.ScheduleID equals d.ScheduleID
                                           where a.TeacherID == teacher.TeacherID
                                           select new
                                           {
                                               a.SubjectID,
                                               b.Name,
                                               d.ClassName,
                                               a.Day,
                                               c.Time
                                           };
                buatHeaderGuru();
            }
        }

        private void buatHeaderGuru()
        {
            dataGridView1.Columns[0].HeaderText = "Subject ID";
            dataGridView1.Columns[1].HeaderText = "Subject";
            dataGridView1.Columns[2].HeaderText = "Class Name";
            dataGridView1.Columns[3].HeaderText = "Day";
            dataGridView1.Columns[4].HeaderText = "Time";

            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].Width = 200;
            dataGridView1.Columns[2].Width = 100;
            dataGridView1.Columns[3].Width = 100;
            dataGridView1.Columns[4].Width = 100;
        }

        private void tampilSiswa()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                dataGridView2.DataSource = from a in db.DetailSchedules
                                           join b in db.Subjects on a.SubjectID equals b.SubjectID
                                           join c in db.Shifts on a.ShiftID equals c.ShiftID
                                           join d in db.HeaderSchedules on a.ScheduleID equals d.ScheduleID
                                           join e in db.Classes on d.ClassName equals e.ClassName
                                           join f in db.DetailClasses on e.ClassName equals f.ClassName
                                           join g in db.Students on f.StudentID equals g.StudentID
                                           where a.Day == day && c.Time == time && b.SubjectID == subjectid
                                           select new
                                           {
                                               g.StudentID,
                                               g.Name,
                                               g.Gender
                                           };
                buatHeaderSiswa();
            }
        }

        private void buatHeaderSiswa()
        {
            dataGridView2.Columns[0].HeaderText = "Student ID";
            dataGridView2.Columns[1].HeaderText = "Student Name";
            dataGridView2.Columns[2].HeaderText = "Gender";

            dataGridView2.Columns[0].Width = 100;
            dataGridView2.Columns[1].Width = 200;
            dataGridView2.Columns[2].Width = 80;
        }

        private void isiSiswa(int x)
        {
            subjectid = dataGridView1.Rows[x].Cells[0].Value.ToString();
            day = dataGridView1.Rows[x].Cells[3].Value.ToString();
            time = dataGridView1.Rows[x].Cells[4].Value.ToString();
        }

        private void FrmViewScheduleTeacher_Load(object sender, EventArgs e)
        {
            tampilGuru();
            //tampilSiswa();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            isiSiswa(e.RowIndex);
            tampilSiswa();
        }
    }
}
