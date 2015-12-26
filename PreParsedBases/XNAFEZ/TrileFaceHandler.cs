using FmbLib;
using System;
using System.IO;
using FezEngine;
using FezEngine.Structure;
using Microsoft.Xna.Framework.Content;

namespace FmbLib.TypeHandlers.Fez {
	public class TrileFaceHandler : TypeHandler<TrileFace> {

		public override object Read(BinaryReader reader, bool xnb) {
			TrileFace obj = new TrileFace();

			obj.Id = FmbUtil.ReadObject<TrileEmplacement>(reader, xnb);
			obj.Face = FmbUtil.ReadObject<FaceOrientation>(reader, xnb);

			return obj;
		}

		public override void Write(BinaryWriter writer, object obj_) {
			TrileFace obj = (TrileFace) obj_;

			FmbUtil.WriteObject(writer, obj.Id);
			FmbUtil.WriteObject(writer, obj.Face);
		}
	}
}
