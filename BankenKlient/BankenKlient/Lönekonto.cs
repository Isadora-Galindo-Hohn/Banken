using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankenKlient
{
    class Lönekonto: Konto
    {
        double lön;

        public Lönekonto(string kontotyp, string kontonamn, double saldo, string kundnummer, int kontonummer, double lön) :
            base(kontotyp, kontonamn, saldo, kundnummer, kontonummer)
        {
            this.lön = lön;
        }

        public void ÄndraLön(double nyLön)
        {
            lön = nyLön;
        }

        public double FåLön
        {
            get
            {
                return lön;
            }
        }

        public void GeUtLön()
        {
            saldo += lön;
            kontoHistorik.Add("Löneinsättning: \t\t" + lön);
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
            Console.WriteLine("Lön: " + lön);
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
