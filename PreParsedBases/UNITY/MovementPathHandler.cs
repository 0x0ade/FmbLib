using FmbLib;
using System;
using System.IO;
using FezEngine.Structure;
using UnityEngine;
using System.Collections.Generic;

namespace FmbLib.TypeHandlers.Fez {
	public class MovementPathHandler : TypeHandler<MovementPath> {

		public override object Read(BinaryReader reader, bool xnb) {
			MovementPath obj = new MovementPath();

			obj.Segments = FmbUtil.ReadObject<List<PathSegment>>(reader, xnb);
			obj.NeedsTrigger = reader.ReadBoolean();
			obj.EndBehavior = FmbUtil.ReadObject<PathEndBehavior>(reader, xnb);
			obj.SoundName = FmbUtil.ReadObject<string>(reader, xnb);
			obj.IsSpline = reader.ReadBoolean();
			obj.OffsetSeconds = reader.ReadSingle();
			obj.SaveTrigger = reader.ReadBoolean();

			return obj;
		}

		public override void Write(BinaryWriter writer, object obj_) {
			MovementPath obj = (MovementPath) obj_;

			FmbUtil.WriteObject(writer, obj.Segments);
			writer.Write(obj.NeedsTrigger);
			FmbUtil.WriteObject(writer, obj.EndBehavior);
			FmbUtil.WriteObject(writer, obj.SoundName);
			writer.Write(obj.IsSpline);
			writer.Write(obj.OffsetSeconds);
			writer.Write(obj.SaveTrigger);
		}
	}
}
