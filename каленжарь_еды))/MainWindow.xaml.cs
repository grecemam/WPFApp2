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

namespace каленжарь_еды__
{
    public partial class MainWindow : Window
    {
        public Ejednevnik ejednevnik = new Ejednevnik();
        public MainWindow()
        {
            InitializeComponent();
            datesel.SelectedDate = ejednevnik.data;
            UpdateListBox();
        }

        private void UpdateListBox()
        {
            SpisonZametok.Items.Clear();
            foreach (int i in ejednevnik.today)
            {
                SpisonZametok.Items.Add(ejednevnik.notes[i].name);
            }
        }
        private void DateChanged(object sender, SelectionChangedEventArgs e)
        {
            nazvanie.Text = "";
            opis.Text = "";
            save.IsEnabled = false;
            ejednevnik.data = (DateTime)datesel.SelectedDate;
            ejednevnik.LoadNotes();
            UpdateListBox();
        }
        private void create_Click(object sender, RoutedEventArgs e)
        {
            string name = nazvanie.Text;
            string opisanie = opis.Text;
            nazvanie.Text = "";
            opis.Text = "";
            DateTime date = ejednevnik.data;
            Note note = new Note(name, opisanie, date);
            ejednevnik.AddNote(note);
            UpdateListBox();
        }

        private void SpisonZametok_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selected = SpisonZametok.SelectedIndex;
            if (selected == -1)
            {
                return;
            }
            int index = ejednevnik.today[selected];
            Note note = ejednevnik.notes[index];
            nazvanie.Text = note.name;
            opis.Text = note.opis;
            save.IsEnabled = true;
            del.IsEnabled = true;
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            int selected = SpisonZametok.SelectedIndex;
            Note note = new Note(nazvanie.Text, opis.Text, ejednevnik.data);
            ejednevnik.notes[selected] = note;
            save.IsEnabled = false;
            nazvanie.Text = "";
            opis.Text = "";
            ejednevnik.LoadNotes();
            UpdateListBox();
        }

        private void del_Click(object sender, RoutedEventArgs e)
        {
            int selected = SpisonZametok.SelectedIndex;
            ejednevnik.notes.RemoveAt(selected);
            del.IsEnabled = false;
            nazvanie.Text = "";
            opis.Text = "";
            ejednevnik.LoadNotes();
            UpdateListBox();
        }
    }
}