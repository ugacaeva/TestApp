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
    public partial class AddEditForm : Form
    {
        public string connectionStr = "";

        public AddEditForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var con = new SqlConnection(this.connectionStr);
            try
            {
                con.Open();

                var sql = "INSERT INTO users(login, password) VALUES('" + textBox1.Text + "', '" + textBox2.Text + "')";

                var cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();

                MessageBox.Show("record add");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                con.Close();
            }
        }

        private void AddEditForm_Load(object sender, EventArgs e)
        {
            this.connectionStr = @"Data Source=.\SQLEXPRESS;
                          AttachDbFilename=C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\test_app_db.mdf;
                          Integrated Security=True;
                          Connect Timeout=30;
                          User Instance=True";
        }
    }
}
