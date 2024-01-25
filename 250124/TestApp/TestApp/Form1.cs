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
    public partial class Form1 : Form
    {
        public string connectionStr = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var con = new SqlConnection(this.connectionStr);
            try
            {
                con.Open();

                var sql = "SELECT [password] FROM users WHERE [login] = '" + textBox1.Text + "'";

                var cmd = new SqlCommand(sql, con);
                var password = cmd.ExecuteScalar();

                if(password != null)
                {
                    if (password.ToString() == textBox2.Text)
                    {
                        var mainForm = new MainForm();
                        mainForm.Show();

                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("password is wrong");
                    }
                }

                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                con.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.connectionStr = @"Data Source=.\SQLEXPRESS;
                          AttachDbFilename=C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\test_app_db.mdf;
                          Integrated Security=True;
                          Connect Timeout=30;
                          User Instance=True";
        }
    }
}
