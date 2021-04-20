using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalServerDb
{
    public class Answer
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool isRight { get; set; }
        public virtual ICollection<UserAnswear> UserAnswears { get; set; }
        public Answer()
        {
            UserAnswears = new List<UserAnswear>();
        }
    }
}
