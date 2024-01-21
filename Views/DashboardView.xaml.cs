using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Gestion_Annur.Views
{
    public partial class DashboardView : UserControl
    {
        string connectionString = "Server=localhost;Database=annur;User ID=root;Password=;";
        public DashboardView()
        {
            InitializeComponent();
            statistics();
        }

        private void statistics()
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT COUNT(*) FROM etudiants";
                    //string query1 = "SELECT COUNT(*) FROM equipements";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    //MySqlCommand cmd1 = new MySqlCommand(query1, con);
                    blocEtudiants.Number = "" + Convert.ToInt32(cmd.ExecuteScalar());
                    //blocFilieres.Number = "" + Convert.ToInt32(cmd1.ExecuteScalar());
                    //blocInscription.Number = "" + Convert.ToInt32(cmd.ExecuteScalar());
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
