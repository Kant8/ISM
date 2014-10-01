using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Crypto;
using Crypto.Block;
using Crypto.Shift;
using Crypto.Stream;

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
            textBoxMessage.Text += "The quick brown fox jumps over the lazy dog";
            textBoxMessage.Text += Environment.NewLine;
            textBoxMessage.Text += "Съешь же ещё этих мягких французских булок да выпей чаю.";
            textBoxMessage.Text += Environment.NewLine;
            textBoxMessage.Text += "1234567890~`!@#$%^&*()_-+={}[]:;\"'<>,.?/|\\";
            textBoxEncodeKey.Text += "123456789";

            textBoxMessage.SelectionLength = 0;

            buttonGo.Focus();
        }

        private void buttonGo_Click(object sender, EventArgs e)
        {
            try
            {
                var coder = GetSelectedCoder();

                if (radioButtonEncrypt.Checked)
                {
                    coder.Key = textBoxEncodeKey.Text;

                    _encodedMsg = coder.Encode(textBoxMessage.Text.GetUtf16Bytes());

                    textBoxCrypted.Text = _encodedMsg.GetUtf16String();

                    checkBoxAreSame.Checked = false;
                    _decodeCached = true;
                }
                else
                {
                    coder.Key = checkBoxOneKey.Checked 
                        ? textBoxEncodeKey.Text 
                        : textBoxDecodeKey.Text;
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
            throw new InvalidEnumArgumentException("Не выбран алгоритм шифрования");
        }

        private void textBoxCrypted_TextChanged(object sender, EventArgs e)
        {
            if (_decodeCached)
                _decodeCached = false;
        }
    }
}
