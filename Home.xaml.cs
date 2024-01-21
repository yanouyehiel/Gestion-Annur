using System.Windows;
using MySql.Data.MySqlClient;
using System.Windows.Input;
using System;
using System.Windows.Media;
using System.Collections.ObjectModel;
using Gestion_Annur.Views;

namespace Gestion_Annur
{
    /// <summary>
    /// Logique d'interaction pour Home.xaml
    /// </summary>
    public partial class Home : Window
    {
        public int nbEtudiants = 0;
        private bool isMaximized = false;
        //Style menuButtonActive = (Style)FindResource("menuButtonActive");
        public Home()
        {
            InitializeComponent();
            contentControl.Content = new DashboardView();
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

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void pnlControlBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void pnlControlBar_MouseEnter(object sender, MouseEventArgs e)
        {
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else this.WindowState = WindowState.Normal;
        }


        private void menuDashboardBtn_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new DashboardView();
        }

        private void menuEtudiantsBtn_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new EtudiantsView();
        }

        private void menuScolariteBtn_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new ScolariteView();
        }

        private void menuEnseignantsBtn_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new EnseignantsView();
        }

        private void menuFilieresBtn_Click(object sender, RoutedEventArgs e)
        {
            contentControl.Content = new FilieresView();
        }

        private void menuLogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow m = new MainWindow();
            m.Show();
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
