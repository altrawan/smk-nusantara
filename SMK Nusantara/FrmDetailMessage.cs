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
    public partial class FrmDetailMessage : Form
    {
        private Form close;
        public string sender, title, detail;
        public FrmDetailMessage(Form close)
        {
            InitializeComponent();
            this.close = close;
        }
        
        private void getSender()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                
                if (sender.Substring(0, 1) == "T")
                {
                    var query = from t in db.Teachers
                                where t.TeacherID == sender
                                select new
                                {
                                    t.Name
                                };
                    IDbCommand command = db.GetCommand(query);
                    command.Connection.Open();
                    IDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        lblFrom.Text = sender + " - " + reader[0].ToString();
                    }
                } else
                {
                    var query = from s in db.Students
                                where s.StudentID == sender
                                select new
                                {
                                    s.Name
                                };
                    IDbCommand command = db.GetCommand(query);
                    command.Connection.Open();
                    IDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        lblFrom.Text = sender + " - " + reader[0].ToString();
                    }
                }
            }
        }

        private void FrmDetailMessage_Load(object sender, EventArgs e)
        {
            getSender();
            lblTitle.Text = title;
            textBox1.Text = detail;
        }
    }
}
