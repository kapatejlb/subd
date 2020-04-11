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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            comboBox1.Items.Add("Admin");
            comboBox1.Items.Add("User");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string firstName = textBox1.Text;
                string secondName = textBox2.Text;
                string Patronymic = textBox3.Text;

                string login = textBox6.Text;

                string password = textBox5.Text;
                string repPassword = textBox4.Text;

                string MySqlConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=11111;database=mydb";
                MySqlConnection databaseConnection = new MySqlConnection(MySqlConnectionString);


                if (firstName == "" || secondName == "" || Patronymic == "" ||
                    login == "" || password == "" || repPassword == "" || password != repPassword)

                {
                    MessageBox.Show("Whong Input!!! Not all fields are filled or login is already exist о.о");
                    return;
                }


                databaseConnection.Open();

                MySqlCommand command = new MySqlCommand("lock tables mydb.human write, mydb.name1 write, mydb.name2 write, mydb.name3 write; " +
                                   "call mydb.proc_registration(@fn, @ln, @pat, @act, @log, @pass, @rol, @stat); " +
                                   "unlock tables; "
                                   , databaseConnection);

                command.Parameters.Add("@fn", MySqlDbType.VarChar).Value = firstName;
                command.Parameters.Add("@ln", MySqlDbType.VarChar).Value = secondName;
                command.Parameters.Add("@pat", MySqlDbType.VarChar).Value = Patronymic;
                command.Parameters.Add("@act", MySqlDbType.Int32).Value = 1;
                command.Parameters.Add("@log", MySqlDbType.VarChar).Value = login;
                command.Parameters.Add("@pass", MySqlDbType.Int32).Value = Convert.ToInt32(password);
                command.Parameters.Add("@rol", MySqlDbType.Int32).Value = 1;
                command.Parameters.Add("@stat", MySqlDbType.Int32).Value = 1;

            command.ExecuteNonQuery();

                //adapter.InsertCommand = command;

                this.Hide();

                Form4 f4 = new Form4();
                f4.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Hide();

            Form2 f2 = new Form2();
            f2.Show();

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
