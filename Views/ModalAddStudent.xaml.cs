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
using System.Windows.Shapes;

namespace Gestion_Annur.Views
{
    public partial class ModalAddStudent : Window
    {
        string connectionString = "Server=localhost;Database=annur;User ID=root;Password=;";

        public ModalAddStudent()
        {
            InitializeComponent();
        }

        private void addStudent_Click(object sender, RoutedEventArgs e)
        {
            if (!nom.Text.Equals("") && !prenom.Text.Equals("") && !email.Text.Equals("") && !tel.Text.Equals("") &&
                !tel_urgence.Text.Equals("") && !nom_mere.Text.Equals("") && !nom_pere.Text.Equals("") && 
                date_naissance.SelectedDate != null && diplome.SelectedIndex != -1 && filiere.SelectedIndex != -1)
            {
                using (MySqlConnection connect = new MySqlConnection(connectionString))
                {
                    connect.Open();
                    string query = "INSERT INTO etudiants VALUES (@mat, @nom, @pre, @email, @tel, @naiss, @tel_urg, @dip, @fil, @pere, @mere)";
                    try
                    {
                        using (MySqlCommand cmd = new MySqlCommand(query, connect))
                        {
                            ComboBoxItem selectedItemDiplome = (ComboBoxItem)diplome.SelectedItem;
                            string diplomeSelected = selectedItemDiplome.Content.ToString();

                            ComboBoxItem selectedItemFiliere = (ComboBoxItem)filiere.SelectedItem;
                            string filiereSelected = selectedItemFiliere.Content.ToString();


                            cmd.Parameters.AddWithValue("@nom", nom.Text);
                            cmd.Parameters.AddWithValue("@pre", prenom.Text);
                            cmd.Parameters.AddWithValue("@mat", this.GenererMatriculeEtudiant());
                            cmd.Parameters.AddWithValue("@email", email.Text);
                            cmd.Parameters.AddWithValue("@tel", tel.Text);
                            cmd.Parameters.AddWithValue("@naiss", date_naissance.SelectedDate.ToString());
                            cmd.Parameters.AddWithValue("@tel_urg", tel_urgence.Text);
                            cmd.Parameters.AddWithValue("@dip", diplomeSelected);
                            cmd.Parameters.AddWithValue("@fil", filiereSelected);
                            cmd.Parameters.AddWithValue("@pere", nom_pere.Text);
                            cmd.Parameters.AddWithValue("@mere", nom_mere.Text);

                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Etudiant inscrit avec succès !");
                            Close();
                        }
                    } catch (MySqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            } else
            {
                MessageBox.Show("Veuillez remplir tous les champs");
            }
        }

        public string GenererMatriculeEtudiant()
        {
            const string caracteresPossibles = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Random random = new Random();
            StringBuilder matricule = new StringBuilder();

            matricule.Append("ANR");
            matricule.Append(DateTime.UtcNow.Year.ToString().Substring(2));
            for (int i = 0; i < 7; i++)
            {
                matricule.Append(caracteresPossibles[random.Next(caracteresPossibles.Length)]);
            }

            return matricule.ToString();
        }
    }
}
