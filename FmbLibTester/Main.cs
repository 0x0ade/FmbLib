using System;
using FmbLib;
using FezEngine.Structure;

namespace FmbLibTester {
    class MainClass {

        public static void Main(string[] args) {
            if (args.Length != 1) {
                Console.WriteLine("FmbLib requires one parameter: the path to the xnb.");
                Console.WriteLine("Example: FmbLib.exe level.xnb");
                return;
            }
            
            object obj = FmbUtil.ReadObject(args[0]);
            
            Console.WriteLine("Asset type: " + obj.GetType().FullName);
            
            if (obj is TrileSet) {
                TrileSet ts = (TrileSet) obj;
                Console.WriteLine("TrileSet name: " + ts.Name);
                Console.WriteLine("trile count: " + ts.Triles.Count);
                foreach (Trile trile in ts.Triles.Values) {
                    Console.WriteLine(trile.Id + ": " + trile.Name);
                }
            }
            
        }

    }
}
