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
    public partial class User : Form
    {
        String times = DateTime.Now.ToString("hh:mm tt");
        String dates = DateTime.Now.ToString("MMMM dd, yyyy");
        string conString = "datasource=sql6.freemysqlhosting.net; port=3306; username=sql6459251; password=isWWC7KVcW";
        public User()
        {
            InitializeComponent();

            settings.Visible = false;
            o2.Visible = false; o2.Visible = false;
            timer1.Start();
        }

        #region

        void GetAttendance() {
            
            MySqlConnection conDatabase = new MySqlConnection(conString);
            MySqlCommand cmdDatabase = new MySqlCommand("SELECT TimeIn as 'Time In', TimeOut as 'Time Out', DateIn as 'Date In', DateOut as 'Date Out', Checker FROM" +
                " sql6459251.sched WHERE username = '"+userr.Text+"'", conDatabase);

            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmdDatabase;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                BindingSource bs = new BindingSource();
                bs.DataSource = dt;
                attendance.DataSource = bs;
                sda.Update(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void InsertTimeIn() {
            
            string query = "INSERT INTO sql6459251.sched (`sid`, `TimeIn`, `TimeOut`, `DateIn`, `DateOut`, `username`, `Checker`) " +
                "VALUES ('','"+times+"','Not Yet','"+dates+"','Not Yet','"+userr.Text+"','Working')";

            MySqlConnection conDB = new MySqlConnection(conString);
            MySqlCommand cmd = new MySqlCommand(query, conDB);
            MySqlDataReader myReader;

            try
            {
                conDB.Open();
                myReader = cmd.ExecuteReader();
                MessageBox.Show("Time In!");
                tin.Text = times;
                GetAttendance();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        void InsertTimeOut() {
            
            string query = "UPDATE sql6459251.sched SET TimeOut = '"+times+"', DateOut = '"+dates+"', Checker = 'Done' WHERE username = '"+userr.Text+"' and Checker = 'Working'";

            MySqlConnection conDB = new MySqlConnection(conString);
            MySqlCommand cmd = new MySqlCommand(query, conDB);
            MySqlDataReader myReader;

            try
            {
                conDB.Open();
                myReader = cmd.ExecuteReader();
                MessageBox.Show("Time Out!");
                tout.Text = times;
                GetAttendance();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void Leaver() {
            //IF NOT YET TIMED OUT BUT CLOSED THE SYSTEM
            
            MySqlConnection conDB = new MySqlConnection(conString);
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM sql6459251.sched WHERE username = '" + userr.Text + "' and Checker = 'Working'", conDB);
            MySqlDataReader myReader;

            try
            {
                conDB.Open();
                myReader = cmd.ExecuteReader();
                if (myReader.Read())
                {
                    tin.Text = myReader["TimeIn"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        private void timer1_Tick(object sender, EventArgs e)
        {
            String dates = DateTime.Now.ToString("MMMM dd, yyyy");
            String times = DateTime.Now.ToString("hh:mm:ss tt");
            date.Text = dates + " " + times;
        }

        private void panel10_MouseClick(object sender, MouseEventArgs e)
        {
            MySqlConnection conDB = new MySqlConnection(conString);
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM sql6459251.sched WHERE username = '" + userr.Text + "' and Checker = 'Working'", conDB);
            MySqlDataReader myReader;

            try
            {
                conDB.Open();
                myReader = cmd.ExecuteReader();
                if (myReader.Read())
                {
                    MessageBox.Show("You're already timed in!");
                }
                else
                {
                    InsertTimeIn();
                    tout.Text = "NOT YET";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void panel11_MouseClick(object sender, MouseEventArgs e)
        {
            

            MySqlConnection conDB = new MySqlConnection(conString);
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM sql6459251.sched WHERE username = '" + userr.Text + "' and Checker = 'Working'", conDB);
            MySqlDataReader myReader;

            try
            {
                conDB.Open();
                myReader = cmd.ExecuteReader();
                if (myReader.Read()){
                    InsertTimeOut();
                }
                else {
                    MessageBox.Show("You're not timed in yet!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void panel4_MouseClick(object sender, MouseEventArgs e)
        {
            

            MySqlConnection conDB = new MySqlConnection(conString);
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM sql6459251.user WHERE uname = '"+userr.Text+"'", conDB);
            MySqlDataReader myReader;

            try
            {
                conDB.Open();
                myReader = cmd.ExecuteReader();
                if (myReader.Read())
                {
                    g1.Text = myReader["fullname"].ToString();
                    g2.Text = myReader["pass"].ToString();
                    g3.Text = myReader["age"].ToString();
                    g4.Text = myReader["gender"].ToString();

                    dashboard.Visible = false; settings.Visible = true;
                    o1.Visible = false; o2.Visible = true;
                    title.Text = "USER SETTINGS";
                }
                else
                {
                    MessageBox.Show("You're not time in yet!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void panel8_MouseClick(object sender, MouseEventArgs e)
        {
            GetAttendance();
            dashboard.Visible = true; settings.Visible=false;
            o1.Visible = true; o2.Visible = false;
            title.Text = "DASHBOARD";
        }

        private void label3_Click(object sender, EventArgs e)
        {
            dashboard.Visible = true; settings.Visible=false;
            o1.Visible = true; o2.Visible = false;
            title.Text = "DASHBOARD";
        }

        private void l1_Click(object sender, EventArgs e)
        {
            dashboard.Visible = false; settings.Visible=true;
            o1.Visible = false; o2.Visible = true;
            title.Text = "USER SETTINGS";
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (tout.Text.Equals("NOT YET") && !tin.Text.Equals("NOT YET"))
            {
                DialogResult dr = MessageBox.Show("You're not yet timed out, Are you sure?", "Logout", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    Login log = new Login();
                    log.Show();
                    Hide();
                }
            }
            else {
                Login log = new Login();
                log.Show();
                Hide();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string query = "UPDATE sql6459251.user SET `pass`='"+g2.Text+"',`fullname`='"+g1.Text+"',`gender`='"+g4.Text+"',`age`='"+g3.Text+"' WHERE uname = '"+userr.Text+"'";

            MySqlConnection conDB = new MySqlConnection(conString);
            MySqlCommand cmd = new MySqlCommand(query, conDB);
            MySqlDataReader myReader;

            try
            {
                conDB.Open();
                myReader = cmd.ExecuteReader();
                MessageBox.Show("Updated!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void User_Shown(object sender, EventArgs e)
        {
            GetAttendance(); Leaver();
        }
    }
}
