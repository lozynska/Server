using DalServerDb;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form_Test : Form
    {
        private static TimeSpan SEC = new TimeSpan(0,0,1);
        private Test test;
        private int qNum = 0;
        private int[] marks;
        private int count;
        private TimeSpan time;
        public Question question { get; set; }
        public Form_Test(Test test)
        {
            this.test = test;
            count = test.Questions.Count;
            time = new TimeSpan(test.Hour, test.Minute, 0);
            
            marks = new int[count];
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Visible = false;
            button2.Visible = true;
            button3.Visible = true;
            button4.Visible = true;
            SetData();
            timer.Start();
        }

        private void SetData()
        {
            question = test.Questions.ElementAt(qNum);
            textBox1.Text = question.Title;
            groupBox2.Controls.Clear();
            ICollection<Answer> answers = question.Answers;
            for (int i = 0; i < answers.Count; i++)
            {
                Answer answer = answers.ElementAt(i);
                RadioButton rbtn = new RadioButton();
                rbtn.Text = answer.Title;
                rbtn.Tag = answer.isRight;
                rbtn.Top = 10+i * 25;
                rbtn.Left = 10;
                rbtn.CheckedChanged += Rbtn_CheckedChanged;
                groupBox2.Controls.Add(rbtn);
            }
            
        }

        private void Rbtn_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            if (radioButton.Checked)
            {
                marks[qNum] = (bool)radioButton.Tag ? 1 : 0;                
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            qNum = (qNum + 1) % count;
            SetData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            qNum--;
            if (qNum == -1)
                qNum = count;
            SetData();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Finish();
        }

        private void Finish()
        {
            timer.Stop();
            int mark = marks.Sum();
            mark = (int)Math.Round(12.0 * mark / count);
            MessageBox.Show("Mark: " + mark);
            this.Close();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            time = time.Subtract(SEC);
            if (time.TotalSeconds > 0)
            {
                label1.Text = time.ToString();
            }
            else
            {
                Finish();
            }
        }
    }
}
