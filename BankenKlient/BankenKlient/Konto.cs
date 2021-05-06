using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankenKlient
{
    class Konto: IKontoOperationer
    {
        protected double saldo;
        string kontonummer;
        string kontonamn;
        protected Lista<string> kontoHistorik = new Lista<string>();

        public Konto(string kontonamn, double saldo, string kundnummer, int kontonummer) 
        {
            this.kontonamn = kontonamn;
            this.saldo = saldo;
            this.kontonummer = kundnummer + "-" + kontonummer;
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

        public virtual void Uttag(double summa) 
        {
            saldo -= summa;
            kontoHistorik.Add("Uttag: -" + summa);
        }

        public void Insättning(double summa)
        {
            saldo += summa;
            kontoHistorik.Add("Insättning: " + summa);
        }

    }
}
