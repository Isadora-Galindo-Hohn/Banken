using System;

namespace BankenKlient
{
    class Fondkonto : Konto, IRänta
    {
        double risknivå;
        double ränta;

        public Fondkonto(string kontotyp, string kontonamn, double saldo, string kundnummer, int kontonummer, double risk) :
            base(kontotyp, kontonamn, saldo, kundnummer, kontonummer)
        {
            this.risknivå = risk;
        }

        public void ImplementeraRänta()
        {
            double fondförendring = (saldo * GenereraRänta());
            saldo += fondförendring;
            kontoHistorik.Add("Fondförendring: " + fondförendring);
        }

        double GenereraRänta()
        {
            if (risknivå == 1)
            {
                Random generator = new Random();
                ränta = generator.Next(-10, 10) / 100.0;
            }
            else if (risknivå == 2)
            {
                Random generator = new Random();
                ränta = generator.Next(-20, 20) / 100.0;
            }
            else if (risknivå == 3)
            {
                Random generator = new Random();
                ränta = generator.Next(-30, 30) / 100.0;
            }
            return ränta;
        }

        public double FåRisk
        {
            get
            {
                return risknivå;
            }
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
            Console.WriteLine("Risknivå: " + risknivå);
            Console.WriteLine("Ränta: " + ränta);
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
