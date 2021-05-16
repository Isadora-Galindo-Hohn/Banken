using System;

namespace BankenKlient
{
    class Kund
    {
        string namn;
        long personnummer;
        int antalKonton = 0;
        string kundnummer;
        Lista<Konto> konton = new Lista<Konto>();

        //Här sker overload
        public Kund(string n)
        {
            this.namn = n;
            kundnummer = SkapaKundnummer();
            antalKonton = konton.Length();
        }
        //Här sker en till overload
        public Kund(long p)
        {
            this.personnummer = p;
            kundnummer = SkapaKundnummer();
            antalKonton = konton.Length();
        }
        //Här sker en till overload
        public Kund(string n, long p, string k)
        {
            this.namn = n;
            this.personnummer = p;
            this.kundnummer = k;
        }
        //Metod för att skapa konto utifrån vilken kontotyp som kunden vill skapa
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
        //Returnerar det nyaste kontot
        public Konto FåNyasteKonto
        {
            get
            {
                return konton[konton.Length() - 1];
            }
        }
        //Returnerar en lista på konton
        public Lista<Konto> FåKontolista
        {
            get
            {
                return konton;
            }
        }

        //Returnerar kundnummer
        public string FåKundnummer
        {
            get
            {
                return kundnummer;
            }
        }
        //Returnerar personnummer
        public long FåPersonnummer
        {
            get
            {
                return personnummer;
            }
        }
        //returnerar namn
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
        //Skriver ut alla konton som kunden har
        public void SkrivUtKonton()
        {
            for (int i = 0; i < konton.Length(); i++)
            {
                Console.WriteLine((i + 1) + ". " + konton[i].FåKontoNamn + "\t\t" + konton[i].FåKontoSaldo);
                Console.WriteLine();
            }
        }
        //Skriver ut kunden
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
