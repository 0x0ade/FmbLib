using FmbLib;
using System;
using System.IO;
using FezEngine.Structure;
using UnityEngine;
using System.Collections.Generic;

namespace FmbLib.TypeHandlers.Fez {
	public class TrileInstanceHandler : TypeHandler<TrileInstance> {

		public override object Read(BinaryReader reader, bool xnb) {
			TrileInstance obj = new TrileInstance();

			obj.Position = FmbUtil.ReadObject<Vector3>(reader, xnb, false);
			obj.TrileId = reader.ReadInt32();
			obj.SetPhiLight(reader.ReadByte());
			if (reader.ReadBoolean()) {
			obj.ActorSettings = FmbUtil.ReadObject<InstanceActorSettings>(reader, xnb);
			}
			obj.OverlappedTriles = FmbUtil.ReadObject<List<TrileInstance>>(reader, xnb);

			return obj;
		}

		public override void Write(BinaryWriter writer, object obj_) {
			TrileInstance obj = (TrileInstance) obj_;

			FmbUtil.WriteObject(writer, obj.Position);
			writer.Write(obj.TrileId);
			Console.WriteLine("TODO: TrileInstanceReader precision of phi");
			writer.Write((byte) Math.Floor(FmbHelper.GetW(obj.Data.PositionPhi) / 1.570796f - 2));
			writer.Write(true);
			FmbUtil.WriteObject(writer, obj.ActorSettings);
			FmbUtil.WriteObject(writer, obj.OverlappedTriles);
		}
	}
}
