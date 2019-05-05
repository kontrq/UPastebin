using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace UPastebin
{
    public partial class UPastebin : Form
    {
        public UPastebin()
        {
            InitializeComponent();
            textBox3.Enabled = false;
            textBox3.ReadOnly = true;
        }

        string ResponseText(byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes))
            using (StreamReader reader = new StreamReader(ms))
                return reader.ReadToEnd();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length == 0)
            {
                MessageBox.Show("Empty title!", "Error!");
                return;
            }
        Upload:

            System.Collections.Specialized.NameValueCollection Data
                = new System.Collections.Specialized.NameValueCollection();

            WebClient wb = new WebClient();
            string response;
            byte[] bytes;

            if (textBox4.Text != "Username" && textBox5.Text != "Password")
            {
                Data["api_user_name"] = textBox4.Text;
                Data["api_user_password"] = textBox5.Text;
                Data["api_dev_key"] = "4dd29af09faf6e39abcae2c910ad8f2e";

                bytes = wb.UploadValues("http://pastebin.com/api/api_login.php", Data);
                response = ResponseText(bytes);

                if (response.StartsWith("Bad API request"))
                {
                        Data["api_user_name"] = textBox4.Text;
                        Data["api_user_password"] = textBox5.Text;
                        goto SkipLogin;
                }

                Data["api_user_key"] = response;
            }

        SkipLogin:

            Data["api_paste_private"] = comboBox2.SelectedIndex.ToString();
            Data["api_paste_name"] = textBox2.Text;
            if (comboBox1.SelectedIndex == 0)
            {
                Data["api_paste_expire_date"] = "N";
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                Data["api_paste_expire_date"] = "10M";
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                Data["api_paste_expire_date"] = "1H";
            }
            else if (comboBox1.SelectedIndex == 3)
            {
                Data["api_paste_expire_date"] = "1D";
            }
            else if (comboBox1.SelectedIndex == 4)
            {
                Data["api_paste_expire_date"] = "1W";
            }
            else if (comboBox1.SelectedIndex == 5)
            {
                Data["api_paste_expire_date"] = "2W";
            }
            else if (comboBox1.SelectedIndex == 6)
            {
                Data["api_paste_expire_date"] = "1M";
            }
            else if (comboBox1.SelectedIndex == 7)
            {
                Data["api_paste_expire_date"] = "6M";
            }
            else if (comboBox1.SelectedIndex == 8)
            {
                Data["api_paste_expire_date"] = "1Y";
            }
            else
            {
                MessageBox.Show("Error, unknown element!", "Error!");
            }
            Data["api_paste_code"] = textBox1.Text;
            Data["api_option"] = "paste";

            bytes = wb.UploadValues("http://pastebin.com/api/api_post.php", Data);
            response = ResponseText(bytes);

            if (response.StartsWith("Bad API request"))
            {
                if (MessageBox.Show("Failed to upload!\r\n" + response, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                    goto Upload;
            }
            else
            {
                if (checkBox1.Checked)
                    response = "http://pastebin.com/raw.php?i=" + response.Substring(20);

                Clipboard.SetText(response);
                textBox3.Text = response;
                textBox3.Enabled = true;
                MessageBox.Show("Succesfully uploaded! \r\nLink copied to clipboard.", "Success!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            label4.Hide();
            textBox1.Hide();
            textBox2.Hide();
            label5.Hide();
            comboBox1.Hide();
            label7.Hide();
            comboBox2.Hide();
            label3.Hide();
            textBox3.Hide();
            label6.Hide();
            button1.Hide();
            button2.Hide();
            checkBox1.Hide();
            MaximizeBox = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.Collections.Specialized.NameValueCollection Data
                            = new System.Collections.Specialized.NameValueCollection();

            WebClient wb = new WebClient();
            string response;
            byte[] bytes;
            if (textBox4.Text != "Username" && textBox5.Text != "Password")
            {
                Data["api_user_name"] = textBox4.Text;
                Data["api_user_password"] = textBox5.Text;
                Data["api_dev_key"] = "4dd29af09faf6e39abcae2c910ad8f2e";

                bytes = wb.UploadValues("http://pastebin.com/api/api_login.php", Data);
                response = ResponseText(bytes);

                if (response.StartsWith("Bad API request"))
                {
                    if (MessageBox.Show("Login failed, do you want to upload it as guest?", "Login failed", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                    {
                        Data["api_user_name"] = textBox4.Text;
                        Data["api_user_password"] = textBox5.Text;
                    }
                    else
                        return;
                }

                Data["api_user_key"] = response;
            }
            button3.Hide();
            button4.Hide();
            label2.Hide();
            label1.Hide();
            textBox4.Hide();
            textBox5.Hide();
            label4.Show();
            textBox1.Show();
            textBox2.Show();
            label5.Show();
            comboBox1.Show();
            label7.Show();
            comboBox2.Show();
            label3.Show();
            textBox3.Show();
            label6.Show();
            button1.Show();
            button2.Show();
            checkBox1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Hide();
            button4.Hide();
            label2.Hide();
            label1.Hide();
            textBox4.Hide();
            textBox5.Hide();
            label4.Show();
            textBox1.Show();
            textBox2.Show();
            label5.Show();
            comboBox1.Show();
            label7.Show();
            comboBox2.Show();
            label3.Show();
            textBox3.Show();
            label6.Show();
            button1.Show();
            button2.Show();
            checkBox1.Show();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            About nextForm = new About();
            nextForm.Show();
        }
    }
}
