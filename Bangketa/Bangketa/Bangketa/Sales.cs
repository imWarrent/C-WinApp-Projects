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
    public partial class Sales : Form
    {
        String dates = DateTime.Now.ToString("MMMM dd, yyyy");
        String month = DateTime.Now.ToString("MMMM");
        String year = DateTime.Now.ToString("yyyy");

        //db part
        MySqlDataAdapter sda = new MySqlDataAdapter();
        BindingSource bind = new BindingSource();
        String db = "datasource=localhost; port=3306; username=root; password=";
        public Sales()
        {
            InitializeComponent();
            POS.Visible = true; cart.Visible = false; inventory.Visible = false; countp.Visible = false; salesp.Visible = false;
        }

        #region database  SCRUD functionality
        //S-earching Data
        void SearchProduct()
        {
            if (!psearch.Text.Equals(""))
            {
                MySqlConnection DBconnector = new MySqlConnection(db);
                MySqlCommand DBcomms = new MySqlCommand("SELECT `prod_id` as 'Product ID', `product_name` as 'Product Name', `category` as 'Category', `brand` as 'Brand Name', `quantity` as 'Stocks', `price` as 'Price' FROM bangketa.product WHERE quantity > 0 and (product_name LIKE '%" + psearch.Text + "%' and business_user = '" + user.Text + "')", DBconnector);

                try
                {
                    DataTable dt = new DataTable();
                    sda.SelectCommand = DBcomms;
                    sda.Fill(dt);

                    bind.DataSource = dt;
                    post.DataSource = bind;
                    sda.Update(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else {
                ReadProduct();
            }
        }
        void SearchInventory() {

            if (invs.Text.Equals(""))
            {
                ReadInventory();
            }
            else
            {
                MySqlConnection DBconnector = new MySqlConnection(db);
                MySqlCommand DBcomms = new MySqlCommand("SELECT `prod_id` as 'Product ID', `product_name` as 'Product Name', `category` as 'Category', `brand` as 'Brand Name', `quantity` as 'Stocks', `price` as 'Price' FROM bangketa.product WHERE (product_name LIKE '%" + invs.Text + "%' or prod_id LIKE '%" + invs.Text + "%') and business_user = '" + user.Text + "'", DBconnector);

                try
                {
                    DataTable dt = new DataTable();
                    sda.SelectCommand = DBcomms;
                    sda.Fill(dt);

                    bind.DataSource = dt;
                    dinv.DataSource = bind;
                    sda.Update(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        void SearchStocks() {
            if (title.Text.Equals("INVENTORY SYSTEM (CRITICAL STOCKS)"))
            {
                if (invs.Text.Equals(""))
                {
                    StocksData();
                }
                else
                {
                    MySqlConnection DBconnector = new MySqlConnection(db);
                    MySqlCommand DBcomms = new MySqlCommand("SELECT `prod_id` as 'Product ID', `product_name` as 'Product Name', `category` as 'Category', `brand` as 'Brand Name', `quantity` as 'Stocks', `price` as 'Price' FROM bangketa.product WHERE ((quantity >= 1 and quantity <= 10) and (product_name LIKE '%" + invs.Text + "%' or prod_id LIKE '%" + invs.Text + "%')) and business_user = '" + user.Text + "'", DBconnector);

                    try
                    {
                        DataTable dt = new DataTable();
                        sda.SelectCommand = DBcomms;
                        sda.Fill(dt);

                        bind.DataSource = dt;
                        dinv.DataSource = bind;
                        sda.Update(dt);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else if (title.Text.Equals("INVENTORY SYSTEM (OUT OF STOCKS)"))
            {
                if (invs.Text.Equals(""))
                {
                    StocksData();
                }
                else
                {
                    MySqlConnection DBconnector = new MySqlConnection(db);
                    MySqlCommand DBcomms = new MySqlCommand("SELECT `prod_id` as 'Product ID', `product_name` as 'Product Name', `category` as 'Category', `brand` as 'Brand Name', `quantity` as 'Stocks', `price` as 'Price' FROM bangketa.product WHERE quantity = 0 and (product_name LIKE '%" + invs.Text + "%' or prod_id LIKE '%" + invs.Text + "%') and business_user = '" + user.Text + "'", DBconnector);

                    try
                    {
                        DataTable dt = new DataTable();
                        sda.SelectCommand = DBcomms;
                        sda.Fill(dt);

                        bind.DataSource = dt;
                        dinv.DataSource = bind;
                        sda.Update(dt);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        //C-reating Data
        void CheckOut()
        {
            for (int x = 0; x < pcart.RowCount; x++)
            {
                DataGridViewRow selectedRow = pcart.Rows[x];
                string a = Convert.ToString(selectedRow.Cells["prod_id"].Value);
                string b = Convert.ToString(selectedRow.Cells["prod_name"].Value);
                int c = Convert.ToInt32(selectedRow.Cells["qty"].Value);
                double d = Convert.ToDouble(selectedRow.Cells["price"].Value);

                string query = "INSERT INTO bangketa.sales (`sales_id`, `product_id`, `business_user`, `date`, `month`,`year`, `quantity`, `total`) VALUES ('','" + a + "', '" + user.Text + "', '" + dates + "', '" + month + "', '" + year + "', " + c + "," + d + ")";
                MySqlConnection DBconnector = new MySqlConnection(db);
                MySqlCommand cmd = new MySqlCommand(query, DBconnector);
                MySqlDataReader myReader;

                try
                {
                    DBconnector.Open();
                    myReader = cmd.ExecuteReader();

                    //UPDATING STOCKS
                    DBStocks(x,a);
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            MessageBox.Show("Checked Out!");
            ReadProduct();

            p_id.Text = "Product ID"; pname.Text = "Product Name";
            pbrand.Text = "Product Brand"; pcat.Text = "Category";
            pprice.Text = "1000.00"; stck.Text = "0";
        }
        void AddProducts() {
            int st = Convert.ToInt32(iqty.Text);
            double pr = Convert.ToDouble(ip.Text);
            string query = "INSERT INTO bangketa.product(`prod_id`, `business_user`, `product_name`, `category`, `brand`, `quantity`, `price`) " +
                "VALUES ('','"+user.Text+"','"+pn.Text+"','"+cat.Text+"','"+bn.Text+"',"+st+","+pr+")";
            MySqlConnection DBconnector = new MySqlConnection(db);
            MySqlCommand cmd = new MySqlCommand(query, DBconnector);
            MySqlDataReader myReader;

            try
            {
                DBconnector.Open();
                myReader = cmd.ExecuteReader();
                MessageBox.Show("Product Succesfully Added!");
                ReadInventory();

                pn.Text = "";
                bn.Text = "";
                cat.Text = "";
                iqty.Text = "";
                ip.Text = "";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //R-eading Data
        void ReadProduct() {
            MySqlConnection DBconnector = new MySqlConnection(db);
            MySqlCommand DBcomms = new MySqlCommand("SELECT `prod_id` as 'Product ID', `product_name` as 'Product Name', `category` as 'Category', `brand` as 'Brand Name', `quantity` as 'Stocks', `price` as 'Price' FROM bangketa.product WHERE quantity > 0 and business_user = '" + user.Text + "'", DBconnector);

            try
            {
                DataTable dt = new DataTable();
                sda.SelectCommand = DBcomms;
                sda.Fill(dt);

                bind.DataSource = dt;
                post.DataSource = bind;
                sda.Update(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void ReadInventory()
        {
            MySqlConnection DBconnector = new MySqlConnection(db);
            MySqlCommand DBcomms = new MySqlCommand("SELECT `prod_id` as 'Product ID', `product_name` as 'Product Name', `category` as 'Category', `brand` as 'Brand Name', `quantity` as 'Stocks', `price` as 'Price' FROM bangketa.product WHERE business_user = '" + user.Text + "'", DBconnector);

            try
            {
                DataTable dt = new DataTable();
                sda.SelectCommand = DBcomms;
                sda.Fill(dt);

                bind.DataSource = dt;
                dinv.DataSource = bind;
                sda.Update(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void ReadSales() {
            MySqlConnection DBconnector = new MySqlConnection(db);
            MySqlCommand DBcomms = new MySqlCommand("SELECT `sales_id` as 'Sales ID', `product_id` as 'Product ID', `date` as 'Date', `quantity` as 'Quantity', `total` as 'Total' FROM bangketa.sales WHERE business_user = '"+user.Text+"'", DBconnector);

            try
            {
                DataTable dt = new DataTable();
                sda.SelectCommand = DBcomms;
                sda.Fill(dt);

                bind.DataSource = dt;
                salestb.DataSource = bind;
                sda.Update(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void StocksData()
        {
            if (title.Text.Equals("INVENTORY SYSTEM (CRITICAL STOCKS)"))
            {
                MySqlConnection DBconnector = new MySqlConnection(db);
                MySqlCommand DBcomms = new MySqlCommand("SELECT `prod_id` as 'Product ID', `product_name` as 'Product Name', `category` as 'Category', `brand` as 'Brand Name', `quantity` as 'Stocks', `price` as 'Price' FROM bangketa.product WHERE (quantity >= 1 and quantity <= 10) and business_user = '" + user.Text + "'", DBconnector);

                try
                {
                    DataTable dt = new DataTable();
                    sda.SelectCommand = DBcomms;
                    sda.Fill(dt);

                    bind.DataSource = dt;
                    dinv.DataSource = bind;
                    sda.Update(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (title.Text.Equals("INVENTORY SYSTEM (OUT OF STOCKS)")) {
                MySqlConnection DBconnector = new MySqlConnection(db);
                MySqlCommand DBcomms = new MySqlCommand("SELECT `prod_id` as 'Product ID', `product_name` as 'Product Name', `category` as 'Category', `brand` as 'Brand Name', `quantity` as 'Stocks', `price` as 'Price' FROM bangketa.product WHERE quantity = 0 and business_user = '" + user.Text + "'", DBconnector);

                try
                {
                    DataTable dt = new DataTable();
                    sda.SelectCommand = DBcomms;
                    sda.Fill(dt);

                    bind.DataSource = dt;
                    dinv.DataSource = bind;
                    sda.Update(dt);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        void CriticalData()
        {
            //CRITICAL DATA
            MySqlConnection DBconnector = new MySqlConnection(db);
            MySqlCommand DBcomms = new MySqlCommand("SELECT count(prod_id) as 'total' FROM bangketa.product " +
                "WHERE (quantity >= 1 and quantity <= 10) and business_user = '" + user.Text + "'", DBconnector);
            MySqlDataReader myReader;

            try
            {
                DBconnector.Open();
                myReader = DBcomms.ExecuteReader();
                if (myReader.Read())
                {
                   crit.Text = myReader["total"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void OutStockData()
        {
            //OUT OF STOCK DATA
            MySqlConnection DBconnector = new MySqlConnection(db);
            MySqlCommand DBcomms = new MySqlCommand("SELECT count(prod_id) as 'total' FROM bangketa.product " +
                "WHERE quantity = 0 and business_user = '" + user.Text + "'", DBconnector);
            MySqlDataReader myReader;

            try
            {
                DBconnector.Open();
                myReader = DBcomms.ExecuteReader();
                if (myReader.Read())
                {
                    outs.Text = myReader["total"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void TotalSalesData() {
            MySqlConnection DBconnector = new MySqlConnection(db);
            MySqlCommand DBcomms = new MySqlCommand("SELECT sum(total) as 'totals' FROM bangketa.sales WHERE business_user = '" + user.Text + "'", DBconnector);
            MySqlDataReader myReader;

            try
            {
                DBconnector.Open();
                myReader = DBcomms.ExecuteReader();
                if (myReader.Read())
                {
                    total.Text = "$ " + myReader["totals"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void TotalSumData(string datee, int y) {
            MySqlConnection DBconnector = new MySqlConnection(db);
            MySqlCommand DBcomms = new MySqlCommand("SELECT sum(total) as 'totals' FROM bangketa.sales WHERE (date = '"+datee+"' OR month = '"+datee+"' OR year = '"+datee+"') and business_user = '" + user.Text + "'", DBconnector);
            MySqlDataReader myReader;

            try
            {
                DBconnector.Open();
                myReader = DBcomms.ExecuteReader();
                if (myReader.Read())
                {
                    if (y == 0)
                    {
                        tst.Text = "$ " + myReader["totals"].ToString();
                    }
                    else if (y == 1)
                    {
                        tstm.Text = "$ " + myReader["totals"].ToString();
                    }
                    else if (y == 2)
                    {
                        tsty.Text = "$ " + myReader["totals"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //U-pdate Data
        void UpdateProduct() {
            DialogResult dr = MessageBox.Show("Do you really want to update the Product Data?", "Updating Product Data", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                int st = Convert.ToInt32(iqty.Text);
                double pr = Convert.ToDouble(ip.Text);
                int pid = Convert.ToInt32(p_id.Text);
                string query = "UPDATE bangketa.product SET " +
                    "`business_user`='"+user.Text+"'," +
                    "`product_name`='"+pn.Text+"',`category`='"+cat.Text+"'," +
                    "`brand`='"+bn.Text+"',`quantity`="+st+"," +
                    "`price`="+pr+" WHERE prod_id = '"+pid+"'";

                MySqlConnection conDB = new MySqlConnection(db);
                MySqlCommand cmd = new MySqlCommand(query, conDB);
                MySqlDataReader myReader;

                try
                {
                    conDB.Open();
                    myReader = cmd.ExecuteReader();
                    MessageBox.Show("Product Updated!");
                    ReadInventory();

                    pn.Text = "";
                    bn.Text = "";
                    cat.Text = "";
                    iqty.Text = "";
                    ip.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        void DBStocks(int y, string z) {
            DataGridViewRow selectedRow = pcart.Rows[y];
            int c = Convert.ToInt32(selectedRow.Cells["avstck"].Value);

            string query = "UPDATE bangketa.product SET quantity = '"+c+"' WHERE prod_id = '"+z+"'";

            MySqlConnection conDB = new MySqlConnection(db);
            MySqlCommand cmd = new MySqlCommand(query, conDB);
            MySqlDataReader myReader;

            try
            {
                conDB.Open();
                myReader = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //D-elete Data
        void DeleteProduct() {
            DialogResult dr = MessageBox.Show("Do you really want to update the Product Data?", "Updating Product Data", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                int pid = Convert.ToInt32(p_id.Text);
                string conString = "datasource=localhost; port=3306; username=root; password=";
                string query = "DELETE FROM bangketa.product WHERE prod_id = "+pid+"";

                MySqlConnection conDB = new MySqlConnection(conString);
                MySqlCommand cmd = new MySqlCommand(query, conDB);
                MySqlDataReader myReader;

                try
                {
                    conDB.Open();
                    myReader = cmd.ExecuteReader();
                    MessageBox.Show("Product Deleted!");
                    ReadInventory();

                    pn.Text = "";
                    bn.Text = "";
                    cat.Text = "";
                    iqty.Text = "";
                    ip.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        #endregion

        private void label20_Click(object sender, EventArgs e)
        {
            Close();
        }


        #region POS
        private void post_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (post.SelectedRows.Count > 0)
            {
                int row = post.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = post.Rows[row];

                p_id.Text = Convert.ToString(selectedRow.Cells["Product ID"].Value);
                pname.Text = Convert.ToString(selectedRow.Cells["Product Name"].Value);
                pbrand.Text = Convert.ToString(selectedRow.Cells["Brand Name"].Value);
                pcat.Text = Convert.ToString(selectedRow.Cells["Category"].Value);
                stck.Text = Convert.ToString(selectedRow.Cells["Stocks"].Value);
                pprice.Text = Convert.ToString(selectedRow.Cells["Price"].Value);
            }
        }

        private void panel6_Click(object sender, EventArgs e)
        {
            int x = Convert.ToInt32(stck.Text);
            int y = Convert.ToInt32(pqty.Text);
            if (p_id.Text.Equals("Product ID")) {
                MessageBox.Show("Select a product first.");
            }
            else if (x < y) {
                MessageBox.Show("Not enough stocks.");
            }
            else {
                if (pqty.Text.Equals("0") || pqty.Text.Equals(""))
                {
                    pqty.Text = "1";
                }

                double ptott = Double.Parse(pqty.Text) * Double.Parse(pprice.Text);
                double z = double.Parse(ptotal.Text) + ptott;
                int zx = x - y;

                ptotal.Text = z.ToString();

                pcart.Rows.Add(p_id.Text, pname.Text, pqty.Text, ptott.ToString(), zx.ToString());
                MessageBox.Show("Added to cart!");
                p_id.Text = "Product ID"; pname.Text = "Product Name";
                pbrand.Text = "Product Brand"; pcat.Text = "Category"; stck.Text = "0";
                pprice.Text = "1000.00";
            }
        }
        private void panel8_Click(object sender, EventArgs e)
        {
            if (pcart.RowCount == 0) {
                MessageBox.Show("Add to your cart first.");
            }
            else
            {
                POS.Visible = false; cart.Visible = true;
                title.Text = "YOUR CART LIST";
            }
        }

        private void panel14_Click(object sender, EventArgs e)
        {
            POS.Visible = true; cart.Visible = false;
            title.Text = "POINT OF SALES";
        }

        private void panel16_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure?", "Cancelling Order", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                pcart.Rows.Clear();
                MessageBox.Show("Order Cancelled....");
                POS.Visible = true; cart.Visible = false;
                title.Text = "POINT OF SALES";
            }
        }

        private void panel15_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure?", "Check Out", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                if (camount.Text.Equals(""))
                {
                    MessageBox.Show("Don't leave area textfield.");
                }
                else
                {
                    double x = Double.Parse(camount.Text);
                    double z = Double.Parse(ptotal.Text);
                    
                    if (x < z)
                    {
                        MessageBox.Show("Not Enough Money.");
                    }
                    else
                    {
                        CheckOut();
                        pcart.Rows.Clear();
                        POS.Visible = true; cart.Visible = false;
                        title.Text = "POINT OF SALES";
                    }
                }
            }
        }

        private void pqty_TextChanged(object sender, EventArgs e)
        {
            double ptott = Double.Parse(pqty.Text) * Double.Parse(pprice.Text);
            ptot.Text = ptott.ToString();
        }
        private void psearch_TextChanged(object sender, EventArgs e)
        {
            SearchProduct();
        }
        private void Sales_Shown(object sender, EventArgs e)
        {
            ReadProduct();
        }

        private void camount_TextChanged(object sender, EventArgs e)
        {
            if (!camount.Text.Equals(""))
            {
                double x = Double.Parse(camount.Text) - Double.Parse(ptotal.Text);
                pamount.Text = camount.Text;
                pchange.Text = x.ToString();
            }

        }
        #endregion

        #region colors
        private void p1_MouseEnter(object sender, EventArgs e)
        {
            p1.BackColor = ColorTranslator.FromHtml("#540707");
        }

        private void p1_MouseLeave(object sender, EventArgs e)
        {
            p1.BackColor = ColorTranslator.FromHtml("#800a0a");
        }

        private void p2_MouseEnter(object sender, EventArgs e)
        {
            p2.BackColor = ColorTranslator.FromHtml("#540707");
        }

        private void p2_MouseLeave(object sender, EventArgs e)
        {
            p2.BackColor = ColorTranslator.FromHtml("#800a0a");
        }

        private void p3_MouseEnter(object sender, EventArgs e)
        {
            p3.BackColor = ColorTranslator.FromHtml("#540707");
        }

        private void p3_MouseLeave(object sender, EventArgs e)
        {
            p3.BackColor = ColorTranslator.FromHtml("#800a0a");
        }

        private void panel7_MouseEnter(object sender, EventArgs e)
        {
            panel7.BackColor = ColorTranslator.FromHtml("#540707");
        }

        private void panel7_MouseLeave(object sender, EventArgs e)
        {
            panel7.BackColor = ColorTranslator.FromHtml("#800a0a");
        }

        private void panel24_MouseEnter(object sender, EventArgs e)
        {
            panel24.BackColor = ColorTranslator.FromHtml("#540707");
        }

        private void panel25_MouseEnter(object sender, EventArgs e)
        {
            panel25.BackColor = ColorTranslator.FromHtml("#540707");
        }

        private void panel26_MouseEnter(object sender, EventArgs e)
        {
            panel26.BackColor = ColorTranslator.FromHtml("#540707");
        }

        private void panel24_MouseLeave(object sender, EventArgs e)
        {
            panel24.BackColor = ColorTranslator.FromHtml("#800a0a");
        }

        private void panel25_MouseLeave(object sender, EventArgs e)
        {
            panel25.BackColor = ColorTranslator.FromHtml("#800a0a");
        }

        private void panel26_MouseLeave(object sender, EventArgs e)
        {
            panel26.BackColor = ColorTranslator.FromHtml("#800a0a");
        }
        #endregion

        #region panel button
        private void p1_Click(object sender, EventArgs e)
        {
            POS.Visible = true; cart.Visible = false; inventory.Visible = false; countp.Visible = false; salesp.Visible = false;
            title.Text = "POINT OF SALES";
            ReadProduct();
        }

        private void p2_Click(object sender, EventArgs e)
        {
            POS.Visible = false; cart.Visible = false; inventory.Visible = true; countp.Visible = false; salesp.Visible = false;
            title.Text = "INVENTORY SYSTEM";
            ReadInventory();
        }

        private void p3_MouseClick(object sender, MouseEventArgs e)
        {
            CriticalData(); OutStockData(); TotalSalesData();
            POS.Visible = false; cart.Visible = false; inventory.Visible = false; countp.Visible = true; salesp.Visible = false;
            title.Text = "MONITORING SYSTEM";
        }

        private void panel7_Click(object sender, EventArgs e)
        {
            Login log = new Login();
            log.Show();
            Hide();
        }

        private void panel24_Click(object sender, EventArgs e)
        {
            POS.Visible = false; cart.Visible = false; inventory.Visible = false; countp.Visible = false; salesp.Visible = true;
            title.Text = "SALES MONITORING SYSTEM";
            bus.Text = user.Text;
            ReadSales();

            for (int x = 0; x < 3; x++)
            {
                if (x == 0)
                {
                    TotalSumData(dates, x);
                }
                else if (x == 1)
                {
                    TotalSumData(month, x);
                }
                else if (x == 2) {
                    TotalSumData(year, x);
                }
            }
        }

        private void panel25_Click(object sender, EventArgs e)
        {
            POS.Visible = false; cart.Visible = false; inventory.Visible = true; countp.Visible = false; salesp.Visible = false;
            title.Text = "INVENTORY SYSTEM (CRITICAL STOCKS)";
            StocksData();
        }

        private void panel26_Click(object sender, EventArgs e)
        {
            POS.Visible = false; cart.Visible = false; inventory.Visible = true; countp.Visible = false; salesp.Visible = false;
            title.Text = "INVENTORY SYSTEM (OUT OF STOCKS)";
            StocksData();
        }
        #endregion

        #region Inventory System
        private void dinv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dinv.SelectedRows.Count > 0)
            {

                int row = post.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = post.Rows[row];
                int x = Convert.ToInt32(selectedRow.Cells["Stocks"].Value);

                p_id.Text = Convert.ToString(selectedRow.Cells["Product ID"].Value);
                pn.Text = Convert.ToString(selectedRow.Cells["Product Name"].Value);
                bn.Text = Convert.ToString(selectedRow.Cells["Brand Name"].Value);
                cat.Text = Convert.ToString(selectedRow.Cells["Category"].Value);
                iqty.Text = Convert.ToString(selectedRow.Cells["Stocks"].Value);
                ip.Text = Convert.ToString(selectedRow.Cells["Price"].Value);

                if (x == 0)
                {
                    iqty.BackColor = ColorTranslator.FromHtml("#e01b1b");
                    iqty.ForeColor = System.Drawing.Color.White;
                }
                else if (x <= 10)
                {
                    iqty.BackColor = ColorTranslator.FromHtml("#e0631b");
                    iqty.ForeColor = System.Drawing.Color.White;
                }
                else {
                    iqty.BackColor = System.Drawing.Color.White;
                    iqty.ForeColor = System.Drawing.Color.Black;
                }
            }
        }

        private void invs_TextChanged(object sender, EventArgs e)
        {
            if (title.Text.Equals("INVENTORY SYSTEM"))
            {
                SearchInventory();
            }
            else {
                SearchStocks();
            }
        }

        private void panel13_Click(object sender, EventArgs e)
        {
            //add
            if (pn.Text.Equals("") || bn.Text.Equals("") || cat.Text.Equals("") || iqty.Text.Equals("") || ip.Text.Equals(""))
            {
                MessageBox.Show("There's an empty field");
            }
            else {
                AddProducts();
            }
        }

        private void panel17_Click(object sender, EventArgs e)
        {
            //up
            if (pn.Text.Equals("") || bn.Text.Equals("") || cat.Text.Equals("") || iqty.Text.Equals("") || ip.Text.Equals(""))
            {
                MessageBox.Show("There's an empty field");
            }
            else
            {
                UpdateProduct();
            }
        }

        private void panel12_Click(object sender, EventArgs e)
        {
            //del
            if (pn.Text.Equals("") || bn.Text.Equals("") || cat.Text.Equals("") || iqty.Text.Equals("") || ip.Text.Equals(""))
            {
                MessageBox.Show("There's an empty field");
            }
            else
            {
                DeleteProduct();
            }
        }


        #endregion

        private void panel23_Click(object sender, EventArgs e)
        {
            salesp.Visible =false; countp.Visible = true;
        }
    }
}
