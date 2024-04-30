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
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            precord.Hide();
            employee.Hide();
            prescription.Show();
            stock.Hide();
            table.Hide();
            check.Hide();
        }

        #region DATABASE

        void EmployeeIn() {
            if (emtab.SelectedRows.Count > 0)
            {
                int sri = emtab.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = emtab.Rows[sri];

                eid.Text = Convert.ToString(selectedRow.Cells["EmployeeID"].Value);
                ide.Text = Convert.ToString(selectedRow.Cells["Designation"].Value);
                fname.Text = Convert.ToString(selectedRow.Cells["Fullname"].Value);
                uname.Text = Convert.ToString(selectedRow.Cells["Username"].Value);
                pass.Text = Convert.ToString(selectedRow.Cells["Password"].Value);
                eut.Text = Convert.ToString(selectedRow.Cells["UserType"].Value);
            }
        }

        void GetStocks() {
            //Getting data for table stock
            string conString = "datasource=localhost; port=3306; username=root; password=";
            MySqlConnection conDatabase = new MySqlConnection(conString);
            MySqlCommand cmdDatabase = new MySqlCommand("SELECT * FROM m_inventory.stocks", conDatabase);

            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmdDatabase;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                BindingSource bs = new BindingSource();
                bs.DataSource = dt;
                medtab.DataSource = bs;
                sda.Update(dt);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void GetPatients() {
            //Getting data for table patients
            string conString = "datasource=localhost; port=3306; username=root; password=";
            MySqlConnection conDatabase = new MySqlConnection(conString);
            MySqlCommand cmdDatabase = new MySqlCommand("SELECT * FROM m_inventory.patients", conDatabase);

            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmdDatabase;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                BindingSource bs = new BindingSource();
                bs.DataSource = dt;
                patients.DataSource = bs;
                sda.Update(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void GetEmployees() {
            //Getting data for table employees
            string conString = "datasource=localhost; port=3306; username=root; password=";
            MySqlConnection conDatabase = new MySqlConnection(conString);
            MySqlCommand cmdDatabase = new MySqlCommand("SELECT * FROM m_inventory.employee", conDatabase);

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
        }

        void GetSales()
        {
            //Getting data for table sales
            string conString = "datasource=localhost; port=3306; username=root; password=";
            MySqlConnection conDatabase = new MySqlConnection(conString);
            MySqlCommand cmdDatabase = new MySqlCommand("SELECT * FROM m_inventory.sales", conDatabase);

            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmdDatabase;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                BindingSource bs = new BindingSource();
                bs.DataSource = dt;
                sales.DataSource = bs;
                sda.Update(dt);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void SearchStocks() {
            //Getting data for table stock
            if (!ssearch.Text.Equals(""))
            {
                string conString = "datasource=localhost; port=3306; username=root; password=";
                MySqlConnection conDatabase = new MySqlConnection(conString);
                MySqlCommand cmdDatabase = new MySqlCommand("SELECT * FROM m_inventory.stocks WHERE ItemNo LIKE '%" + ssearch.Text + "%'", conDatabase);

                try
                {
                    MySqlDataAdapter sda = new MySqlDataAdapter();
                    sda.SelectCommand = cmdDatabase;
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    BindingSource bs = new BindingSource();
                    bs.DataSource = dt;
                    medtab.DataSource = bs;
                    sda.Update(dt);


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else {
                GetStocks();
            }
        }
        void SearchPatients() {
            if (!psea.Text.Equals(""))
            {
                //Getting data for table patients
                string conString = "datasource=localhost; port=3306; username=root; password=";
                MySqlConnection conDatabase = new MySqlConnection(conString);
                MySqlCommand cmdDatabase = new MySqlCommand("SELECT * FROM m_inventory.patients WHERE PatientNo LIKE'%"+psea.Text+"%'", conDatabase);

                try
                {
                    MySqlDataAdapter sda = new MySqlDataAdapter();
                    sda.SelectCommand = cmdDatabase;
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    BindingSource bs = new BindingSource();
                    bs.DataSource = dt;
                    patients.DataSource = bs;
                    sda.Update(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else {
                GetPatients();
            }
        }
        void SearchEmployees() {
            if (!esearch.Text.Equals(""))
            {
                //Getting data for table employees
                string conString = "datasource=localhost; port=3306; username=root; password=";
                MySqlConnection conDatabase = new MySqlConnection(conString);
                MySqlCommand cmdDatabase = new MySqlCommand("SELECT * FROM m_inventory.employee WHERE EmployeeID LIKE '%"+esearch.Text+"%'", conDatabase);

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
            }
            else {
                GetEmployees();
            }
        }
        void SearchSales()
        {
            if (!sea.Text.Equals(""))
            {
                //Getting data for table sales
                string conString = "datasource=localhost; port=3306; username=root; password=";
                MySqlConnection conDatabase = new MySqlConnection(conString);
                MySqlCommand cmdDatabase = new MySqlCommand("SELECT * FROM m_inventory.sales WHERE SalesNo LIKE '%" + sea.Text + "%'", conDatabase);

                try
                {
                    MySqlDataAdapter sda = new MySqlDataAdapter();
                    sda.SelectCommand = cmdDatabase;
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    BindingSource bs = new BindingSource();
                    bs.DataSource = dt;
                    sales.DataSource = bs;
                    sda.Update(dt);


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                GetSales();
            }
        }
        void SearchMed() {
            if (!search.Text.Equals(""))
            {
                //Getting data for table medicine
                string conString = "datasource=localhost; port=3306; username=root; password=";
                MySqlConnection conDatabase = new MySqlConnection(conString);
                MySqlCommand cmdDatabase = new MySqlCommand("SELECT * FROM m_inventory.stocks WHERE ItemNo LIKE'%" + search.Text + "%'", conDatabase);

                try
                {
                    MySqlDataAdapter sda = new MySqlDataAdapter();
                    sda.SelectCommand = cmdDatabase;
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    BindingSource bs = new BindingSource();
                    bs.DataSource = dt;
                    list.DataSource = bs;
                    sda.Update(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else {
                //Getting data for table medicine
                string conString = "datasource=localhost; port=3306; username=root; password=";
                MySqlConnection conDatabase = new MySqlConnection(conString);
                MySqlCommand cmdDatabase = new MySqlCommand("SELECT * FROM m_inventory.stocks", conDatabase);

                try
                {
                    MySqlDataAdapter sda = new MySqlDataAdapter();
                    sda.SelectCommand = cmdDatabase;
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    BindingSource bs = new BindingSource();
                    bs.DataSource = dt;
                    list.DataSource = bs;
                    sda.Update(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        void SearchPat() {
            if (!search.Text.Equals("")){
                //Getting data for table patients
                string conString = "datasource=localhost; port=3306; username=root; password=";
                MySqlConnection conDatabase = new MySqlConnection(conString);
                MySqlCommand cmdDatabase = new MySqlCommand("SELECT * FROM m_inventory.patients WHERE PatientNo LIKE'%" + search.Text + "%'", conDatabase);

                try
                {
                    MySqlDataAdapter sda = new MySqlDataAdapter();
                    sda.SelectCommand = cmdDatabase;
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    BindingSource bs = new BindingSource();
                    bs.DataSource = dt;
                    list.DataSource = bs;
                    sda.Update(dt);


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else {
                //Getting data for table patients
                string conString = "datasource=localhost; port=3306; username=root; password=";
                MySqlConnection conDatabase = new MySqlConnection(conString);
                MySqlCommand cmdDatabase = new MySqlCommand("SELECT * FROM m_inventory.patients", conDatabase);

                try
                {
                    MySqlDataAdapter sda = new MySqlDataAdapter();
                    sda.SelectCommand = cmdDatabase;
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    BindingSource bs = new BindingSource();
                    bs.DataSource = dt;
                    list.DataSource = bs;
                    sda.Update(dt);


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        #endregion


        private void button16_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure?", "Deactivating User", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                if (emtab.SelectedRows.Count > 0)
                {
                    int sri = emtab.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = emtab.Rows[sri];
                    string emid = Convert.ToString(selectedRow.Cells["EmployeeID"].Value);

                    string conString = "datasource=localhost; port=3306; username=root; password=";
                    string query = "DELETE FROM m_inventory.employee WHERE EmployeeID = '" + emid + "'";

                    MySqlConnection conDB = new MySqlConnection(conString);
                    MySqlCommand cmd = new MySqlCommand(query, conDB);
                    MySqlDataReader myReader;

                    try
                    {
                        conDB.Open();
                        myReader = cmd.ExecuteReader();
                        MessageBox.Show("Deleted!");
                        eid.Text = "";
                        ide.Text = "";
                        fname.Text = "";
                        uname.Text = "";
                        pass.Text = "";
                        eut.Text = "";
                        eid.Focus();
                        GetEmployees();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("EmployeeID doesn't exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Select Data First!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            stock.Show();
            precord.Hide();
            employee.Hide();
            prescription.Hide();
            table.Hide();
            check.Hide();

            GetStocks();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            stock.Hide();
            precord.Show();
            employee.Hide();
            prescription.Hide();
            table.Hide();
            check.Hide();

            GetPatients();
        }

        private void fadmin_Click(object sender, EventArgs e)
        {
            stock.Hide();
            precord.Hide();
            employee.Show();
            prescription.Hide();
            table.Hide();
            check.Hide();

            GetEmployees();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            stock.Hide();
            precord.Hide();
            employee.Hide();
            prescription.Show();
            table.Hide();
            check.Hide();

            //BLANK
            pin.Text = "";
            pit.Text = "";
            pqty.Text = "";
            punit.Text = "";
            pdos.Text = "";
            ppres.Text = "";
            pex.Text = "";
            pty.Text = "";
            pmed.Text = "";
            ppn.Text = "";
            pna.Text = "";
            padd.Text = "";
            gender.Text = "";
            page.Text = "";
            pdia.Text = "";
            qtyy.Text = "1";
            amount.Text = "0.00";
            total.Text = "0.00";
        }

        private void button18_Click(object sender, EventArgs e)
        {
            table.Hide();
            prescription.Show();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (title.Text.Equals("MEDICINES LIST")) {
                int sri = list.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = list.Rows[sri];

                pin.Text = Convert.ToString(selectedRow.Cells["ItemNo"].Value);
                pit.Text = Convert.ToString(selectedRow.Cells["ItemName"].Value);
                pqty.Text = Convert.ToString(selectedRow.Cells["Quantity"].Value);
                punit.Text = Convert.ToString(selectedRow.Cells["Price"].Value);
                pdos.Text = Convert.ToString(selectedRow.Cells["Dosage"].Value);
                ppres.Text = Convert.ToString(selectedRow.Cells["Prescription"].Value);
                pex.Text = Convert.ToString(selectedRow.Cells["ExpiryDate"].Value);
                pty.Text = Convert.ToString(selectedRow.Cells["Type"].Value);
                pmed.Text = Convert.ToString(selectedRow.Cells["Medication"].Value);

                double a = Double.Parse(punit.Text) * Double.Parse(qtyy.Text);
                total.Text = a.ToString();
            }
            else if (title.Text.Equals("PATIENTS LIST")) {
                int sri = list.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = list.Rows[sri];

                ppn.Text = Convert.ToString(selectedRow.Cells["PatientNo"].Value);
                pna.Text = Convert.ToString(selectedRow.Cells["PatientName"].Value);
                padd.Text = Convert.ToString(selectedRow.Cells["Address"].Value);
                gender.Text = Convert.ToString(selectedRow.Cells["Gender"].Value);
                page.Text = Convert.ToString(selectedRow.Cells["Age"].Value);
                pdia.Text = Convert.ToString(selectedRow.Cells["Diagnosis"].Value);
            }
            table.Hide();
            prescription.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            title.Text = "MEDICINES LIST";
            table.Show();
            prescription.Hide();

            //Getting data for table medicine
            string conString = "datasource=localhost; port=3306; username=root; password=";
            MySqlConnection conDatabase = new MySqlConnection(conString);
            MySqlCommand cmdDatabase = new MySqlCommand("SELECT * FROM m_inventory.stocks", conDatabase);

            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmdDatabase;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                BindingSource bs = new BindingSource();
                bs.DataSource = dt;
                list.DataSource = bs;
                sda.Update(dt);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            title.Text = "PATIENTS LIST";
            table.Show();
            prescription.Hide();

            //Getting data for table patients
            string conString = "datasource=localhost; port=3306; username=root; password=";
            MySqlConnection conDatabase = new MySqlConnection(conString);
            MySqlCommand cmdDatabase = new MySqlCommand("SELECT * FROM m_inventory.patients", conDatabase);

            try
            {
                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmdDatabase;
                DataTable dt = new DataTable();
                sda.Fill(dt);
                BindingSource bs = new BindingSource();
                bs.DataSource = dt;
                list.DataSource = bs;
                sda.Update(dt);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login log = new Login();
            log.Visible = true;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            //ADDING DATA TO EMPLOYEES
            string conString = "datasource=localhost; port=3306; username=root; password=";
            string query = "INSERT INTO m_inventory.employee(`EmployeeID`, `Designation`, `Fullname`, `Username`, `Password`, `UserType`) VALUES ('','"+ide.Text+"','"+fname.Text+"'," +
                "'"+uname.Text+"','"+pass.Text+"','"+eut.Text+"')";

            MySqlConnection conDB = new MySqlConnection(conString);
            MySqlCommand cmd = new MySqlCommand(query, conDB);
            MySqlDataReader myReader;

            try
            {
                conDB.Open();
                myReader = cmd.ExecuteReader();
                MessageBox.Show("Added!");
                eid.Text = ""; 
                ide.Text = "";
                fname.Text = "";
                uname.Text = "";
                pass.Text = "";
                eut.Text = "";
                ide.Focus();
                GetEmployees();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (!eid.Text.Equals(""))
            {
                DialogResult dr = MessageBox.Show("Are you sure?", "Updating User", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {

                    string conString = "datasource=localhost; port=3306; username=root; password=";
                    string query = "UPDATE m_inventory.employee SET " +
                                                      "`Designation`='" + ide.Text + "'," +
                                                      "`Fullname`='" + fname.Text + "'," +
                                                      "`Username`='" + uname.Text + "'," +
                                                      "`Password`='" + pass.Text + "'," +
                                                      "`UserType`='" + eut.Text + "' WHERE EmployeeID = '" + eid.Text + "'";

                    MySqlConnection conDB = new MySqlConnection(conString);
                    MySqlCommand cmd = new MySqlCommand(query, conDB);
                    MySqlDataReader myReader;

                    try
                    {
                        conDB.Open();
                        myReader = cmd.ExecuteReader();
                        MessageBox.Show("Updated!");
                        eid.Text = "";
                        ide.Text = "";
                        fname.Text = "";
                        uname.Text = "";
                        pass.Text = "";
                        eut.Text = "";
                        ide.Focus();
                        GetEmployees();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("EmployeeID doesn't exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else {
                MessageBox.Show("Select data first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void emtab_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            EmployeeIn();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //ADDING DATA TO PATIENTS
            string conString = "datasource=localhost; port=3306; username=root; password=";
            string query = "INSERT INTO m_inventory.patients(`PatientNo`, `PatientName`, `Address`, `Gender`, `Age`, `Diagnosis`) " +
                "VALUES ('','"+pname.Text+ "','" +add.Text + "','" +gen.Text + "'," + age.Text + ",'" +dia.Text + "')";

            MySqlConnection conDB = new MySqlConnection(conString);
            MySqlCommand cmd = new MySqlCommand(query, conDB);
            MySqlDataReader myReader;

            try
            {
                conDB.Open();
                myReader = cmd.ExecuteReader();
                MessageBox.Show("Patient Record Added!");
                pnum.Text = "";
                pname.Text = "";
                add.Text = "";
                gen.Text = "";
                age.Text = "";
                dia.Text = "";
                pname.Focus();
                GetPatients();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //UPDATE
            if (!pnum.Text.Equals(""))
            {
                DialogResult dr = MessageBox.Show("Are you sure?", "Updating Patient Record", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {

                    string conString = "datasource=localhost; port=3306; username=root; password=";
                    string query = "UPDATE m_inventory.patients SET " +
                                                            "`PatientName`='"+pname.Text+"'," +
                                                            "`Address`='" + add.Text + "'," +
                                                            "`Gender`='" + gen.Text + "'," +
                                                            "`Age`='" + age.Text + "'," +
                                                            "`Diagnosis`='" + dia.Text + "' " +
                                                            "WHERE PatientNo = '"+pnum.Text+"'";

                    MySqlConnection conDB = new MySqlConnection(conString);
                    MySqlCommand cmd = new MySqlCommand(query, conDB);
                    MySqlDataReader myReader;

                    try
                    {
                        conDB.Open();
                        myReader = cmd.ExecuteReader();
                        MessageBox.Show("Patient Record Updated!");
                        pnum.Text = "";
                        pname.Text = "";
                        add.Text = "";
                        gen.Text = "";
                        age.Text = "";
                        dia.Text = "";
                        pname.Focus();
                        GetPatients();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("PatientNo doesn't exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Select data first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //DELETE
            DialogResult dr = MessageBox.Show("Are you sure?", "Deleting Patients Record", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                if (patients.SelectedRows.Count > 0)
                {
                    int sri = patients.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = patients.Rows[sri];
                    string emid = Convert.ToString(selectedRow.Cells["PatientNo"].Value);

                    string conString = "datasource=localhost; port=3306; username=root; password=";
                    string query = "DELETE FROM m_inventory.patients WHERE PatientNo = '" + emid + "'";

                    MySqlConnection conDB = new MySqlConnection(conString);
                    MySqlCommand cmd = new MySqlCommand(query, conDB);
                    MySqlDataReader myReader;

                    try
                    {
                        conDB.Open();
                        myReader = cmd.ExecuteReader();
                        MessageBox.Show("Patient Record Deleted!");
                        pnum.Text = "";
                        pname.Text = "";
                        add.Text = "";
                        gen.Text = "";
                        age.Text = "";
                        dia.Text = "";
                        pname.Focus();
                        GetPatients();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("PatientNo doesn't exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Select Data First!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void patients_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (patients.SelectedRows.Count > 0)
            {
                int sri = patients.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = patients.Rows[sri];

                pnum.Text = Convert.ToString(selectedRow.Cells["PatientNo"].Value);
                pname.Text = Convert.ToString(selectedRow.Cells["PatientName"].Value);
                add.Text = Convert.ToString(selectedRow.Cells["Address"].Value);
                gen.Text = Convert.ToString(selectedRow.Cells["Gender"].Value);
                age.Text = Convert.ToString(selectedRow.Cells["Age"].Value);
                dia.Text = Convert.ToString(selectedRow.Cells["Diagnosis"].Value);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //ADDING DATA TO STOCKS
            string datee = sex.Value.ToString("dd/MM/yyyy");
            string conString = "datasource=localhost; port=3306; username=root; password=";
            string query = "INSERT INTO m_inventory.stocks(`ItemNo`, `ItemName`, `Quantity`, `Price`, `Dosage`, `Prescription`, `ExpiryDate`, `Type`, `Medication`) " +
                "VALUES ('','"+iname.Text+"','"+qty.Text+"','"+prc.Text+"','"+dos.Text+"','"+pre.Text+"'," +
                "'"+datee+"','"+type.Text+"','"+med.Text+"')";

            MySqlConnection conDB = new MySqlConnection(conString);
            MySqlCommand cmd = new MySqlCommand(query, conDB);
            MySqlDataReader myReader;

            try
            {
                conDB.Open();
                myReader = cmd.ExecuteReader();
                MessageBox.Show("Stocks Added!");
                inum.Text = "";
                iname.Text = "";
                qty.Text = "";
                prc.Text = "";
                dos.Text = "";
                pre.Text = "";
                type.Text = "";
                med.Text = "";
                iname.Focus();
                GetStocks();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //UPDATE
            if (!inum.Text.Equals(""))
            {
                DialogResult dr = MessageBox.Show("Are you sure?", "Updating Stocks Record", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    String datee = sex.Value.ToString("dd/MM/yyyy");
                    string conString = "datasource=localhost; port=3306; username=root; password=";
                    string query = "UPDATE m_inventory.stocks SET " +
                                                 "`ItemName`='"+iname.Text+"'," +
                                                 "`Quantity`= " + qty.Text + "," +
                                                 "`Price`= "+prc.Text+"," +
                                                 "`Dosage`='"+dos.Text+"'," +
                                                 "`Prescription`='"+pre.Text+"'," +
                                                 "`ExpiryDate`='"+datee+"'," +
                                                 "`Type`='"+type.Text+"'," +
                                                 "`Medication`='"+med.Text+"' WHERE ItemNo = "+inum.Text+"";

                    MySqlConnection conDB = new MySqlConnection(conString);
                    MySqlCommand cmd = new MySqlCommand(query, conDB);
                    MySqlDataReader myReader;

                    try
                    {
                        conDB.Open();
                        myReader = cmd.ExecuteReader();
                        MessageBox.Show("Stocks Updated!");
                        inum.Text = "";
                        iname.Text = "";
                        qty.Text = "";
                        prc.Text = "";
                        dos.Text = "";
                        pre.Text = "";
                        type.Text = "";
                        med.Text = "";
                        iname.Focus();
                        GetStocks();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ItemNo doesn't exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Select data first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //DELETE
            DialogResult dr = MessageBox.Show("Are you sure?", "Deeleting Stocks Record", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                if (medtab.SelectedRows.Count > 0)
                {
                    int sri = medtab.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = medtab.Rows[sri];
                    string emid = Convert.ToString(selectedRow.Cells["ItemNo"].Value);

                    string conString = "datasource=localhost; port=3306; username=root; password=";
                    string query = "DELETE FROM m_inventory.stocks WHERE ItemNo = '" + emid + "'";

                    MySqlConnection conDB = new MySqlConnection(conString);
                    MySqlCommand cmd = new MySqlCommand(query, conDB);
                    MySqlDataReader myReader;

                    try
                    {
                        conDB.Open();
                        myReader = cmd.ExecuteReader();
                        MessageBox.Show("Stocks Deleted!");
                        inum.Text = "";
                        iname.Text = "";
                        qty.Text = "";
                        prc.Text = "";
                        dos.Text = "";
                        pre.Text = "";
                        type.Text = "";
                        med.Text = "";
                        iname.Focus();
                        GetStocks();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("ItemNo doesn't exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Select Data First!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void medtab_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //CELLSCLICK
            if (medtab.SelectedRows.Count > 0)
            {
                int sri = medtab.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = medtab.Rows[sri];

                string ed = Convert.ToString(selectedRow.Cells["ExpiryDate"].Value); ;
                DateTime fed = Convert.ToDateTime(ed);

                inum.Text = Convert.ToString(selectedRow.Cells["ItemNo"].Value);
                iname.Text = Convert.ToString(selectedRow.Cells["ItemName"].Value);
                qty.Text = Convert.ToString(selectedRow.Cells["Quantity"].Value);
                prc.Text = Convert.ToString(selectedRow.Cells["Price"].Value);
                dos.Text = Convert.ToString(selectedRow.Cells["Dosage"].Value);
                pre.Text = Convert.ToString(selectedRow.Cells["Prescription"].Value);
                sex.Value = fed;
                type.Text = Convert.ToString(selectedRow.Cells["Type"].Value);
                med.Text = Convert.ToString(selectedRow.Cells["Medication"].Value);
            }
        }

        private void qtyy_Leave(object sender, EventArgs e)
        {
            if (qtyy.Text.Equals(""))
            {
                qtyy.Text = "";
            }
            else if (Int32.Parse(pqty.Text) < Int32.Parse(qtyy.Text))
            {
                MessageBox.Show("Insufficient Stocks!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                qtyy.Text = pqty.Text;
            }
            else {
                double x = Double.Parse(qtyy.Text) * Double.Parse(punit.Text);
                total.Text = x.ToString();
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            double am = Double.Parse(amount.Text);
            double tot = Double.Parse(total.Text);
            double allt = am - tot;
            if (amount.Text.Equals(""))
            {
                MessageBox.Show("Input Amount Money!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (pin.Text.Equals("") || ppn.Text.Equals("")) {
                MessageBox.Show("Find Medicine/Patient First!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (tot > am)
            {
                MessageBox.Show("Insufficient Money!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (tot <= am)
            {
                DialogResult dr = MessageBox.Show("Patient Name: " + pna.Text + "\nDiagnosis: " + pdia.Text + "\n" +
                    "Item Name: " + pit.Text + "\nPrice: Php " + punit.Text + "\nQuantity: " + qtyy.Text + "\n" +
                    "Amount: Php " + amount.Text + "\nChange: Php " + allt.ToString() + "" +
                    "\nTotal: Php " + total.Text + "\n\n\nConfirm?", "Receipt", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (dr == DialogResult.Yes)
                {
                    double x = Double.Parse(amount.Text) - Double.Parse(total.Text);
                    string datee = DateTime.Now.ToString("dd/MM/yyyy");
                    string conString = "datasource=localhost; port=3306; username=root; password=";
                    string query = "INSERT INTO m_inventory.sales(`SalesNo`, `ItemNo`, `PatientNo`, `Quantity`, `Amount`, `Changee`, `Total`, `Employee`, `Date`) " +
                        "VALUES ('', "+pin.Text+",'"+ppn.Text+"',"+qtyy.Text+","+amount.Text+","+x+",'"+total.Text+"','"+name.Text+"','"+datee+"')";

                    MySqlConnection conDB = new MySqlConnection(conString);
                    MySqlCommand cmd = new MySqlCommand(query, conDB);
                    MySqlDataReader myReader;

                    try
                    {
                        conDB.Open();
                        myReader = cmd.ExecuteReader();
                        GetSales();
                        conDB.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    int s = Int32.Parse(pqty.Text) - Int32.Parse(qtyy.Text);
                    string conStrings = "datasource=localhost; port=3306; username=root; password=";
                    string querys = "UPDATE m_inventory.stocks SET Quantity = " + s + " WHERE ItemNo = " + pin.Text + "";

                    MySqlConnection conDBs = new MySqlConnection(conStrings);
                    MySqlCommand cmds = new MySqlCommand(querys, conDBs);
                    MySqlDataReader myReaders;

                    try
                    {
                        conDBs.Open();
                        myReaders = cmds.ExecuteReader();
                        MessageBox.Show("Record Saved!");
                        //BLANK
                        pin.Text = "";
                        pit.Text = "";
                        pqty.Text = "";
                        punit.Text = "";
                        pdos.Text = "";
                        ppres.Text = "";
                        pex.Text = "";
                        pty.Text = "";
                        pmed.Text = "";
                        ppn.Text = "";
                        pna.Text = "";
                        padd.Text = "";
                        gender.Text = "";
                        page.Text = "";
                        pdia.Text = "";
                        qtyy.Text = "1";
                        amount.Text = "0.00";
                        total.Text = "0.00";

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            check.Hide();
            prescription.Show();
        }

        private void label46_Click(object sender, EventArgs e)
        {
            check.Show();
            prescription.Hide();
            GetSales();
        }

        private void qtyy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (qtyy.Text.Equals(""))
            {
                qtyy.Text = "";
            }
            else if (Int32.Parse(pqty.Text) < Int32.Parse(qtyy.Text)) {
                MessageBox.Show("Insufficient Stocks!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                qtyy.Text = pqty.Text;
            }
            else
            {
                Double x = Double.Parse(qtyy.Text) * Double.Parse(punit.Text);
                total.Text = x.ToString();
            }
        }

        private void amount_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (amount.Text.Equals(""))
            {
                amount.Text = "";
            }
        }

        private void ssearch_TextChanged(object sender, EventArgs e)
        {
            SearchStocks();
        }

        private void psea_TextChanged(object sender, EventArgs e)
        {
            SearchPatients();
        }

        private void sea_TextChanged(object sender, EventArgs e)
        {
            SearchSales();
        }

        private void esearch_TextChanged(object sender, EventArgs e)
        {
            SearchEmployees();
        }

        private void search_TextChanged(object sender, EventArgs e)
        {
            if (title.Text.Equals("MEDICINES LIST"))
            {
                SearchMed();
            }
            else if (title.Text.Equals("PATIENTS LIST")) {
                SearchPat();
            }
        }
    }
}
