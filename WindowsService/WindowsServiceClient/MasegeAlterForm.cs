using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsServiceClient
{
    public partial class MasegeAlterForm : Form
    {
        public MasegeAlterForm(string massage)
        {
            InitializeComponent();
            this.MassageText.Text = massage;
            this.MassageText.Width = 150;
            this.MassageText.Height = 150; 
            this.Opacity = 1;
        }

        private void MassageText_Click(object sender, EventArgs e)
        {

        }

        private void MasegeAlterForm_Load(object sender, EventArgs e)
        {

        }
    }
}
