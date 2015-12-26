using FmbLib;
using System;
using System.IO;
using FezEngine;
using FezEngine.Structure;
using UnityEngine;
using System.Collections.Generic;

namespace FmbLib.TypeHandlers.Fez {
	public class VolumeHandler : TypeHandler<Volume> {

		public override object Read(BinaryReader reader, bool xnb) {
			Volume obj = new Volume();

			obj.Orientations = FmbHelper.HashSetOrList<FaceOrientation>(FmbUtil.ReadObject<FaceOrientation[]>(reader, xnb), FaceOrientationComparer.Default);
			obj.From = FmbUtil.ReadObject<Vector3>(reader, xnb, false);
			obj.To = FmbUtil.ReadObject<Vector3>(reader, xnb, false);
			obj.ActorSettings = FmbUtil.ReadObject<VolumeActorSettings>(reader, xnb);

			return obj;
		}

		public override void Write(BinaryWriter writer, object obj_) {
			Volume obj = (Volume) obj_;

			Console.WriteLine("TODO: VOLUME WRITER");
			FmbUtil.WriteObject(writer, obj.From);
			FmbUtil.WriteObject(writer, obj.To);
			FmbUtil.WriteObject(writer, obj.ActorSettings);
		}
	}
}
