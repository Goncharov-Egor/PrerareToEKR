using System;
using System.Collections;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.IO;
using System.Linq;
using EKRLIb1;
using System.Collections.Generic;
using System.Xml;

namespace Deserialization {
    class MainClass {

        /*Кросплатформенный путь*/
        public static readonly string sep = Path.DirectorySeparatorChar.ToString();
        public static readonly string path = ".." + sep + ".." + sep + ".." + sep
                                             + "Serialization" + sep + "bin" + sep
                                             + "Debug"  + sep ;

        public static void Main(string[] args) {

            try {
                Collection<Freight> fr;
                using (FileStream fs = new FileStream(path + "freights.xml", FileMode.Open, FileAccess.Read )) {
                    DataContractSerializer xml = new DataContractSerializer(typeof(Collection<Freight>));
                    fr = (xml.ReadObject(fs)) as Collection<Freight>;
                    foreach(var i in fr)
                        Console.WriteLine(i);
                }
                    return;
            }catch(Exception ex) {
                Console.WriteLine(ex.Message);
                return;
            }

            do {
                Console.Clear();
                try {
                    Collection<Freight> freights;
                    using (FileStream fs = new FileStream(path + "freights.json", FileMode.Open, FileAccess.Read)) {
                        DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(Collection<Freight>));
                        freights = (js.ReadObject(fs) as Collection<Freight>);
                        if (freights == null) throw new Exception("It was desirialization error");
                    }


                    Console.WriteLine("Sucsess deserializtion:");
                    foreach(var it in freights)
                        Console.WriteLine(it);
                    Console.WriteLine();
                    Console.WriteLine();
                    foreach (var i in Linq1(freights))
                        Console.WriteLine(i);
                    Console.WriteLine();
                    Console.WriteLine();
                    foreach (var i in Linq2(freights)) {
                        Console.WriteLine("Elements with weight " + i.Key + " are:");
                        foreach(var j in i)
                            Console.WriteLine(j);
                    }
                    Console.WriteLine();
                    foreach (var i in Linq3(freights)) Console.WriteLine(i);

                } catch (Exception ex) {
                    Console.WriteLine(ex.Message + " " + ex.HelpLink);
                }
                Console.WriteLine("If you wanna exit -> press escape");
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }

        public static IEnumerable<Freight> Linq1(IEnumerable<Freight> freights) {
            var ans = from i in freights
                      where i.GetRealValue() > 3.0
                      orderby i.Weight descending
                      select i;
            Console.WriteLine("Elements with real weight above 3 are " + ans.Count());
            return ans;
        }

        public static IEnumerable<IGrouping<double, Freight>> Linq2(IEnumerable<Freight> freights) {
            var ans = from i in freights
                      group i by i.Weight;
            return ans;
        }
        public static IEnumerable<Freight> Linq3(IEnumerable<Freight> freights) {
            var ans = from i in freights
                      where i.Weight == freights.Max(x => x.Weight)
                      select i;
            return ans;
        }
    }
}
