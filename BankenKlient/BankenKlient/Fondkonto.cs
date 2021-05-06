using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankenKlient
{
    class Fondkonto : Konto, IRänta
    {
        int risknivå;
        double ränta;

        public Fondkonto(string kontonamn, int saldo, string kundnummer, int kontonummer, int risk) :
            base(kontonamn, saldo, kundnummer, kontonummer)
        {
            this.risknivå = risk;
        }

        //overridar basklassens uttag för att räkna med skatten med
        public override void Uttag(double summa)
        {
            base.Uttag(summa * 1.3);  //simulerar att man betalar 30% skatt på fondkonton
        }

        public void ImplemiteraRänta()
        {
            double fondförendring = (saldo * GenereraRänta()) - saldo;
            saldo += fondförendring;
            kontoHistorik.Add("Fondförendring: " + fondförendring);
        }

        double GenereraRänta()
        {
            if (risknivå == 1)
            {
                Random generator = new Random();
                ränta = generator.Next(-200, 200) / 100.0;
            }
            else if (risknivå == 2)
            {
                Random generator = new Random();
                ränta = generator.Next(-300, 300) / 100.0;
            }
            else if (risknivå == 3)
            {
                Random generator = new Random();
                ränta = generator.Next(-400, 400) / 100.0;
            }
            return ränta;
        }

    }
}
