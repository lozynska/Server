using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalServerDb
{
    [Serializable]
    public class UserAnswear
    {
        public int Id { get; set; }
        public DateTime Dt { get; set; }
        public User User { get; set; }
        public Answer Answer { get; set; }
    }
}
