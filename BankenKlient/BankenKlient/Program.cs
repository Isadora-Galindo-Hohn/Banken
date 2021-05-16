using System;
using System.Text;
using System.Xml;
using System.Net.Sockets;

namespace BankenKlient
{
    class Program
    {
        static void Main(string[] args)
        {
            Lista<Kund> kunder = new Lista<Kund>();
            Kund inloggadKund = null;
            bool loggatUt = false;
            try
            {
                kunder = TaEmotKunder();
            }
            catch (Exception)
            {
                Console.WriteLine("Något gick fel kontakta kundservice försök igen senare");
                Console.ReadLine();
                loggatUt = true;
            }

            while (loggatUt == false)
            {
                //meny som består av en switch som skickar använderen till olika metoder beroende på vilket case som anropas
                string menyVal = "";

                while (inloggadKund == null || menyVal != "5" || loggatUt == true)
                {
                    Console.Clear();
                    Console.WriteLine();
                    Console.WriteLine("----------------------------------------------------");
                    Console.WriteLine("                     MENY                           ");
                    Console.WriteLine("----------------------------------------------------");
                    Console.WriteLine();
                    Console.WriteLine("1. Skapa ny kund");
                    Console.WriteLine("2. Logga in");
                    Console.WriteLine("3. Visa alla kunder");
                    Console.WriteLine("4. Radera kund");
                    Console.WriteLine("5. Spara och avsluta programmet");
                    Console.WriteLine("Skriv in siffra mellan 1-3 och tryck sedan Enter...");
                    Console.WriteLine();

                    menyVal = Console.ReadLine();

                    Console.Clear();
                    switch (menyVal)
                    {
                        case "1":
                            inloggadKund = SkapaNyKund(kunder);
                            if (inloggadKund != null)
                            {
                                kunder.Add(inloggadKund);
                            }
                            break;

                        case "2":
                            inloggadKund = LoggaIn(kunder);
                            break;

                        case "3":
                            if (kunder.Length() < 1)
                            {
                                Console.WriteLine("Det finns inga konton.");
                                Console.ReadLine();
                            }
                            else
                            {
                                VisaKunder(kunder);
                            }
                            break;

                        case "4":
                            if (kunder.Length() < 1)
                            {
                                Console.WriteLine("Det finns inga konton att radera.");
                                Console.ReadLine();
                            }
                            else
                            {
                                kunder = RaderaKund(kunder);
                            }
                            break;

                        case "5":
                            loggatUt = SkickaKunderTillServer(kunder);
                            if (loggatUt == false)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Du lyckades inte logga ut.");
                                Console.ReadLine();
                            }
                            break;

                        default: //default gör att så läge stringen inte är lika med 1-3 går den hit och visar menyn igen
                            break;
                    }
                    break;
                }
                if (inloggadKund != null)
                {
                    while (inloggadKund != null || menyVal != "7")
                    {
                        Console.Clear();
                        Console.WriteLine();
                        Console.WriteLine("----------------------------------------------------");
                        Console.WriteLine("                      Konton                        ");
                        Console.WriteLine("----------------------------------------------------");
                        Console.WriteLine();
                        Console.WriteLine("1. Skapa nytt konto");
                        Console.WriteLine("2. Visa konton");
                        Console.WriteLine("3. Se kontohändelser och saldo");
                        Console.WriteLine("4. Gör konto insättning");
                        Console.WriteLine("5. Gör konto uttag");
                        Console.WriteLine("6. Radera konto");
                        Console.WriteLine("7. Återgå till huvudmeny och logga ut");
                        Console.WriteLine("Skriv in siffra mellan 1-6 och tryck sedan Enter...");
                        Console.WriteLine();

                        menyVal = Console.ReadLine();

                        Console.Clear();
                        switch (menyVal)
                        {
                            case "1":
                                kunder = SkapaNyttKonto(kunder, inloggadKund);
                                break;

                            case "2":
                                if (inloggadKund.FåKontolista.Length() < 1)
                                {
                                    Console.WriteLine("Du har inga konton.");
                                    Console.ReadLine();
                                }
                                else
                                {
                                    VisaKonton(inloggadKund);
                                }
                                break;

                            case "3":
                                if (inloggadKund.FåKontolista.Length() < 1)
                                {
                                    Console.WriteLine("Du har inga konton.");
                                    Console.ReadLine();
                                }
                                else
                                {
                                    VisaKontoHistorik(inloggadKund);
                                }
                                break;

                            case "4":
                                if (inloggadKund.FåKontolista.Length() < 1)
                                {
                                    Console.WriteLine("Du har inga konton.");
                                    Console.ReadLine();
                                }
                                else
                                {
                                    kunder = KontoInsättningar(kunder, inloggadKund);
                                }
                                break;

                            case "5":
                                if (inloggadKund.FåKontolista.Length() < 1)
                                {
                                    Console.WriteLine("Du har inga konton.");
                                    Console.ReadLine();
                                }
                                else
                                {
                                    kunder = KontoUttag(kunder, inloggadKund);
                                }
                                break;

                            case "6":
                                if (inloggadKund.FåKontolista.Length() < 1)
                                {
                                    Console.WriteLine("Du har inga konton.");
                                    Console.ReadLine();
                                }
                                else
                                {
                                    kunder = RaderaKonto(kunder, inloggadKund);
                                }
                                break;

                            case "7":
                                inloggadKund = null;
                                break;

                            default: //default gör att så läge stringen inte är lika med 1-3 går den hit och visar menyn igen
                                break;
                        }
                    }
                }
            }
            if (loggatUt == true)
            {
                Console.WriteLine("Du har loggat ut!");
                Console.ReadLine();
            }
        }

