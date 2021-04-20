using DalServerDb;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public Main_Form(User user)
        {
            InitializeComponent();
            admin = user;
        }
    }
}
