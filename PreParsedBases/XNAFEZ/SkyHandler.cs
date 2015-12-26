using FmbLib;
using System;
using System.IO;
using FezEngine.Structure;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace FmbLib.TypeHandlers.Fez {
	public class SkyHandler : TypeHandler<Sky> {

		public override object Read(BinaryReader reader, bool xnb) {
			Sky obj = new Sky();

			obj.Name = reader.ReadString();
			obj.Background = reader.ReadString();
			obj.WindSpeed = reader.ReadSingle();
			obj.Density = reader.ReadSingle();
			obj.FogDensity = reader.ReadSingle();
			obj.Layers = FmbUtil.ReadObject<List<SkyLayer>>(reader, xnb);
			obj.Clouds = FmbUtil.ReadObject<List<string>>(reader, xnb);
			obj.Shadows = FmbUtil.ReadObject<string>(reader, xnb);
			obj.Stars = FmbUtil.ReadObject<string>(reader, xnb);
			obj.CloudTint = FmbUtil.ReadObject<string>(reader, xnb);
			obj.VerticalTiling = reader.ReadBoolean();
			obj.HorizontalScrolling = reader.ReadBoolean();
			obj.LayerBaseHeight = reader.ReadSingle();
			obj.InterLayerVerticalDistance = reader.ReadSingle();
			obj.InterLayerHorizontalDistance = reader.ReadSingle();
			obj.HorizontalDistance = reader.ReadSingle();
			obj.VerticalDistance = reader.ReadSingle();
			obj.LayerBaseSpacing = reader.ReadSingle();
			obj.WindParallax = reader.ReadSingle();
			obj.WindDistance = reader.ReadSingle();
			obj.CloudsParallax = reader.ReadSingle();
			obj.ShadowOpacity = reader.ReadSingle();
			obj.FoliageShadows = reader.ReadBoolean();
			obj.NoPerFaceLayerXOffset = reader.ReadBoolean();
			obj.LayerBaseXOffset = reader.ReadSingle();

			return obj;
		}

		public override void Write(BinaryWriter writer, object obj_) {
			Sky obj = (Sky) obj_;

			writer.Write(obj.Name);
			writer.Write(obj.Background);
			writer.Write(obj.WindSpeed);
			writer.Write(obj.Density);
			writer.Write(obj.FogDensity);
			FmbUtil.WriteObject(writer, obj.Layers);
			FmbUtil.WriteObject(writer, obj.Clouds);
			FmbUtil.WriteObject(writer, obj.Shadows);
			FmbUtil.WriteObject(writer, obj.Stars);
			FmbUtil.WriteObject(writer, obj.CloudTint);
			writer.Write(obj.VerticalTiling);
			writer.Write(obj.HorizontalScrolling);
			writer.Write(obj.LayerBaseHeight);
			writer.Write(obj.InterLayerVerticalDistance);
			writer.Write(obj.InterLayerHorizontalDistance);
			writer.Write(obj.HorizontalDistance);
			writer.Write(obj.VerticalDistance);
			writer.Write(obj.LayerBaseSpacing);
			writer.Write(obj.WindParallax);
			writer.Write(obj.WindDistance);
			writer.Write(obj.CloudsParallax);
			writer.Write(obj.ShadowOpacity);
			writer.Write(obj.FoliageShadows);
			writer.Write(obj.NoPerFaceLayerXOffset);
			writer.Write(obj.LayerBaseXOffset);
		}
	}
}
