using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace LogSorter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (string folder in Directory.GetDirectories(Directory.GetCurrentDirectory()))
            {
                listBox1.Items.Add(folder.Split('\\')[folder.Split('\\').Length - 1]);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (string line in File.ReadLines(listBox1.SelectedItem.ToString() + "\\Passwords.txt"))
                {
                    if (line.Contains("http") & !line.Contains("REDLINESUPPORT"))
                    {
                        listBox2.Items.Add(line);
                    }
                }
            }
            catch { };
        }

        public void cookieManage(string line)
        {
            string[] stringSeparators = new string[] { "	" };
            var lll = line.Split(stringSeparators, 7, StringSplitOptions.None);
            //richTextBox1.Text += "\n    {\n";
            //richTextBox1.Text += "        \"name\": \"" + lll[5] + "\",\n";
            //richTextBox1.Text += "        \"value\": \"" + lll[6] + "\",\n";
            //richTextBox1.Text += "        \"domain\": \"" + lll[0] + "\",\n";
            //richTextBox1.Text += "        \"hostonly\": " + lll[1] + ",\n";
            //richTextBox1.Text += "        \"path\": \"" + lll[2] + "\",\n";
            //richTextBox1.Text += "        \"secure\": " + lll[3] + ",\n";
            //richTextBox1.Text += "        \"httpOnly\": " + lll[3] + ",\n";
            //richTextBox1.Text += "        \"sameSite\": \"no_restriction\",\n";
            //richTextBox1.Text += "        \"session\": true,\n";
            //richTextBox1.Text += "        \"firstPartyDomain\": \"\",\n";
            //richTextBox1.Text += "        \"expirationDate\": \"" + lll[4] + "\",\n";
            //richTextBox1.Text += "        \"storeId\": null\n";
            //richTextBox1.Text += "    },\n";
            //richTextBox1.Text.Replace("},", "},\n{");

            richTextBox1.Text += "{";
            richTextBox1.Text += "\"domain\": \"" + lll[0] + "\",";
            richTextBox1.Text += "\"expirationDate\": " + lll[4] + ",";
            richTextBox1.Text += "\"httpOnly\": " + lll[3].ToLower() + ",";
            richTextBox1.Text += "\"name\": \"" + lll[5] + "\",";
            richTextBox1.Text += "\"path\": \"" + lll[2] + "\",";
            richTextBox1.Text += "\"secure\": " + lll[3].ToLower() + ",";
            richTextBox1.Text += "\"value\": \"" + lll[6] + "\",";
            richTextBox1.Text += "\"hostonly\": " + lll[1].ToLower();
            richTextBox1.Text += "},";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                richTextBox1.Text = string.Empty;
                richTextBox1.Text += "[";
                string newLink = listBox2.SelectedItem.ToString().Split('/')[2];
                foreach (string file in Directory.GetFiles(Directory.GetCurrentDirectory() + "\\" + listBox1.SelectedItem.ToString() + "\\Cookies\\"))
                {
                    foreach (string line in File.ReadLines(file))
                    {
                        if (line.Contains(newLink))
                        {
                            cookieManage(line);
                        }
                    }
                }
                richTextBox1.Text = richTextBox1.Text.Substring(0, richTextBox1.Text.Length - 2);
                richTextBox1.Text += "}]";
            }
            catch { richTextBox1.Text = string.Empty; }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = string.Empty;
            textBox2.Text = string.Empty;
            textBox3.Text = string.Empty;
            richTextBox1.Text = string.Empty;
            int c0 = 0;
            using (StreamReader streamReader = new StreamReader(listBox1.SelectedItem.ToString() + "\\Passwords.txt", Encoding.Default))
            {
                var fileLine = File.ReadAllLines(listBox1.SelectedItem.ToString() + "\\Passwords.txt");
                for (int i = 0; i <= fileLine.Length; i++)
                {
                    try
                    {
                        if (fileLine[i].Contains(listBox2.SelectedItem.ToString()))
                        {
                            textBox1.Text += fileLine[i].Replace("URL: ", string.Empty) + "\n";
                            textBox2.Text += fileLine[i + 1].Replace("Username: ", string.Empty) + "\n";
                            textBox3.Text += fileLine[i + 2].Replace("Password: ", string.Empty) + "\n";
                        }
                    }
                    catch { }
                }
            }
            try
            {
                richTextBox1.Text += "[";
                string newLink = listBox2.SelectedItem.ToString().Split('/')[2];
                foreach (string file in Directory.GetFiles(Directory.GetCurrentDirectory() + "\\" + listBox1.SelectedItem.ToString() + "\\Cookies\\"))
                {
                    foreach (string line in File.ReadLines(file))
                    {
                        if (line.Contains(newLink))
                        {
                            cookieManage(line);
                        }
                    }
                }
                richTextBox1.Text = richTextBox1.Text.Substring(0, richTextBox1.Text.Length - 2);
                richTextBox1.Text += "\n}]";
            }
            catch { richTextBox1.Text = string.Empty; richTextBox1.Text = "NO COOKIES"; }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            textBox1.Text = "https://addons.mozilla.org/ru/firefox/addon/cookie-editor/?utm_source=addons.mozilla.org&utm_medium=referral&utm_content=search";
            textBox2.Text = "https://chrome.google.com/webstore/detail/cookie-editor/hlkenndednhfkekhgcdicdfddnkalmdm";
            textBox3.Text = "Created by Jazis";
        }
    }
}
