using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace DalServerDb
{
    public class ServerContext : DbContext
    {
        public ServerContext(string conStr) : base(conStr) { }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<UserAnswear> UserAnswers { get; set; }
        public virtual DbSet<Result> Results { get; set; }
    }
}
