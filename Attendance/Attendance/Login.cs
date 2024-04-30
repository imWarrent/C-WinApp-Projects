using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Attendance
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (user.Text.Equals("") || pass.Text.Equals(""))
            {
                MessageBox.Show("Don't leave textfield empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string conString = "datasource=sql6.freemysqlhosting.net; port=3306; username=sql6459251; password=isWWC7KVcW";

                MySqlConnection conDB = new MySqlConnection(conString);
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM sql6459251.user WHERE uname = '" + this.user.Text + "' and pass = '" + this.pass.Text + "'", conDB);
                MySqlDataReader myReader;

                try
                {
                    conDB.Open();
                    myReader = cmd.ExecuteReader();
                    if (myReader.Read())
                    {
                        Admin ma = new Admin();
                        User us = new User();
                        String x = myReader["user_type"].ToString();
                        String y = myReader["uname"].ToString();
                        String z = myReader["status"].ToString();

                        if (z.Equals("Deactive"))
                        {
                            user.Text = "";
                            pass.Text = "";
                            user.Focus();
                            MessageBox.Show("You're account is deactivated. \n\n Note: Contact the Admin for support.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else if (x.Equals("Master"))
                        {
                            ma.p5.Visible = true;
                            ma.user.Text = y;

                            ma.Show();
                            Hide();
                        }
                        else if (x.Equals("Admin"))
                        {
                            ma.p5.Visible = false;
                            ma.user.Text = y;

                            ma.Show();
                            Hide();
                        }
                        else {
                            us.userr.Text = y;
                            us.Show();
                            Hide();
                        }
                        
                    }
                    else
                    {
                        user.Text = "";
                        pass.Text = "";
                        user.Focus();
                        MessageBox.Show("Invalid Username/Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
