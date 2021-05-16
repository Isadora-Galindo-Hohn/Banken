using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankenKlient
{
    class Sparkonto : Konto, IRänta
    {
        double ränta = 1.03;

        public Sparkonto(string kontotyp, string kontonamn, double saldo, string kundnummer, int kontonummer) :
            base(kontotyp, kontonamn, saldo, kundnummer, kontonummer) { }

        public double FåRänta
        {
            get
            {
                return ränta;
            }
        }

        public void ImplementeraRänta()
        {
            double räntevinst = (saldo * ränta) - saldo;
            saldo += räntevinst;
            kontoHistorik.Add("Räntevinst: " + räntevinst);
        }

        public override void SkrivUtKontoHistorik()
        {
            Console.Clear();
            Console.WriteLine("Händelser för konto " + FåKontoNamn + ": ");
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Kontotyp: " + FåKontotyp);
            Console.WriteLine("Kontonummer: " + kontonummer);
            Console.WriteLine("Saldo: " + saldo);
            Console.WriteLine("Ränta: " + ränta);
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
