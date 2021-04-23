using DalServerDb;
using Newtonsoft.Json;
using Repositiryex1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class TestServer
    {
        IGenericReposiyory<User> repositoryUser;

        public TestServer(GenericUnitOfWork work)
        {
            repositoryUser = work.Reposiyory<User>();
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {                
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            };

        }

        public void Start()
        {
            //TcpClient client = null;
            //NetworkStream stream;
            IPAddress local_host = IPAddress.Parse("127.0.0.1");

            TcpListener server = new TcpListener(local_host, 8080);
            server.Start();
            Task.Run(() =>
            {
                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();
                    NetworkStream ns = client.GetStream();
                    StreamReader reader = new StreamReader(ns, Encoding.UTF8);
                    string json = reader.ReadLine();
                    
                    User user = JsonConvert.DeserializeObject<User>(json);
                    User result = login(user);
                    json = JsonConvert.SerializeObject(result);
                    ns = client.GetStream();
                    StreamWriter writer = new StreamWriter(ns, Encoding.UTF8, 2 * json.Length);
                    writer.WriteLine(json);
                    writer.Close();                   
                    ns.Close();
                }
            });
        }

        private User login(User loginUser)
        {
            User user = repositoryUser.FindAll(x => x.Login == loginUser.Login && x.Password == loginUser.Password).FirstOrDefault();
           
            if (user == null)
            {
                user = new User();
                user.Id = -1;
            }
            else
            {
                user.Groups.ToString();
            }
            return user;
        }


            
        
    }
}
