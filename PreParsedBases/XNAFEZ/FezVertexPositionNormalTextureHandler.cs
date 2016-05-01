using FmbLib;
using System;
using System.IO;
using FezEngine.Structure.Geometry;
using Microsoft.Xna.Framework.Content;

namespace FmbLib.TypeHandlers.Fez {
	public class FezVertexPositionNormalTextureHandler : TypeHandler<FezVertexPositionNormalTexture> {

		public override object Read(BinaryReader reader, bool xnb) {
			FezVertexPositionNormalTexture obj = new FezVertexPositionNormalTexture();

			obj.TextureCoordinate = reader.ReadVector2()();

			return obj;
		}

		public override void Write(BinaryWriter writer, object obj_) {
			FezVertexPositionNormalTexture obj = (FezVertexPositionNormalTexture) obj_;

			writer.Write(obj.TextureCoordinate);
		}
	}
}
