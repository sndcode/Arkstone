using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arkstone
{
    public partial class frmLoader : Form
    {
        public frmLoader()
        {
            InitializeComponent();
        }

        private void frmLoader_Load(object sender, EventArgs e)
        {
            this.Text = "Waiting for wow...";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Process.GetProcessesByName(frmTrainer.ExeName).Length != 0) 
            {
                textStatus.Text = "Trainer Ready";
                timer1.Stop();
                this.Hide();
                frmTrainer trn = new frmTrainer();
                trn.ShowDialog();
            }
            else
            {
                textStatus.Text = "Searching for " + frmTrainer.ExeName + " Process..."; 
             }
        }
    }
}
