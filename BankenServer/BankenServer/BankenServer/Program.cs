using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Xml;
using System.Net.Sockets;

namespace BankenServer
{
    class Program
    {
        static TcpListener tcpListener;

        static void Main(string[] args)
        {
            // Skapa ett TcpListener-objekt, börja lyssna och vänta på anslutning
            IPAddress myIp = IPAddress.Parse("127.0.0.1");
            tcpListener = new TcpListener(myIp, 8001);
            tcpListener.Start();

            while (true)
            {
                try
                {
                    //meny som består av en switch som skickar använderen till olika metoder beroende på vilket case som anropas
                    string menyVal = "";

                    Console.WriteLine("Väntar på anslutning...");
                    Console.WriteLine("I meny");

                    // Någon försöker ansluta. Acceptera anslutningen
                    Socket socket = tcpListener.AcceptSocket();
                    Console.WriteLine("Anslutning accepterad från " + socket.RemoteEndPoint);

                    // Tag emot metodval
                    byte[] menyValByte = new byte[1000];
                    int menyValByteStorlek = socket.Receive(menyValByte);
                    menyVal = "";

                    for (int i = 0; i < menyValByteStorlek; i++)
                    {
                        if (i % 2 == 0)
                        {
                            menyVal += Convert.ToChar(menyValByte[i]);
                        }
                    }

                    if (menyVal == "1")
                    {
                        SickaKunder();
                    }
                    else if (menyVal == "2")
                    {
                        TaEmotKunder();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                    Console.ReadLine();
                }
            }
        }

        static void SickaKunder()
        {
            try
            {
                Socket socket = tcpListener.AcceptSocket();

                Console.WriteLine("Startades");

                if (File.Exists("kunder.xml"))
                {
                    using (NetworkStream ns = new NetworkStream(socket))
                    {
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.Load("kunder.xml");
                        Console.WriteLine("Save");
                        xmlDoc.Save(ns);
                        Console.WriteLine("Close");
                        ns.Flush();
                        ns.Close();
                    }
                    socket.Close();
                }
                else
                {
                    XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
                    xmlWriterSettings.Indent = true;
                    xmlWriterSettings.NewLineOnAttributes = true;

                    using (XmlWriter xmlWriter = XmlWriter.Create("kunder.xml", xmlWriterSettings))
                    {
                        //Skapar layouten och tillsätter den första användaren
                        xmlWriter.WriteStartDocument();

                        xmlWriter.WriteStartElement("kunder");
                        xmlWriter.WriteEndElement();

                        xmlWriter.WriteEndDocument();
                        xmlWriter.Flush();
                        xmlWriter.Close();
                    }

                    using (NetworkStream ns = new NetworkStream(socket))
                    {
                        XmlDocument xmlDoc = new XmlDocument();
                        xmlDoc.Load("kunder.xml");

                        xmlDoc.Save(ns);

                        ns.Flush();
                        ns.Close();
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Fel: Kunde inte sicka kunder");
                Console.ReadLine();
            }
        }

        static void TaEmotKunder()
        {
            try
            {
                TcpClient c = tcpListener.AcceptTcpClient();

                using (NetworkStream stream = c.GetStream())
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(stream);
                    xmlDoc.Save("kunder.xml");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Fel: Kunde inte ta emot kunder");
                Console.ReadLine();
            }
        }
    }
}
