
namespace CW_RC5
{
    partial class Edit
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.keyLenUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.CancelButton1 = new System.Windows.Forms.Button();
            this.OkButton = new System.Windows.Forms.Button();
            this.checkDigit = new System.Windows.Forms.CheckBox();
            this.checkLowerCase = new System.Windows.Forms.CheckBox();
            this.checkUpperCase = new System.Windows.Forms.CheckBox();
            this.checkSpecChar = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.keyLenUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 69.63351F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.36649F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.CancelButton1, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.OkButton, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.keyLenUpDown1, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.checkLowerCase, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.checkUpperCase, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.checkSpecChar, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.checkDigit, 0, 1);
            this.tableLayoutPanel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.01739F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 59.49367F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40.50633F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 43F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 46F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(382, 265);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(259, 45);
            this.label1.TabIndex = 1;
            this.label1.Text = "Настройка сложности:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 180);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(259, 46);
            this.label2.TabIndex = 2;
            this.label2.Text = "Минимальное кол-во символов:";
            // 
            // keyLenUpDown1
            // 
            this.keyLenUpDown1.Location = new System.Drawing.Point(268, 183);
            this.keyLenUpDown1.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.keyLenUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.keyLenUpDown1.Name = "keyLenUpDown1";
            this.keyLenUpDown1.Size = new System.Drawing.Size(62, 26);
            this.keyLenUpDown1.TabIndex = 0;
            this.keyLenUpDown1.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // CancelButton1
            // 
            this.CancelButton1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton1.Location = new System.Drawing.Point(10, 229);
            this.CancelButton1.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.CancelButton1.Name = "CancelButton1";
            this.CancelButton1.Size = new System.Drawing.Size(100, 30);
            this.CancelButton1.TabIndex = 5;
            this.CancelButton1.Text = "Отмена";
            this.CancelButton1.UseVisualStyleBackColor = true;
            this.CancelButton1.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.Location = new System.Drawing.Point(272, 229);
            this.OkButton.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(100, 30);
            this.OkButton.TabIndex = 4;
            this.OkButton.Text = "ОК";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // checkDigit
            // 
            this.checkDigit.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.checkDigit.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.checkDigit, 2);
            this.checkDigit.Location = new System.Drawing.Point(3, 48);
            this.checkDigit.Name = "checkDigit";
            this.checkDigit.Size = new System.Drawing.Size(84, 24);
            this.checkDigit.TabIndex = 6;
            this.checkDigit.Text = "Цифры";
            this.checkDigit.UseVisualStyleBackColor = true;
            // 
            // checkLowerCase
            // 
            this.checkLowerCase.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.checkLowerCase.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.checkLowerCase, 2);
            this.checkLowerCase.Location = new System.Drawing.Point(3, 79);
            this.checkLowerCase.Name = "checkLowerCase";
            this.checkLowerCase.Size = new System.Drawing.Size(152, 24);
            this.checkLowerCase.TabIndex = 7;
            this.checkLowerCase.Text = "Строчные буквы";
            this.checkLowerCase.UseVisualStyleBackColor = true;
            // 
            // checkUpperCase
            // 
            this.checkUpperCase.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.checkUpperCase.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.checkUpperCase, 2);
            this.checkUpperCase.Location = new System.Drawing.Point(3, 110);
            this.checkUpperCase.Name = "checkUpperCase";
            this.checkUpperCase.Size = new System.Drawing.Size(161, 24);
            this.checkUpperCase.TabIndex = 8;
            this.checkUpperCase.Text = "Прописные буквы";
            this.checkUpperCase.UseVisualStyleBackColor = true;
            // 
            // checkSpecChar
            // 
            this.checkSpecChar.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.checkSpecChar, 2);
            this.checkSpecChar.Location = new System.Drawing.Point(3, 140);
            this.checkSpecChar.Name = "checkSpecChar";
            this.checkSpecChar.Size = new System.Drawing.Size(141, 24);
            this.checkSpecChar.TabIndex = 9;
            this.checkSpecChar.Text = "Спец. символы";
            this.checkSpecChar.UseVisualStyleBackColor = true;
            // 
            // Edit
            // 
            this.AcceptButton = this.OkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 265);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MaximumSize = new System.Drawing.Size(400, 310);
            this.MinimumSize = new System.Drawing.Size(400, 310);
            this.Name = "Edit";
            this.Text = "Настройки парольной фразы";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.keyLenUpDown1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown keyLenUpDown1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button CancelButton1;
        private System.Windows.Forms.CheckBox checkLowerCase;
        private System.Windows.Forms.CheckBox checkUpperCase;
        private System.Windows.Forms.CheckBox checkSpecChar;
        private System.Windows.Forms.CheckBox checkDigit;
    }
}