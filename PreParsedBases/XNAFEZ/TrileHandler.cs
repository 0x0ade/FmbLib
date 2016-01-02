using FmbLib;
using System;
using System.IO;
using FezEngine;
using FezEngine.Structure;
using FezEngine.Structure.Geometry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace FmbLib.TypeHandlers.Fez {
	public class TrileHandler : TypeHandler<Trile> {

		public override object Read(BinaryReader reader, bool xnb) {
			Trile obj = new Trile();

			obj.Name = reader.ReadString();
			obj.CubemapPath = reader.ReadString();
			obj.Size = FmbUtil.ReadObject<Vector3>(reader, xnb, false);
			obj.Offset = FmbUtil.ReadObject<Vector3>(reader, xnb, false);
			obj.Immaterial = reader.ReadBoolean();
			obj.SeeThrough = reader.ReadBoolean();
			obj.Thin = reader.ReadBoolean();
			obj.ForceHugging = reader.ReadBoolean();
			obj.Faces = FmbUtil.ReadObject<Dictionary<FaceOrientation, CollisionType>>(reader, xnb);
			obj.Geometry = FmbUtil.ReadObject<ShaderInstancedIndexedPrimitives<VertexPositionNormalTextureInstance, Vector4>>(reader, xnb);
			obj.ActorSettings.Type = FmbUtil.ReadObject<ActorType>(reader, xnb);
			obj.ActorSettings.Face = FmbUtil.ReadObject<FaceOrientation>(reader, xnb);
			obj.SurfaceType = FmbUtil.ReadObject<SurfaceType>(reader, xnb);
			obj.AtlasOffset = FmbUtil.ReadObject<Vector2>(reader, xnb, false);

			return obj;
		}

		public override void Write(BinaryWriter writer, object obj_) {
			Trile obj = (Trile) obj_;

			writer.Write(obj.Name);
			writer.Write(obj.CubemapPath);
			FmbUtil.GetTypeHandler<Vector3>().Write(writer, obj.Size);
			FmbUtil.GetTypeHandler<Vector3>().Write(writer, obj.Offset);
			writer.Write(obj.Immaterial);
			writer.Write(obj.SeeThrough);
			writer.Write(obj.Thin);
			writer.Write(obj.ForceHugging);
			FmbUtil.WriteObject(writer, obj.Faces);
			FmbUtil.WriteObject(writer, obj.Geometry);
			FmbUtil.WriteObject(writer, obj.ActorSettings.Type);
			FmbUtil.WriteObject(writer, obj.ActorSettings.Face);
			FmbUtil.WriteObject(writer, obj.SurfaceType);
			FmbUtil.GetTypeHandler<Vector2>().Write(writer, obj.AtlasOffset);
		}
	}
}
