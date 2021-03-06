using FmbLib;
using System;
using System.IO;
using FezEngine.Structure;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace FmbLib.TypeHandlers.Fez {
	public class TrileInstanceHandler : TypeHandler<TrileInstance> {

		public override object Read(BinaryReader reader, bool xnb) {
			TrileInstance obj = new TrileInstance();

			obj.Position = FmbUtil.ReadObject<Vector3>(reader, xnb, false);
			obj.TrileId = reader.ReadInt32();
			byte pPhi = reader.ReadByte();
			if (pPhi != 137) {
			obj.SetPhiLight(reader.ReadByte());
			} else {
			obj.Phi = reader.ReadSingle();
			}
			if (reader.ReadBoolean()) {
			obj.ActorSettings = FmbUtil.ReadObject<InstanceActorSettings>(reader, xnb);
			}
			obj.OverlappedTriles = FmbUtil.ReadObject<List<TrileInstance>>(reader, xnb);

			return obj;
		}

		public override void Write(BinaryWriter writer, object obj_) {
			TrileInstance obj = (TrileInstance) obj_;

			FmbUtil.GetTypeHandler<Vector3>().Write(writer, obj.Position);
			writer.Write(obj.TrileId);
			writer.Write((byte) 137);
			writer.Write(obj.Phi);
			writer.Write(true);
			FmbUtil.WriteObject(writer, obj.ActorSettings);
			FmbUtil.WriteObject(writer, obj.OverlappedTriles);
		}
	}
}
