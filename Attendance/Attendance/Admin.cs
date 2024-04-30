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
    public partial class Admin : Form
    {
        string conString = "datasource=sql6.freemysqlhosting.net; port=3306; username=sql6459251; password=isWWC7KVcW";
        public Admin()
        {
            InitializeComponent();
            o2.Visible = false; o3.Visible = false; o4.Visible = false; o5.Visible = false;
            employee.Visible = false; all.Visible = false; attendance.Visible = false; ontime.Visible = false;

            GetNoEmployee(); GetNoUser(); GetNoTime(); GetNoAttendance();GetOT();DOT();DAT();
        }

        #region MouseControl
        private void p1_MouseEnter(object sender, EventArgs e)
        {
            l1.ForeColor = System.Drawing.Color.Gray;
        }
        private void p2_MouseEnter(object sender, EventArgs e)
        {
            l2.ForeColor = System.Drawing.Color.Gray;
        }

        private void p3_MouseEnter(object sender, EventArgs e)
        {
            l3.ForeColor = System.Drawing.Color.Gray;
        }

        private void p4_MouseEnter(object sender, EventArgs e)
        {
            l4.ForeColor = System.Drawing.Color.Gray;
        }

        private void p5_MouseEnter(object sender, EventArgs e)
        {
            l5.ForeColor = System.Drawing.Color.Gray;
        }

        private void p1_MouseLeave(object sender, EventArgs e)
        {
            l1.ForeColor = System.Drawing.Color.White;
        }

        private void p2_MouseLeave(object sender, EventArgs e)
        {
            l2.ForeColor = System.Drawing.Color.White;
        }

        private void p3_MouseLeave(object sender, EventArgs e)
        {
            l3.ForeColor = System.Drawing.Color.White;
        }

        private void p4_MouseLeave(object sender, EventArgs e)
        {
            l4.ForeColor = System.Drawing.Color.White;
        }

        private void p5_MouseLeave(object sender, EventArgs e)
        {
            l5.ForeColor = System.Drawing.Color.White;
        }

        private void p1_MouseClick(object sender, MouseEventArgs e)
        {
            GetNoEmployee(); GetNoUser(); GetNoTime(); GetNoAttendance();
            o1.Visible = true;  o2.Visible = false; o3.Visible = false; o4.Visible = false; o5.Visible = false;
            dashboard.Visible = true; employee.Visible = false; all.Visible = false; attendance.Visible = false; ontime.Visible = false;
        }

        private void p2_MouseClick(object sender, MouseEventArgs e)
        {
            GetEmployee();
            o1.Visible = false; o2.Visible = true; o3.Visible = false; o4.Visible = false; o5.Visible = false;
            dashboard.Visible = false; employee.Visible = true; all.Visible = false; attendance.Visible = false; ontime.Visible = false;

            g1.Text = ""; g2.Text = ""; g3.Text = ""; g4.Text = ""; uid.Text = "None";
            g1.Focus();
        }

        private void p3_MouseClick(object sender, MouseEventArgs e)
        {
            o1.Visible = false; o2.Visible = false; o3.Visible = true; o4.Visible = false; o5.Visible = false;
            dashboard.Visible = false; employee.Visible = false; all.Visible = false; attendance.Visible = false; ontime.Visible = true;
            sot.Items.Clear();
            GetOT(); DOT();
        }

        private void p4_MouseClick(object sender, MouseEventArgs e)
        {
            o1.Visible = false; o2.Visible = false; o3.Visible = false; o4.Visible = true; o5.Visible = false;
            dashboard.Visible = false; employee.Visible = false; all.Visible = false; attendance.Visible = true; ontime.Visible = false;
            sam.Items.Clear();
            GetAttendance();DAT();
        }

        private void p5_MouseClick(object sender, MouseEventArgs e)
        {
            if (p5.Visible == true)
            {
                o1.Visible = false; o2.Visible = false; o3.Visible = false; o4.Visible = false; o5.Visible = true;
                dashboard.Visible = false; employee.Visible = false; all.Visible = true; attendance.Visible = false; ontime.Visible = false;
                GetUser();
            }
        }
        #endregion
        #region Database
        //DISTINCT DATA
        void DOT() {
            String dates = DateTime.Now.ToString("MMMM dd, yyyy");
            MySqlConnection conDatabase = new MySqlConnection(conString);
            MySqlCommand cmdDatabase = new MySqlCommand("SELECT distinct(username) as 'Username' " +
                "FROM sql6459251.sched WHERE DateIn = '" + dates + "'", conDatabase);
            MySqlDataReader myReader;

            try
            {
                conDatabase.Open();
                myReader = cmdDatabase.ExecuteReader();
                while (myReader.Read())
                {
                    sot.Items.Add(myReader["Username"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conDatabase.Close();
        }
        void DAT() {
            String dates = DateTime.Now.ToString("MMMM dd, yyyy");
 
            MySqlConnection conDatabase = new MySqlConnection(conString);
            MySqlCommand cmdDatabase = new MySqlCommand("SELECT distinct(username) as 'Username' " +
                "FROM sql6459251.sched", conDatabase);
            MySqlDataReader myReader;

            try
            {
                conDatabase.Open();
                myReader = cmdDatabase.ExecuteReader();
                while (myReader.Read())
                {
                    sam.Items.Add(myReader["Username"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conDatabase.Close();
        }
        //SEARCH DATA
        void SearchEmployee() {
            if (!sel.Text.Equals(""))
            {
                //Getting data for table employees
     
                MySqlConnection conDatabase = new MySqlConnection(conString);
                MySqlCommand cmdDatabase = new MySqlCommand("SELECT `uid` as 'UserID', `uname` as 'Username', `pass` as 'Password', `fullname` as 'Fullname', `gender` as 'Gender', `age` as 'Age', `status` as 'Status' " +
                "FROM sql6459251.user WHERE uname LIKE '%" + sel.Text + "%' AND user_type != 'master'", conDatabase);

                try
                {
                    MySqlDataAdapter sda = new MySqlDataAdapter();
                    sda.SelectCommand = cmdDatabase;
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    BindingSource bs = new BindingSource();
                    bs.DataSource = dt;
                    emtab.DataSource = bs;
                    sda.Update(dt);


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                conDatabase.Close();
            }
            else
            {
                GetEmployee();
            }
        }
        void SearchOT() {
            String dates = DateTime.Now.ToString("MMMM dd, yyyy");
 
            MySqlConnection conDatabase = new MySqlConnection(conString);
            MySqlCommand cmdDatabase = new MySqlCommand("SELECT username as 'Username', `TimeIn` as 'Time In', `TimeOut` as 'Time Out', `DateIn` as 'Date', `Checker` as 'Status' " +
                "FROM sql6459251.sched WHERE DateIn = '" + dates + "' and username = '"+sot.Text+"'", conDatabase);

            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmdDatabase;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                BindingSource bs = new BindingSource();
                bs.DataSource = dt;
                otm.DataSource = bs;
                sda.Update(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conDatabase.Close();
        }
        void SearchAttendance() {
            String dates = DateTime.Now.ToString("MMMM dd, yyyy");
 
            MySqlConnection conDatabase = new MySqlConnection(conString);
            MySqlCommand cmdDatabase = new MySqlCommand("SELECT username as 'Username', `TimeIn` as 'Time In', `TimeOut` as 'Time Out', `DateIn` as 'Date In', DateOut as 'Date Out', `Checker` as 'Status' " +
                "FROM sql6459251.sched WHERE username = '"+sam.Text+"'", conDatabase);

            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmdDatabase;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                BindingSource bs = new BindingSource();
                bs.DataSource = dt;
                amd.DataSource = bs;
                sda.Update(dt);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conDatabase.Close();
        }

        //RETRIEVING DATA
        void GetUser() {
 
            MySqlConnection conDatabase = new MySqlConnection(conString);
            MySqlCommand cmdDatabase = new MySqlCommand("SELECT `uid` as 'UserID', `fullname` as 'Fullname', `uname` as 'Username', `pass` as 'Password', user_type as 'UserType', `status` as 'Status' " +
                "FROM sql6459251.user", conDatabase);

            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmdDatabase;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                BindingSource bs = new BindingSource();
                bs.DataSource = dt;
                dal.DataSource = bs;
                sda.Update(dt);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conDatabase.Close();
        }
        void GetEmployee() {
 
            MySqlConnection conDatabase = new MySqlConnection(conString);
            MySqlCommand cmdDatabase = new MySqlCommand("SELECT `uid` as 'UserID', `uname` as 'Username', `pass` as 'Password', `fullname` as 'Fullname', `gender` as 'Gender', `age` as 'Age', `status` as 'Status' " +
                "FROM sql6459251.user WHERE user_type = 'Employee'", conDatabase);

            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmdDatabase;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                BindingSource bs = new BindingSource();
                bs.DataSource = dt;
                emtab.DataSource = bs;
                sda.Update(dt);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conDatabase.Close();
        }
        void GetOT()
        {
            String dates = DateTime.Now.ToString("MMMM dd, yyyy");
 
            MySqlConnection conDatabase = new MySqlConnection(conString);
            MySqlCommand cmdDatabase = new MySqlCommand("SELECT username as 'Username', `TimeIn` as 'Time In', `TimeOut` as 'Time Out', `DateIn` as 'Date', `Checker` as 'Status' " +
                "FROM sql6459251.sched WHERE DateIn = '"+dates+"'", conDatabase);

            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmdDatabase;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                BindingSource bs = new BindingSource();
                bs.DataSource = dt;
                otm.DataSource = bs;
                sda.Update(dt);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conDatabase.Close();
        }
        void GetAttendance() {
            String dates = DateTime.Now.ToString("MMMM dd, yyyy");
 
            MySqlConnection conDatabase = new MySqlConnection(conString);
            MySqlCommand cmdDatabase = new MySqlCommand("SELECT username as 'Username', `TimeIn` as 'Time In', `TimeOut` as 'Time Out', `DateIn` as 'Date In', DateOut as 'Date Out', `Checker` as 'Status' " +
                "FROM sql6459251.sched", conDatabase);

            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmdDatabase;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                BindingSource bs = new BindingSource();
                bs.DataSource = dt;
                amd.DataSource = bs;
                sda.Update(dt);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conDatabase.Close();
        }

        //FOR COUNTER DATA
        void GetNoEmployee() {
 
            MySqlConnection conDatabase = new MySqlConnection(conString);
            MySqlCommand cmdDatabase = new MySqlCommand("SELECT count(uid) as UID FROM sql6459251.user WHERE user_type = 'Employee' ", conDatabase);
            MySqlDataReader myReader;

            try
            {
                conDatabase.Open();
                myReader = cmdDatabase.ExecuteReader();
                if (myReader.Read())
                {
                    n1.Text = myReader["UID"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conDatabase.Close();
        }
        void GetNoTime()
        {
            string dates = DateTime.Now.ToString("MMMM dd, yyyy");
 
            MySqlConnection conDatabase = new MySqlConnection(conString);
            MySqlCommand cmdDatabase = new MySqlCommand("SELECT count(sid) as UID FROM sql6459251.sched WHERE DateIn = '"+dates+"'", conDatabase);
            MySqlDataReader myReader;

            try
            {
                conDatabase.Open();
                myReader = cmdDatabase.ExecuteReader();
                if (myReader.Read())
                {
                    n2.Text = myReader["UID"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conDatabase.Close();
        }
        void GetNoAttendance()
        {
            string dates = DateTime.Now.ToString("MMMM dd, yyyy");
 
            MySqlConnection conDatabase = new MySqlConnection(conString);
            MySqlCommand cmdDatabase = new MySqlCommand("SELECT count(sid) as UID FROM sql6459251.sched", conDatabase);
            MySqlDataReader myReader;

            try
            {
                conDatabase.Open();
                myReader = cmdDatabase.ExecuteReader();
                if (myReader.Read())
                {
                    n3.Text = myReader["UID"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conDatabase.Close();
        }
        void GetNoUser()
        {
 
            MySqlConnection conDatabase = new MySqlConnection(conString);
            MySqlCommand cmdDatabase = new MySqlCommand("SELECT count(uid) as UID FROM sql6459251.user", conDatabase);
            MySqlDataReader myReader;

            try
            {
                conDatabase.Open();
                myReader = cmdDatabase.ExecuteReader();
                if (myReader.Read())
                {
                    n4.Text = myReader["UID"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            conDatabase.Close();
        }

        //INSERTING DATA
        void InsertEmployee() {
            int agee = Int32.Parse(g4.Text);
 
            string query = "INSERT INTO sql6459251.user(`uid`, `uname`, `pass`, `fullname`, `gender`, `age`, `user_type`, `status`) " +
                "VALUES ('','"+g2.Text+"','"+ g3.Text + "','"+ g1.Text + "','"+ g5.Text + "',"+ agee + ",'Employee','"+ g6.Text + "')";

            MySqlConnection conDB = new MySqlConnection(conString);
            MySqlCommand cmd = new MySqlCommand(query, conDB);
            MySqlDataReader myReader;

            try
            {
                conDB.Open();
                myReader = cmd.ExecuteReader();
                MessageBox.Show("Added!");
                g1.Text = "";
                g2.Text = "";
                g3.Text = "";
                g4.Text = "";
                g1.Focus();
                GetEmployee();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            conDB.Close();
        }

        void InsertUser() {
 
            string query = "INSERT INTO sql6459251.user(`uid`, `uname`, `pass`, `fullname`, `gender`, `age`, `user_type`, `status`) " +
                "VALUES ('', '"+f2.Text+ "', '"+f3.Text+ "', '"+f1.Text+ "', 'Undefined', '18', '"+f4.Text+ "', '"+f5.Text+"')";

            MySqlConnection conDB = new MySqlConnection(conString);
            MySqlCommand cmd = new MySqlCommand(query, conDB);
            MySqlDataReader myReader;

            try
            {
                conDB.Open();
                myReader = cmd.ExecuteReader();
                MessageBox.Show("Added!");
                f1.Text = "";
                f2.Text = "";
                f3.Text = "";
                f4.Text = "";
                f5.Text = "";
                f1.Focus();
                GetUser();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            conDB.Close();
        }

        //UPDATING DATA
        void UpdateEmployee()
        {
            DialogResult dr = MessageBox.Show("Are you sure?", "Updating Employee Data", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                int userid = Int32.Parse(uid.Text);
                int agee = Int32.Parse(g4.Text);
     
                string query = "UPDATE sql6459251.user SET " +
                    "`pass`='"+g3.Text+"'," +
                    "`fullname`='" + g1.Text + "'," +
                    "`gender`='" + g5.Text + "'," +
                    "`age`="+agee+"," +
                    "`status`='"+g6.Text+"' WHERE uid = "+userid+"";

                MySqlConnection conDB = new MySqlConnection(conString);
                MySqlCommand cmd = new MySqlCommand(query, conDB);
                MySqlDataReader myReader;

                try
                {
                    conDB.Open();
                    myReader = cmd.ExecuteReader();
                    MessageBox.Show("Employee Data Updated!");
                    GetEmployee();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                conDB.Close();
            }
        }

        void UpdateUser()
        {
            DialogResult dr = MessageBox.Show("Are you sure? \n\n Note: You cannot change the Username.", "Updating User Data", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                int userid = Int32.Parse(uid.Text);
     
                string query = "UPDATE sql6459251.user SET " +
                    "`pass`='" + f3.Text + "'," +
                    "`fullname`='" + f1.Text + "'," +
                    "`user_type`='" + f4.Text + "'," +
                    "`status`= '" + f5.Text + "' WHERE uid = " + userid + "";

                MySqlConnection conDB = new MySqlConnection(conString);
                MySqlCommand cmd = new MySqlCommand(query, conDB);
                MySqlDataReader myReader;

                try
                {
                    conDB.Open();
                    myReader = cmd.ExecuteReader();
                    MessageBox.Show("User Data Updated!");
                    GetUser();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                conDB.Close();
            }
        }

        //DELETING DATA
        void DeleteEmployee() {

            DialogResult dr = MessageBox.Show("Are you sure?", "Deleting User Data", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
     
                string query = "DELETE FROM sql6459251.user WHERE uid = '" + uid.Text + "'";

                MySqlConnection conDB = new MySqlConnection(conString);
                MySqlCommand cmd = new MySqlCommand(query, conDB);
                MySqlDataReader myReader;

                try
                {
                    conDB.Open();
                    myReader = cmd.ExecuteReader();
                    MessageBox.Show("User Data Deleted!");
                    g1.Text = "";
                    g2.Text = "";
                    g3.Text = "";
                    g4.Text = "";
                    f1.Text = "";
                    f2.Text = "";
                    f3.Text = "";
                    g1.Focus();
                    GetEmployee();
                    GetUser();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                conDB.Close();
            }
        }
        #endregion
        #region PROCESS

        //INSERTING EMPLOYEE
        private void button3_Click(object sender, EventArgs e)
        {
            if (g1.Text.Equals("") || g2.Text.Equals("") || g3.Text.Equals("") || g4.Text.Equals("") || g5.SelectedItem == null || g6.SelectedItem == null)
            {
                MessageBox.Show("There's an empty field!");
            }
            else
            {
                //USERNAME TESTING
     

                MySqlConnection conDB = new MySqlConnection(conString);
                MySqlCommand cmd = new MySqlCommand("SELECT uname FROM sql6459251.user WHERE uname = '"+g2.Text+"'", conDB);
                MySqlDataReader myReader;

                try
                {
                    conDB.Open();
                    myReader = cmd.ExecuteReader();
                    if (myReader.Read())
                    {
                        MessageBox.Show("Username already exist!");
                        g2.Text = "";
                        g2.Focus();
                    }
                    else
                    {
                        InsertEmployee();
                    }
                }
                catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }
                conDB.Close();
            }
        }
        
        //UPDATING EMPLOYEE
        private void button1_Click(object sender, EventArgs e)
        {
            if (g1.Text.Equals("") || g2.Text.Equals("") || g3.Text.Equals("") || g4.Text.Equals("") || g5.SelectedItem == null || g6.SelectedItem == null)
            {
                MessageBox.Show("There's an empty field!");
            }
            else if (uid.Text.Equals("None")) {
                MessageBox.Show("No User Selected!");
            }
            else
            {
                UpdateEmployee();
            }
        }

        //TABLE CODE
        private void emtab_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (emtab.SelectedRows.Count > 0)
            {
                int sri = emtab.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = emtab.Rows[sri];

                uid.Text = Convert.ToString(selectedRow.Cells["UserID"].Value);
                g1.Text = Convert.ToString(selectedRow.Cells["Fullname"].Value);
                g2.Text = Convert.ToString(selectedRow.Cells["Username"].Value);
                g3.Text = Convert.ToString(selectedRow.Cells["Password"].Value);
                g4.Text = Convert.ToString(selectedRow.Cells["Age"].Value);
                g5.Text = Convert.ToString(selectedRow.Cells["Gender"].Value);
                g6.Text = Convert.ToString(selectedRow.Cells["Status"].Value);
            }
        }

        private void sel_TextChanged(object sender, EventArgs e)
        {
            SearchEmployee();
        }

        //DELETE EMPLOYEE
        private void button2_Click(object sender, EventArgs e)
        {
            if (g1.Text.Equals("") || g2.Text.Equals("") || g3.Text.Equals("") || g4.Text.Equals("") || g5.SelectedItem == null || g6.SelectedItem == null)
            {
                MessageBox.Show("There's an empty field!");
            }
            else if (uid.Text.Equals("None"))
            {
                MessageBox.Show("No User Selected!");
            }
            else
            {
                DeleteEmployee();
            }
        }

        private void pictureBox6_MouseClick(object sender, MouseEventArgs e)
        {
            Login log = new Login();
            log.Show();
            Hide();
        }

        private void sam_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchAttendance();
        }

        private void sot_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchOT();
        }

        #endregion

        private void panel12_MouseClick(object sender, MouseEventArgs e)
        {
            GetAttendance();
        }

        private void panel13_MouseClick(object sender, MouseEventArgs e)
        {
            GetOT();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (uid.Text.Equals("None"))
            {
                MessageBox.Show("No User Selected!");
            }
            else
            {
                DeleteEmployee();
            }
        }

        private void dal_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dal.SelectedRows.Count > 0)
            {
                int sri = dal.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dal.Rows[sri];

                uid.Text = Convert.ToString(selectedRow.Cells["UserID"].Value);
                f1.Text = Convert.ToString(selectedRow.Cells["Fullname"].Value);
                f2.Text = Convert.ToString(selectedRow.Cells["Username"].Value);
                f3.Text = Convert.ToString(selectedRow.Cells["Password"].Value);
                f4.Text = Convert.ToString(selectedRow.Cells["UserType"].Value);
                f5.Text = Convert.ToString(selectedRow.Cells["Status"].Value);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (f1.Text.Equals("") || f2.Text.Equals("") || f3.Text.Equals("") || f4.SelectedItem == null || f5.SelectedItem == null)
            {
                MessageBox.Show("There's an empty field!");
            }
            else
            {
                //USERNAME TESTING
                MySqlConnection conDB = new MySqlConnection(conString);
                MySqlCommand cmd = new MySqlCommand("SELECT uname FROM sql6459251.user WHERE uname = '" + f2.Text + "'", conDB);
                MySqlDataReader myReader;

                try
                {
                    conDB.Open();
                    myReader = cmd.ExecuteReader();
                    if (myReader.Read())
                    {
                        MessageBox.Show("Username already exist!");
                        f2.Text = "";
                        f2.Focus();
                    }
                    else
                    {
                        InsertUser();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                conDB.Close();
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (f1.Text.Equals("") || f2.Text.Equals("") || f3.Text.Equals("") || f4.SelectedItem == null || f5.SelectedItem == null)
            {
                MessageBox.Show("There's an empty field!");
            }
            else if (uid.Text.Equals("None"))
            {
                MessageBox.Show("No User Selected!");
            }
            else
            {
                UpdateUser();
            }
        }
    }
}
