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
    public partial class FrmSendMessage : Form
    {
        public FrmSendMessage()
        {
            InitializeComponent();
        }

        private void isiType()
        {
            comboBox1.DisplayMember = "Text";
            comboBox1.ValueMember = "Value";
            var items = new[]
            {
                new { Text = "Teacher", Value = "Teacher" },
                new { Text = "Student", Value = "Student" }
            };
            comboBox1.DataSource = items;
        }

        private void isiTarget()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                if (comboBox1.Text == "Teacher")
                {
                    comboBox2.Items.Clear();
                    var query = from t in db.Teachers
                                select new
                                {
                                    t.TeacherID,
                                    t.Name
                                };
                    IDbCommand command = db.GetCommand(query);
                    command.Connection.Open();
                    IDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        comboBox2.Items.Add(reader[0] + " - " + reader[1]);
                    }
                }
                else if (comboBox1.Text == "Student")
                {
                    comboBox2.Items.Clear();
                    var query = from s in db.Students
                                select new
                                {
                                    s.StudentID,
                                    s.Name
                                };
                    IDbCommand command = db.GetCommand(query);
                    command.Connection.Open();
                    IDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        comboBox2.Items.Add(reader[0] + " - " + reader[1]);
                    }
                }
            }
        }

        private void bersih()
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        private void FrmSendMessage_Load(object sender, EventArgs e)
        {
            isiType();
            comboBox2.Focus();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiTarget();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {

        }
    }
}
