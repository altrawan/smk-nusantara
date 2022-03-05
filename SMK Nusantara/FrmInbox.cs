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
    public partial class FrmInbox : Form
    {
        private Form close;
        private User user;
        private string id, senttime;
        
        public FrmInbox(Form close, User user)
        {
            InitializeComponent();
            this.close = close;
            this.user = user;
        }

        private void total()
        {
            label3.Text = "Total Message : " + dataGridView1.RowCount;
        }

        public void tampil()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                dataGridView1.DataSource = from m in db.Messages
                                           where m.Receiver == user.Username
                                           select new
                                           {
                                               m.Sender,
                                               m.Title,
                                               m.Detail,
                                               m.SentTime,
                                               m.Status
                                           };
                buatHeader();
                total();
            }
        }

        private void buatHeader()
        {
            dataGridView1.Columns[0].HeaderText = "Sender";
            dataGridView1.Columns[1].HeaderText = "Title";
            dataGridView1.Columns[2].HeaderText = "Content";
            dataGridView1.Columns[3].HeaderText = "Sent Time";
            dataGridView1.Columns[4].HeaderText = "Status";

            dataGridView1.Columns[0].Width = 80;
            dataGridView1.Columns[1].Width = 100;
            dataGridView1.Columns[2].Width = 300;
            dataGridView1.Columns[3].Width = 100;
            dataGridView1.Columns[4].Width = 60;
        }
        
        private void newMessage()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                var query = (from m in db.Messages
                            where m.Status == "Unread"
                            && m.Receiver == user.Username
                            select new
                            {
                                m.Status
                            }).ToList();
               if (query.Count > 0)
               {
                    MessageBox.Show("You Have : " + query.Count + " New Message");
                }
            }
        }

        private void search()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                var searchValue = txtSearch.Text.Trim();
                dataGridView1.DataSource = (from m in db.Messages
                                           where m.Receiver == user.Username 
                                           && (m.Sender.Contains(searchValue)
                                           || m.Title.Contains(searchValue)
                                           || m.Detail.Contains(searchValue))
                                           select new
                                           {
                                               m.Sender,
                                               m.Title,
                                               m.Detail,
                                               m.SentTime,
                                               m.Status
                                           }).ToList();
                buatHeader();
                total();
            }
        }

        private void awal()
        {
            tampil();
            txtSearch.Clear();
            btnOpen.Enabled = false;
            btnDelete.Enabled = false;
        }

        private void FrmInbox_Load(object sender, EventArgs e)
        {
            awal();
            newMessage();
        }

        private void btnCompose_Click(object sender, EventArgs e)
        {
            (new FrmSendMessage()).Show();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                DataGridViewRow r = dataGridView1.CurrentRow;
                id = r.Cells["Sender"].Value.ToString();
                senttime = r.Cells["SentTime"].Value.ToString();

                Message m = db.Messages.Where(s => s.Receiver == user.Username
                                            && s.Sender == id
                                            && s.SentTime == senttime).FirstOrDefault();
                m.Status = "Read";
                db.SubmitChanges();

                FrmDetailMessage detailMessage = new FrmDetailMessage(this);
                detailMessage.sender = r.Cells["Sender"].Value.ToString();
                detailMessage.title = r.Cells["Title"].Value.ToString();
                detailMessage.detail = r.Cells["Detail"].Value.ToString();
                detailMessage.Show();
                awal();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                if (MessageBox.Show("Are You Sure To Delete The Message From : " + id + " ?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var delete = from m in db.Messages
                                 where m.Sender == id
                                 && m.Receiver == user.Username
                                 && m.SentTime == senttime
                                 select m;
                    db.Messages.DeleteAllOnSubmit(delete);
                    db.SubmitChanges();
                    awal();
                    MessageBox.Show("Successfully Deleted Data", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnOpen.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtSearch.Text == "")
            {
                tampil();
            }
            else
            {
                search();
            }
        }
    }
}
