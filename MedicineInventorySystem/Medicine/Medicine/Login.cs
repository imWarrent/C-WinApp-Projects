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

namespace Medicine
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (user.Text.Equals("") || pass.Text.Equals(""))
            {
                MessageBox.Show("Don't leave textfield empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string conString = "datasource=localhost; port=3306; username=root; password=";

                MySqlConnection conDB = new MySqlConnection(conString);
                MySqlCommand cmd = new MySqlCommand("SELECT Fullname, Username,Password,Usertype FROM m_inventory.employee WHERE Username = '" + this.user.Text + "' and Password = '" + this.pass.Text + "' and (Usertype = 'Admin' or Usertype = 'Employee')", conDB);
                MySqlDataReader myReader;

                try
                {
                    conDB.Open();
                    myReader = cmd.ExecuteReader();
                    if (myReader.Read())
                    {
                        Main ma = new Main();
                        String x = myReader["Usertype"].ToString();

                        if (x.Equals("Employee")) {
                            ma.adb.Visible = false;
                            ma.adb1.Visible = false;
                            ma.adb2.Visible = false;
                        }
                        ma.name.Text = x + ": " + myReader["Fullname"].ToString();
                        ma.Show();
                        Hide();
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
