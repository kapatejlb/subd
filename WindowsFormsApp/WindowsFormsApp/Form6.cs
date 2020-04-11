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
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();

            Form4 f4 = new Form4();
            f4.Show();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            string ingName = textBox4.Text;
            string ingType = textBox5.Text;

            string MySqlConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=11111;database=mydb";
            MySqlConnection databaseConnection = new MySqlConnection(MySqlConnectionString);

            if (ingName == "" || ingType == "")
            {
                MessageBox.Show("Whong Input!!! Not all fields are filled");
                return;
            }

            databaseConnection.Open();

            MySqlCommand command = new MySqlCommand("call mydb.proc_AddIngredient(@in, @it)"
                               , databaseConnection);

            command.Parameters.Add("@in", MySqlDbType.VarChar).Value = ingName;
            command.Parameters.Add("@it", MySqlDbType.VarChar).Value = ingType;

            command.ExecuteNonQuery();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            RefreshIngredientTable();
        }

        private void RefreshIngredientTable()
        {
            string MySqlConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=11111;database=mydb";

            MySqlConnection databaseConnection = new MySqlConnection(MySqlConnectionString);
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(
                "call proc_RefreshIngredient();",
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

        private void button12_Click(object sender, EventArgs e)
        {
            string MySqlConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=11111;database=mydb";

            MySqlConnection databaseConnection = new MySqlConnection(MySqlConnectionString);
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("call mydb.proc_searchIngredient(@in, @it) ", databaseConnection);

            command.Parameters.Add("@in", MySqlDbType.VarChar).Value = textBox4.Text;
            command.Parameters.Add("@it", MySqlDbType.VarChar).Value = textBox5.Text;


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

        private void button14_Click(object sender, EventArgs e)
        {
            var current = dataGridView3.CurrentRow.Cells;

            int colid = Convert.ToInt32(current[0].Value);
            string ingName = current[1].Value.ToString();
            string ingType = current[2].Value.ToString();


            string MySqlConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=11111;database=mydb";
            MySqlConnection databaseConnection = new MySqlConnection(MySqlConnectionString);
            databaseConnection.Open();

            MySqlCommand command20 = new MySqlCommand(" call proc_updateIngredient(@id, @in, @it); "
            , databaseConnection);

            command20.Parameters.Add("@id", MySqlDbType.Int32).Value = colid;
            command20.Parameters.Add("@in", MySqlDbType.VarChar).Value = ingName;
            command20.Parameters.Add("@it", MySqlDbType.VarChar).Value = ingType;

            command20.ExecuteNonQuery();

            RefreshIngredientTable();
        }
    }
}
