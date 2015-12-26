using FmbLib;
using System;
using System.IO;
using FezEngine.Structure;
using FezEngine.Structure.Geometry;
using UnityEngine;

namespace FmbLib.TypeHandlers.Fez {
	public class ArtObjectHandler : TypeHandler<ArtObject> {

		public override object Read(BinaryReader reader, bool xnb) {
			ArtObject obj = new ArtObject();

			obj.Name = reader.ReadString();
			obj.Cubemap = FmbUtil.ReadObject<Texture2D>(reader, xnb);
			obj.Size = FmbUtil.ReadObject<Vector3>(reader, xnb, false);
			obj.Geometry = FmbUtil.ReadObject<ShaderInstancedIndexedPrimitives<VertexPositionNormalTextureInstance, Matrix>>(reader, xnb);
			obj.ActorType = FmbUtil.ReadObject<ActorType>(reader, xnb);
			obj.NoSihouette = reader.ReadBoolean();

			return obj;
		}

		public override void Write(BinaryWriter writer, object obj_) {
			ArtObject obj = (ArtObject) obj_;

			writer.Write(obj.Name);
			FmbUtil.WriteObject(writer, obj.Cubemap);
			FmbUtil.WriteObject(writer, obj.Size);
			FmbUtil.WriteObject(writer, obj.Geometry);
			FmbUtil.WriteObject(writer, obj.ActorType);
			writer.Write(obj.NoSihouette);
		}
	}
}