        static Kund SkapaNyKund(Lista<Kund> kunder)
        {
            Kund nyKund = null;
            string menyVal = "";
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine("                 SKAPA EN NY KUND                   ");
            Console.WriteLine("            Hur vill du skapa en kund?              ");
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("1. Med personnummer");
            Console.WriteLine("2. Med namn");
            Console.WriteLine("3. Gå tillbaka till menyn");
            Console.WriteLine("Skriv in siffra mellan 1-3 och tryck sedan Enter...");
            Console.WriteLine();

            menyVal = Console.ReadLine();

            bool anvädareExisterar = false;

            Console.Clear();
            switch (menyVal)
            {
                case "1":
                    Console.WriteLine("Personnummret ska vara skrivet i siffror och max 12 siffror långt. ex. 200211041111");
                    Console.Write("Personnummer: ");
                    string text = Console.ReadLine();
                    Console.WriteLine();
                    long p; // då personummret ska kunna vara 12 siffror långt(int tar bara 10 och första måste vara högts 2)
                    bool lyckad = long.TryParse(text, out p);

                    if (lyckad == false || p.ToString().Length > 12)
                    {
                        while (lyckad == false || p.ToString().Length > 12 || p == 0)
                        {
                            Console.WriteLine("Personnummret ska vara skrivet i siffror och max 12 siffror långt. ex. 200211041111");
                            Console.WriteLine("Personnummret får inte vara 0");
                            Console.Write("Personnummer: ");
                            text = Console.ReadLine();
                            Console.WriteLine();
                            lyckad = long.TryParse(text, out p);
                        }
                    }

                    for (int i = 0; i < kunder.Length(); i++)
                    {
                        if (p == kunder[i].FåPersonnummer)
                        {
                            anvädareExisterar = true;
                        }
                    }

                    if (anvädareExisterar == true)
                    {
                        Console.WriteLine("Det personnummert är redan användt");
                    }
                    else
                    {
                        nyKund = new Kund(p);
                        Console.WriteLine("Du har nu skapat och loggat in med personnummret " + p + " med kundnummret " + nyKund.FåKundnummer + "!");
                    }
                    Console.ReadLine();
                    break;

                case "2":
                    Console.Write("Namn: ");

                    string n = (Console.ReadLine());

                    nyKund = new Kund(n);
                    Console.WriteLine("Du har nu skapat och loggat in med namnet " + n + " med kundnummret " + nyKund.FåKundnummer + "!");

                    Console.ReadLine();
                    break;

                default: //default gör att så läge stringen inte är lika med 1-2 går den hit och visar menyn igen
                    break;
            }

            return nyKund;
        }

