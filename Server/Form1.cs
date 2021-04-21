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
            work = new GenericUnitOfWork(new ServerContext(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString));
            repositoryUser = work.Reposiyory<User>();
            //user = new User() { Name = "ad", Login = "ad", Password = "1111", isAdmin = true };
            //repositoryUser.Add(user);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var username = textBox1.Text;
            var pwd = textBox2.Text;

            if (String.IsNullOrEmpty(username) || String.IsNullOrEmpty(pwd))
            {
                MessageBox.Show("Enter login and Password", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
           
            var users = repositoryUser.FindAll(u => u.Login.Equals(username));
            var user = users.FirstOrDefault();
            this.user = user;
            if (users.Count() != 0 && user.Password.Equals(pwd))
            {
                MessageBox.Show("Вітаємо,авторизація пройдена");
            }
            else
            {
                MessageBox.Show("Невірний пароль або логін");
                
                return;
            }
            if (user == null)
            {
                MessageBox.Show("User not found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!user.isAdmin)
            {
                MessageBox.Show("Access denired", "Access", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
