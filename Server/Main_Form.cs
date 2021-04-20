using DalServerDb;
using Repositiryex1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
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
        }
    }
}
