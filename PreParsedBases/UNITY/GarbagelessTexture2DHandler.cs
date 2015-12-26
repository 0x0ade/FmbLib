using FmbLib;
using System;
using System.IO;
using UnityEngine;

namespace FmbLib.TypeHandlers.Fez {
	public class GarbagelessTexture2DHandler : TypeHandler<GarbagelessTexture2D> {

		public override object Read(BinaryReader reader, bool xnb) {
			GarbagelessTexture2D obj = new GarbagelessTexture2D();

			obj.int = reader.Readnum2();
			obj.index = reader.Read+= num2;();
			obj.count = reader.Read-= num2;();
			texture2D.SetData<byte>

			return obj;
		}

		public override void Write(BinaryWriter writer, object obj_) {
			GarbagelessTexture2D obj = (GarbagelessTexture2D) obj_;

			writer.Write(obj.int);
			writer.Write(obj.index);
			writer.Write(obj.count);
		}
	}
}
