using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            string insättningString = "Uttag: ," + summa;
            string[] split = insättningString.Split(',');
            kontoHistorik.Add(split[0]);
            kontoHistorik.Add(split[1]);
        }

        public virtual void Insättning(double summa)
        {
            saldo += summa;
            string insättningString = "Insättning: ," + summa;
            string[] split = insättningString.Split(',');
            kontoHistorik.Add(split[0]);
            kontoHistorik.Add(split[1]);
        }

        public Lista<string> FåKontohistorik
        {
            get
            {
                return kontoHistorik;
            }
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
                Console.WriteLine(kontoHistorik[i] + " " + kontoHistorik[i+1]);
                Console.WriteLine();
                i++;
            }
        }
    }
}
