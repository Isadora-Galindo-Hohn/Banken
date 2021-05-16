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
        long personnummer;
        int antalKonton = 0;
        string kundnummer;
        Lista<Konto> konton = new Lista<Konto>();

        //Här sker overlode 
        public Kund(string n)
        {
            this.namn = n;
            kundnummer = SkapaKundnummer();
            antalKonton = konton.Length();
        }

        public Kund(long p)
        {
            this.personnummer = p;
            kundnummer = SkapaKundnummer();
            antalKonton = konton.Length();
        }

        public Kund(string n, long p, string k)
        {
            this.namn = n;
            this.personnummer = p;
            this.kundnummer = k;
        }

        public void SkapaNyttKonto(string kontotyp, string kontonamn, double saldo, double extraParameter = 0)
        {
            if (kontotyp == "Debitkonto")
            {
                konton.Add(new Konto("Debitkonto", kontonamn, saldo, this.kundnummer, this.antalKonton++));
            }
            else if (kontotyp == "Lönekonto")
            {
                konton.Add(new Lönekonto("Lönekonto", kontonamn, saldo, this.kundnummer, this.antalKonton++, extraParameter));
            }
            else if (kontotyp == "Sparkonto")
            {
                konton.Add(new Sparkonto("Sparkonto", kontonamn, saldo, this.kundnummer, this.antalKonton++));
            }
            else if (kontotyp == "Fondkonto")
            {
                konton.Add(new Fondkonto("Fondkonto", kontonamn, saldo, this.kundnummer, this.antalKonton++, extraParameter));
            }
        }

        public Konto FåNyasteKonto
        {
            get
            {
                return konton[konton.Length() - 1];
            }
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

        public long FåPersonnummer
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

        public void SkrivUtKonton()
        {
            for (int i = 0; i < konton.Length(); i++)
            {
                Console.WriteLine((i + 1) + ". " + konton[i].FåKontoNamn + "\t\t" + konton[i].FåKontoSaldo);
                Console.WriteLine();
            }
        }

        public void SkrivUtKund()
        {
            Console.WriteLine("Namn: " + FåNamn);
            Console.WriteLine("Personnummer: " + FåPersonnummer);
            Console.WriteLine("Kundnummer: " + FåKundnummer);
            Console.WriteLine();
            Console.WriteLine("Konton");
            Console.WriteLine();
            SkrivUtKonton();
            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine();
        }
    }
}
