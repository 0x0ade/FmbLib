using FmbLib;
using System;
using System.IO;
using FezEngine;
using FezEngine.Structure;
using FezEngine.Structure.Scripting;
using UnityEngine;
using System.Collections.Generic;

namespace FmbLib.TypeHandlers.Fez {
	public class LevelHandler : TypeHandler<Level> {

		public override object Read(BinaryReader reader, bool xnb) {
			Level obj = new Level();

			obj.Name = FmbUtil.ReadObject<string>(reader, xnb);
            Console.WriteLine("debug: Level: Name: " + obj.Name);
			obj.Size = FmbUtil.ReadObject<Vector3>(reader, xnb, false);
            Console.WriteLine("debug: Level: Size: " + obj.Size.x + ", " + obj.Size.y + ", " + obj.Size.z);
			obj.StartingPosition = FmbUtil.ReadObject<TrileFace>(reader, xnb);
            Console.WriteLine("debug: Level: StartingPosition: Id: " + obj.StartingPosition.Id);
            Console.WriteLine("debug: Level: StartingPosition: Face: " + obj.StartingPosition.Face);
			obj.SequenceSamplesPath = FmbUtil.ReadObject<string>(reader, xnb);
			Console.WriteLine("debug: Level: SequenceSamplesPath: " + obj.SequenceSamplesPath);
			obj.Flat = reader.ReadBoolean();
			obj.SkipPostProcess = reader.ReadBoolean();
			obj.BaseDiffuse = reader.ReadSingle();
			obj.BaseAmbient = reader.ReadSingle();
			obj.GomezHaloName = FmbUtil.ReadObject<string>(reader, xnb);
            Console.WriteLine("debug: Level: GomezHaloName: " + obj.GomezHaloName);
			obj.HaloFiltering = reader.ReadBoolean();
			obj.BlinkingAlpha = reader.ReadBoolean();
			obj.Loops = reader.ReadBoolean();
			obj.WaterType = FmbUtil.ReadObject<LiquidType>(reader, xnb);
			obj.WaterHeight = reader.ReadSingle();
			obj.SkyName = reader.ReadString();
			obj.TrileSetName = FmbUtil.ReadObject<string>(reader, xnb);
            Console.WriteLine("debug: Level: TrileSetName: " + obj.TrileSetName);
			obj.Volumes = FmbUtil.ReadObject<Dictionary<int, Volume>>(reader, xnb);
			obj.Scripts = FmbUtil.ReadObject<Dictionary<int, Script>>(reader, xnb);
			obj.SongName = FmbUtil.ReadObject<string>(reader, xnb);
			obj.FAPFadeOutStart = reader.ReadInt32();
			obj.FAPFadeOutLength = reader.ReadInt32();
			obj.Triles = FmbUtil.ReadObject<Dictionary<TrileEmplacement, TrileInstance>>(reader, xnb);
			obj.ArtObjects = FmbUtil.ReadObject<Dictionary<int, ArtObjectInstance>>(reader, xnb);
			obj.BackgroundPlanes = FmbUtil.ReadObject<Dictionary<int, BackgroundPlane>>(reader, xnb);
			obj.Groups = FmbUtil.ReadObject<Dictionary<int, TrileGroup>>(reader, xnb);
			obj.NonPlayerCharacters = FmbUtil.ReadObject<Dictionary<int, NpcInstance>>(reader, xnb);
			obj.Paths = FmbUtil.ReadObject<Dictionary<int, MovementPath>>(reader, xnb);
			obj.Descending = reader.ReadBoolean();
			obj.Rainy = reader.ReadBoolean();
			obj.LowPass = reader.ReadBoolean();
			obj.MutedLoops = FmbUtil.ReadObject<List<string>>(reader, xnb);
			obj.AmbienceTracks = FmbUtil.ReadObject<List<AmbienceTrack>>(reader, xnb);
			obj.NodeType = FmbUtil.ReadObject<LevelNodeType>(reader, xnb);
			obj.Quantum = reader.ReadBoolean();
			obj.OnDeserialization();

			return obj;
		}

		public override void Write(BinaryWriter writer, object obj_) {
			Level obj = (Level) obj_;

			Console.WriteLine("debug: Level: Name: " + obj.Name);
			FmbUtil.WriteObject(writer, obj.Name);
            FmbUtil.GetTypeHandler<Vector3>().Write(writer, obj.Size);//<Vector3>(reader, xnb, false); means name is skipped
			FmbUtil.WriteObject(writer, obj.StartingPosition);
			Console.WriteLine("debug: Level: SequenceSamplesPath: " + obj.SequenceSamplesPath);
			FmbUtil.WriteObject(writer, obj.SequenceSamplesPath);
			writer.Write(obj.Flat);
			writer.Write(obj.SkipPostProcess);
			writer.Write(obj.BaseDiffuse);
			writer.Write(obj.BaseAmbient);
			Console.WriteLine("debug: Level: GomezHaloName: " + obj.GomezHaloName);
			FmbUtil.WriteObject(writer, obj.GomezHaloName);
			writer.Write(obj.HaloFiltering);
			writer.Write(obj.BlinkingAlpha);
			writer.Write(obj.Loops);
			FmbUtil.WriteObject(writer, obj.WaterType);
			writer.Write(obj.WaterHeight);
			writer.Write(obj.SkyName);
			Console.WriteLine("debug: Level: TrileSetName: " + obj.TrileSetName);
			FmbUtil.WriteObject(writer, obj.TrileSetName);
			FmbUtil.WriteObject(writer, obj.Volumes);
			FmbUtil.WriteObject(writer, obj.Scripts);
			FmbUtil.WriteObject(writer, obj.SongName);
			writer.Write(obj.FAPFadeOutStart);
			writer.Write(obj.FAPFadeOutLength);
			FmbUtil.WriteObject(writer, obj.Triles);
			FmbUtil.WriteObject(writer, obj.ArtObjects);
			FmbUtil.WriteObject(writer, obj.BackgroundPlanes);
			FmbUtil.WriteObject(writer, obj.Groups);
			FmbUtil.WriteObject(writer, obj.NonPlayerCharacters);
			FmbUtil.WriteObject(writer, obj.Paths);
			writer.Write(obj.Descending);
			writer.Write(obj.Rainy);
			writer.Write(obj.LowPass);
			FmbUtil.WriteObject(writer, obj.MutedLoops);
			FmbUtil.WriteObject(writer, obj.AmbienceTracks);
			FmbUtil.WriteObject(writer, obj.NodeType);
			writer.Write(obj.Quantum);
		}
	}
}
