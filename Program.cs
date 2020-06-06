using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Xml;
using EKRLIb1;

namespace Serialization {
    class MainClass {

        public static Random rnd = new Random();

        public static void Main(string[] args) {
            
            do {
                Console.Clear();

                Collection<Freight> freights = new Collection<Freight>();
                Console.WriteLine("Enter N:");
                uint N;
                while(!uint.TryParse(Console.ReadLine(), out N))
                    Console.WriteLine("Invalid N, try again");

                for (int i = 0; i < N; i++) {
                    try {
                        freights.Add(FreightGenerator());
                    } catch (Exception ex) {
                        --i;
                    } 
                }
                Console.WriteLine();
                foreach(var el in freights)
                    Console.WriteLine(el);
                try {
                    using (FileStream fs = new FileStream("freights.json", FileMode.Create, FileAccess.Write)) {
                        DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(Collection<Freight>));
                        js.WriteObject(fs, freights);
                    }

                    using (FileStream fs = new FileStream("freights.xml", FileMode.Create, FileAccess.Write)) {
                        DataContractSerializer js = new DataContractSerializer(typeof(Collection<Freight>));
                        js.WriteObject(fs, freights);
                    }
                } catch (Exception ex ) {
                    Console.WriteLine(ex.Message + " " + ex.HelpLink);
                }

                    Console.WriteLine("Press Esc to exit");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);

        }

        public static Freight FreightGenerator() {
            double weight = rnd.Next(-3, 10) * rnd.NextDouble();
            double a = rnd.Next(-3, 10) * rnd.NextDouble(); 
            double b = rnd.Next(-3, 10) * rnd.NextDouble();
            double c = rnd.Next(-3, 10) * rnd.NextDouble();
            var res = new Freight(weight, a, b, c);
            Console.WriteLine(res);
            return res;
        }
    }
}
