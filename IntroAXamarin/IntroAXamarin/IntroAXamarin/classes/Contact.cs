using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroAXamarin.classes
{
    public class Contact
    {
        private int id;
        private string nom;
        private string numero;

        public int Id { get => id; set => id = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Numero { get => numero; set => numero = value; }

        public Contact() { }

        public Contact(int id, string nom, string num)
        {
            this.Id = id;
            this.Nom = nom;
            this.Numero = num;
        }

        public Contact(string nom, string num)
        {
            this.Nom = nom;
            this.Numero = num;
        }
    }
}
