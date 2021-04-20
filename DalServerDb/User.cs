using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalServerDb
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool isAdmin { get; set; }
        public virtual ICollection<UserAnswear> UserAnswears { get; set; }
        public User()
        {
            UserAnswears = new List<UserAnswear>();
        }
    }
}
