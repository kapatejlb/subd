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
            UpdateTable();
        }

        private void UpdateTable()
        {
            string MySqlConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=11111;database=mydb";


            MySqlConnection databaseConnection = new MySqlConnection(MySqlConnectionString);
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand command = new MySqlCommand(
                "select h.id, n1.Name1, n2.Name2, n3.Name3, h.login, h.password, h.status, h.role, h.active " +
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
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            databaseConnection.Open();

            MySqlCommand command = new MySqlCommand(
                "delete from mydb.human where mydb.human.id = @id "
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

            MySqlCommand command = new MySqlCommand(
                "select * from mydb.name1 " +
                "where mydb.name1.Name1 = @fN"
                , databaseConnection);


             MySqlCommand command2 = new MySqlCommand(
                "select * from mydb.name2 " +
                "where mydb.name2.Name2 = @sN"
                , databaseConnection);

             MySqlCommand command3 = new MySqlCommand(
                "select * from mydb.name3 " +
                "where mydb.name3.Name3 = @pat"
                , databaseConnection);

            command.Parameters.Add("@fN", MySqlDbType.VarChar).Value = firstName;
            command2.Parameters.Add("@sN", MySqlDbType.VarChar).Value = secondName;
            command3.Parameters.Add("@pat", MySqlDbType.VarChar).Value = Patronymic;

            string newName = "", newSecName = "", newPatr = "";

            adapter.SelectCommand = command;
            DataTable table1= new DataTable();
            adapter.Fill(table1);
            if(table1.Rows.Count == 0)
            {
                MySqlCommand command4 = new MySqlCommand(
                "insert into mydb.name1 " +
                "set name1.Name1 = @fN;"
                ,databaseConnection);
                command4.Parameters.Add("@fN", MySqlDbType.VarChar).Value = firstName;
                //databaseConnection.Open();
                command4.ExecuteNonQuery();
            }
            else
            {
                newName = table1.AsEnumerable().ToArray()[0].ItemArray[0].ToString();
            }

            adapter.SelectCommand = command2;
            DataTable table2 = new DataTable(); 
            adapter.Fill(table2);
            if (table2.Rows.Count == 0)
            {
                MySqlCommand command5 = new MySqlCommand(
                "insert into mydb.name2 " +
                "set name2.Name2 = @sN;"
                , databaseConnection);
                command5.Parameters.Add("@sN", MySqlDbType.VarChar).Value = secondName;
                //databaseConnection.Open();
                command5.ExecuteNonQuery();
            }
            else
            {
                newSecName = table2.AsEnumerable().ToArray()[0].ItemArray[0].ToString();
            }

            adapter.SelectCommand = command3;
            DataTable table3 = new DataTable();
            adapter.Fill(table3);
            if (table3.Rows.Count == 0)
            {
                MySqlCommand command6 = new MySqlCommand(
                "insert into mydb.name3 " +
                "set name3.Name3 = @pat;"
                , databaseConnection);
                command6.Parameters.Add("@pat", MySqlDbType.VarChar).Value = Patronymic;
                //databaseConnection.Open();
                command6.ExecuteNonQuery();
            }
            else
            {
                newPatr = table3.AsEnumerable().ToArray()[0].ItemArray[0].ToString();
            }

            //databaseConnection.Open();
            //command.ExecuteNonQuery();

            string fn, sn, pat;

            if(newName != "")
            {
                fn = newName;
            }
            else
            {
                MySqlCommand command10 = new MySqlCommand(
                "select * from mydb.name1 " +
                "where mydb.name1.Name1 = @fN"
                , databaseConnection);
                command10.Parameters.Add("@fN", MySqlDbType.VarChar).Value = firstName;

                adapter.SelectCommand = command;
                DataTable table10 = new DataTable();
                adapter.Fill(table10);

                fn = table10.AsEnumerable().ToArray()[0].ItemArray[0].ToString();
            }
            if (newSecName != "")
            {
                sn = newSecName;
            }
            else
            {
                MySqlCommand command10 = new MySqlCommand(
                "select * from mydb.name2 " +
                "where mydb.name2.Name2 = @sN"
                , databaseConnection);
                command10.Parameters.Add("@sN", MySqlDbType.VarChar).Value = secondName;

                adapter.SelectCommand = command;
                DataTable table11 = new DataTable();
                adapter.Fill(table11);

                sn = table11.AsEnumerable().ToArray()[0].ItemArray[0].ToString();
            }
            if (newPatr != "")
            {
                pat = newPatr;
            }
            else
            {
                MySqlCommand command10 = new MySqlCommand(
                "select * from mydb.name3 " +
                "where mydb.name3.Name3 = @pat"
                , databaseConnection);
                command.Parameters.Add("@pat", MySqlDbType.VarChar).Value = Patronymic;

                adapter.SelectCommand = command;
                DataTable table12 = new DataTable();
                adapter.Fill(table12);

                pat = table12.AsEnumerable().ToArray()[0].ItemArray[0].ToString();
            }

            string id = current[0].Value.ToString();
            string login = current[4].Value.ToString();

            string password = current[5].Value.ToString();
            string status = current[6].Value.ToString();
            string role = current[7].Value.ToString();


            MySqlCommand command20 = new MySqlCommand(
                "update mydb.human " +
                "set mydb.human.Name1_idName1 = @n1, " +
                "mydb.human.Name2_idName2 = @n2, " +
                "mydb.human.Name3_idName3 = @n3, " +
                "mydb.human.login = @l, " +
                "mydb.human.password = @p, " +
                "mydb.human.role = @r, " +
                "mydb.human.status = @s, " +
                "mydb.human.active = @a " +
                "where mydb.human.id = @id"
            , databaseConnection);

            command20.Parameters.Add("@n1", MySqlDbType.Int32).Value = fn;
            command20.Parameters.Add("@n2", MySqlDbType.Int32).Value = sn;
            command20.Parameters.Add("@n3", MySqlDbType.Int32).Value = pat;
            command20.Parameters.Add("@l", MySqlDbType.VarChar).Value = login;
            command20.Parameters.Add("@p", MySqlDbType.VarChar).Value = password;
            command20.Parameters.Add("@r", MySqlDbType.VarChar).Value = role;
            command20.Parameters.Add("@s", MySqlDbType.VarChar).Value = status;
            command20.Parameters.Add("@a", MySqlDbType.VarChar).Value = "1";
            command20.Parameters.Add("@id", MySqlDbType.Int32).Value = Convert.ToUInt32(current[0].Value);



            int numrows = command20.ExecuteNonQuery();

            UpdateTable();


        }
    }
}
