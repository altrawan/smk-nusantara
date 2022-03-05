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
    public partial class FrmFinalize : Form
    {
        private Form close;
        public FrmFinalize(Form close)
        {
            InitializeComponent();
            this.close = close;
        }

        private void isiCombo()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                cbClass.DataSource = db.Classes.Select(s => s.ClassName);
            }
        }

        private void tampil()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                dataGridView1.DataSource = from a in db.DetailSchedules
                                           join b in db.Subjects on a.SubjectID equals b.SubjectID
                                           join c in db.Teachers on a.TeacherID equals c.TeacherID
                                           join d in db.HeaderSchedules on a.ScheduleID equals d.ScheduleID
                                           where d.ClassName == cbClass.SelectedItem.ToString()
                                           select new
                                           {
                                               a.SubjectID,
                                               b.Name,
                                               a.TeacherID,
                                               t = c.Name,
                                               a.Day,
                                               a.ShiftID
                                           };
                buatHeader();
            }
        }

        private void buatHeader()
        {
            dataGridView1.Columns[0].HeaderText = "Subject ID";
            dataGridView1.Columns[1].HeaderText = "Subject";
            dataGridView1.Columns[2].HeaderText = "Teacher ID";
            dataGridView1.Columns[3].HeaderText = "Teacher Name";
            dataGridView1.Columns[4].HeaderText = "Day";
            dataGridView1.Columns[5].HeaderText = "Shift";

            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].Width = 100;
            dataGridView1.Columns[3].Width = 150;
            dataGridView1.Columns[4].Width = 100;
            dataGridView1.Columns[5].Width = 50;
        }

        private void finalize()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                var query = from h in db.HeaderSchedules
                            where h.ClassName == cbClass.Text && h.Finalize == 1
                            select new
                            {
                                h.Finalize
                            };
                IDbCommand command = db.GetCommand(query);
                command.Connection.Open();
                IDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    btnFinalize.Enabled = false;
                }
                else if (!reader.Read())
                {
                    btnFinalize.Enabled = true;
                }
            }
        }

        private void FrmFinalize_Load(object sender, EventArgs e)
        {
            isiCombo();
            tampil();
        }

        private void cbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            finalize();
            tampil();
        }

        private void btnFinalize_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount < 16)
            {
                MessageBox.Show("Shift masih kosong");
            }
            else if (dataGridView1.RowCount > 16)
            {
                MessageBox.Show("Shift melebihi batas");
            }
            else
            {
                if (MessageBox.Show("Apakah anda yakin ingin memfinalisasi kelas ini?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (DataClasses1DataContext db = new DataClasses1DataContext())
                    {
                        HeaderSchedule h = db.HeaderSchedules.Where(s => s.ClassName == cbClass.Text).FirstOrDefault();
                        h.Finalize = 1;
                        db.SubmitChanges();
                        MessageBox.Show("Successfully Saved Data", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
    }
}
