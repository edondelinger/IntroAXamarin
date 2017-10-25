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
using Plugin.Geolocator;

namespace IntroAXamarin
{
    public partial class MainPage : ContentPage
    {
        public static ObservableCollection<Contact> lesContacts;
        public static int nbContacts;
        public static 

        public MainPage()
        {
            MainPage.nbContacts = 0;
            InitializeComponent();
            MainPage.lesContacts = new ObservableCollection<Contact>();

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

            

            var position = locator.GetPositionAsync(timeoutMilliseconds: 10000);

            Console.WriteLine("Position Status: {0}", position.Timestamp);
            Console.WriteLine("Position Latitude: {0}", position.Latitude);
            Console.WriteLine("Position Longitude: {0}", position.Longitude);
        }

        void ajoutBTClicked(object sender, EventArgs e)
        {
            string[] coords = emplacementLabel.Text.Split(';');
            double lat = Double.Parse(coords[0]);
            double lon = Double.Parse(coords[1]);
            Contact c = new Contact(nomContact.Text, prenomContact.Text, mailContact.Text, numeroContact.Text, lat, lon);
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
            foreach(Contact c in MainPage.lesContacts)
            {
                toSaveText += c.Nom + ";" + c.Prenom + ";" + c.Email + ";" + c.Numero + ";" +c.Latitude + ";" + c.Longitude + "\n";
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
                    Contact c = new Contact(leContact[0], leContact[1], leContact[2], leContact[3], Double.Parse(leContact[4]), Double.Parse(leContact[5]));
                    lesContacts.Add(c);
                }
            }
        }

        void OnButtonAideClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Aide());
        }

        void OnButtonCarteClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Carte());
        }

        void OnButtonChercherClicked(object sender, EventArgs e)
        {

            Navigation.PushAsync(new RepertoireListe());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if(Carte.latitude != 0.0)
            {
                emplacementLabel.Text = Carte.latitude + ";" + Carte.longitdude;
                //DisplayAlert("Alerte", "Retour ici", "OK");
            }
            System.Diagnostics.Debug.WriteLine("*****Here*****");
            
        }

    }
}
