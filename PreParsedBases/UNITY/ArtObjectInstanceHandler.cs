using FmbLib;
using System;
using System.IO;
using FezEngine.Structure;
using UnityEngine;

namespace FmbLib.TypeHandlers.Fez {
	public class ArtObjectInstanceHandler : TypeHandler<ArtObjectInstance> {

		public override object Read(BinaryReader reader, bool xnb) {
			ArtObjectInstance obj = new ArtObjectInstance(reader.ReadString());

			obj.Position = FmbUtil.ReadObject<Vector3>(reader, xnb, false);
			obj.Rotation = FmbUtil.ReadObject<Quaternion>(reader, xnb, false);
			obj.Scale = FmbUtil.ReadObject<Vector3>(reader, xnb, false);
			obj.ActorSettings = FmbUtil.ReadObject<ArtObjectActorSettings>(reader, xnb);

			return obj;
		}

		public override void Write(BinaryWriter writer, object obj_) {
			ArtObjectInstance obj = (ArtObjectInstance) obj_;

			writer.Write(obj.ArtObjectName);
			FmbUtil.WriteObject(writer, obj.Position);
			FmbUtil.WriteObject(writer, obj.Rotation);
			FmbUtil.WriteObject(writer, obj.Scale);
			FmbUtil.WriteObject(writer, obj.ActorSettings);
		}
	}
}
