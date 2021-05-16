namespace BankenKlient
{
    interface IKontoOperationer
    {
        //Ett interface, tvingar alla klasser där de implementeras att metoden uttag och insättningar ska finnas
        void Uttag(double summa);

        void Insättning(double summa);

    }
}
