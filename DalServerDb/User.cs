using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalServerDb
{
    [Serializable]
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool isAdmin { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public User()
        {
            Groups = new List<Group>();
        }
        public override string ToString()
        {
            return $"{Name} ";
        }
    }
}
