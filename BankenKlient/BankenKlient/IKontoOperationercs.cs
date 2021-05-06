using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankenKlient
{
    interface IKontoOperationer
    {
        void Uttag(double summa);

        void Insättning(double summa);

    }
}
