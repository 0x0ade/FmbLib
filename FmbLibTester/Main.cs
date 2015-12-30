using System;
using FmbLib;
using FezEngine.Structure;
using System.IO;
using System.Reflection;

namespace FmbLibTester {
    class MainClass {

        private static void Print(object obj) {
            Console.WriteLine("Asset type: " + obj.GetType().FullName);
            
            if (obj is TrileSet) {
                TrileSet ts = (TrileSet) obj;
                Console.WriteLine("TrileSet name: " + ts.Name);
                Console.WriteLine("Trile count: " + ts.Triles.Count);
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

            if (obj is Sky) {
                Sky sky = (Sky) obj;
                Console.WriteLine("Sky name: " + sky.Name);
                Console.WriteLine("Background: " + sky.Background);
                Console.WriteLine("Stars: " + sky.Stars);
                Console.WriteLine("Layer count: " + sky.Layers.Count);
                for (int i = 0; i < sky.Layers.Count; i++) {
                    Console.WriteLine(i + ": " + sky.Layers[i].Name);
                }
            }
        }

        public static void Main(string[] args) {
            FmbUtil.IsTEST = true;

            if (args.Length < 1) {
                Console.WriteLine("FmbLib requires at least one parameter: the path to the xnb.");
                Console.WriteLine("Example: FmbLib.exe level.xnb");
                Console.WriteLine("Alternatively, run with --preparse / -pp + path as parameters");
                Console.WriteLine("to generate the .cs sources for the TypeHandlerBase .txts.");
                Console.WriteLine("Using default testing args instead.");
                args = new string[] {
                    //"../../../cmycave.xnb"
                    //"../../../gateao.xnb"
                    //"../../../fox.xnb"
                    "../../../waterfront.xnb"
                    //"-pp", "../../../PreParsedBases/" + (FmbUtil.IsUNITY ? "UNITY" : "XNAFEZ")
                };
            }

            if (args.Length == 2 && (args[0] == "-pp" || args[0] == "--preparse")) {
                Console.WriteLine("Pre-parsing all TypeHandlerBases...");

                Directory.CreateDirectory(args[1]);

                Assembly assembly = typeof(FmbUtil).Assembly;
                string[] manifestResourceNames = assembly.GetManifestResourceNames();

                for (int i = 0; i < manifestResourceNames.Length; i++) {
                    if (!(
                        manifestResourceNames[i].EndsWith("Reader.txt") ||
                        manifestResourceNames[i].EndsWith("Handler.txt")
                    )) {
                        continue;
                    }

                    string path = manifestResourceNames[i];
                    if (path.EndsWith("Reader.txt")) {
                        path = path.Substring(0, path.Length - 10);
                    } else if (path.EndsWith("Handler.txt")) {
                        path = path.Substring(0, path.Length - 11);
                    }

                    string[] split = path.Split('.');

                    string source;
                    using (Stream s = assembly.GetManifestResourceStream(manifestResourceNames[i])) {
                        if (s == null) {
                            Console.WriteLine(manifestResourceNames[i] + " cannot be loaded.");
                            continue;
                        }
                        using (StreamReader sr = new StreamReader(s)) {
                            source = FmbUtil.GenerateHandlerSource(sr, split[split.Length - 1], null, split[split.Length - 1] + "Handler", "FmbLib.TypeHandlers." + split[split.Length - 2]);
                        }
                    }

                    Console.Write(source);

                    using (Stream s = File.OpenWrite(Path.Combine(args[1], split[split.Length - 1] + "Handler.cs"))) {
                        using (StreamWriter sw = new StreamWriter(s)) {
                            sw.Write(source);
                        }
                    }
                }

                return;
            }

            for (int i = 0; i < args.Length; i++) {
                Console.WriteLine("asset " + i + ": " + args[i]);

                Console.WriteLine("reading xnb");
                object obj = FmbUtil.ReadObject(args[i]);
                Print(obj);

                Console.WriteLine("writing fmb");
                string fmbPath = args[i].Substring(args[i].Length-3) + "fmb";
                if (File.Exists(fmbPath)) {
                    File.Delete(fmbPath);
                }
                FmbUtil.WriteObject(fmbPath, obj);

                Console.WriteLine("reading fmb");
                obj = FmbUtil.ReadObject(fmbPath);
                Print(obj);
            }
            
        }

    }
}
