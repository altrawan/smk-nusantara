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
    public partial class FrmViewSchedule : Form
    {
        private Form close;
        private Student student;
        public FrmViewSchedule(Form close, Student student)
        {
            InitializeComponent();
            this.close = close;
            this.student = student;
        }

        private void isiDay()
        {
            cbDay.DisplayMember = "Text";
            cbDay.ValueMember = "Value";
            var items = new[]
            {
                new { Text = "Monday", Value = "Monday" },
                new { Text = "Tuesday", Value = "Tuesday" },
                new { Text = "Wednesday", Value = "Wednesday" },
                new { Text = "Thursday", Value = "Thursday" },
                new { Text = "Friday", Value = "Friday" },
                new { Text = "Saturday", Value = "Saturday" },
                new { Text = "Sunday", Value = "Sunday" },
            };
            cbDay.DataSource = items;
        }

        private void className()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                var query = from d in db.DetailClasses
                            where d.StudentID == student.StudentID
                            select new
                            {
                                d.ClassName
                            };
                IDbCommand command = db.GetCommand(query);
                command.Connection.Open();
                IDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    lblName.Text = reader[0].ToString();
                }
            }
        }

        private void tampil()
        {
            try
            {
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    dataGridView1.DataSource = from a in db.DetailSchedules
                                               join b in db.Subjects on a.SubjectID equals b.SubjectID
                                               join c in db.Teachers on a.TeacherID equals c.TeacherID
                                               join d in db.HeaderSchedules on a.ScheduleID equals d.ScheduleID
                                               join e in db.Shifts on a.ShiftID equals e.ShiftID
                                               join f in db.DetailClasses on d.ClassName equals f.ClassName
                                               where f.ClassName == lblName.Text
                                               && f.StudentID == student.StudentID
                                               && a.Day == cbDay.Text
                                               select new
                                               {
                                                   a.SubjectID,
                                                   b.Name,
                                                   a.Day,
                                                   e.Time,
                                                   t = c.Name
                                               };
                    buatHeader();
                }
            } catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
        }

        private void buatHeader()
        {
            dataGridView1.Columns[0].HeaderText = "Subject ID";
            dataGridView1.Columns[1].HeaderText = "Subject";
            dataGridView1.Columns[2].HeaderText = "Day";
            dataGridView1.Columns[3].HeaderText = "Time";
            dataGridView1.Columns[4].HeaderText = "Teacher Name";

            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].Width = 100;
            dataGridView1.Columns[3].Width = 100;
            dataGridView1.Columns[4].Width = 150;
        }

        private void FrmViewSchedule_Load(object sender, EventArgs e)
        {
            isiDay();
            className();
            tampil();
        }

        private void cbDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            tampil();
        }
    }
}