        static Kund LoggaIn(Lista<Kund> kunder)
        {
            Kund inloggadKund = null;
            string kundnummer = "";
            string menyVal = "";
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine("                     Logga in                       ");
            Console.WriteLine("      Välkommen tillbaka hur vill du logga in?      ");
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("1. Med personnummer");
            Console.WriteLine("2. Med namn");
            Console.WriteLine("3. Gå tillbaka till menyn");
            Console.WriteLine("Skriv in siffra mellan 1-3 och tryck sedan Enter...");
            Console.WriteLine();

            menyVal = Console.ReadLine();
            bool anvädareExisterar = false;

            Console.Clear();
            switch (menyVal)
            {
                case "1":
                    Console.WriteLine("Personnummret ska vara skrivet i siffror och max 12 siffror långt. ex. 200211041111");
                    Console.Write("Personnummer: ");
                    string text = Console.ReadLine();
                    Console.WriteLine();
                    long p; // då personummret ska kunna vara 12 siffror långt(int tar bara 10 och första måste vara högts 2)
                    bool lyckad = long.TryParse(text, out p);

                    if (lyckad == false || p.ToString().Length > 12)
                    {
                        while (lyckad == false || p.ToString().Length > 12 || p == 0)
                        {
                            Console.WriteLine("Personnummret ska vara skrivet i siffror och max 12 siffror långt. ex. 200211041111");
                            Console.WriteLine("Personnummret får inte vara 0");
                            Console.Write("Personnummer: ");
                            text = Console.ReadLine();
                            Console.WriteLine();
                            lyckad = long.TryParse(text, out p);
                        }
                    }

                    for (int i = 0; i < kunder.Length(); i++)
                    {
                        if (p == kunder[i].FåPersonnummer)
                        {
                            anvädareExisterar = true;
                            inloggadKund = kunder[i];
                            kundnummer = inloggadKund.FåKundnummer;
                        }
                    }

                    if (anvädareExisterar == true)
                    {
                        Console.WriteLine("Du är nu inloggad med personnummret " + p + " med kundnummret " + kundnummer + "!");

                    }
                    else
                    {
                        Console.WriteLine("Det finns ingen kund med personnumret " + p);
                    }
                    Console.ReadLine();

                    break;

                case "2":
                    Console.Write("Namn: ");

                    string n = (Console.ReadLine());
                    bool namnAnvänt = false;
                    Lista<Kund> kunderMedSammaNamn = new Lista<Kund>();
                    int j = 1;

                    for (int i = 0; i < kunder.Length(); i++)
                    {
                        if (n == kunder[i].FåNamn)
                        {
                            namnAnvänt = true;
                            Console.WriteLine(j + ". " + kunder[i].FåNamn + ":\t" + kunder[i].FåKundnummer);
                            kunderMedSammaNamn.Add(kunder[i]);
                            j++;
                        }
                    }

                    if (namnAnvänt == true)
                    {
                        Console.Write("Välj en kund: ");
                        int index = int.Parse(Console.ReadLine());
                        inloggadKund = kunderMedSammaNamn[index - 1];
                        Console.WriteLine();
                        Console.WriteLine("Du är nu inloggad som " + n + " med kundnummret " + inloggadKund.FåKundnummer + " !");
                        kundnummer = inloggadKund.FåKundnummer;
                    }
                    else
                    {
                        Console.WriteLine("Det finns tyvärr ingen kund med namnet " + n);
                    }
                    Console.ReadLine();
                    break;

                default: //default gör att så läge stringen inte är lika med 1-2 går den hit och visar menyn igen
                    break;
            }
            return inloggadKund;
        }

