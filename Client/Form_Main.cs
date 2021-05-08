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
    public partial class Form_Main : Form
    {
        User user;
        List<Test> tests;
        public Form_Main()
        {
            InitializeComponent();
            
            TestClient client = new TestClient();
            client.Strart();
            ClientForm clientForm = new ClientForm();
            if (clientForm.ShowDialog() == DialogResult.OK)
            {
                var login = clientForm.textBox1.Text;
                var pwd = clientForm.textBox2.Text;
                 user = client.Login(login,pwd);
                if (user.Id == -1)
                {
                    MessageBox.Show("Access denied");
                    Load += (s, e) => Close();
                    return;
                }
                tests = user.Groups.SelectMany(g => g.Tests).ToList();
                dataGridView1.DataSource= tests
                    .Select(x => new { Id = x.Id, Title = x.Title, Author = x.Author, DtCreate = x.DtCreate })
                    .ToList();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int id = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
                Test test = tests.Find(t => t.Id == id);
                Form_Test form_Test = new Form_Test(test);
                form_Test.ShowDialog();
            }
        }
    }
}
