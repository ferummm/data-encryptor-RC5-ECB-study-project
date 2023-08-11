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
    public partial class ConfirmForm : Form
    {
        private readonly string  passPhrase;
        public ConfirmForm(string PassPhraseText)
        {
            InitializeComponent();
            passPhrase = PassPhraseText;
            ConfirmButton.Enabled = false;
            PasswordTextBox.UseSystemPasswordChar = true;
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = (PasswordTextBox.Text == this.passPhrase) ? DialogResult.OK : this.DialogResult = DialogResult.Abort;
        }

        private void ShowСheckBox_CheckedChanged(object sender, EventArgs e)
        {
            PasswordTextBox.UseSystemPasswordChar = !ShowСheckBox.Checked;
        }

        private void PasswordTextBox_TextChanged(object sender, EventArgs e)
        {
            ConfirmButton.Enabled = PasswordTextBox.Text != "";
        }

        private void ConfirmForm_Load(object sender, EventArgs e)
        {
            this.ActiveControl = PasswordTextBox;
        }

    }
}
