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
    public partial class FrmReportScore : Form
    {
        private Form close;
        public FrmReportScore(Form close)
        {
            InitializeComponent();
            this.close = close;
        }

        private void isiCombo()
        {
            using (DataClasses1DataContext db = new DataClasses1DataContext())
            {
                comboBox1.DataSource = db.Subjects.Select(s => s.Name).Distinct();
            }
        }

        private void FrmReportScore_Load(object sender, EventArgs e)
        {
            isiCombo();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //
        }
    }
}