        static Lista<Kund> SkapaNyttKonto(Lista<Kund> kunder, Kund inloggadKund)
        {
            string menyVal = "";
            string saldoIText;
            double saldo = 0;
            double lön = 0;
            int risk = 0;
            string kontonamn = "";

            while (menyVal != "5")
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine("                   Välj kontotyp                    ");
                Console.WriteLine("----------------------------------------------------");
                Console.WriteLine();
                Console.WriteLine("1. Debitkonto");
                Console.WriteLine("2. Lönekonto");
                Console.WriteLine("3. Sparkonto");
                Console.WriteLine("4. Fondkonto");
                Console.WriteLine("5. Gå tillbaka till menyn");
                Console.WriteLine("Skriv in siffra mellan 1-5 och tryck sedan Enter...");
                Console.WriteLine();

                menyVal = Console.ReadLine();

                Console.Clear();
                switch (menyVal)
                {
                    case "1":
                        Console.WriteLine("Skapa ett debitkonto!");
                        Console.Write("Kontonamn: ");
                        kontonamn = Console.ReadLine();

                        Console.Write("Saldo: ");

                        saldoIText = Console.ReadLine();
                        bool lyckad = double.TryParse(saldoIText, out saldo);

                        if (lyckad == false || saldo < 0)
                        {
                            while (lyckad == false || saldo < 0)
                            {
                                Console.WriteLine("Saldot ska vara skrivet med siffror och måste vara större än 0");
                                Console.Write("Saldo: ");
                                saldoIText = Console.ReadLine();
                                lyckad = double.TryParse(saldoIText, out saldo);
                            }
                        }

                        inloggadKund.SkapaNyttKonto("Debitkonto", kontonamn, saldo);
                        Console.WriteLine("Du har nu skapat ett debitkonto med kontonummret " + inloggadKund.FåNyasteKonto.FåKontoNummer + "!");
                        Console.ReadLine();
                        break;

                    case "2":
                        Console.WriteLine("Skapa ett lönekonto!");
                        Console.Write("Kontonamn: ");
                        kontonamn = Console.ReadLine();

                        Console.Write("Lön: ");
                        saldoIText = Console.ReadLine();
                        lyckad = double.TryParse(saldoIText, out lön);

                        if (lyckad == false || saldo < 0)
                        {
                            while (lyckad == false || saldo < 0)
                            {
                                Console.WriteLine("Lönen ska vara skrivet med siffror och måste vara större än 0");
                                Console.Write("Lön: ");
                                saldoIText = Console.ReadLine();
                                lyckad = double.TryParse(saldoIText, out lön);
                            }
                        }

                        inloggadKund.SkapaNyttKonto("Lönekonto", kontonamn, lön, lön);
                        Console.WriteLine("Du har nu skapat ett lönekonto med kontonummret " + inloggadKund.FåNyasteKonto.FåKontoNummer + "!");
                        Console.ReadLine();
                        break;

                    case "3":
                        Console.WriteLine("Skapa ett sparkonto!");
                        Console.Write("Kontonamn: ");
                        kontonamn = Console.ReadLine();

                        Console.Write("Saldo: ");
                        saldoIText = Console.ReadLine();
                        lyckad = double.TryParse(saldoIText, out saldo);
                        if (lyckad == false || saldo < 0)
                        {
                            while (lyckad == false || saldo < 0)
                            {
                                Console.WriteLine("Saldot ska vara skrivet med siffror och måste vara större än 0");
                                Console.Write("Saldo: ");
                                saldoIText = Console.ReadLine();
                                lyckad = double.TryParse(saldoIText, out saldo);
                            }
                        }

                        inloggadKund.SkapaNyttKonto("Sparkonto", kontonamn, saldo);
                        Konto nyttKonto = inloggadKund.FåNyasteKonto;
                        Console.WriteLine("Du har nu skapat ett sparkonto med kontonummret " + inloggadKund.FåNyasteKonto.FåKontoNummer + " och har en ränta på 3%!");
                        Console.ReadLine();
                        break;

                    case "4":
                        Console.WriteLine("Skapa ett Fondkonto!");
                        Console.Write("Kontonamn: ");
                        kontonamn = Console.ReadLine();
                        Console.WriteLine();

                        Console.WriteLine("Välj risk alternativ:");
                        Console.WriteLine("1. Risk 1, +-1% vinst");
                        Console.WriteLine("2. Risk 2, +-2% vinst ");
                        Console.WriteLine("3. Risk 3, +-3% vinst ");
                        Console.Write("Risk: ");

                        saldoIText = Console.ReadLine();
                        lyckad = int.TryParse(saldoIText, out risk);

                        if (lyckad == false || risk != 1 && risk != 2 && risk != 3)
                        {
                            while (lyckad == false || risk != 1 && risk != 2 && risk != 3)
                            {
                                Console.WriteLine("Välj risk alternativ:");
                                Console.WriteLine("1. Risk 1, +-10% vinst");
                                Console.WriteLine("2. Risk 2, +-20% vinst ");
                                Console.WriteLine("3. Risk 3, +-30% vinst ");
                                Console.Write("Risk: ");
                                saldoIText = Console.ReadLine();
                                lyckad = int.TryParse(saldoIText, out risk);
                            }
                        }

                        Console.Write("Saldo: ");

                        saldoIText = Console.ReadLine();
                        lyckad = double.TryParse(saldoIText, out saldo);
                        if (lyckad == false || saldo < 0)
                        {
                            while (lyckad == false || saldo < 0)
                            {
                                Console.WriteLine("Saldot ska vara skrivet med siffror och måste vara större än 0");
                                Console.Write("Saldo: ");

                                saldoIText = Console.ReadLine();
                                lyckad = double.TryParse(saldoIText, out saldo);
                            }
                        }

                        inloggadKund.SkapaNyttKonto("Fondkonto", kontonamn, saldo, risk);
                        Console.WriteLine("Du har nu skapat ett fondkonto med kontonummret " + inloggadKund.FåNyasteKonto.FåKontoNummer + " och risk " + risk + "!");
                        Console.ReadLine();
                        break;

                    default: //default gör att så läge stringen inte är lika med 1-3 går den hit och visar menyn igen
                        break;
                }
            }

