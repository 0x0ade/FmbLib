using FmbLib;
using System;
using System.IO;
using FezEngine.Structure;
using UnityEngine;

namespace FmbLib.TypeHandlers.Fez {
	public class SkyLayerHandler : TypeHandler<SkyLayer> {

		public override object Read(BinaryReader reader, bool xnb) {
			SkyLayer obj = new SkyLayer();

			obj.Name = reader.ReadString();
			obj.InFront = reader.ReadBoolean();
			obj.Opacity = reader.ReadSingle();
			obj.FogTint = reader.ReadSingle();

			return obj;
		}

		public override void Write(BinaryWriter writer, object obj_) {
			SkyLayer obj = (SkyLayer) obj_;

			writer.Write(obj.Name);
			writer.Write(obj.InFront);
			writer.Write(obj.Opacity);
			writer.Write(obj.FogTint);
		}
	}
}
