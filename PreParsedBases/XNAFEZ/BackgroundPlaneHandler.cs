using FmbLib;
using System;
using System.IO;
using FezEngine.Structure;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace FmbLib.TypeHandlers.Fez {
	public class BackgroundPlaneHandler : TypeHandler<BackgroundPlane> {

		public override object Read(BinaryReader reader, bool xnb) {
			BackgroundPlane obj = new BackgroundPlane();

			obj.Position = FmbUtil.ReadObject<Vector3>(reader, xnb, false);
			obj.Rotation = FmbUtil.ReadObject<Quaternion>(reader, xnb, false);
			obj.Scale = FmbUtil.ReadObject<Vector3>(reader, xnb, false);
			obj.Size = FmbUtil.ReadObject<Vector3>(reader, xnb, false);
			obj.TextureName = reader.ReadString();
			obj.LightMap = reader.ReadBoolean();
			obj.AllowOverbrightness = reader.ReadBoolean();
			obj.Filter = FmbUtil.ReadObject<Color>(reader, xnb, false);
			obj.Animated = reader.ReadBoolean();
			obj.Doublesided = reader.ReadBoolean();
			obj.Opacity = reader.ReadSingle();
			obj.AttachedGroup = FmbUtil.ReadObject<int?>(reader, xnb);
			obj.Billboard = reader.ReadBoolean();
			obj.SyncWithSamples = reader.ReadBoolean();
			obj.Crosshatch = reader.ReadBoolean();
			reader.ReadBoolean();
			obj.AlwaysOnTop = reader.ReadBoolean();
			obj.Fullbright = reader.ReadBoolean();
			obj.PixelatedLightmap = reader.ReadBoolean();
			obj.XTextureRepeat = reader.ReadBoolean();
			obj.YTextureRepeat = reader.ReadBoolean();
			obj.ClampTexture = reader.ReadBoolean();
			obj.ActorType = FmbUtil.ReadObject<ActorType>(reader, xnb);
			obj.AttachedPlane = FmbUtil.ReadObject<int?>(reader, xnb);
			obj.ParallaxFactor = reader.ReadSingle();

			return obj;
		}

		public override void Write(BinaryWriter writer, object obj_) {
			BackgroundPlane obj = (BackgroundPlane) obj_;

			FmbUtil.GetTypeHandler<Vector3>().Write(writer, obj.Position);
			FmbUtil.GetTypeHandler<Quaternion>().Write(writer, obj.Rotation);
			FmbUtil.GetTypeHandler<Vector3>().Write(writer, obj.Scale);
			FmbUtil.GetTypeHandler<Vector3>().Write(writer, obj.Size);
			writer.Write(obj.TextureName);
			writer.Write(obj.LightMap);
			writer.Write(obj.AllowOverbrightness);
			FmbUtil.GetTypeHandler<Color>().Write(writer, obj.Filter);
			writer.Write(obj.Animated);
			writer.Write(obj.Doublesided);
			writer.Write(obj.Opacity);
			FmbUtil.WriteObject(writer, obj.AttachedGroup);
			writer.Write(obj.Billboard);
			writer.Write(obj.SyncWithSamples);
			writer.Write(obj.Crosshatch);
			writer.Write(false);
			writer.Write(obj.AlwaysOnTop);
			writer.Write(obj.Fullbright);
			writer.Write(obj.PixelatedLightmap);
			writer.Write(obj.XTextureRepeat);
			writer.Write(obj.YTextureRepeat);
			writer.Write(obj.ClampTexture);
			FmbUtil.WriteObject(writer, obj.ActorType);
			FmbUtil.WriteObject(writer, obj.AttachedPlane);
			writer.Write(obj.ParallaxFactor);
		}
	}
}
