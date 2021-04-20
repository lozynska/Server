using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositiryex1;
using System.Windows.Forms;
using DalServerDb;
using System.Configuration;

namespace Server
{
    public partial class Form1 : Form
    {
        GenericUnitOfWork work;
        IGenericReposiyory<User> repositoryUser;
        
        User user;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            work=new GenericUnitOfWork(new ServerContext(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString));
            if(String.IsNullOrEmpty(textBox1.Text)|| String.IsNullOrEmpty(textBox2.Text)){

            }
            user = repositoryUser.FindAll(x => x.Login == textBox1.Text && x.Password == textBox2.Text).FirstOrDefault();
            if (user == null)
            {
                MessageBox.Show("User not found","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
            if (!user.isAdmin)
            {
                MessageBox.Show("User not found", "Access", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                Main_Form main_Form = new Main_Form(user);
                main_Form.ShowDialog();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
