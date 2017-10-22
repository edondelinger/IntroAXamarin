using IntroAXamarin.classes;
using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Reflection;

namespace IntroAXamarin
{
    public partial class MainPage : ContentPage
    {
        private ObservableCollection<Contact> lesContacts;
        public static int nbContacts;

        public MainPage()
        {
            MainPage.nbContacts = 0;
            InitializeComponent();
            lesContacts = new ObservableCollection<Contact>();

            /*
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream("IntroAXamarin.storage.save_file.txt");
            string text = "";
            using (var reader = new System.IO.StreamReader(stream))
            {
                text = reader.ReadToEnd();
            }
            */

            this.loadContacts();

            // DependencyService.Get<ISaveAndLoad>().SaveText("temp.txt", "Eric;0621211212");

            ContactView.ItemsSource = lesContacts;

            ContactView.ItemSelected += ContactView_ItemSelected;         
        }

        void ajoutBTClicked(object sender, EventArgs e)
        {
            Contact c = new Contact(nomContact.Text, numeroContact.Text);
            lesContacts.Add(c);
            confirmLabel.Text = String.Format("{0} avec le numéro {1}",
                                       nomContact.Text, numeroContact.Text);
            this.saveContacts();

            /*
            string line = string.Format("{0};{1}\n", nomContact.Text, numeroContact.Text);
            var document = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var filename = Path.Combine(document, "MyContacts.txt");
            File.AppendAllText(filename, line);
            */
        }

        public void saveContacts()
        {
            string toSaveText = "";
            foreach(Contact c in this.lesContacts)
            {
                toSaveText += c.Nom + ";" + c.Numero + "\n";
            }
            DependencyService.Get<ISaveAndLoad>().SaveText("temp.txt", toSaveText);
        }

        public void loadContacts()
        {
            String recupere = DependencyService.Get<ISaveAndLoad>().LoadText("temp.txt");

            String[] lesLignesContact = recupere.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            foreach (var item in lesLignesContact)
            {
                MainPage.nbContacts++;
                if (item.Length > 0)
                {
                    string[] leContact = item.Split(';');
                    Contact c = new Contact(leContact[0], leContact[1]);
                    lesContacts.Add(c);
                }
            }
        }

        void OnButtonAideClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Aide());
        }

        private void ContactView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            DisplayAlert("Alerte", "Vous avez choisi un élément de la liste", "OK");
        }
    }
}
