﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Xml;
using System.Net.Sockets;

namespace BankenKlient
{
    class Program
    {
        static void Main(string[] args)
        {

            Lista<Kund> kunder = new Lista<Kund>();

            while (true)
            {
                try
                {
                    //meny som består av en switch som skickar använderen till olika metoder beroende på vilket case som anropas
                    string menyVal = "";
                    Kund inloggadKund = null;


                    while (menyVal != "5")
                    {
                        Console.Clear();
                        Console.WriteLine();
                        Console.WriteLine("----------------------------------------------------");
                        Console.WriteLine("                     MENY                           ");
                        Console.WriteLine("----------------------------------------------------");
                        Console.WriteLine();
                        Console.WriteLine("1. Skapa ny kund");
                        Console.WriteLine("2. Logga in");
                        Console.WriteLine("3. Konto operationer");
                        Console.WriteLine("4. Logga ut");
                        Console.WriteLine("5. Avsluta programmet");
                        Console.WriteLine("Skriv in siffra mellan 1-5 och tryck sedan Enter...");
                        Console.WriteLine();

                        menyVal = Console.ReadLine();

                        Console.Clear();
                        switch (menyVal)
                        {
                            case "1":
                                kunder = SkapaNyKund(kunder);
                                break;

                            case "2":
                                if (inloggadKund == null)
                                {
                                    Console.WriteLine("Du har redan loggat in.");
                                    Console.WriteLine("Om du vill logga ut måste du först logga ut.");
                                    Console.WriteLine("Tryck på enter för att gå vidare till menyn.");
                                    Console.ReadLine();
                                }
                                else
                                {
                                    inloggadKund = LoggaIn(kunder);
                                }
                                break;

                            case "3":
                                if (inloggadKund == null)
                                {

                                }
                                else
                                {
                                    Console.WriteLine("Du måste logga in för att göra konto operationer");
                                    Console.WriteLine("Tryck på enter för att gå vidare till menyn.");
                                    Console.ReadLine();
                                }
                                break;

                            case "4":
                                Console.WriteLine("Du är nu utloggad");
                                inloggadKund = null;
                                Console.ReadLine();
                                break;

                            default: //default gör att så läge stringen inte är lika med 1-3 går den hit och visar menyn igen
                                break;
                        }
                    }

                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
            }
        }

        static Lista<Kund> SkapaNyKund(Lista<Kund> kunder)
        {
            string menyVal = "";
            while (menyVal != "3")
            {
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
                        Console.Write("Personnummer: ");

                        int p = int.Parse(Console.ReadLine());

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
                            kunder.Add(new Kund(p));
                            Console.WriteLine("En ny kund med personnummret " + p + " har skapats!");
                            Console.WriteLine("Ditt kundnummer är " + kunder[kunder.Length() - 1].FåKundnummer);
                        }
                        Console.ReadLine();
                        break;

                    case "2":
                        Console.Write("Namn: ");

                        string n = (Console.ReadLine());

                        kunder.Add(new Kund(n));
                        Console.WriteLine("En ny kund med personnummret " + n + " har skapats!");
                        Console.WriteLine("Ditt kundnummer är " + kunder[kunder.Length() - 1].FåKundnummer);

                        Console.ReadLine();
                        break;

                    default: //default gör att så läge stringen inte är lika med 1-2 går den hit och visar menyn igen
                        break;
                }
            }
            return kunder;
        }

        static Kund LoggaIn(Lista<Kund> kunder)
        {
            Kund inloggadKund = null;
            string kundnummer = "";
            string menyVal = "";
            while (menyVal != "3")
            {
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
                        Console.Write("Personnummer: ");

                        int p = int.Parse(Console.ReadLine());

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
                            Console.WriteLine("Du är nu inloggad som " + p + " med kundnummret " + kundnummer + "!");
                        }
                        else
                        {
                            Console.WriteLine("Det finns ingen kund med personnumret" + p);
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
            }
            return inloggadKund;
        }

        static Lista<Kund> KontoOperationer(Lista<Kund> kunder, Kund inloggadKund)
        {
            string menyVal = "";


            while (menyVal != "6")
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
                Console.WriteLine("4. Gör konto uttag");
                Console.WriteLine("5. Gör konto insättning");
                Console.WriteLine("6. Gå tillbaka till menyn");
                Console.WriteLine("Skriv in siffra mellan 1-6 och tryck sedan Enter...");
                Console.WriteLine();

                menyVal = Console.ReadLine();

                Console.Clear();
                switch (menyVal)
                {
                    case "1":

                        break;

                    case "2":
                        if (inloggadKund.FåKontolista.Length() < 0)
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
                        if (inloggadKund.FåKontolista.Length() < 0)
                        {
                            Console.WriteLine("Du har inga konton.");
                            Console.ReadLine();
                        }
                        else
                        {
                            VisaKonton(inloggadKund);
                        }
                        break;

                    case "4":

                        break;

                    case "5":

                        break;

                    default: //default gör att så läge stringen inte är lika med 1-3 går den hit och visar menyn igen
                        break;
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

        }
    }
}