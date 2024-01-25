using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestApp
{
    public partial class MainForm : Form
    {
        public string connectionStr = "";
        public List<User> listOfUser = null;

        public class User
        {
            public int ID { get; set; }
            public string Login { get; set; }
            public string Password { get; set; }
        }

        public MainForm()
        {
            InitializeComponent();
            this.listOfUser = new List<User>();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.connectionStr = @"Data Source=.\SQLEXPRESS;
                          AttachDbFilename=C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\test_app_db.mdf;
                          Integrated Security=True;
                          Connect Timeout=30;
                          User Instance=True";

            var con = new SqlConnection(this.connectionStr);
            try
            {
                con.Open();

                var sql = "SELECT * FROM users";

                var cmd = new SqlCommand(sql, con);
                SqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read())
                {
                    listOfUser.Add(new User() { ID = Convert.ToInt32(reader[0].ToString()), Login = reader[1].ToString(), Password = reader[2].ToString() });
                }

                reader.Close();
                con.Close();

                dataGridView1.DataSource = listOfUser;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                con.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var addEditFrom = new AddEditForm();
            addEditFrom.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var index = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);

            var con = new SqlConnection(this.connectionStr);
            try
            {
                con.Open();

                var sql = "DELETE FROM users WHERE id = " + index + "";

                var cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("record is delete");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                con.Close();
            }
        }
    }
}
