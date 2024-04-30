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

namespace Bangketa
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void label20_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            Signup su = new Signup();
            su.Show();
            Hide();
        }

        private void panel6_Click(object sender, EventArgs e)
        {
            if (user.Text.Equals("") || pass.Text.Equals(""))
            {
                MessageBox.Show("Don't leave textfield empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string conString = "datasource=localhost; port=3306; username=root; password=";

                MySqlConnection conDB = new MySqlConnection(conString);
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM bangketa.business WHERE username = '" + this.user.Text + "' and password = '" + this.pass.Text + "'", conDB);
                MySqlDataReader myReader;

                try
                {
                    conDB.Open();
                    myReader = cmd.ExecuteReader();
                    if (myReader.Read())
                    {
                        Sales sa = new Sales();
                        sa.user.Text = myReader["username"].ToString();
                        sa.bname.Text = myReader["business_name"].ToString();
                        sa.Show();
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
