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
    public partial class Signup : Form
    {
        public Signup()
        {
            InitializeComponent();
        }

        void CreateUser() {
            long con = long.Parse(contact.Text);
            string conString = "datasource=localhost; port=3306; username=root; password=";
            string query = "INSERT INTO bangketa.business (`business_id`, `username`, `password`, `business_name`, `owner`, `address`, `contact`, `email`) " +
                "VALUES ('','"+uname.Text+"','"+pass.Text+"','"+business.Text+"','"+owner.Text+"','"+add.Text+"',"+con+",'"+email.Text+"')";

            MySqlConnection DBConnector = new MySqlConnection(conString);
            MySqlCommand cmd = new MySqlCommand(query, DBConnector);
            MySqlDataReader myReader;

            try
            {
                DBConnector.Open();
                myReader = cmd.ExecuteReader();
                Sales sa = new Sales();
                sa.user.Text = uname.Text;
                sa.bname.Text = business.Text;
                sa.Show();
                Hide();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void label20_Click(object sender, EventArgs e)
        {
              Close();
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            Login li = new Login();
            li.Show();
            Hide();
        }

        private void panel6_Click(object sender, EventArgs e)
        {
            if (uname.Text.Equals("") || pass.Text.Equals("") || email.Text.Equals("") || add.Text.Equals("") || contact.Text.Equals("") || business.Text.Equals("") || owner.Text.Equals(""))
            {
                MessageBox.Show("There's an empty field!");
            }
            else
            {
                string conString = "datasource=localhost; port=3306; username=root; password=";

                MySqlConnection conDB = new MySqlConnection(conString);
                MySqlCommand cmd = new MySqlCommand("SELECT username FROM bangketa.business WHERE username = '" + uname.Text + "'", conDB);
                MySqlDataReader myReader;

                try
                {
                    conDB.Open();
                    myReader = cmd.ExecuteReader();
                    if (myReader.Read())
                    {
                        MessageBox.Show("Username already exist!");
                        uname.Text = "";
                        uname.Focus();
                    }
                    else
                    {
                        CreateUser();
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
