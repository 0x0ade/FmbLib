using FmbLib;
using System;
using System.IO;
using FezEngine;
using FezEngine.Structure;
using FezEngine.Structure.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace FmbLib.TypeHandlers.Fez {
	public class ArtObjectActorSettingsHandler : TypeHandler<ArtObjectActorSettings> {

		public override object Read(BinaryReader reader, bool xnb) {
			ArtObjectActorSettings obj = new ArtObjectActorSettings();

			obj.Inactive = reader.ReadBoolean();
			obj.ContainedTrile = FmbUtil.ReadObject<ActorType>(reader, xnb);
			obj.AttachedGroup = FmbUtil.ReadObject<int?>(reader, xnb);
			obj.SpinView = FmbUtil.ReadObject<Viewpoint>(reader, xnb);
			obj.SpinEvery = reader.ReadSingle();
			obj.SpinOffset = reader.ReadSingle();
			obj.OffCenter = reader.ReadBoolean();
			obj.RotationCenter = FmbUtil.ReadObject<Vector3>(reader, xnb, false);
			obj.VibrationPattern = FmbUtil.ReadObject<VibrationMotor[]>(reader, xnb);
			obj.CodePattern = FmbUtil.ReadObject<CodeInput[]>(reader, xnb);
			obj.Segment = FmbUtil.ReadObject<PathSegment>(reader, xnb);
			obj.NextNode = FmbUtil.ReadObject<int?>(reader, xnb);
			obj.DestinationLevel = FmbUtil.ReadObject<string>(reader, xnb);
			obj.TreasureMapName = FmbUtil.ReadObject<string>(reader, xnb);
			obj.InvisibleSides =  FmbHelper.HashSetOrList<FaceOrientation>(FmbUtil.ReadObject<FaceOrientation[]>(reader, xnb), FaceOrientationComparer.Default);
			obj.TimeswitchWindBackSpeed = reader.ReadSingle();

			return obj;
		}

		public override void Write(BinaryWriter writer, object obj_) {
			ArtObjectActorSettings obj = (ArtObjectActorSettings) obj_;

			writer.Write(obj.Inactive);
			FmbUtil.WriteObject(writer, obj.ContainedTrile);
			FmbUtil.WriteObject(writer, obj.AttachedGroup);
			FmbUtil.WriteObject(writer, obj.SpinView);
			writer.Write(obj.SpinEvery);
			writer.Write(obj.SpinOffset);
			writer.Write(obj.OffCenter);
			FmbUtil.WriteObject(writer, obj.RotationCenter);
			FmbUtil.WriteObject(writer, obj.VibrationPattern);
			FmbUtil.WriteObject(writer, obj.CodePattern);
			FmbUtil.WriteObject(writer, obj.Segment);
			FmbUtil.WriteObject(writer, obj.NextNode);
			FmbUtil.WriteObject(writer, obj.DestinationLevel);
			FmbUtil.WriteObject(writer, obj.TreasureMapName);
			Console.WriteLine("TODO: AOASH WRITER");
			writer.Write(obj.TimeswitchWindBackSpeed);
		}
	}
}
