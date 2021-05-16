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

        public string FåKontoNummer
        {
            get
            {
                return kontonummer;
            }
        }

        public string FåKontoNamn
        {
            get
            {
                return kontonamn;
            }
        }

        public double FåKontoSaldo
        {
            get
            {
                return saldo;
            }
        }

        public string FåKontotyp
        {
            get
            {
                return kontotyp;
            }
        }

        public virtual void Uttag(double summa)
        {
            saldo -= summa;
            kontoHistorik.Add("Uttag: " + summa);
        }

        public virtual void Insättning(double summa)
        {
            saldo += summa;
            kontoHistorik.Add("Insättning: " + summa);
        }

        public Lista<string> FåKontohistorik
        {
            get
            {
                return kontoHistorik;
            }
        }

        public void LäggTillKontoHändelse(string s)
        {
            kontoHistorik.Add(s);
        }

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
