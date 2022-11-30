
namespace CW_RC5
{
    partial class AboutBox1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.InfoBox1 = new System.Windows.Forms.TextBox();
            this.OkButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // InfoBox1
            // 
            this.InfoBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InfoBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.InfoBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.InfoBox1.Enabled = false;
            this.InfoBox1.Location = new System.Drawing.Point(15, 25);
            this.InfoBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.InfoBox1.Multiline = true;
            this.InfoBox1.Name = "InfoBox1";
            this.InfoBox1.ReadOnly = true;
            this.InfoBox1.Size = new System.Drawing.Size(332, 120);
            this.InfoBox1.TabIndex = 0;
            this.InfoBox1.Text = "Курсовая работа. Вариант 21.\r\n\r\nА-05-19 Андреева Полина\r\n\r\nПрограммна реализация " +
    "криптоалгоритма RC5 для шифрования в режиме электронной кодовой книги (ECB)\r\n";
            this.InfoBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.Location = new System.Drawing.Point(267, 162);
            this.OkButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(80, 30);
            this.OkButton.TabIndex = 1;
            this.OkButton.Text = "ОК";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // AboutBox1
            // 
            this.AcceptButton = this.OkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(362, 205);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.InfoBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(380, 400);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(380, 215);
            this.Name = "AboutBox1";
            this.Padding = new System.Windows.Forms.Padding(12, 11, 12, 11);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "О программе";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox InfoBox1;
        private System.Windows.Forms.Button OkButton;
    }
}
