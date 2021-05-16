using System;

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
        //Returnerar lön
        public double FåLön
        {
            get
            {
                return lön;
            }
        }
        //Ger ut lönen till ett konto
        public void GeUtLön()
        {
            saldo += lön;
            kontoHistorik.Add("Löneinsättning: " + lön);
        }
        //Skriver ut kontohistorik med lön.
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
