using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SubTitleEditor
{
   
    public partial class Form1 : Form
    {
        absTitle fObj;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (StreamReader sr = new StreamReader(openFileDialog1.FileName))
                {
                    fObj = SubTitleFactory.getTitleClass(openFileDialog1.FileName);
                    label5.Text = openFileDialog1.SafeFileName;
                }
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            fObj.updateFile(int.Parse(textBox1.Text), int.Parse(textBox2.Text), int.Parse(textBox3.Text), int.Parse(textBox4.Text));
            button4_Click(null, null);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (fObj != null)
            {
                List<string> ls = fObj.searchData(textBox5.Text);
                if (ls.Count > 0)
                {
                    TimeSpan ts = new TimeSpan(int.Parse(textBox6.Text), int.Parse(textBox7.Text), int.Parse(textBox8.Text));
                    if (ts.TotalSeconds > 0)
                    {
                        TimeSpan diff =ts.Subtract(TimeSpan.Parse(ls[0]));
                        textBox1.Text = diff.Hours.ToString();
                        textBox2.Text = diff.Minutes.ToString();
                        textBox3.Text = diff.Seconds.ToString();
                    }
                    label2.Text = ls[0];
                    label4.Text = ls[1];
                    label6.Text = ls[2];
                }
            }
        }       
    }
}
