using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
    public static class VisualTreeHelpers
    {
        public static T FindVisualParent<T>(UIElement element) where T : UIElement
        {
            UIElement parent = VisualTreeHelper.GetParent(element) as UIElement;
            while (parent != null)
            {
                if (parent is T) return (T)parent;
                parent = VisualTreeHelper.GetParent(parent) as UIElement;
            }
            return null;
        }
    }
    public partial class EtudiantsView : UserControl
    {
        string connectionString = "Server=localhost;Database=annur;User ID=root;Password=;";
        public EtudiantsView()
        {
            InitializeComponent();
            loadEtudiants();
        }

        private void addEtudiantBtn_Click(object sender, RoutedEventArgs e)
        {
            ModalAddStudent m = new ModalAddStudent();
            m.Show();
        }

        private void loadEtudiants()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT matricule as Matricule, nom as Nom, prenom as Prénom, email as Email, telephone as Téléphone, " +
                        "date_naissance as `Date de naissance`, tel_urgence as `Téléphone d'urgence`, diplome as `Diplome d'entré`, " +
                        "filiere as Filière, nom_pere as `Nom du père`, nom_mere as `Nom de la mère`  FROM etudiants";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dataGrid.AutoGenerateColumns = false;

                    foreach (DataColumn column in dt.Columns)
                    {
                        DataGridTextColumn gridColumn = new DataGridTextColumn();
                        gridColumn.Header = column.ColumnName;
                        gridColumn.Binding = new Binding(column.ColumnName);
                        dataGrid.Columns.Add(gridColumn);
                    }

                    dataGrid.ItemsSource = dt.DefaultView;
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void dataGrid_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.RightButton == MouseButtonState.Pressed)
            {
                var dataGrid = (DataGrid)sender;

                var row = VisualTreeHelpers.FindVisualParent<DataGridRow>(e.OriginalSource as FrameworkElement);

                if (row != null)
                {
                    var item = dataGrid.SelectedItem;

                    e.Handled = true;
                }
            }
        }
    }
}
