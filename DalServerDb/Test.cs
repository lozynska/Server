﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalServerDb
{
    [Serializable]
    public class Test
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime DtCreate { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ICollection<Group> Groups { get; set; }
        public Test()
        {
            Questions = new List<Question>();
            Groups = new List<Group>();
        }
        public override string ToString()
        {
            return $"{Title}";
        }
    }
}
