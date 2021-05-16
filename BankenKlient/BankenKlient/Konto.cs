using System;

namespace BankenKlient
{
    class Konto : IKontoOperationer
    {
        string kontotyp;
        protected double saldo;
        protected string kontonummer;
        string kontonamn;
        protected Lista<string> kontoHistorik = new Lista<string>();
        
        public Konto(string kontotyp, string kontonamn, double saldo, string kundnummer, int kontonummer)
        {
            this.kontotyp = kontotyp;
            this.kontonamn = kontonamn;
            this.saldo = saldo;
            this.kontonummer = kundnummer + "-" + kontonummer;
        }
        //Returnerar kontonummer
        public string FåKontoNummer
        {
            get
            {
                return kontonummer;
            }
        }
        //Returnerar kontonamn
        public string FåKontoNamn
        {
            get
            {
                return kontonamn;
            }
        }
        //Returnrear saldo
        public double FåKontoSaldo
        {
            get
            {
                return saldo;
            }
        }
        //Returnerar kontotyp
        public string FåKontotyp
        {
            get
            {
                return kontotyp;
            }
        }
        //En  metod för uttag
        public void Uttag(double summa)
        {
            saldo -= summa;
            kontoHistorik.Add("Uttag: " + summa);
        }
        //En virituell metod för insättning, olika kontotyper sätter in pengar på olika sätt
        public virtual void Insättning(double summa)
        {
            saldo += summa;
            kontoHistorik.Add("Insättning: " + summa);
        }
        //Returnerar kontohistorik
        public Lista<string> FåKontohistorik
        {
            get
            {
                return kontoHistorik;
            }
        }
        //Lägger till kontohistorik. används vid skapande av kundlista
        public void LäggTillKontoHändelse(string s)
        {
            kontoHistorik.Add(s);
        }
        //Skriver ut kontohistorik för ett konto
        public virtual void SkrivUtKontoHistorik()
        {
            Console.Clear();
            Console.WriteLine("Händelser för konto " + kontonamn + ": ");
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Kontotyp: " + kontotyp);
            Console.WriteLine("Kontonummer: " + kontonummer);
            Console.WriteLine("Saldo: " + saldo);
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------");
            for (int i = 0; i < kontoHistorik.Length(); i++)
            {
                Console.WriteLine(kontoHistorik[i]);
                Console.WriteLine();
            }
        }
    }
}
