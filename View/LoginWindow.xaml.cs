using MyApp.View;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyApp
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            
        }

        private void UserName_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlConn = new SqlConnection(@"Data Source=OLSI\MYSQL; Initial Catalog = LoginDB; Integrated Security = True");

            try
            {
                sqlConn.Open();

                String queryString = "SELECT COUNT(1) FROM dbo.tblUser WHERE UserName = @UserName AND Password = @Password";
                SqlCommand command = new SqlCommand(queryString, sqlConn);
                command.CommandType = System.Data.CommandType.Text;
                command.Parameters.AddWithValue("@UserName", txtUsername.Text);
                command.Parameters.AddWithValue("@Password", txtPassword.Text);
                int count = (Int32)command.ExecuteScalar();
                if(count == 1)
                {
                    HomePage home = new HomePage();
                    home.Show();
                    Close();
                }

            }
            catch (SqlException)
            {
                MessageBoxResult result = MessageBox.Show("Hello MessageBox");
                return;
            }
            finally
            {
                sqlConn.Close(); 
            }

        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Register reg = new Register();
            reg.Show();
            Close();
        }
    }
}
