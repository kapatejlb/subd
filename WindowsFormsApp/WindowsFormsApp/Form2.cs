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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string MySqlConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=11111;database=mydb";

            string userName = textBox1.Text;
            string userPassword = textBox2.Text;


            MySqlConnection databaseConnection = new MySqlConnection(MySqlConnectionString);
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(" " +
                                                    " call mydb.proc_login(@uL, @uP); " +
                                                    " ", databaseConnection);
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = userName;
            command.Parameters.Add("@uP", MySqlDbType.Int32).Value = userPassword;

            adapter.SelectCommand = command;

            DataTable table = new DataTable();
            adapter.Fill(table);

            //MessageBox.Show(Convert.ToString(table.Rows.Count));

            if (table.Rows.Count == 0)
                MessageBox.Show("Wrong login or password!");
            else
            {


                this.Hide();

                Form4 f4 = new Form4();
                f4.Show();

            }


        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Hide();

            Form3 f3 = new Form3();
            f3.Show();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
