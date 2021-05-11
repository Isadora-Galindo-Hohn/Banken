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
        int antalKonton = 0;
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

        public void SkapaNyttKonto(string kontotyp, string kontonamn, double saldo, double extraParameter = 0)
        {
            if (kontotyp == "Debitkonto")
            {
                konton.Add(new Konto(kontonamn, saldo, this.kundnummer, this.antalKonton++));
            }
            else if (kontotyp == "Lönekonto")
            {
                konton.Add(new Lönekonto(kontonamn, saldo, this.kundnummer, this.antalKonton++, extraParameter));
            }
            else if (kontotyp == "Sparkonto")
            {
                konton.Add(new Sparkonto(kontonamn, saldo, this.kundnummer, this.antalKonton++));
            }
            else if (kontotyp == "Fondkonto")
            {
                konton.Add(new Fondkonto(kontonamn, saldo, this.kundnummer, this.antalKonton++, extraParameter));
            }
        }

        public Konto FåNyasteKonto
        {
            get
            {
                return konton[konton.Length()-1];
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

        public void SkrivUtKonton(int val)
        {
            if (val == 1)
            {
                for (int i = 0; i < konton.Length(); i++)
                {
                    Console.WriteLine(konton[i].FåKontoNamn + "\t\t" + konton[i].FåKontoSaldo);
                    Console.WriteLine();
                }
            }
            else if (val == 2)
            {
                for (int i = 0; i < konton.Length(); i++)
                {
                    Console.WriteLine((i+1) +konton[i].FåKontoNamn + "\t\t" + konton[i].FåKontoSaldo);
                    Console.WriteLine();
                }
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
