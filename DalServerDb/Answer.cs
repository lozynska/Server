using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalServerDb
{
    [Serializable]
    public class Answer
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool isRight { get; set; }
        
        
    }
}
