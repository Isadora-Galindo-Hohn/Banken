using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankenKlient
{
    class Sparkonto : Konto, IRänta
    {
        double ränta = 1.025;

        public Sparkonto(string kontonamn, double saldo, string kundnummer, int kontonummer) :
            base(kontonamn, saldo, kundnummer, kontonummer) { }

        public void ImplemiteraRänta()
        {
            double räntevinst = (saldo * ränta) - saldo;
            saldo += räntevinst;
            kontoHistorik.Add("Räntevinst: " + räntevinst);
        }
    }
}
