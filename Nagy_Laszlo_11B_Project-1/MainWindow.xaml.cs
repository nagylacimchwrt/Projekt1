using System;
using System.Collections.Generic;
using System.IO;
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

namespace Nagy_Laszlo_11B_Project_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<CheckBox> feladatok = new List<CheckBox>();
        List<CheckBox> toroltek = new List<CheckBox>();
        public MainWindow()
        {
            InitializeComponent();
            Application.Current.Exit += new ExitEventHandler (bezarTxtBeMent);
            Betoltes();
            feladatokListaja.ItemsSource = feladatok;
            toroltekListaja.ItemsSource = toroltek;
        }

        CheckBox utolso = null;

        private void ujElemHozzadasa_Click(object sender, RoutedEventArgs e)
        {
            if (beSzoveg.Text != "")
            {
                CheckBox uj = new CheckBox();
                uj.Content = beSzoveg.Text;
                uj.Checked += new RoutedEventHandler(CheckBox_Checked);
                uj.Unchecked += new RoutedEventHandler(CheckBox_Unchecked);
                feladatok.Add(uj);
                feladatokListaja.Items.Refresh();
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox aktualis = (CheckBox)sender;
            aktualis.FontStyle = FontStyles.Italic;
            aktualis.Foreground = Brushes.Gray;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox aktualis = (CheckBox)sender;
            aktualis.FontStyle = FontStyles.Normal;
            aktualis.Foreground = Brushes.Black;
        }

        private void torlesGomb_Click(object sender, RoutedEventArgs e)
        {
            CheckBox kijelolt = (CheckBox)feladatokListaja.SelectedItem;
            toroltek.Add(kijelolt);
            feladatok.Remove(kijelolt);
            feladatokListaja.Items.Refresh();
            toroltekListaja.Items.Refresh();
        }
        
        private void visszaallitGomb_Click(object sender, RoutedEventArgs e)
        {
            CheckBox kijelolt = (CheckBox)toroltekListaja.SelectedItem;
            feladatok.Add(kijelolt);
            toroltek.Remove(kijelolt);
            feladatokListaja.Items.Refresh();
            toroltekListaja.Items.Refresh();
        }

        private void veglegesTorlesGomb_Click(object sender, RoutedEventArgs e)
        {
            CheckBox kijelolt = (CheckBox)toroltekListaja.SelectedItem;
            toroltek.Remove(kijelolt);
            toroltekListaja.Items.Refresh();
        }

        private void feladatokListaja_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CheckBox kijelolt = (CheckBox)feladatokListaja.SelectedItem;
            if (kijelolt == null) return;
            utolso = kijelolt;
            beSzoveg.Text = kijelolt.Content.ToString();
        }

        private void kijeloltFeladatModositasa_Click(object sender, RoutedEventArgs e)
        {
            if (feladatokListaja.SelectedItem == null) return;
            utolso.Content = beSzoveg.Text;
        }

        private void bezarTxtBeMent (object sender, ExitEventArgs e)
        {
            string[] feladatok = new string[feladatokListaja.Items.Count];
            string[] toroltek = new string[toroltekListaja.Items.Count];

            for (int i = 0; i < feladatok.Length; i++)
            {
                CheckBox kijelolt = (CheckBox)feladatokListaja.Items[i];
                if (kijelolt == null) continue;
                feladatok[i] = kijelolt.Content.ToString() + ";" + kijelolt.IsChecked;
            }

            File.WriteAllLines("feladatok.txt", feladatok);

            for (int i = 0; i < toroltek.Length; i++)
            {
                CheckBox kijelolt = (CheckBox)toroltekListaja.Items[i];
                if (kijelolt == null) continue;
                toroltek[i] = kijelolt.Content.ToString() + ";" + kijelolt.IsChecked;
            }

            File.WriteAllLines("toroltfeladatok.txt", toroltek);
        }

        private void Betoltes()
        {
            if (!File.Exists("feladatok.txt")) return;
            string[] feladatokTomb = File.ReadAllLines("feladatok.txt");
            List<CheckBox> kijeloltBox = new List<CheckBox>();

            for (int i = 0; i < feladatokTomb.Length; i++)
            {
                string[] row = feladatokTomb[i].Split(';');
                CheckBox kijeloltToAdd = new CheckBox();
                kijeloltToAdd.Content = row[0];
                if (row[1] == "True")
                {
                    kijeloltToAdd.IsChecked = true;
                }
                kijeloltToAdd.Checked += new RoutedEventHandler(CheckBox_Checked);
                kijeloltToAdd.Unchecked += new RoutedEventHandler(CheckBox_Unchecked);
                if (kijeloltToAdd.IsChecked == true)
                {
                    kijeloltToAdd.FontStyle = FontStyles.Italic;
                    kijeloltToAdd.Foreground = Brushes.Gray;
                }
                else
                {
                    kijeloltToAdd.FontStyle = FontStyles.Normal;
                    kijeloltToAdd.Foreground = Brushes.Black;
                }
                kijeloltBox.Add(kijeloltToAdd);
                feladatok.Add(kijeloltToAdd);
            }
            feladatokListaja.ItemsSource = kijeloltBox;

            if (!File.Exists("toroltfeladatok.txt")) return;
            string[] toroltTomb = File.ReadAllLines("toroltfeladatok.txt");
            List<CheckBox> toroltBox = new List<CheckBox>();

            for (int i = 0; i < toroltTomb.Length; i++)
            {
                string[] row = toroltTomb[i].Split(';');
                CheckBox kijeloltToAdd = new CheckBox();
                kijeloltToAdd.Content = row[0];
                if (row[1] == "True")
                {
                    kijeloltToAdd.IsChecked = true;
                }
                kijeloltToAdd.Checked += new RoutedEventHandler(CheckBox_Checked);
                kijeloltToAdd.Unchecked += new RoutedEventHandler(CheckBox_Unchecked);
                if (kijeloltToAdd.IsChecked == true)
                {
                    kijeloltToAdd.FontStyle = FontStyles.Italic;
                    kijeloltToAdd.Foreground = Brushes.Gray;
                }
                else
                {
                    kijeloltToAdd.FontStyle = FontStyles.Normal;
                    kijeloltToAdd.Foreground = Brushes.Black;
                }

                toroltBox.Add(kijeloltToAdd);
                toroltek.Add(kijeloltToAdd);
            }
            toroltekListaja.ItemsSource = toroltBox;
        }
    }
}
