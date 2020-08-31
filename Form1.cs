using SqlExample;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentInfo
{
    public partial class Form1 : Form
    {
        private SQLHelper helper = new SQLHelper();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnDone_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnConnection_Click(object sender, EventArgs e)
        {
            // Always see if we have all the details to make a connection string
            if (!makeDataBaseConnection())
                // leave immediately if we can't build the connection string
                return;

            if (helper.TestConnection())
                MessageBox.Show("Success.");

        }

        private bool makeDataBaseConnection()
        {
            if (txtUser.TextLength == 0 | txtPassword.TextLength == 0 )
            {
                MessageBox.Show("UserName, Password field is empty");
                return false;
            }
            if (txtDBName.TextLength == 0)
            {
                txtDBName.Text = txtUser.Text;
            }
            helper.DBName = txtDBName.Text;
            helper.UserId = txtUser.Text;
            helper.Password = txtPassword.Text;
            helper.ServerName = "essql1.walton.uark.edu";
            return true;
        }
    }
}
