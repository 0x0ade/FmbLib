using System;
using FmbLib;
using FezEngine.Structure;

namespace FmbLibTester {
    class MainClass {

        public static void Main(string[] args) {
            if (args.Length < 1) {
                Console.WriteLine("FmbLib requires at least one parameter: the path to the xnb.");
                Console.WriteLine("Example: FmbLib.exe level.xnb");
                Console.WriteLine("Using default testing args instead.");
                args = new string[] { "../../../cmycave.xnb", "../../../gateao.xnb" };
            }

            for (int i = 0; i < args.Length; i++) {
                Console.WriteLine("asset " + i + ": " + args[i]);

                object obj = FmbUtil.ReadObject(args[i]);
                
                Console.WriteLine("Asset type: " + obj.GetType().FullName);
                
                if (obj is TrileSet) {
                    TrileSet ts = (TrileSet) obj;
                    Console.WriteLine("TrileSet name: " + ts.Name);
                    Console.WriteLine("trile count: " + ts.Triles.Count);
                    foreach (Trile trile in ts.Triles.Values) {
                        Console.WriteLine(trile.Id + " Name: " + trile.Name);
                        Console.WriteLine(trile.Id + " SurfaceType: " + trile.SurfaceType);
                    }
                }

                if (obj is ArtObject) {
                    ArtObject ao = (ArtObject) obj;
                    Console.WriteLine("ArtObject name: " + ao.Name);
                    Console.WriteLine("ActorType: " + ao.ActorType);
                }
            }
            
        }

    }
}
