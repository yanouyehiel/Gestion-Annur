using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Gestion_Annur.Materiel_Control
{
    /// <summary>
    /// Logique d'interaction pour Mouvement.xaml
    /// </summary>
    public partial class Mouvement : UserControl
    {
        public Mouvement()
        {
            InitializeComponent();
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(Mouvement), new PropertyMetadata(null));

        public string Desc
        {
            get { return (string)GetValue(DescProperty); }
            set { SetValue(DescProperty, value); }
        }

        public static readonly DependencyProperty DescProperty = DependencyProperty.Register("Desc", typeof(string), typeof(Mouvement), new PropertyMetadata(null));

        public FontAwesome.Sharp.IconChar Icon
        {
            get { return (FontAwesome.Sharp.IconChar)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(string), typeof(Mouvement), new PropertyMetadata(null));
    }
}
