using DalServerDb;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;


namespace Client
{
   public class TestClient
    {
        TcpClient client;
        public void Strart()
        {
            client = new TcpClient("localHost", 8080);
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            };
        }

        public User Login(string login, string pwd)
        {
            User user = new User();
            user.Login = login;
            user.Password = pwd;
            user = Send(user);
            return user;
        }

        private User Send(User user)
        {
           NetworkStream ns = client.GetStream();
              
                string json = JsonConvert.SerializeObject(user);
                StreamWriter writer = new StreamWriter(ns, Encoding.UTF8);
                writer.WriteLine(json);
            writer.Flush();
           
            StreamReader reader = new StreamReader(ns, Encoding.UTF8);
                json = reader.ReadToEnd();
                user = JsonConvert.DeserializeObject<User>(json);
            writer.Close();
            reader.Close();
                ns.Close();
           
            return user;
        }
    }
}
