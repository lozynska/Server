using DalServerDb;
using Repositiryex1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SerialDeserial = TestLib.SerialDeserial;
using XmlTest = TestLib.Test;

namespace Server
{
    public partial class Main_Form : Form
    {
        User admin;
        GenericUnitOfWork work;
        IGenericReposiyory<User> repositoryUser;
        IGenericReposiyory<Group> repositoryGroup;
        IGenericReposiyory<Question> repositoryQuestion;
        IGenericReposiyory<Result> repositoryResult;
        IGenericReposiyory<Test> repositoryTest;
        IGenericReposiyory<Answer> repositoryAnswer;
        IGenericReposiyory<UserAnswear> repositoryUserAnswear;
        int portSend = 5555;
        int portRecive = 5556;
        int portSend_Test = 5558;
        int portRecive_Test = 5557;
        int portSend_Res = 5560;
        int portRecive_Res = 5559;
        int filter = 0;
        string file;
        Test test;
        public Main_Form(User user)
        {
            InitializeComponent();
            admin = user;
            work = new GenericUnitOfWork(new ServerContext(ConfigurationManager.ConnectionStrings["conStr"].ConnectionString));
            repositoryUser = work.Reposiyory<User>();
            repositoryGroup = work.Reposiyory<Group>();
            repositoryQuestion = work.Reposiyory<Question>();
            repositoryResult = work.Reposiyory<Result>();
            repositoryTest = work.Reposiyory<Test>();
            repositoryAnswer = work.Reposiyory<Answer>();
            repositoryUserAnswear = work.Reposiyory<UserAnswear>();


            dataGridView1.Visible = false;
            comboBox1.Visible = false;
            label1.Visible = false;
            groupBox2.Visible = false;
            comboBox1.DisplayMember = "Title";

            repositoryGroup.GetAll().ToList().ForEach(row =>
            {

                comboBox1.Items.Add(row);
            });


            //TcpClient client = null;
            //NetworkStream stream;
            //IPAddress local_host = IPAddress.Parse("127.0.0.1");

            //TcpListener server = new TcpListener(local_host, portRecive);
            //server.Start();

            //TcpListener serverTest = new TcpListener(local_host, portRecive_Test);
            //serverTest.Start();

            //TcpListener serverRes = new TcpListener(local_host, portRecive_Res);
            //serverRes.Start();

            ////Users
            //Task.Run(() =>
            //{
            //    while (true)
            //    {
            //        TcpClient client1 = server.AcceptTcpClient();
            //        NetworkStream st = client1.GetStream();
            //        byte[] buff = new byte[1024];
            //        MemoryStream ms = new MemoryStream();

            //        do
            //        {
            //            int bytes = st.Read(buff, 0, buff.Length);
            //           //ms.Append(buff);
            //        } while (st.DataAvailable);
            //        BinaryFormatter bf = new BinaryFormatter();
            //        ms.Position = 0;
            //        User user1 =(User) bf.Deserialize(ms);
            //        ms.Close();

            //        var user2 = repositoryUser.FindAll(x => x.Login == user1.Login && x.Password == user1.Password).First();

            //        if (user2 != null)
            //        {
            //            user1.Id = user2.Id;
            //            user1.Name = user2.Name;
            //            user1.Login = user2.Login;
            //            user1.Password = user2.Password;
            //            user1.isAdmin = user2.isAdmin;

            //            byte[] data;
            //            using(ms=new MemoryStream())
            //            {
            //                var b_f = new BinaryFormatter();
            //                b_f.Serialize(ms, user1);
            //                data = ms.ToArray();
            //            }
            //            client = new TcpClient();
            //            client.Connect("localhost", portSend);
            //            stream = client.GetStream();
            //            stream.Write(data, 0, data.Length);
            //            ms.Close();
            //        }
            //        else
            //        {
            //            user1.Id = -1;
            //            byte[] data;
            //            using (ms = new MemoryStream())
            //            {
            //                var b_f = new BinaryFormatter();
            //                b_f.Serialize(ms, user1);
            //                data = ms.ToArray();
            //            }
            //            client = new TcpClient();
            //            client.Connect("localhost", portSend);
            //            stream = client.GetStream();
            //            stream.Write(data, 0, data.Length);
            //            ms.Close();


            //        }
            //    }
            //});
        }

