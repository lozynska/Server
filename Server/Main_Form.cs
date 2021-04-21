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
            dataGridView1.Visible = true;
            filter = 5;
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
    }
}
