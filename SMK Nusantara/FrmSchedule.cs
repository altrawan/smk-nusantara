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
    public partial class FrmSchedule : Form
    {
        private Form close;
        private string id, subjek;
        private int grade, scheduleid;
        public FrmSchedule(Form close)
        {
            InitializeComponent();
            this.close = close;
        }

        private void isiClass()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                cbClass.DataSource = db.Classes.Select(s => s.ClassName);
            }
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

        private void tampil()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                dataGridView1.DataSource = from a in db.DetailSchedules
                                           join b in db.Subjects on a.SubjectID equals b.SubjectID
                                           join c in db.Teachers on a.TeacherID equals c.TeacherID
                                           join d in db.HeaderSchedules on a.ScheduleID equals d.ScheduleID
                                           where d.ClassName == cbClass.SelectedItem.ToString()
                                           && a.Day == cbDay.Text
                                           select new
                                           {
                                               a.SubjectID,
                                               b.Name,
                                               a.TeacherID,
                                               t = c.Name,
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
            dataGridView1.Columns[4].HeaderText = "Shift";

            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].Width = 100;
            dataGridView1.Columns[3].Width = 150;
            dataGridView1.Columns[4].Width = 50;
        }

        private void isiCombo(int x)
        {
            cbSubject.Text = dataGridView1.Rows[x].Cells[0].Value + " - " + dataGridView1.Rows[x].Cells[1].Value;
            cbShift.Text = dataGridView1.Rows[x].Cells[4].Value.ToString();
            cbTeacher.Text = dataGridView1.Rows[x].Cells[2].Value + " - " + dataGridView1.Rows[x].Cells[3].Value;
            id = dataGridView1.Rows[x].Cells[4].Value.ToString();
        }

        private void disabled()
        {
            cbSubject.Enabled = false;
            cbShift.Enabled = false;
            cbTeacher.Enabled = false;
        }

        private void enabled()
        {
            cbSubject.Enabled = true;
            cbShift.Enabled = true;
            cbTeacher.Enabled = true;
        }

        private void bersih()
        {
            cbSubject.Text = "";
            cbShift.Text = "";
            cbTeacher.Text = "";
        }

        private void isiSubject()
        {
            //cbSubject.Items.Clear();
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                /*
                var dataSource = db.Subjects.Select
                (s => new
                {
                    Display = s.SubjectID + " - " + s.Name,
                    Value = s.SubjectID
                }).ToList();*/
                var dataSource = from s in db.Subjects
                                 where s.Grade == grade
                                 select new
                                 {
                                     Display = s.SubjectID + " - " + s.Name,
                                     Value = s.SubjectID
                                 };

                cbSubject.DisplayMember = "Display";
                cbSubject.ValueMember = "Value";
                cbSubject.DataSource = dataSource;
                
            }
        }

        private void isiTeacher()
        {
            //cbTeacher.Items.Clear();
            subjek = cbSubject.Text.Substring(0, 5);
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                var dataSource = from t in db.Teachers
                                 join e in db.Expertises
                                 on t.TeacherID equals e.TeacherID
                                 where e.SubjectID == subjek
                                 select new
                                 {
                                     Display = e.TeacherID + " - " + t.Name,
                                     Value = t.TeacherID
                                 };

                cbTeacher.DisplayMember = "Display";
                cbTeacher.ValueMember = "Value";
                cbTeacher.DataSource = dataSource;
            }
        }

        private void isiShift()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                cbShift.DataSource = db.Shifts.Select(s => s.ShiftID);
            }
        }

        private void btnNonAktif()
        {
            btnInsert.Visible = true;
            btnUpdate.Visible = true;
            btnDelete.Visible = true;
            btnSave.Visible = false;
            btnCancel.Visible = false;
        }

        private void btnAktif()
        {
            btnInsert.Visible = false;
            btnUpdate.Visible = false;
            btnDelete.Visible = false;
            btnSave.Visible = true;
            btnCancel.Visible = true;
        }

        private void awal()
        {
            isiClass();
            isiDay();
            isiSubject();
            isiShift();
            isiTeacher();
            bersih();
            disabled();
            btnNonAktif();
        }

        private void FrmSchedule_Load(object sender, EventArgs e)
        {
            awal();
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
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            isiCombo(e.RowIndex);
        }

        private void cbSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiTeacher();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            (new FrmViewSubjectNeeded()).Show();
        }

        private bool newdata;
        private void btnInsert_Click(object sender, EventArgs e)
        {
            enabled();
            bersih();
            isiSubject();
            isiShift();
            isiTeacher();
            btnAktif();
            newdata = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (cbSubject.Text == "" || cbShift.Text == "" || cbTeacher.Text == "")
            {
                MessageBox.Show("Data Cant Be Empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                enabled();
                btnAktif();
                newdata = false;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                if (cbSubject.Text == "" || cbShift.Text == "" || cbTeacher.Text == "")
                {
                    MessageBox.Show("Data Cant Be Empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (MessageBox.Show("Are you sure want to delete data ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        //DetailSchedule ds = db.DetailSchedules.Where(s => s.SubjectID == subjek).FirstOrDefault();
                        var delete = from d in db.DetailSchedules
                                     where d.SubjectID == cbSubject.Text.Substring(0, 5)
                                     && d.ShiftID == int.Parse(cbShift.Text)
                                     && d.TeacherID == cbTeacher.Text.Substring(0, 5)
                                     && d.Day == cbDay.Text
                                     select d;
                        db.DetailSchedules.DeleteAllOnSubmit(delete);
                        db.SubmitChanges();
                        awal();
                        MessageBox.Show("Successfully Deleted Data", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    if (cbShift.Text == dataGridView1.Rows[i].Cells[4].Value.ToString())
                    {
                        MessageBox.Show("Ensure in 1 class, the schedule cannot collide each other", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                using (DataClasses1DataContext db = new DataClasses1DataContext())
                {
                    if (cbSubject.Text == "" || cbShift.Text == "" || cbTeacher.Text == "")
                    {
                        MessageBox.Show("Data Cant Be Empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else if (dataGridView1.RowCount == 8)
                    {
                        MessageBox.Show("Shift sudah penuh maksimal shift '8'", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        if (cbClass.Text == "XA")
                        {
                            scheduleid = 1;
                        }
                        else if (cbClass.Text == "XB")
                        {
                            scheduleid = 2;
                        }
                        else if (cbClass.Text == "XIA")
                        {
                            scheduleid = 3;
                        }
                        else if (cbClass.Text == "XIB")
                        {
                            scheduleid = 4;
                        }
                        else if (cbClass.Text == "XIIA")
                        {
                            scheduleid = 5;
                        }
                        else if (cbClass.Text == "XIIB")
                        {
                            scheduleid = 6;
                        }
                        string subjectid = cbSubject.Text.Substring(0, 5);
                        string teacherid = cbTeacher.Text.Substring(0, 5);
                        if (newdata == true)
                        {
                            db.DetailSchedules.InsertOnSubmit(new DetailSchedule()
                            {
                                ScheduleID = scheduleid,
                                SubjectID = subjectid,
                                TeacherID = teacherid,
                                ShiftID = int.Parse(cbShift.Text),
                                Day = cbDay.Text
                            });
                            db.SubmitChanges();
                            awal();
                            MessageBox.Show("Successfully Added Data", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            DetailSchedule d = db.DetailSchedules.Where(s => s.ScheduleID == scheduleid
                                            && s.ShiftID == int.Parse(cbShift.Text)
                                            && s.Day == cbDay.Text).FirstOrDefault();
                            d.ScheduleID = scheduleid;
                            d.SubjectID = subjectid;
                            d.TeacherID = teacherid;
                            d.ShiftID = int.Parse(cbShift.Text);
                            d.Day = cbDay.Text;
                            db.SubmitChanges();
                            awal();
                            MessageBox.Show("Successfully Saved Data", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Saving Data, Please Contact Admin" + "\n" + ex.ToString(), "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do You Want to Cancel ?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                awal();
            }
        }

        private void cbDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            tampil();
        }
    }
}