        private void showToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ClearEvents();
            dataGridView1.Visible = true;
            filter = 1;
            dataGridView1.DataSource = repositoryGroup.GetAll().Select(x => new { Id = x.Id, Tille = x.Title }).ToList();
            removeButton.Click += RemoveGroup;
        }

        private void RemoveGroup(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
            var group = repositoryGroup.FindById(id);
            group.Tests.Clear();
            group.Users.Clear();
            repositoryGroup.Update(group);
            repositoryGroup.Remove(group);
            dataGridView1.DataSource = repositoryGroup.GetAll().Select(x => new { Id = x.Id, Tille = x.Title }).ToList();
        }

        private void addToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
            Form_AddGroup addGroup = new Form_AddGroup();
            if (addGroup.ShowDialog() == DialogResult.OK)
            {
                Group group = new Group()
                {
                    Title = addGroup.textBox1.Text,

                };
                repositoryGroup.Add(group);
                dataGridView1.DataSource = repositoryGroup.GetAll().Select(x => new { Id = x.Id, Tille = x.Title }).ToList();
            }

        }

        private void updateToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
            Form_AddGroup addGroup = new Form_AddGroup();
            int id = Int32.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            var group = repositoryGroup.FindById(id);
            addGroup.textBox1.Text = group.Title;
            if (addGroup.ShowDialog() == DialogResult.OK)
            {
                group.Title = addGroup.textBox1.Text;
                repositoryGroup.Update(group);
            }
            dataGridView1.DataSource = repositoryGroup.GetAll().Select(x => new { Id = x.Id, Tille = x.Title }).ToList();
        }

        private void addUaerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearEvents();
            comboBox1.Visible = true;
            comboBox1.SelectedIndex = 0;
            dataGridView1.Visible = true;
            label1.Visible = true;
            dataGridView1.DataSource = repositoryUser.GetAll().Select(x => new { Id = x.Id, Name = x.Name, Login = x.Login, Password = x.Password, isAdmin = x.isAdmin }).ToList();
            comboBox1.SelectedIndexChanged += AddUserSelected;
        }

        private void AddUserSelected(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    var group = (Group)comboBox1.SelectedItem;
                    if (group != null)
                    {
                        User user = repositoryUser.FindById(row.Cells[0].Value);
                        user.Groups.Add(group);
                        repositoryUser.Update(user);
                    }
                }
            }
        }

        private void showUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearEvents();
            comboBox1.Visible = true;
            comboBox1.SelectedIndex = 0;
            dataGridView1.Visible = true;
            label1.Visible = true;
            ShowUsers(sender, e);
            comboBox1.SelectedIndexChanged += ShowUsers;
        }

        private void ShowUsers(object sender, EventArgs e)
        {
            var group = (Group)comboBox1.SelectedItem;
            if (group != null)
            {
                dataGridView1.DataSource = group.Users;
            }
        }

        private void ClearEvents()
        {
            comboBox1.SelectedIndexChanged -= AddUserSelected;
            comboBox1.SelectedIndexChanged -= ShowUsers;
            comboBox1.SelectedIndexChanged -= ShowTests;
            comboBox1.SelectedIndexChanged -= AddTestSelected;
            removeButton.Click -= RemoveGroup;
            removeButton.Click -= RemoveUser;
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearEvents();
            dataGridView1.Visible = true;
            filter = 2;
            dataGridView1.DataSource = repositoryUser.GetAll().Select(x => new { Id = x.Id, Name = x.Name, Login = x.Login, Password = x.Password, isAdmin = x.isAdmin }).ToList();
            removeButton.Click += RemoveUser;
        }
        private void RemoveUser(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.SelectedCells[0].Value.ToString());
            var user = repositoryUser.FindById(id);
            user.Groups.Clear();
            repositoryUser.Update(user);
            repositoryUser.Remove(user);
            dataGridView1.DataSource = repositoryUser.GetAll().Select(x => new { Id = x.Id, Name = x.Name, Login = x.Login, Password = x.Password, isAdmin = x.isAdmin }).ToList();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
            FormUser formUser = new FormUser();
            if (formUser.ShowDialog() == DialogResult.OK)
            {
                User user = new User()
                {
                    Name = formUser.textBox1.Text,
                    Login = formUser.textBox2.Text,
                    Password = formUser.textBox3.Text,
                    isAdmin = Convert.ToBoolean(formUser.comboBox1.SelectedItem),

                };
                repositoryUser.Add(user);
                dataGridView1.DataSource = repositoryUser.GetAll().Select(x => new { Id = x.Id, Name = x.Name, Login = x.Login, Password = x.Password, isAdmin = x.isAdmin }).ToList();
            }
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
            FormUser formUser = new FormUser();
            int id = Int32.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            var user = repositoryUser.FindById(id);
            formUser.textBox1.Text = user.Name;
            formUser.textBox2.Text = user.Login;
            formUser.textBox3.Text = user.Password;
            formUser.comboBox1.SelectedItem = user.isAdmin;
            if (formUser.ShowDialog() == DialogResult.OK)
            {
                user.Name = formUser.textBox1.Text;
                user.Login = formUser.textBox2.Text;
                user.Password = formUser.textBox3.Text;
                user.isAdmin = Convert.ToBoolean(formUser.comboBox1.SelectedItem);
                repositoryUser.Update(user);
            }
            dataGridView1.DataSource = repositoryUser.GetAll().Select(x => new { Id = x.Id, Name = x.Name, Login = x.Login, Password = x.Password, isAdmin = x.isAdmin }).ToList();
        }

        private void showAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = true;
            filter = 3;
            dataGridView1.DataSource = repositoryTest.GetAll().Select(x => new { Id = x.Id, Title = x.Title, Author = x.Author, DtCreate = x.DtCreate }).ToList();
        }

        private void loadTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = true;
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                test = TestConverter.convert(openFileDialog1.FileName);
                textBox1.Text = test.Author;
                textBox2.Text = test.Title;
                textBox3.Text = test.Questions.Count.ToString();

            }
        }

        private void showTestsOfGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearEvents();
            comboBox1.Visible = true;
            comboBox1.SelectedIndex = 0;
            dataGridView1.Visible = true;
            label1.Visible = true;
            ShowTests(sender, e);
            comboBox1.SelectedIndexChanged += ShowTests;
        }
        private void ShowTests(object sender, EventArgs e)
        {
            var group = (Group)comboBox1.SelectedItem;
            if (group != null)
            {
                dataGridView1.DataSource = group.Tests;
            }
        }

        private void asignessTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearEvents();
            comboBox1.Visible = true;
            comboBox1.SelectedIndex = 0;
            dataGridView1.Visible = true;
            label1.Visible = true;
            dataGridView1.DataSource = repositoryTest.GetAll().Select(x => new { Id = x.Id, Title = x.Title, Author = x.Author, DtCreate = x.DtCreate }).ToList();
            comboBox1.SelectedIndexChanged += AddTestSelected;
        }
        private void AddTestSelected(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                {
                    var group = (Group)comboBox1.SelectedItem;
                    if (group != null)
                    {
                        Test test = repositoryTest.FindById(row.Cells[0].Value);
                        test.Groups.Add(group);
                        repositoryTest.Update(test);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            test.Hour = Convert.ToInt32(numericUpDown1.Value);
            test.Minute = Convert.ToInt32(numericUpDown2.Value);
            repositoryTest.Add(test);
            MessageBox.Show("Test is loaded");
        }
    }
}
