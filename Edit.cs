using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CW_RC5
{
    public partial class Edit : Form
    {
        public Edit()
        {
            InitializeComponent();
            checkDigit.Checked = MainForm.iD;
            checkLowerCase.Checked = MainForm.iLC;
            checkUpperCase.Checked = MainForm.iUC;
            checkSpecChar.Checked = MainForm.iSC;
            keyLenUpDown1.Value = MainForm.minKeyLength;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            MainForm.iD = checkDigit.Checked; 
            MainForm.iLC = checkLowerCase.Checked;
            MainForm.iUC = checkUpperCase.Checked;
            MainForm.iSC = checkSpecChar.Checked;
            MainForm.minKeyLength = (int)keyLenUpDown1.Value;
            Close();
        }
    }
}
