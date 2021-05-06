using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankenKlient
{
    class Kund
    {
        string namn;
        int personnummer;
        int antalKonton;
        string kundnummer;
        Lista<Konto> konton = new Lista<Konto>();

        //Här sker overlode 
        public Kund (string n)
        {
            this.namn = n;
            kundnummer = SkapaKundnummer();
            antalKonton = konton.Length();
        }

        public Kund(int p)
        {
            this.personnummer = p;
            kundnummer = SkapaKundnummer();
            antalKonton = konton.Length();
        }

        public void SkapaNyttKonto(string kontonamn, int saldo)
        {
            konton.Add(new Sparkonto(kontonamn, saldo, this.kundnummer, this.antalKonton++));
        }

        public Lista<Konto> FåKontolista
        {
            get
            {
                return konton;
            }
        }

        public string FåKundnummer
        {
            get
            {
                return kundnummer;
            }
        }

        public int FåPersonnummer
        {
            get
            {
                return personnummer;
            }
        }
        public string FåNamn
        {
            get
            {
                return namn;
            }
        }

        public void SkrivUtKonton()
        {
            for (int i = 0; i < konton.Length(); i++)
            {
                Console.WriteLine(konton[i].FåKontoNamn +"\t\t" + konton[i].FåKontoSaldo);
                Console.WriteLine();
            }
        }


        //skapar kundnummer efter tiden då kunden skapas
        private string SkapaKundnummer() 
        {
            int nr = (int)DateTime.Now.ToFileTime();

            if (nr < 0)
            {
                nr *= -1;
            }
            return nr.ToString();
        }
    }
}
