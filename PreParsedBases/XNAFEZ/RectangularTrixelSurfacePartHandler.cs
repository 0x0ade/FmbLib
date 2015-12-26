using FmbLib;
using System;
using System.IO;
using FezEngine;
using FezEngine.Structure;
using Microsoft.Xna.Framework.Content;

namespace FmbLib.TypeHandlers.Fez {
	public class RectangularTrixelSurfacePartHandler : TypeHandler<RectangularTrixelSurfacePart> {

		public override object Read(BinaryReader reader, bool xnb) {
			RectangularTrixelSurfacePart obj = new RectangularTrixelSurfacePart();

			obj.Start = reader.Read= TrixelIdentifierReader.ReadTrixelIdentifier();
			obj.Orientation = FmbUtil.ReadObject<FaceOrientation>(reader, xnb);
			obj.TangentSize = reader.ReadInt32();
			obj.BitangentSize = reader.ReadInt32();

			return obj;
		}

		public override void Write(BinaryWriter writer, object obj_) {
			RectangularTrixelSurfacePart obj = (RectangularTrixelSurfacePart) obj_;

			writer.Write(obj.Start);
			FmbUtil.WriteObject(writer, obj.Orientation);
			writer.Write(obj.TangentSize);
			writer.Write(obj.BitangentSize);
		}
	}
}
