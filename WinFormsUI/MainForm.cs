using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Crypto;
using Crypto.Assymetric;
using Crypto.Block;
using Crypto.Helpers;
using Crypto.Shift;
using Crypto.Stream;
using RSA = Crypto.Assymetric.RSA;

namespace WinFormsUI
{
    public partial class MainForm : Form
    {
        private byte[] _encodedMsg;
        private bool _decodeCached;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            textBoxMessage.Clear();
            textBoxMessage.Text = TestText;
            textBoxEncodeKey.Text = "123456789";

            textBoxMessage.SelectionLength = 0;

            buttonGo.Focus();
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            try
            {
                var coder = GetSelectedCoder();

                if (checkBoxOneKey.Checked)
                    coder.Key = textBoxEncodeKey.Text;
                else
                {
                    if (radioButtonEncrypt.Checked)
                        coder.CryptoKey.EncodeKeyFromString(textBoxEncodeKey.Text);
                    else
                        coder.CryptoKey.DecodeKeyFromString(textBoxDecodeKey.Text);
                }

                #region EDS

                if (radioButtonEDS.Checked)
                {
                    

                    if (radioButtonEncrypt.Checked)
                    {
                        var eds = new EDS {SignKey = textBoxDecodeKey.Text};
                        //eds.CheckKey = textBoxEncodeKey.Text;
                        textBoxCrypted.Text = eds.Sign(textBoxMessage.Text);
                        checkBoxAreSame.Checked = false;
                    }
                    else
                    {
                        var encodedMsg = textBoxCrypted.Text.GetUtf16Bytes();

                        var eds = new EDS {CheckKey = textBoxEncodeKey.Text};
                        var signed = eds.CheckSign(textBoxMessage.Text, textBoxCrypted.Text);

                        if (signed)
                        {
                            checkBoxAreSame.Checked = true;
                            MessageBox.Show("Подпись верна");
                        }
                        else
                        {
                            checkBoxAreSame.Checked = false;
                            MessageBox.Show("Подпись или текст изменены!");
                        }
                    }
                    return;
                }

                #endregion EDS

                if (radioButtonEncrypt.Checked)
                {
                    _encodedMsg = coder.Encode(textBoxMessage.Text.GetUtf16Bytes());

                    textBoxCrypted.Text = _encodedMsg.GetUtf16String();

                    checkBoxAreSame.Checked = false;
                    _decodeCached = true;
                }
                else
                {
                    var encodedMsg = _decodeCached
                        ? _encodedMsg
                        : textBoxCrypted.Text.GetUtf16Bytes();
                    
                    var startMessage = textBoxMessage.Text;

                    textBoxMessage.Text = coder.Decode(encodedMsg).GetUtf16String();

                    checkBoxAreSame.Checked = startMessage == textBoxMessage.Text;
                    _decodeCached = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private ICryptoCoder GetSelectedCoder()
        {
            if (radioButtonCaesar.Checked)
            {
                checkBoxOneKey.Checked = true;
                return new Caesar();
            }
            if (radioButtonScrambler.Checked)
            {
                checkBoxOneKey.Checked = true;
                return new Scrambler();
            }
            if (radioButtonFeistel.Checked)
            {
                checkBoxOneKey.Checked = true;
                return new Feistel();
            }
            if (radioButtonDes.Checked)
            {
                checkBoxOneKey.Checked = true;
                return new Des();
            }
            if (radioButtonGOST.Checked)
            {
                checkBoxOneKey.Checked = true;
                return new GOST();
            }
            if (radioButtonRSA.Checked || radioButtonEDS.Checked)
            {
                checkBoxOneKey.Checked = false;
                return new RSA();
            }
            throw new InvalidEnumArgumentException("Не выбран алгоритм шифрования");
        }

        private void textBoxCrypted_TextChanged(object sender, EventArgs e)
        {
            if (_decodeCached)
                _decodeCached = false;
        }

        private void buttonGenerateAsymKeys_Click(object sender, EventArgs e)
        {
            var coder = GetSelectedCoder();
            try
            {
                coder.GenerateCryptoKey();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Этот алгоритм не умеет генерировать ключи");
            }
            textBoxEncodeKey.Text = coder.CryptoKey.EncodeKeyToString();
            textBoxDecodeKey.Text = coder.CryptoKey.DecodeKeyToString();
            //if (coder.CryptoKey is RsaKey)
            //    MessageBox.Show("Длина ключа: " + Math.Floor(BigInteger.Log(((RsaKey)coder.CryptoKey).N, 2)));
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            textBoxMessage.Clear();
            textBoxMessage.Text = TestText;
            textBoxCrypted.Clear();
        }

        private const string TestText = "The quick brown fox jumps over the lazy dog" + "\r\n"
            + "1234567890~`!@#$%^&*()_-+={}[]:;\"'<>,.?/|\\";

        private void radioButtonEDS_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonEDS.Checked)
            {
                labelCypher.Text = "Подпись";
            }
            else
            {
                labelCypher.Text = "Зашифрованное сообщение";
            }
        }
    }
}
