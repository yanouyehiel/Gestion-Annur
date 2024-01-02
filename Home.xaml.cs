using System.Windows;
using MySql.Data.MySqlClient;
using System.Windows.Input;
using System;
using System.Windows.Media;
using System.Collections.ObjectModel;

namespace Gestion_Annur
{
    /// <summary>
    /// Logique d'interaction pour Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        public int nbEtudiants = 0;
        private bool isMaximized = false;
        public Home()
        {
            InitializeComponent();

            nbEtudiants = this.Count_Etudiants();
            var converter = new BrushConverter();
            ObservableCollection<Etudiant> etudiants = new ObservableCollection<Etudiant>();

            //Insertion des donnees dans le DataGrid
            etudiants.Add(new Etudiant { Number="1", Matricule = "ANR-001", Character = "E", BgColor = (Brush)converter.ConvertFromString("#0a13ec"), Diplome = "BTS", Email = "yanou.yehiel@yahoo.com", Filiere = "LP GL", Nom = "Yanou Yehiel", Telephone = "695707732" });
            etudiantsDataGrid.ItemsSource = etudiants;
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        public void Button_Deconnect(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow login = new MainWindow();
            login.Show();
        }

        public void Add_Student(object sender, RoutedEventArgs e)
        {
            ModalAddStudent modalAddStudent = new ModalAddStudent();
            modalAddStudent.Show();
        }

        private int Count_Etudiants()
        {
            string connectionString = "Server=localhost;Database=annur;User Id=root;Password=";
            MySqlConnection connection = new MySqlConnection(connectionString);

            try
            {
                connection.Open();

                string query = "SELECT COUNT(*) As NbEtudiants FROM etudiants";
                MySqlCommand command = new MySqlCommand(query, connection);

                MySqlDataReader reader = command.ExecuteReader();

                this.nbEtudiants = reader.FieldCount;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Erreur de connexion à la base de données: " + ex.Message);
            }

            return this.nbEtudiants;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (isMaximized)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1920;
                    this.Height = 1080;
                    isMaximized = false;
                } else
                {
                    this.WindowState = WindowState.Maximized;
                    isMaximized = true;
                }
            }
        }
    }

    public class Etudiant
    {
        public string Number { get; set; }
        public string Character { get; set; }
        public string Matricule { get; set; }
        public string Nom { get; set; }
        public string Filiere { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Diplome { get; set; }
        public Brush BgColor { get; set; }

    }
}
