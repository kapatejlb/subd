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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var a = dataGridView1.CurrentRow.Cells[0].Value;
        }

        

        private void Form4_Load(object sender, EventArgs e)
        {
            string MySqlConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=11111;database=mydb";
            MySqlConnection databaseConnection = new MySqlConnection(MySqlConnectionString);
            databaseConnection.Open();

            MySqlCommand command = new MySqlCommand(
                "drop trigger if exists mydb.beforeDeleteBackup; " +
                "drop trigger if exists mydb.beforeUpdateBackup; " +

                "create trigger mydb.beforeUpdateBackup  " +
                "before update on mydb.human for each row  " +

                "INSERT INTO mydb.humanbackup  " +
                    "(mydb.humanbackup.id, mydb.humanbackup.Name2_idName2, mydb.humanbackup.Name1_idName1, mydb.humanbackup.Name3_idName3,  " +
                    "mydb.humanbackup.login, mydb.humanbackup.password, mydb.humanbackup.status, mydb.humanbackup.role,  " +
                    "mydb.humanbackup.active, mydb.humanbackup.blockedfrom, mydb.humanbackup.blockedtill)  " +

                "SELECT * FROM mydb.human WHERE mydb.human.id = old.id;  " +

                "create trigger mydb.beforeDeleteBackup  " +
                "before delete on mydb.human for each row  " +

                "INSERT INTO mydb.humanbackup  " +
                    "(mydb.humanbackup.id, mydb.humanbackup.Name2_idName2, mydb.humanbackup.Name1_idName1, mydb.humanbackup.Name3_idName3,  " +
                    "mydb.humanbackup.login, mydb.humanbackup.password, mydb.humanbackup.status, mydb.humanbackup.role,  " +
                    "mydb.humanbackup.active, mydb.humanbackup.blockedfrom, mydb.humanbackup.blockedtill)  " +

                "SELECT * FROM mydb.human WHERE mydb.human.id = old.id;  "
                , databaseConnection);
            command.ExecuteNonQuery();


            UpdateTable();
        }

        private void UpdateTable()
        {
            string MySqlConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=11111;database=mydb";


            MySqlConnection databaseConnection = new MySqlConnection(MySqlConnectionString);
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(
                "select h.id, n1.Name1, n2.Name2, n3.Name3, h.login, h.password, h.status, h.role, h.blockedfrom, h.blockedtill " +
                "from mydb.human h " +
                "inner join mydb.name1 n1 on n1.idName1 = h.Name1_idName1 " +
                "inner join mydb.name2 n2 on n2.idName2 = h.Name2_idName2 " +
                "inner join mydb.name3 n3 on n3.idName3 = h.Name3_idName3 ",
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
                    row.ItemArray[7],
                    row.ItemArray[8],
                    row.ItemArray[9]
                });
            }

        }



        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CancelRowEdit(object sender, QuestionEventArgs e)
        {

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var current = dataGridView1.CurrentRow.Cells;
            string id = current[0].Value.ToString();

            string MySqlConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=11111;database=mydb";
            MySqlConnection databaseConnection = new MySqlConnection(MySqlConnectionString);
            databaseConnection.Open();

            MySqlCommand command = new MySqlCommand(
                " call mydb.proc_delete(@id); "
                , databaseConnection);

            command.Parameters.Add("@id", MySqlDbType.Int32).Value = Convert.ToInt32(id);

            command.ExecuteNonQuery();
            
            UpdateTable();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UpdateTable();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var current = dataGridView1.CurrentRow.Cells;



            string firstName = current[1].Value.ToString();
            string secondName = current[2].Value.ToString();
            string Patronymic = current[3].Value.ToString();

            string MySqlConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=11111;database=mydb";
            MySqlConnection databaseConnection = new MySqlConnection(MySqlConnectionString);
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            databaseConnection.Open();

            

            string login = current[4].Value.ToString();

            string password = current[5].Value.ToString();
            string status = current[6].Value.ToString();
            string role = current[7].Value.ToString();


            MySqlCommand command20 = new MySqlCommand("lock tables mydb.human write, mydb.name1 write, mydb.name2 write, mydb.name3 write; " +
                "call mydb.proc_update(@id, @n1, @n2, @n3, @a, @l, @p, @r, @s); " +
                "unlock tables;"

            , databaseConnection);

            command20.Parameters.Add("@id", MySqlDbType.Int32).Value = Convert.ToUInt32(current[0].Value);
            command20.Parameters.Add("@n1", MySqlDbType.VarChar).Value = firstName;
            command20.Parameters.Add("@n2", MySqlDbType.VarChar).Value = secondName;
            command20.Parameters.Add("@n3", MySqlDbType.VarChar).Value = Patronymic;
            command20.Parameters.Add("@l", MySqlDbType.VarChar).Value = login;
            command20.Parameters.Add("@p", MySqlDbType.VarChar).Value = password;
            command20.Parameters.Add("@r", MySqlDbType.Int32).Value = 1;
            command20.Parameters.Add("@s", MySqlDbType.Int32).Value = 1;
            command20.Parameters.Add("@a", MySqlDbType.Int32).Value = 1;
            



            int numrows = command20.ExecuteNonQuery();

            UpdateTable();


        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            int seconds = Convert.ToInt32(textBox1.Text);

            string ev_name = "mydb.User_" + Convert.ToString(id) + "_blocked_on_" + Convert.ToString(seconds);
            
            string MySqlConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=11111;database=mydb";
            MySqlConnection databaseConnection = new MySqlConnection(MySqlConnectionString);

            MySqlCommand command4 = new MySqlCommand(

                "lock tables mydb.human write; " +

                "call mydb.proc_timeblock(@id, @sec); " +

                "CREATE EVENT IF NOT EXISTS mydb.qwe " +
                "ON SCHEDULE AT DATE_ADD(NOW(),INTERVAL @sec second) " +
                "DO " +
                    "update mydb.human " +
                    "set mydb.human.status = 1 " +
                    "where mydb.human.id = @id ; " +
                    " unlock tables; "
                , databaseConnection);
            command4.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
            command4.Parameters.Add("@sec", MySqlDbType.Int32).Value = seconds;

            databaseConnection.Open();
            command4.ExecuteNonQuery();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            string MySqlConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=11111;database=mydb";

            MySqlConnection databaseConnection = new MySqlConnection(MySqlConnectionString);
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("call mydb.proc_search(@id, @fn, @ln, @pat, @log, @stat, @rol); ", databaseConnection);


            command.Parameters.Add("@id", MySqlDbType.Int32).Value = Convert.ToInt32(textBox4.Text);
            command.Parameters.Add("@fn", MySqlDbType.VarChar).Value = textBox7.Text;
            command.Parameters.Add("@ln", MySqlDbType.VarChar).Value = textBox2.Text;
            command.Parameters.Add("@pat", MySqlDbType.VarChar).Value = textBox3.Text;
            command.Parameters.Add("@log", MySqlDbType.VarChar).Value = textBox6.Text;
            command.Parameters.Add("@stat", MySqlDbType.VarChar).Value = textBox5.Text;
            command.Parameters.Add("@rol", MySqlDbType.VarChar).Value = comboBox1.Text;


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
                    row.ItemArray[7],
                    row.ItemArray[8],
                });
            }

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            var current = dataGridView1.CurrentRow.Cells;

            int id = Convert.ToInt32(current[0].Value);

            string MySqlConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=11111;database=mydb";
            MySqlConnection databaseConnection = new MySqlConnection(MySqlConnectionString);
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            databaseConnection.Open();

            MySqlCommand command = new MySqlCommand(

                " lock tables mydb.human write; " +
                " lock tables mydb.humanbackup write; "+

                "drop trigger if exists mydb.beforeDeleteBackup; " +
                "drop trigger if exists mydb.beforeUpdateBackup; " +

                

                "delete from mydb.human " +
                "where mydb.human.id = @id; " +

                "insert into mydb.human " +
                "select mydb.humanbackup.id, mydb.humanbackup.Name2_idName2, mydb.humanbackup.Name1_idName1, mydb.humanbackup.Name3_idName3, " +
                        "mydb.humanbackup.login, mydb.humanbackup.password, mydb.humanbackup.status, mydb.humanbackup.role, " +
                        "mydb.humanbackup.active, mydb.humanbackup.blockedfrom, mydb.humanbackup.blockedtill " +
                "from mydb.humanbackup " +
                "where mydb.humanbackup.id = @id  " +
                "and mydb.humanbackup.idhumanBackup = (select max(mydb.humanbackup.idhumanBackup) from mydb.humanbackup); " +

                "delete from mydb.humanbackup " +
                "where mydb.humanbackup.id = @id;  " +

                "create trigger mydb.beforeUpdateBackup  " +
                "before update on mydb.human for each row  " +

                "INSERT INTO mydb.humanbackup  " +
                    "(mydb.humanbackup.id, mydb.humanbackup.Name2_idName2, mydb.humanbackup.Name1_idName1, mydb.humanbackup.Name3_idName3,  " +
                    "mydb.humanbackup.login, mydb.humanbackup.password, mydb.humanbackup.status, mydb.humanbackup.role,  " +
                    "mydb.humanbackup.active, mydb.humanbackup.blockedfrom, mydb.humanbackup.blockedtill)  " +

                "SELECT * FROM mydb.human WHERE mydb.human.id = old.id;  " +

                "create trigger mydb.beforeDeleteBackup  " +
                "before delete on mydb.human for each row  " +

                "INSERT INTO mydb.humanbackup  " +
                    "(mydb.humanbackup.id, mydb.humanbackup.Name2_idName2, mydb.humanbackup.Name1_idName1, mydb.humanbackup.Name3_idName3,  " +
                    "mydb.humanbackup.login, mydb.humanbackup.password, mydb.humanbackup.status, mydb.humanbackup.role,  " +
                    "mydb.humanbackup.active, mydb.humanbackup.blockedfrom, mydb.humanbackup.blockedtill)  " +

                "SELECT * FROM mydb.human WHERE mydb.human.id = old.id;  " +
                
                " unlock tables; "
                , databaseConnection);
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;
            command.ExecuteNonQuery();



        }
    }
}
