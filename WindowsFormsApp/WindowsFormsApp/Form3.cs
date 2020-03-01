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


            if(firstName == "" || secondName == "" || Patronymic == "" ||
                login == "" || password == "" || repPassword == "" || password != repPassword)
            {
                MessageBox.Show("Whong Input!!!");
                return;
            }


            MySqlConnection databaseConnection = new MySqlConnection(MySqlConnectionString);
            MySqlDataAdapter adapter = new MySqlDataAdapter();


            MySqlCommand command = new MySqlCommand(
            "insert into mydb.name1 "+
            "set name1.Name1 = @fN;" +
            
            "insert into mydb.name2 "+
            "set name2.Name2 = @sN;" +
            
            "insert into mydb.name3 " +
            "set name3.Name3 = @pat;"        
            , databaseConnection);



            MySqlCommand command2 = new MySqlCommand(
                "insert into mydb.human(mydb.human.Name1_idName1, mydb.human.Name2_idName2, mydb.human.Name3_idName3, " +
                "mydb.human.active, mydb.human.login, mydb.human.password, mydb.human.role, mydb.human.status) " +
                "select name1.idName1, name2.idName2, name3.idName3, 1, @log, @pass, @rol, 1 from( " +
                "(select idName1 from mydb.name1 order by mydb.name1.idName1 desc limit 1) as name1, " +
                "(select idName2 from mydb.name2 order by mydb.name2.idName2 desc limit 1) as name2, " +
                "(select idName3 from mydb.name3 order by mydb.name3.idName3 desc limit 1) as name3) "
            ,databaseConnection);

            command.Parameters.Add("@fN", MySqlDbType.VarChar).Value = firstName;
            command.Parameters.Add("@sN", MySqlDbType.VarChar).Value = secondName;
            command.Parameters.Add("@pat", MySqlDbType.VarChar).Value = Patronymic;
            command2.Parameters.Add("@log", MySqlDbType.VarChar).Value = login;
            command2.Parameters.Add("@pass", MySqlDbType.Int32).Value = password;
            command2.Parameters.Add("@rol", MySqlDbType.Int32).Value = 1;

            databaseConnection.Open();
            command.ExecuteNonQuery();
            command2.ExecuteNonQuery();

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
