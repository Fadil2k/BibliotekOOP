using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Bibliotek
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Velkommen til biblioteket!");
            Console.WriteLine("Skriv 'Hjælp' for at se kommandoer");
            List<Forfatter> lstForfatter = loadForfattere(@"/Users/fk/Downloads/Forfattere.csv"); //Husk at ændre
            List<Bog> lstBooks = loadBooks(@"/Users/fk/Downloads/Bøger.csv"); // begge to til dem jeg har vedhæftet med projektet


            while (true)
            { string a = Console.ReadLine();

                if (a.ToLower() == "bøger")
                {
                    foreach (Bog books in lstBooks)
                    {
                        string udlånt;
                        if (books.udlånt)
                        {
                            udlånt = "Udlånt";

                        }
                        else
                        {
                            udlånt = "Ikke udlånt";
                        }

                        string strForfatterFornavn = lstForfatter.Where(x => x.ForfatterID.Equals(books.ForfatterID)).Select(x => x.Fornavn).FirstOrDefault().ToString();
                        string strForfatterEfternavn = lstForfatter.Where(x => x.ForfatterID.Equals(books.ForfatterID)).Select(x => x.Efternavn).FirstOrDefault().ToString();
                        string strForfatter = strForfatterFornavn + " " + strForfatterEfternavn;



                        Console.WriteLine(books.Titel + "af " + strForfatter + "|" + books.udgivelsesår + "|" + udlånt);
                    }
                }

                else if (a.ToLower() == "aflever")
                {
                    bool tryAgain = true;
                    while (tryAgain)
                    {
                        try
                        {
                            Console.WriteLine("Hvilken bog vil du aflevere?");
                            string bog = Console.ReadLine();
                            lstBooks.Single(x => x.Titel == bog).udlånt = false;
                            tryAgain = false;
                            Console.WriteLine("Du har afleveret " + bog);

                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Fejl: Måske har du skrevet forkert?");
                        }
                    }
                }

                else if (a.ToLower() == "lån")
                {
                    bool tryAgain = true;
                    while (tryAgain)
                    { try {
                            Console.WriteLine("Hvilken bog vil du låne?");
                            string bog = Console.ReadLine();
                            lstBooks.Single(x => x.Titel == bog).udlånt = true;
                            tryAgain = false;
                            Console.WriteLine("Du har lånt " + bog);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Fejl: Måske har du skrevet forkert?");

                        }


                    }
                }
                else if (a.ToLower() == "hjælp")
                {
                    Console.WriteLine("Skriv 'bøger' for at få en liste over bøgere og udlåns status.");
                    Console.WriteLine("Skriv 'lån' for at låne en bog.");
                    Console.WriteLine("Skriv 'aflever' for at aflevere en bog.");
                }

                else
                {
                    Console.WriteLine("Ukendt kommando");
                }



            }

        }

        public static List<Forfatter> loadForfattere(string fil)
        {
            List<Forfatter> lstForfatter = new List<Forfatter>();
            var vAllLines = File.ReadAllLines(fil).Skip(1);
            lstForfatter.Clear();

            foreach (var line in vAllLines)
            {
                if (line != "")
                {
                    var vLinesSplit = line.Replace("\"", "").Split(',');
                    lstForfatter.Add(new Forfatter() { ForfatterID = int.Parse(vLinesSplit[0]), Fornavn = vLinesSplit[1], Efternavn = vLinesSplit[2] });
                }
            }

            return lstForfatter;
        }

        public static List<Bog> loadBooks(string fil)
        {
            List<Bog> lstBooks = new List<Bog>();
            var vAllLines = File.ReadAllLines(fil).Skip(1);
            lstBooks.Clear();

            foreach (var line in vAllLines)
            {
                if (line != "")
                {
                    var vLinesSplit = line.Replace("\"", "").Split(',');
                    lstBooks.Add(new Bog() { BogID = int.Parse(vLinesSplit[0]), ForfatterID = int.Parse(vLinesSplit[1]), Titel = vLinesSplit[2], udgivelsesår = int.Parse(vLinesSplit[3]), udlånt = false });
                }
            }

            return lstBooks;
        }
    }

    public class Forfatter
    {
        public int ForfatterID;
        public string Fornavn;
        public string Efternavn;

        public Forfatter(int ForfatterID, string Fornavn, string Efternavn)
        {
            this.ForfatterID = ForfatterID;
            this.Fornavn = Fornavn;
            this.Efternavn = Efternavn;
        }

        public Forfatter()
        {

        }
    }

    public class Bog
    {
        public int BogID;
        public int ForfatterID;
        public string Titel;
        public int udgivelsesår;
        public bool udlånt;
        public Bog(int BogID, int ForfatterID, string Titel, int udgivelsesår, bool udlånt)
        {
            this.BogID = BogID;
            this.ForfatterID = ForfatterID;
            this.Titel = Titel;
            this.udgivelsesår = udgivelsesår;
            this.udlånt = udlånt;
        }

        public Bog()
        {

        }
    }
}


