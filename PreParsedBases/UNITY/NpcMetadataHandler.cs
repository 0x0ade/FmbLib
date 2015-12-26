using FmbLib;
using System;
using System.IO;
using FezEngine.Structure;
using UnityEngine;
using System.Collections.Generic;

namespace FmbLib.TypeHandlers.Fez {
	public class NpcMetadataHandler : TypeHandler<NpcMetadata> {

		public override object Read(BinaryReader reader, bool xnb) {
			NpcMetadata obj = new NpcMetadata();

			obj.WalkSpeed = reader.ReadSingle();
			obj.AvoidsGomez = reader.ReadBoolean();
			obj.SoundPath = FmbUtil.ReadObject<string>(reader, xnb);
			obj.SoundActions = FmbUtil.ReadObject<List<NpcAction>>(reader, xnb);

			return obj;
		}

		public override void Write(BinaryWriter writer, object obj_) {
			NpcMetadata obj = (NpcMetadata) obj_;

			writer.Write(obj.WalkSpeed);
			writer.Write(obj.AvoidsGomez);
			FmbUtil.WriteObject(writer, obj.SoundPath);
			FmbUtil.WriteObject(writer, obj.SoundActions);
		}
	}
}
