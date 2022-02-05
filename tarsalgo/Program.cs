namespace tarsalgo
{
    public class Tarsalgo
    {
        public int Ora { get; set; }
        public int Perc { get; set; }
        public int Azonosito { get; set; }
        public string Athaladas { get; set; }
    }
    public class Program
    {
        static List<Tarsalgo> tars = new List<Tarsalgo>();
        static int bekertAzonosito = 0;

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            //1. feladat
            Beolvas();

            //2. feladat
            Feladat2();

            //3. feladat
            Feladat3();

            //4. feladat
            Feladat4();

            //5. feladat
            Feladat5();

            //6. feladat
            Feladat6();

            //7. feladat
            Feladat7();

            //8. feladat
            Feladat8();

            Console.ReadLine();
        }

        private static void Beolvas()
        {
            StreamReader sr = new StreamReader(@"ajto.txt");

            while (!sr.EndOfStream)
            {
                string[] sor = sr.ReadLine().Split(' ');
                Tarsalgo szemely = new Tarsalgo();

                szemely.Ora = Convert.ToInt32(sor[0]);
                szemely.Perc = Convert.ToInt32(sor[1]);
                szemely.Azonosito = Convert.ToInt32(sor[2]);
                szemely.Athaladas = sor[3];

                tars.Add(szemely);
            }
        }
        private static void Feladat2() 
        {
            Console.WriteLine("2. feladat");
            var be = tars.Where(x => x.Athaladas.Equals("be"));
            var ki = tars.Where(x => x.Athaladas.Equals("ki"));

            Console.WriteLine("Az első belépő: {0}", be.Select(x => x.Azonosito).FirstOrDefault());
            Console.WriteLine("Az utolsó kilépő: {0}", ki.Select(x => x.Azonosito).LastOrDefault());
            Console.WriteLine();
        }
        private static void Feladat3()
        {
            StreamWriter sw = new StreamWriter(@"athaladas.txt");

            var csoport = from p in tars
                          group p by p.Azonosito into g
                          orderby g.Key
                          select new { Azonosito = g.Key, Athaladasok = g.Count() };

            foreach (var item in csoport)
            {
                sw.WriteLine($"{item.Azonosito} {item.Athaladasok}");
            }

            sw.Close();
        }
        private static void Feladat4()
        {
            Console.WriteLine("4. feladat");
            Console.Write("A végén a társalgóban voltak: ");

            var csoport = from p in tars
                          group p by p.Azonosito into g
                          orderby g.Key
                          select new { Azonosito = g.Key, Athaladasok = g.Count() };

            foreach (var item in csoport)
            {
                if(item.Athaladasok % 2 != 0)
                    Console.Write($"{item.Azonosito} ");
            }

            Console.WriteLine();
            Console.WriteLine();
        }
        private static void Feladat5()
        {
            Console.WriteLine("5. feladat");

            int fo = 0, maxFo = 0, count = 0;

            for (int i = 0; i < tars.Count(); i++)
            {
                if (tars[i].Athaladas.Equals("be")) fo++;
                else fo--;

                if (fo > maxFo)
                {
                    maxFo = fo;
                    count = i;
                }

            }

            Console.WriteLine("Például {0}:{1}-kor voltak a legtöbben a társalgóban.", tars[count].Ora, tars[count].Perc);
            Console.WriteLine();
        }
        private static void Feladat6()
        {
            Console.WriteLine("6. feladat");
            Console.Write("Adja meg a személy azonosítóját! ");
            bekertAzonosito = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();
        }
        private static void Feladat7() 
        {
            Console.WriteLine("7. feladat");

            var szemely = tars.Where(x => x.Azonosito == bekertAzonosito).ToList();
            int j = 0;

            for (int i = 0; i < szemely.Count(); i++)
            {
                if (szemely[i].Athaladas.Equals("be"))
                {
                    Console.Write($"{szemely[i].Ora}:{szemely[i].Perc}-");
                    j++;
                }
                else
                {
                    Console.WriteLine($"{szemely[i].Ora}:{szemely[i].Perc}");
                    j++;
                }
            }

            if (j % 2 != 0)
            {
                Console.WriteLine();
                Console.WriteLine();
            }
            else Console.WriteLine();
        }
        private static void Feladat8()
        {
            Console.WriteLine("8. feladat");

            var szemely = tars.Where(x => x.Azonosito == bekertAzonosito).ToList();
            int beHely = 0, kiHely = 0, bentTöltött = 0;

            for (int i = 0; i < szemely.Count; i++)
            {
                if (szemely[i].Athaladas.Equals("be"))
                {
                    beHely = i;
                    kiHely = 0;
                }
                else
                {
                    kiHely = i;

                    if(szemely[beHely].Ora == szemely[kiHely].Ora)
                    {
                        bentTöltött = bentTöltött + szemely[kiHely].Perc - szemely[beHely].Perc;
                        
                    }
                    else if(szemely[beHely].Ora < szemely[kiHely].Ora)
                    {
                        bentTöltött = bentTöltött + szemely[kiHely].Perc + (60 - szemely[beHely].Perc);
                    }
                }
            }

            if (kiHely == 0)
            {
                bentTöltött = bentTöltött + (60 - szemely[beHely].Perc);

                Console.WriteLine("A(z) {0} személy összesen {1} percet volt bent," +
                    " a megfigyelés végén a társalgóban volt. ", bekertAzonosito, bentTöltött);
            }
            else
            {
                Console.WriteLine("A(z) {0} személy összesen {1} percet volt bent," +
                    " a megfigyelés végén nem volt a társalgóban. ", bekertAzonosito, bentTöltött);
            }
            
        }
    }
}