            for (int i = 0; i < kunder.Length(); i++)
            {
                if (kunder[i].FåKundnummer == inloggadKund.FåKundnummer)
                {
                    kunder[i] = inloggadKund;
                }
            }

            return kunder;
        }

        static void VisaKonton(Kund inloggadKund)
        {
            Console.WriteLine("Kontonamn: \t\tSaldo: ");
            Console.WriteLine("---------------------------------------------------------------");
            inloggadKund.SkrivUtKonton();
            Console.ReadLine();
        }

        static void VisaKontoHistorik(Kund inloggadKund)
        {
            Console.WriteLine("Vilket konto vill du se?");
            Console.WriteLine();
            Konto konto;

            inloggadKund.SkrivUtKonton();

            string textIndex = Console.ReadLine();
            int index;
            bool lyckad = int.TryParse(textIndex, out index);

            if (lyckad == false || index > inloggadKund.FåKontolista.Length())
            {
                while (lyckad == false || index > inloggadKund.FåKontolista.Length())
                {
                    Console.WriteLine("Ange ett konto som t.ex. 1.");
                    textIndex = Console.ReadLine();
                    Console.WriteLine();
                    lyckad = int.TryParse(textIndex, out index);
                }
            }

            konto = inloggadKund.FåKontolista[index - 1];

            konto.SkrivUtKontoHistorik();
            Console.ReadLine();
        }

        static Lista<Kund> KontoInsättningar(Lista<Kund> kunder, Kund inloggadKund)
        {
            Console.WriteLine("Vilket konto vill du sätta in pengar på?");
            Console.WriteLine();
            Konto konto;

            inloggadKund.SkrivUtKonton();

            string text = Console.ReadLine();
            int index;
            bool lyckad = int.TryParse(text, out index);

            if (lyckad == false || index > inloggadKund.FåKontolista.Length())
            {
                while (lyckad == false || index > inloggadKund.FåKontolista.Length())
                {
                    Console.WriteLine("Fel"); //fixa
                    text = Console.ReadLine();
                    Console.WriteLine();
                    lyckad = int.TryParse(text, out index);
                }
            }

            konto = inloggadKund.FåKontolista[index - 1];

            if (konto.FåKontotyp == "Debitkonto")
            {
                Console.Write("Insättning: ");
                text = Console.ReadLine();
                double insättning;
                lyckad = double.TryParse(text, out insättning);

                if (lyckad == false)
                {
                    while (lyckad == false)
                    {
                        Console.WriteLine("Skriv insättningen med siffror t.ex. 20 för 20 kr");
                        Console.Write("Insättning: ");
                        text = Console.ReadLine();
                        Console.WriteLine();
                        lyckad = double.TryParse(text, out insättning);
                    }
                }

                konto.Insättning(insättning);
                Console.WriteLine("Du har nu gjort en insättning på " + insättning + "kr på " + konto.FåKontoNamn + "!");
                Console.ReadLine();
            }
            else if (konto.FåKontotyp == "Lönekonto")
            {
                ((Lönekonto)konto).GeUtLön();
                Console.WriteLine("Lönen på " + ((Lönekonto)konto).FåLön + "kr har givts ut på " + konto.FåKontoNamn + "!");
                Console.WriteLine("Ditt nya saldo är " + konto.FåKontoSaldo);
                Console.ReadLine();
            }
            else if (konto.FåKontotyp == "Sparkonto")
            {
                ((Sparkonto)konto).ImplementeraRänta();
                Console.WriteLine("Räntan har implementerats med en ränta på " + ((Sparkonto)konto).FåRänta + "% på kontot " + konto.FåKontoNamn + "!");
                Console.WriteLine("Ditt nya saldo är " + konto.FåKontoSaldo);
                Console.ReadLine();
            }
            else if (konto.FåKontotyp == "Fondkonto")
            {
                ((Fondkonto)konto).ImplementeraRänta();
                Console.WriteLine("Räntan har implementerats på kontot " + konto.FåKontoNamn + "!");
                Console.WriteLine("Ditt nya saldo är " + konto.FåKontoSaldo);
                Console.ReadLine();
            }

            for (int i = 0; i < kunder.Length(); i++)
            {
                if (kunder[i].FåKundnummer == inloggadKund.FåKundnummer)
                {
                    kunder[i] = inloggadKund;
                }
            }

            return kunder;
        }

        static Lista<Kund> KontoUttag(Lista<Kund> kunder, Kund inloggadKund)
        {
            try
            {
                Console.WriteLine("Vilket konto vill du ta ut pengar från?");
                Console.WriteLine();
                Konto konto;

                inloggadKund.SkrivUtKonton();

                string text = Console.ReadLine();
                int index;
                bool lyckad = int.TryParse(text, out index);

                if (lyckad == false || index > inloggadKund.FåKontolista.Length())
                {
                    while (lyckad == false || index > inloggadKund.FåKontolista.Length())
                    {
                        Console.WriteLine("Ange ett konto");
                        text = Console.ReadLine();
                        Console.WriteLine();
                        lyckad = int.TryParse(text, out index);
                    }
                }

                konto = inloggadKund.FåKontolista[index - 1];

                Console.Write("Uttag: ");
                text = Console.ReadLine();
                double uttag;
                lyckad = double.TryParse(text, out uttag);

                if (lyckad == false)
                {
                    while (lyckad == false)
                    {
                        Console.WriteLine("Skriv uttaget med siffror t.ex. 20 för 20 kr");
                        Console.Write("Uttag: ");
                        text = Console.ReadLine();
                        Console.WriteLine();
                        lyckad = double.TryParse(text, out uttag);
                    }
                }

                ValideraSaldo(uttag, konto);
                konto.Uttag(uttag);
                Console.WriteLine("Du har tagit ut " + uttag + " kr på " + konto.FåKontoNamn + "!");
                Console.ReadLine();

                for (int i = 0; i < kunder.Length(); i++)
                {
                    if (kunder[i].FåKundnummer == inloggadKund.FåKundnummer)
                    {
                        kunder[i] = inloggadKund;
                    }
                }

            }
            catch (SaldoBlirNegativt e)
            {
                Console.WriteLine(e);
                Console.ReadLine();
            }

            return kunder;
        }

        static Lista<Kund> RaderaKonto(Lista<Kund> kunder, Kund inloggadKund)
        {
            Console.WriteLine("Vilket konto vill du radera?");
            Console.WriteLine();

            inloggadKund.SkrivUtKonton();

            string text = Console.ReadLine();
            int index;
            bool lyckad = int.TryParse(text, out index);

            if (lyckad == false || index > inloggadKund.FåKontolista.Length())
            {
                while (lyckad == false || index > inloggadKund.FåKontolista.Length())
                {
                    Console.WriteLine("Ange ett konto");
                    text = Console.ReadLine();
                    Console.WriteLine();
                    lyckad = int.TryParse(text, out index);
                }
            }

            Console.WriteLine("Du har nu raderat kontot " + inloggadKund.FåKontolista[index - 1].FåKontoNamn + ".");
            inloggadKund.FåKontolista.Remove(index - 1);
            Console.ReadLine();

            for (int i = 0; i < kunder.Length(); i++)
            {
                if (kunder[i].FåKundnummer == inloggadKund.FåKundnummer)
                {
                    kunder[i] = inloggadKund;
                }
            }

            return kunder;
        }

        static void VisaKunder(Lista<Kund> kunder)
        {
            for (int i = 0; i < kunder.Length(); i++)
            {
                kunder[i].SkrivUtKund();
            }

            Console.ReadLine();
        }

        static Lista<Kund> RaderaKund(Lista<Kund> kunder)
        {
            Console.WriteLine("Vilken kund vill du radera?");
            Console.WriteLine();

            for (int i = 0; i < kunder.Length(); i++)
            {
                Console.WriteLine((i + 1) + ": " + kunder[i].FåKundnummer);
            }

            string text = Console.ReadLine();
            int index;
            bool lyckad = int.TryParse(text, out index);

            if (lyckad == false || index > kunder.Length())
            {
                while (lyckad == false || index > kunder.Length())
                {
                    Console.WriteLine("Ange en kund");
                    text = Console.ReadLine();
                    Console.WriteLine();
                    lyckad = int.TryParse(text, out index);
                }
            }

            Console.WriteLine("Du har nu raderat kunden " + kunder[index - 1].FåKundnummer + ".");
            kunder.Remove(index - 1);
            Console.ReadLine();

            return kunder;
        }

        static void ValideraSaldo(double summa, Konto konto)
        {
            if ((konto.FåKontoSaldo - summa) < 0)
            {
                throw new SaldoBlirNegativt();
            }
        }

        static bool SkickaKunderTillServer(Lista<Kund> kunder)
        {
            SkickaValTillServer("2");
            bool lyckad = false;
            try //Försöker ansluta till servern om det inte fungerar går det vidare till exception
            {
                string address = "127.0.0.1"; // är en local host
                int port = 8001;

                TcpClient tcpClient = new TcpClient();
                tcpClient.Connect(address, port);

                NetworkStream tcpStream = tcpClient.GetStream();

                XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                xmlWriterSettings.Indent = true;
                xmlWriterSettings.NewLineOnAttributes = true;

                using (XmlWriter xmlWriter = XmlWriter.Create(tcpStream, xmlWriterSettings))
                {
                    //Skapar layouten och tillsätter den första användaren
                    xmlWriter.WriteStartDocument();
                    xmlWriter.WriteStartElement("kunder");

                    for (int i = 0; i < kunder.Length(); i++)
                    {
                        xmlWriter.WriteStartElement("kund");

                        xmlWriter.WriteElementString("namn", kunder[i].FåNamn);
                        xmlWriter.WriteElementString("personnummer", kunder[i].FåPersonnummer.ToString());
                        xmlWriter.WriteElementString("kundnummer", kunder[i].FåKundnummer);

                        xmlWriter.WriteStartElement("konton");
                        for (int j = 0; j < kunder[i].FåKontolista.Length(); j++)
                        {
                            xmlWriter.WriteStartElement("konto");
                            xmlWriter.WriteElementString("kontotyp", kunder[i].FåKontolista[j].FåKontotyp);
                            xmlWriter.WriteElementString("kontonamn", kunder[i].FåKontolista[j].FåKontoNamn);
                            xmlWriter.WriteElementString("kontonummer", kunder[i].FåKontolista[j].FåKontoNummer);
                            xmlWriter.WriteElementString("saldo", kunder[i].FåKontolista[j].FåKontoSaldo.ToString());

                            if (kunder[i].FåKontolista[j] is Fondkonto)
                            {
                                xmlWriter.WriteElementString("risknivå", ((Fondkonto)kunder[i].FåKontolista[j]).FåRisk.ToString());
                            }
                            else if (kunder[i].FåKontolista[j] is Lönekonto)
                            {
                                xmlWriter.WriteElementString("lön", ((Lönekonto)kunder[i].FåKontolista[j]).FåLön.ToString());
                            }
                            else if (kunder[i].FåKontolista[j] is Sparkonto)
                            {
                                xmlWriter.WriteElementString("ränta", ((Sparkonto)kunder[i].FåKontolista[j]).FåRänta.ToString());
                            }

                            xmlWriter.WriteStartElement("kontohistorik");
                            xmlWriter.WriteElementString("kontonummer", kunder[i].FåKontolista[j].FåKontoNummer);
                            for (int k = 0; k < kunder[i].FåKontolista[j].FåKontohistorik.Length(); k++)
                            {
                                xmlWriter.WriteElementString("kontohändelse", kunder[i].FåKontolista[j].FåKontohistorik[k]);
                            }
                            xmlWriter.WriteEndElement();

                            xmlWriter.WriteEndElement();
                        }
                        xmlWriter.WriteEndElement();

                        xmlWriter.WriteEndElement();
                    }

                    xmlWriter.WriteEndDocument();
                    xmlWriter.Flush(); //Sickas iväg
                    xmlWriter.Close();
                }

                tcpClient.Close();
                lyckad = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e);
            }
            return lyckad;
        }

        static Lista<Kund> TaEmotKunder()
        {
            SkickaValTillServer("1");
            Lista<Kund> kunder = new Lista<Kund>();

            string address = "127.0.0.1"; // är en local host
            int port = 8001;

            TcpClient tcpClient = new TcpClient();
            tcpClient.Connect(address, port);

            using (NetworkStream stream = tcpClient.GetStream())
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(stream);

                XmlNodeList kunderna = xmlDoc.SelectNodes("kunder/kund");
                XmlNodeList konton = xmlDoc.SelectNodes("kunder/kund/konton/konto");
                XmlNodeList kontohistoriken = xmlDoc.SelectNodes("kunder/kund/konton/konto/kontohistorik");

                //Skapa ett Book-element för varje nod och lägg i media listan:
                foreach (XmlNode kund in kunderna)
                {
                    string namn = kund.SelectSingleNode("namn").InnerText;
                    long personnummer = long.Parse(kund.SelectSingleNode("personnummer").InnerText);
                    string kundnummer = kund.SelectSingleNode("kundnummer").InnerText;
                    Kund tempK = new Kund(namn, personnummer, kundnummer);

                    foreach (XmlNode konto in konton)
                    {
                        string kontonummer = konto.SelectSingleNode("kontonummer").InnerText;
                        string[] kundnummerArr = kontonummer.Split('-');

                        if (kundnummerArr[0] == kundnummer)
                        {
                            string kontotyp = konto.SelectSingleNode("kontotyp").InnerText;
                            string kontonamn = konto.SelectSingleNode("kontonamn").InnerText;
                            double saldo = double.Parse(konto.SelectSingleNode("saldo").InnerText);

                            if (kontotyp == "Lönekonto")
                            {
                                double lön = double.Parse(konto.SelectSingleNode("lön").InnerText);
                                tempK.SkapaNyttKonto(kontotyp, kontonamn, saldo, lön);
                            }
                            else if (kontotyp == "Fondkonto")
                            {
                                double risknivå = double.Parse(konto.SelectSingleNode("risknivå").InnerText);
                                tempK.SkapaNyttKonto(kontotyp, kontonamn, saldo, risknivå);
                            }
                            else
                            {
                                tempK.SkapaNyttKonto(kontotyp, kontonamn, saldo);
                            }

                            foreach (XmlNode kontohistorik in kontohistoriken)
                            {
                                if (kontohistorik.ParentNode.SelectSingleNode("kontonummer").InnerText == kontonummer)
                                {
                                    for (int i = 1; i < kontohistorik.ChildNodes.Count; i++)
                                    {
                                        string händelse = kontohistorik.ChildNodes[i].InnerText;
                                        tempK.FåNyasteKonto.LäggTillKontoHändelse(händelse);

                                    }

                                }
                            }
                        }
                    }
                    kunder.Add(tempK);
                }
            }
            return kunder;
        }

        static void SkickaValTillServer(string val)
        {
            try //Försöker ansluta till servern om det inte fungerar går det vidare till exception
            {
                string address = "127.0.0.1"; // är en local host
                int port = 8001;

                TcpClient tcpClient = new TcpClient();
                tcpClient.Connect(address, port);

                byte[] menyValByte = Encoding.Unicode.GetBytes(val);

                //Sickar iväg menyvalet till servern
                NetworkStream tcpStream = tcpClient.GetStream();
                tcpStream.Write(menyValByte, 0, menyValByte.Length);

                // Stäng anslutningen:
                tcpClient.Close();
            }
            catch (Exception e)//felmedelande ifall servern inte svarar
            {
                Console.WriteLine("Error: " + e.Message);
            }

        }
    }
}
