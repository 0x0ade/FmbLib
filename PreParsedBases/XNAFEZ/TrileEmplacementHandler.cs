using FmbLib;
using System;
using System.IO;
using FezEngine.Structure;
using Microsoft.Xna.Framework.Content;

namespace FmbLib.TypeHandlers.Fez {
	public class TrileEmplacementHandler : TypeHandler<TrileEmplacement> {

		public override object Read(BinaryReader reader, bool xnb) {
			TrileEmplacement obj = new TrileEmplacement();

			obj.X = reader.ReadInt32();
			obj.Y = reader.ReadInt32();
			obj.Z = reader.ReadInt32();

			return obj;
		}

		public override void Write(BinaryWriter writer, object obj_) {
			TrileEmplacement obj = (TrileEmplacement) obj_;

			writer.Write(obj.X);
			writer.Write(obj.Y);
			writer.Write(obj.Z);
		}
	}
}
