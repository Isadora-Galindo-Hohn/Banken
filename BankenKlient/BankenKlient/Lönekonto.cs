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

        public Lönekonto(string kontonamn, double saldo, string kundnummer, int kontonummer, double lön) :
            base(kontonamn, saldo, kundnummer, kontonummer)
        {
            this.lön = lön;
        }

        public void ÄndraLön(double nyLön)
        {
            lön = nyLön;
        }

        public void GeUtLön()
        {
            saldo += lön;
            kontoHistorik.Add("Löneinsättning: \t\t" + lön);
        }
    }
}
