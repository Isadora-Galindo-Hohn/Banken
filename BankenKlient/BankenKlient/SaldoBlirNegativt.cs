using System;
namespace BankenKlient
{
    class SaldoBlirNegativt : Exception
    {
        public SaldoBlirNegativt() 
        {
            Console.WriteLine("Saldo blir negativt!");
        }

    }
}
