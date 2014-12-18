namespace WinFormsUI
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
            this.labelCypher = new System.Windows.Forms.Label();
            this.panel = new System.Windows.Forms.Panel();
            this.buttonGo = new System.Windows.Forms.Button();
            this.radioButtonDecrypt = new System.Windows.Forms.RadioButton();
            this.radioButtonEncrypt = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButtonRSA = new System.Windows.Forms.RadioButton();
            this.radioButtonGOST = new System.Windows.Forms.RadioButton();
            this.radioButtonDes = new System.Windows.Forms.RadioButton();
            this.radioButtonFeistel = new System.Windows.Forms.RadioButton();
            this.radioButtonScrambler = new System.Windows.Forms.RadioButton();
            this.radioButtonCaesar = new System.Windows.Forms.RadioButton();
            this.textBoxEncodeKey = new System.Windows.Forms.TextBox();
            this.textBoxDecodeKey = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBoxOneKey = new System.Windows.Forms.CheckBox();
            this.checkBoxAreSame = new System.Windows.Forms.CheckBox();
            this.buttonGenerateAsymKeys = new System.Windows.Forms.Button();
            this.buttonReset = new System.Windows.Forms.Button();
            this.radioButtonEDS = new System.Windows.Forms.RadioButton();
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
            this.textBoxMessage.Size = new System.Drawing.Size(365, 212);
            this.textBoxMessage.TabIndex = 1;
            // 
            // textBoxCrypted
            // 
            this.textBoxCrypted.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxCrypted.Location = new System.Drawing.Point(462, 29);
            this.textBoxCrypted.Multiline = true;
            this.textBoxCrypted.Name = "textBoxCrypted";
            this.textBoxCrypted.Size = new System.Drawing.Size(365, 212);
            this.textBoxCrypted.TabIndex = 3;
            this.textBoxCrypted.TextChanged += new System.EventHandler(this.textBoxCrypted_TextChanged);
            // 
            // labelCypher
            // 
            this.labelCypher.AutoSize = true;
            this.labelCypher.Location = new System.Drawing.Point(459, 13);
            this.labelCypher.Name = "labelCypher";
            this.labelCypher.Size = new System.Drawing.Size(150, 13);
            this.labelCypher.TabIndex = 2;
            this.labelCypher.Text = "Зашифрованное сообщение";
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
            this.groupBox1.Controls.Add(this.radioButtonEDS);
            this.groupBox1.Controls.Add(this.radioButtonRSA);
            this.groupBox1.Controls.Add(this.radioButtonGOST);
            this.groupBox1.Controls.Add(this.radioButtonDes);
            this.groupBox1.Controls.Add(this.radioButtonFeistel);
            this.groupBox1.Controls.Add(this.radioButtonScrambler);
            this.groupBox1.Controls.Add(this.radioButtonCaesar);
            this.groupBox1.Location = new System.Drawing.Point(12, 368);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(815, 82);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Алгоритмы";
            // 
            // radioButtonRSA
            // 
            this.radioButtonRSA.AutoSize = true;
            this.radioButtonRSA.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.radioButtonRSA.Location = new System.Drawing.Point(432, 19);
            this.radioButtonRSA.Name = "radioButtonRSA";
            this.radioButtonRSA.Size = new System.Drawing.Size(33, 30);
            this.radioButtonRSA.TabIndex = 5;
            this.radioButtonRSA.Text = "RSA";
            this.radioButtonRSA.UseVisualStyleBackColor = true;
            // 
            // radioButtonGOST
            // 
            this.radioButtonGOST.AutoSize = true;
            this.radioButtonGOST.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.radioButtonGOST.Location = new System.Drawing.Point(361, 19);
            this.radioButtonGOST.Name = "radioButtonGOST";
            this.radioButtonGOST.Size = new System.Drawing.Size(39, 30);
            this.radioButtonGOST.TabIndex = 4;
            this.radioButtonGOST.Text = "ГОСТ";
            this.radioButtonGOST.UseVisualStyleBackColor = true;
            // 
            // radioButtonDes
            // 
            this.radioButtonDes.AutoSize = true;
            this.radioButtonDes.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.radioButtonDes.Location = new System.Drawing.Point(283, 19);
            this.radioButtonDes.Name = "radioButtonDes";
            this.radioButtonDes.Size = new System.Drawing.Size(33, 30);
            this.radioButtonDes.TabIndex = 3;
            this.radioButtonDes.Text = "DES";
            this.radioButtonDes.UseVisualStyleBackColor = true;
            // 
            // radioButtonFeistel
            // 
            this.radioButtonFeistel.AutoSize = true;
            this.radioButtonFeistel.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.radioButtonFeistel.Location = new System.Drawing.Point(189, 19);
            this.radioButtonFeistel.Name = "radioButtonFeistel";
            this.radioButtonFeistel.Size = new System.Drawing.Size(63, 30);
            this.radioButtonFeistel.TabIndex = 2;
            this.radioButtonFeistel.Text = "Фейстель";
            this.radioButtonFeistel.UseVisualStyleBackColor = true;
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
            this.checkBoxOneKey.Location = new System.Drawing.Point(382, 262);
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
            // buttonGenerateAsymKeys
            // 
            this.buttonGenerateAsymKeys.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonGenerateAsymKeys.Location = new System.Drawing.Point(382, 299);
            this.buttonGenerateAsymKeys.Name = "buttonGenerateAsymKeys";
            this.buttonGenerateAsymKeys.Size = new System.Drawing.Size(74, 48);
            this.buttonGenerateAsymKeys.TabIndex = 3;
            this.buttonGenerateAsymKeys.Text = "Сгенерировать ключи";
            this.buttonGenerateAsymKeys.UseVisualStyleBackColor = true;
            this.buttonGenerateAsymKeys.Click += new System.EventHandler(this.buttonGenerateAsymKeys_Click);
            // 
            // buttonReset
            // 
            this.buttonReset.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonReset.Location = new System.Drawing.Point(384, 211);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(72, 30);
            this.buttonReset.TabIndex = 3;
            this.buttonReset.Text = "Очистить";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // radioButtonEDS
            // 
            this.radioButtonEDS.AutoSize = true;
            this.radioButtonEDS.CheckAlign = System.Drawing.ContentAlignment.TopCenter;
            this.radioButtonEDS.Location = new System.Drawing.Point(504, 19);
            this.radioButtonEDS.Name = "radioButtonEDS";
            this.radioButtonEDS.Size = new System.Drawing.Size(34, 30);
            this.radioButtonEDS.TabIndex = 6;
            this.radioButtonEDS.Text = "ЭЦП";
            this.radioButtonEDS.UseVisualStyleBackColor = true;
            this.radioButtonEDS.CheckedChanged += new System.EventHandler(this.radioButtonEDS_CheckedChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 462);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.buttonGenerateAsymKeys);
            this.Controls.Add(this.checkBoxAreSame);
            this.Controls.Add(this.checkBoxOneKey);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxDecodeKey);
            this.Controls.Add(this.textBoxEncodeKey);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.textBoxCrypted);
            this.Controls.Add(this.labelCypher);
            this.Controls.Add(this.textBoxMessage);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
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
        private System.Windows.Forms.Label labelCypher;
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
        private System.Windows.Forms.RadioButton radioButtonFeistel;
        private System.Windows.Forms.RadioButton radioButtonDes;
        private System.Windows.Forms.RadioButton radioButtonGOST;
        private System.Windows.Forms.Button buttonGenerateAsymKeys;
        private System.Windows.Forms.RadioButton radioButtonRSA;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.RadioButton radioButtonEDS;
    }
}

