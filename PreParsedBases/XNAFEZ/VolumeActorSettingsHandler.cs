using FmbLib;
using System;
using System.IO;
using FezEngine.Structure;
using FezEngine.Structure.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace FmbLib.TypeHandlers.Fez {
	public class VolumeActorSettingsHandler : TypeHandler<VolumeActorSettings> {

		public override object Read(BinaryReader reader, bool xnb) {
			VolumeActorSettings obj = new VolumeActorSettings();

			obj.FarawayPlaneOffset = FmbUtil.ReadObject<Vector2>(reader, xnb, false);
			obj.IsPointOfInterest = reader.ReadBoolean();
			obj.DotDialogue = FmbUtil.ReadObject<List<DotDialogueLine>>(reader, xnb);
			obj.WaterLocked = reader.ReadBoolean();
			obj.CodePattern = FmbUtil.ReadObject<CodeInput[]>(reader, xnb);
			obj.IsBlackHole = reader.ReadBoolean();
			obj.NeedsTrigger = reader.ReadBoolean();
			obj.IsSecretPassage = reader.ReadBoolean();

			return obj;
		}

		public override void Write(BinaryWriter writer, object obj_) {
			VolumeActorSettings obj = (VolumeActorSettings) obj_;

			FmbUtil.WriteObject(writer, obj.FarawayPlaneOffset);
			writer.Write(obj.IsPointOfInterest);
			FmbUtil.WriteObject(writer, obj.DotDialogue);
			writer.Write(obj.WaterLocked);
			FmbUtil.WriteObject(writer, obj.CodePattern);
			writer.Write(obj.IsBlackHole);
			writer.Write(obj.NeedsTrigger);
			writer.Write(obj.IsSecretPassage);
		}
	}
}
