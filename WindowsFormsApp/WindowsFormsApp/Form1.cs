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

namespace WindowsFormsApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            runQuery();
        }
        private void runQuery()
        {
            string query = textBox1.Text;

            if (query == "")
            {
                MessageBox.Show("Please enter your query");
                return;
            }
            string MySqlConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=11111;database=mydb";

            MySqlConnection databaseConnection = new MySqlConnection(MySqlConnectionString);

            MySqlCommand commandDatabase = new MySqlCommand(query, databaseConnection);

            commandDatabase.CommandTimeout = 60;

            try
            {
                databaseConnection.Open();

                MySqlDataReader myReader = commandDatabase.ExecuteReader();

                if (myReader.HasRows)
                {
                    MessageBox.Show("Your query generated results");

                    while (myReader.Read())
                    {
                        MessageBox.Show(myReader.GetString(0) + " - " + myReader.GetString(1));
                    }
                }
                else
                {
                    MessageBox.Show("Querry succesfully executed");
                }
            }
            catch (Exception e) 
            {
                MessageBox.Show("Query error: " + e.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
