using System;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CW_RC5
{
    public partial class MainForm : Form
    {
        public static int minKeyLength = 5;

        public static bool iD = false;
        public static bool iLC = false;
        public static bool iUC = false;
        public static bool iSC = false;

        public MainForm()
        {
            InitializeComponent();
            DecryptButton.Enabled = false;
            EncryptButton.Enabled = false;
            DeleteFileButton.Enabled = false;
            PasswordTextBox.UseSystemPasswordChar = true;
            SaveButton.Enabled = false;
        }

        private void ОпрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 aboutBox = new AboutBox1();
            aboutBox.ShowDialog();
        }

        private void ВыходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void НастройкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Edit editBox = new Edit();
            editBox.ShowDialog();
        }

        private void EncryptButton_Click(object sender, EventArgs e)
        {
            RC5CryptoManager rc5 = new RC5CryptoManager();
            string err;
            try
            {
                if ((err = PassPharseHasError(rc5, PasswordTextBox.Text)) != "") throw new ArgumentException(err);
                if (!GotConfirmation()) return;
                byte[] pass_bytes = Encoding.Unicode.GetBytes(PasswordTextBox.Text);
                switch (tabControl1.SelectedIndex)
                {
                    case 0:
                        EncryptFile(rc5, pass_bytes);
                        break;
                    case 1:
                        EncryptMessage(rc5, pass_bytes);
                        break;
                }

                MessageBox.Show("Шифрование завершено.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SelectButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Размер выбранного файла не должен превышать 2Гб.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != DialogResult.OK) return;
            try
            {
                if (new FileInfo(ofd.FileName).Length / 1024 / 1024 / 1024 > 2) throw new ArgumentException("Размер выбранного файла превышает 2Гб.");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            PathTextBox.Text = ofd.FileName;
        }

        private void PathTextBox_TextChanged(object sender, EventArgs e)
        {
            DecryptButton.Enabled = PathTextBox.Text != "";
            EncryptButton.Enabled = PathTextBox.Text != "";
            DeleteFileButton.Enabled = PathTextBox.Text != "";
        }

        private void DecryptButton_Click(object sender, EventArgs e)
        {
            RC5CryptoManager rc5 = new RC5CryptoManager();
            try
            {
                if (PasswordTextBox.Text == "") throw new ArgumentException("Введите парольную фразу.");
                byte[] pass_bytes = Encoding.Unicode.GetBytes(PasswordTextBox.Text);
                switch (tabControl1.SelectedIndex)
                {
                    case 0:
                        DecryptFile(rc5, pass_bytes);
                        break;
                    case 1:
                        DecryptMessage(rc5, pass_bytes);
                        break;
                }

                MessageBox.Show("Расшифрование завершено.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowСheckBox_CheckedChanged(object sender, EventArgs e)
        {
            PasswordTextBox.UseSystemPasswordChar = !ShowСheckBox.Checked;
        }

        private void TabControl1_Selected(object sender, TabControlEventArgs e)
        {
            DecryptButton.Enabled = IsTabFileInUse() || IsTabMsgInUse();
            EncryptButton.Enabled = IsTabFileInUse() || IsTabMsgInUse();
            DeleteFileButton.Enabled = IsTabFileInUse();
            SaveButton.Enabled = IsTabMsgInUse();
        }

        private void MessageTextBox_TextChanged(object sender, EventArgs e)
        {
            DecryptButton.Enabled = MessageTextBox.Text != "";
            EncryptButton.Enabled = MessageTextBox.Text != "";
            SaveButton.Enabled = MessageTextBox.Text != "";
        }

        private void DeleteFileButton_Click(object sender, EventArgs e)
        {
            DialogResult dr_deletingFile = MessageBox.Show("Вы действительно хотите удалить выбранный файл без возможности восстановления?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr_deletingFile == DialogResult.Yes)
            {
                try
                {
                    File.Delete(PathTextBox.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                PathTextBox.Text = "";
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog()
            {
                Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*",
                RestoreDirectory = true
            };

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Stream messageStream = saveFileDialog1.OpenFile();
                using (StreamWriter outputFile = new StreamWriter(messageStream))
                {
                    outputFile.Write(MessageTextBox.Text);
                }
                messageStream.Close();
                MessageBox.Show("Сообщение сохранено в файл.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.ActiveControl = (tabControl1.SelectedIndex == 1) ? MessageTextBox : PasswordTextBox;
        }

        //------------------------------------------------------------------------------------------//
        public bool IsTabFileInUse()
        {
            if (PathTextBox.Text != "" && tabControl1.SelectedIndex == 0) return true;
            return false;
        }

        public bool IsTabMsgInUse()
        {
            if (MessageTextBox.Text != "" && tabControl1.SelectedIndex == 1) return true;
            return false;
        }

        public void EncryptMessage(RC5CryptoManager rc5, byte[] pass_bytes)
        {
            byte[] text_bytes = Encoding.Unicode.GetBytes(MessageTextBox.Text);     // get bytes from textbox
            byte[] enc_bytes = rc5.Encrypt(text_bytes, pass_bytes);                 // encrypt
            MessageTextBox.Text = Convert.ToBase64String(enc_bytes);                // write to textbox
        }

        public void EncryptFile(RC5CryptoManager rc5, byte[] pass_bytes)
        {
            if (!File.Exists(PathTextBox.Text)) throw new ArgumentException("Файл, который вы хотите зашифровать, не существует.");
            byte[] file_bytes = File.ReadAllBytes(PathTextBox.Text);      // get bytes from input file 
            byte[] enc_bytes = rc5.Encrypt(file_bytes, pass_bytes);       // encrypt
            SelectFile_WriteBytes(enc_bytes);                             // write to file
        }

        public void DecryptMessage(RC5CryptoManager rc5, byte[] pass_bytes)
        {
            byte[] text_bytes;
            try 
            {
                text_bytes = Convert.FromBase64String(MessageTextBox.Text);     // get bytes from textbox  
            } 
            catch (Exception)
            {
                throw new ArgumentException("Расшифрование невозможно");
            }
            byte[] dec_bytes = rc5.Decrypt(text_bytes, pass_bytes);                  // decrypt
            if (!rc5.IsPassPhraseCorrect) throw new ArgumentException("Неверная парольная фраза.");
            MessageTextBox.Text = Encoding.Unicode.GetString(dec_bytes);            // write to textbox
        }

        public void DecryptFile(RC5CryptoManager rc5, byte[] pass_bytes)
        {
            if (!File.Exists(PathTextBox.Text)) throw new ArgumentException("Файл, который вы хотите расшифровать, не существует.");
            byte[] file_bytes = File.ReadAllBytes(PathTextBox.Text);                // get bytes from input file
            byte[] dec_bytes = rc5.Decrypt(file_bytes, pass_bytes);                 // decrypt
            if (!rc5.IsPassPhraseCorrect) throw new ArgumentException("Неверная парольная фраза.");
            SelectFile_WriteBytes(dec_bytes);                                       // write to file
        }

        public void SelectFile_WriteBytes(byte[] bytes)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog()
            {
                Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*",
                RestoreDirectory = true
            };

            if (saveFileDialog1.ShowDialog() != DialogResult.OK) throw new ArgumentException("Операция была прервана, так как не выбран файл для сохранения.");

            using (Stream stream = saveFileDialog1.OpenFile())
            {
                stream.Write(bytes, 0, bytes.Length);                               // write to file
            }
        }

        public string PassPharseHasError(RC5CryptoManager rc5, string pass)
        {
            if (pass.Length < minKeyLength)
                return "Количество символов в парольной фразе должно быть не меньше " + minKeyLength + ".";

            bool[] check;

            if ((check = rc5.CheckPasswordPhrase(pass, iD, iLC, iUC, iSC)) == null)
                return "";

            string err = "Парольная фраза должна содержать: \n";
            err += (!check[0]) ? "Цифру\n" : "";
            err += (!check[1]) ? "Строчную букву\n" : "";
            err += (!check[2]) ? "Прописную букву\n" : "";
            err += (!check[3]) ? "Спец. символ\n" : "";

            return err;
        }

        private bool GotConfirmation()
        {
            ConfirmForm confirm = new ConfirmForm(PasswordTextBox.Text);
            DialogResult dr;
            if ((dr = confirm.ShowDialog()) == DialogResult.Cancel) return false;
            if (dr == DialogResult.Abort) throw new ArgumentException("Парольные фразы не совпадают.");
            return true;
        }
    }
} 

