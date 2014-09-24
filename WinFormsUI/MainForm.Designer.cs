﻿namespace WinFormsUI
{
    partial class MainForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxMessage = new System.Windows.Forms.TextBox();
            this.textBoxCrypted = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel = new System.Windows.Forms.Panel();
            this.buttonGo = new System.Windows.Forms.Button();
            this.radioButtonDecrypt = new System.Windows.Forms.RadioButton();
            this.radioButtonEncrypt = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonScrambler = new System.Windows.Forms.RadioButton();
            this.radioButtonCaesar = new System.Windows.Forms.RadioButton();
            this.textBoxEncodeKey = new System.Windows.Forms.TextBox();
            this.textBoxDecodeKey = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBoxOneKey = new System.Windows.Forms.CheckBox();
            this.checkBoxAreSame = new System.Windows.Forms.CheckBox();
            this.panel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Сообщение";
            // 
            // textBoxMessage
            // 
            this.textBoxMessage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxMessage.Location = new System.Drawing.Point(12, 29);
            this.textBoxMessage.Multiline = true;
            this.textBoxMessage.Name = "textBoxMessage";
            this.textBoxMessage.Size = new System.Drawing.Size(365, 205);
            this.textBoxMessage.TabIndex = 1;
            // 
            // textBoxCrypted
            // 
            this.textBoxCrypted.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCrypted.Location = new System.Drawing.Point(462, 29);
            this.textBoxCrypted.Multiline = true;
            this.textBoxCrypted.Name = "textBoxCrypted";
            this.textBoxCrypted.Size = new System.Drawing.Size(365, 205);
            this.textBoxCrypted.TabIndex = 3;
            this.textBoxCrypted.TextChanged += new System.EventHandler(this.textBoxCrypted_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(459, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(150, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Зашифрованное сообщение";
            // 
            // panel
            // 
            this.panel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panel.Controls.Add(this.buttonGo);
            this.panel.Controls.Add(this.radioButtonDecrypt);
            this.panel.Controls.Add(this.radioButtonEncrypt);
            this.panel.Controls.Add(this.label4);
            this.panel.Controls.Add(this.label3);
            this.panel.Location = new System.Drawing.Point(391, 29);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(58, 119);
            this.panel.TabIndex = 0;
            // 
            // buttonGo
            // 
            this.buttonGo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGo.Location = new System.Drawing.Point(10, 86);
            this.buttonGo.Name = "buttonGo";
            this.buttonGo.Size = new System.Drawing.Size(40, 30);
            this.buttonGo.TabIndex = 0;
            this.buttonGo.Text = "Go";
            this.buttonGo.UseVisualStyleBackColor = true;
            this.buttonGo.Click += new System.EventHandler(this.buttonGo_Click);
            // 
            // radioButtonDecrypt
            // 
            this.radioButtonDecrypt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonDecrypt.AutoSize = true;
            this.radioButtonDecrypt.Location = new System.Drawing.Point(22, 44);
            this.radioButtonDecrypt.Name = "radioButtonDecrypt";
            this.radioButtonDecrypt.Size = new System.Drawing.Size(14, 13);
            this.radioButtonDecrypt.TabIndex = 2;
            this.radioButtonDecrypt.UseVisualStyleBackColor = true;
            // 
            // radioButtonEncrypt
            // 
            this.radioButtonEncrypt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.radioButtonEncrypt.AutoSize = true;
            this.radioButtonEncrypt.Checked = true;
            this.radioButtonEncrypt.Location = new System.Drawing.Point(22, 19);
            this.radioButtonEncrypt.Name = "radioButtonEncrypt";
            this.radioButtonEncrypt.Size = new System.Drawing.Size(14, 13);
            this.radioButtonEncrypt.TabIndex = 1;
            this.radioButtonEncrypt.TabStop = true;
            this.radioButtonEncrypt.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "<-------------";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "------------->";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButtonScrambler);
            this.groupBox1.Controls.Add(this.radioButtonCaesar);
            this.groupBox1.Location = new System.Drawing.Point(12, 368);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(815, 82);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Алгоритмы";
            // 
            // radioButtonScrambler
            // 
            this.radioButtonScrambler.AutoSize = true;
            this.radioButtonScrambler.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.radioButtonScrambler.Location = new System.Drawing.Point(87, 19);
            this.radioButtonScrambler.Name = "radioButtonScrambler";
            this.radioButtonScrambler.Size = new System.Drawing.Size(68, 30);
            this.radioButtonScrambler.TabIndex = 1;
            this.radioButtonScrambler.Text = "Скремблер";
            this.radioButtonScrambler.UseVisualStyleBackColor = true;
            // 
            // radioButtonCaesar
            // 
            this.radioButtonCaesar.AutoSize = true;
            this.radioButtonCaesar.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.radioButtonCaesar.Checked = true;
            this.radioButtonCaesar.Location = new System.Drawing.Point(6, 19);
            this.radioButtonCaesar.Name = "radioButtonCaesar";
            this.radioButtonCaesar.Size = new System.Drawing.Size(49, 30);
            this.radioButtonCaesar.TabIndex = 0;
            this.radioButtonCaesar.TabStop = true;
            this.radioButtonCaesar.Text = "Цезарь";
            this.radioButtonCaesar.UseVisualStyleBackColor = true;
            // 
            // textBoxEncodeKey
            // 
            this.textBoxEncodeKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxEncodeKey.Location = new System.Drawing.Point(12, 262);
            this.textBoxEncodeKey.Multiline = true;
            this.textBoxEncodeKey.Name = "textBoxEncodeKey";
            this.textBoxEncodeKey.Size = new System.Drawing.Size(365, 100);
            this.textBoxEncodeKey.TabIndex = 2;
            // 
            // textBoxDecodeKey
            // 
            this.textBoxDecodeKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxDecodeKey.Location = new System.Drawing.Point(462, 262);
            this.textBoxDecodeKey.Multiline = true;
            this.textBoxDecodeKey.Name = "textBoxDecodeKey";
            this.textBoxDecodeKey.Size = new System.Drawing.Size(365, 100);
            this.textBoxDecodeKey.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 246);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Ключ шифрования";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(459, 246);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Ключ дешифрования";
            // 
            // checkBoxOneKey
            // 
            this.checkBoxOneKey.AutoSize = true;
            this.checkBoxOneKey.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.checkBoxOneKey.Location = new System.Drawing.Point(383, 297);
            this.checkBoxOneKey.Name = "checkBoxOneKey";
            this.checkBoxOneKey.Size = new System.Drawing.Size(74, 31);
            this.checkBoxOneKey.TabIndex = 5;
            this.checkBoxOneKey.Text = "Общий ключ";
            this.checkBoxOneKey.UseVisualStyleBackColor = true;
            // 
            // checkBoxAreSame
            // 
            this.checkBoxAreSame.AutoSize = true;
            this.checkBoxAreSame.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.checkBoxAreSame.Enabled = false;
            this.checkBoxAreSame.Location = new System.Drawing.Point(384, 174);
            this.checkBoxAreSame.Name = "checkBoxAreSame";
            this.checkBoxAreSame.Size = new System.Drawing.Size(72, 31);
            this.checkBoxAreSame.TabIndex = 11;
            this.checkBoxAreSame.Text = "Совпадение";
            this.checkBoxAreSame.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 462);
            this.Controls.Add(this.checkBoxAreSame);
            this.Controls.Add(this.checkBoxOneKey);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxDecodeKey);
            this.Controls.Add(this.textBoxEncodeKey);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.textBoxCrypted);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxMessage);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.Text = "Crypto";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxMessage;
        private System.Windows.Forms.TextBox textBoxCrypted;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Button buttonGo;
        private System.Windows.Forms.RadioButton radioButtonDecrypt;
        private System.Windows.Forms.RadioButton radioButtonEncrypt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox textBoxEncodeKey;
        private System.Windows.Forms.TextBox textBoxDecodeKey;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkBoxOneKey;
        private System.Windows.Forms.RadioButton radioButtonScrambler;
        private System.Windows.Forms.RadioButton radioButtonCaesar;
        private System.Windows.Forms.CheckBox checkBoxAreSame;
    }
}
