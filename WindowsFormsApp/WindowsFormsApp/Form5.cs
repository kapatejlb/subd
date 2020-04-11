using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();

            Form4 f4 = new Form4();
            f4.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {
            string CollegeName = textBox4.Text;
            string CollegeAbr = textBox5.Text;

            string MySqlConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=11111;database=mydb";
            MySqlConnection databaseConnection = new MySqlConnection(MySqlConnectionString);

            if (CollegeName == "" || CollegeAbr == "")
            {
                MessageBox.Show("Whong Input!!! You should fill all fields");
                return;
            }

            databaseConnection.Open();

            MySqlCommand command = new MySqlCommand(" call proc_addCollege(@cn, @ca)"
                               , databaseConnection);

            command.Parameters.Add("@cn", MySqlDbType.VarChar).Value = CollegeName;
            command.Parameters.Add("@ca", MySqlDbType.VarChar).Value = CollegeAbr;

            command.ExecuteNonQuery();
            RefreshCollegeTable();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            string CookType = textBox1.Text;

            string MySqlConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=11111;database=mydb";
            MySqlConnection databaseConnection = new MySqlConnection(MySqlConnectionString);

            if (CookType == "")
            {
                MessageBox.Show("Whong Input!!! You should fill all fields");
                return;
            }

            databaseConnection.Open();

            MySqlCommand command = new MySqlCommand(" call proc_addCookType(@ct)"
                               , databaseConnection);

            command.Parameters.Add("@ct", MySqlDbType.VarChar).Value = CookType;

            command.ExecuteNonQuery();
            RefreshCookTable();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            RefreshCollegeTable();
        }
        private void RefreshCollegeTable()
        {
            string MySqlConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=11111;database=mydb";


            MySqlConnection databaseConnection = new MySqlConnection(MySqlConnectionString);
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(
                "call proc_refreshTableColl();",
                databaseConnection
                );

            adapter.SelectCommand = command;

            DataTable table = new DataTable();
            adapter.Fill(table);

            dataGridView3.Rows.Clear();

            foreach (var row in table.AsEnumerable())
            {
                dataGridView3.Rows.Add(
                new object[]
                {
                    row.ItemArray[0],

                    row.ItemArray[1],
                    row.ItemArray[2]
                });
            }

        }
        private void RefreshCookTable()
        {
            string MySqlConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=11111;database=mydb";


            MySqlConnection databaseConnection = new MySqlConnection(MySqlConnectionString);
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(
                "call proc_refreshTableCook();",
                databaseConnection
                );

            adapter.SelectCommand = command;

            DataTable table = new DataTable();
            adapter.Fill(table);

            dataGridView2.Rows.Clear();

            foreach (var row in table.AsEnumerable())
            {
                dataGridView2.Rows.Add(
                new object[]
                {
                    row.ItemArray[0],

                    row.ItemArray[1]
                });
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            RefreshCookTable();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            string MySqlConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=11111;database=mydb";

            MySqlConnection databaseConnection = new MySqlConnection(MySqlConnectionString);
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("call mydb.proc_search_cooktype(@ct) ", databaseConnection);

            command.Parameters.Add("@ct", MySqlDbType.VarChar).Value = textBox4.Text;


            adapter.SelectCommand = command;
            DataTable table11 = new DataTable();
            adapter.Fill(table11);

            dataGridView2.Rows.Clear();

            foreach (var row in table11.AsEnumerable())
            {
                dataGridView2.Rows.Add(
                new object[]
                {
                    row.ItemArray[0],

                    row.ItemArray[1],
                    row.ItemArray[2]
                });
            }

        }

        private void button12_Click(object sender, EventArgs e)
        {
            string MySqlConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=11111;database=mydb";

            MySqlConnection databaseConnection = new MySqlConnection(MySqlConnectionString);
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("call mydb.proc_search_college(@cn, @abr) ", databaseConnection);

            command.Parameters.Add("@cn", MySqlDbType.VarChar).Value = textBox4.Text;
            command.Parameters.Add("@abr", MySqlDbType.VarChar).Value = textBox5.Text;


            adapter.SelectCommand = command;
            DataTable table11 = new DataTable();
            adapter.Fill(table11);

            dataGridView3.Rows.Clear();

            foreach (var row in table11.AsEnumerable())
            {
                dataGridView3.Rows.Add(
                new object[]
                {
                    row.ItemArray[0],

                    row.ItemArray[1],
                    row.ItemArray[2]
                });
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            var current = dataGridView2.CurrentRow.Cells;

            int ctid = Convert.ToInt32(current[0].Value);
            string cookType = current[1].Value.ToString();

            string MySqlConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=11111;database=mydb";
            MySqlConnection databaseConnection = new MySqlConnection(MySqlConnectionString);
            databaseConnection.Open();

            MySqlCommand command20 = new MySqlCommand(" call proc_updateCookType(@id, @ct); "
            , databaseConnection);

            command20.Parameters.Add("@id", MySqlDbType.Int32).Value = ctid;
            command20.Parameters.Add("@ct", MySqlDbType.VarChar).Value = cookType;

            command20.ExecuteNonQuery();

            RefreshCookTable();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            var current = dataGridView3.CurrentRow.Cells;

            int colid = Convert.ToInt32(current[0].Value);
            string colName = current[1].Value.ToString();
            string colAbr = current[2].Value.ToString();


            string MySqlConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=11111;database=mydb";
            MySqlConnection databaseConnection = new MySqlConnection(MySqlConnectionString);
            databaseConnection.Open();

            MySqlCommand command20 = new MySqlCommand(" call proc_updateCollege(@id, @cn, @ca); "
            , databaseConnection);

            command20.Parameters.Add("@id", MySqlDbType.Int32).Value = colid;
            command20.Parameters.Add("@cn", MySqlDbType.VarChar).Value = colName;
            command20.Parameters.Add("@ca", MySqlDbType.VarChar).Value = colAbr;

            command20.ExecuteNonQuery();

            RefreshCollegeTable();

        }

        private void button9_Click(object sender, EventArgs e)
        {
            string human = textBox3.Text;
            string collegeName = textBox8.Text;
            string collegeAbr = textBox6.Text;

            string cookType = textBox9.Text;

            string MySqlConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=11111;database=mydb";
            MySqlConnection databaseConnection = new MySqlConnection(MySqlConnectionString);


            if (human == "" || collegeName == "" || collegeAbr == "" ||
                cookType == "")

            {
                MessageBox.Show("Whong Input!!! Not all fields are filled");
                return;
            }


            databaseConnection.Open();

            MySqlCommand command = new MySqlCommand("call mydb.proc_AddCook(@hum, @cn, @ca, @ct)"
                               , databaseConnection);

            command.Parameters.Add("@hum", MySqlDbType.VarChar).Value = human;
            command.Parameters.Add("@cn", MySqlDbType.VarChar).Value = collegeName;
            command.Parameters.Add("@ca", MySqlDbType.VarChar).Value = collegeAbr;
            command.Parameters.Add("@ct", MySqlDbType.VarChar).Value = cookType;

            command.ExecuteNonQuery();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RefreshCookerTable();
        }
        private void RefreshCookerTable()
        {
            string MySqlConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=11111;database=mydb";


            MySqlConnection databaseConnection = new MySqlConnection(MySqlConnectionString);
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(
                "call proc_RefreshCooker();",
                databaseConnection
                );

            adapter.SelectCommand = command;

            DataTable table = new DataTable();
            adapter.Fill(table);

            dataGridView1.Rows.Clear();

            foreach (var row in table.AsEnumerable())
            {
                dataGridView1.Rows.Add(
                new object[]
                {
                    row.ItemArray[0],

                    row.ItemArray[1],
                    row.ItemArray[2],
                    row.ItemArray[3],
                    row.ItemArray[4],
                    row.ItemArray[5],
                    row.ItemArray[6],
                    row.ItemArray[7]
                });
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {//3869
            string MySqlConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=11111;database=mydb";

            MySqlConnection databaseConnection = new MySqlConnection(MySqlConnectionString);
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("call mydb.proc_SearchCooker(@log, @cn, @ca, @ct) ", databaseConnection);

            command.Parameters.Add("@log", MySqlDbType.VarChar).Value = textBox3.Text;
            command.Parameters.Add("@cn", MySqlDbType.VarChar).Value = textBox8.Text;
            command.Parameters.Add("@ca", MySqlDbType.VarChar).Value = textBox6.Text;
            command.Parameters.Add("@ct", MySqlDbType.VarChar).Value = textBox9.Text;

            adapter.SelectCommand = command;
            DataTable table11 = new DataTable();
            adapter.Fill(table11);

            dataGridView1.Rows.Clear();

            foreach (var row in table11.AsEnumerable())
            {
                dataGridView1.Rows.Add(
                new object[]
                {
                    row.ItemArray[0],
                    row.ItemArray[1],
                    row.ItemArray[2],
                    row.ItemArray[3],
                    row.ItemArray[4],
                    row.ItemArray[5],
                    row.ItemArray[6],
                    row.ItemArray[7]
                });
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            int seconds = Convert.ToInt32(textBox2.Text);


            string MySqlConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=11111;database=mydb";
            MySqlConnection databaseConnection = new MySqlConnection(MySqlConnectionString);

            MySqlCommand command4 = new MySqlCommand(
                "call mydb.proc_makeWeekend(@id, @sec); " +

                "CREATE EVENT IF NOT EXISTS mydb.q " +
                "ON SCHEDULE AT DATE_ADD(NOW(),INTERVAL @sec second) " +
                "DO " +
                    "update mydb.cook  " +
                    "set mydb.cook.isWorkNow = 1 " +
                    "where mydb.cook.idCook = @id ; " 
                , databaseConnection);
            command4.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
            command4.Parameters.Add("@sec", MySqlDbType.Int32).Value = seconds;

            databaseConnection.Open();
            command4.ExecuteNonQuery();
        }
    }
}
