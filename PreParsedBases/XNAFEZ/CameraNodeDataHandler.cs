using FmbLib;
using System;
using System.IO;
using FezEngine.Structure;
using Microsoft.Xna.Framework.Content;

namespace FmbLib.TypeHandlers.Fez {
	public class CameraNodeDataHandler : TypeHandler<CameraNodeData> {

		public override object Read(BinaryReader reader, bool xnb) {
			CameraNodeData obj = new CameraNodeData();

			obj.Perspective = reader.ReadBoolean();
			obj.PixelsPerTrixel = reader.ReadInt32();
			obj.SoundName = FmbUtil.ReadObject<string>(reader, xnb);

			return obj;
		}

		public override void Write(BinaryWriter writer, object obj_) {
			CameraNodeData obj = (CameraNodeData) obj_;

			writer.Write(obj.Perspective);
			writer.Write(obj.PixelsPerTrixel);
			FmbUtil.WriteObject(writer, obj.SoundName);
		}
	}
}
