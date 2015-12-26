using FmbLib;
using System;
using System.IO;
using FezEngine.Structure;
using UnityEngine;

namespace FmbLib.TypeHandlers.Fez {
	public class DotDialogueLineHandler : TypeHandler<DotDialogueLine> {

		public override object Read(BinaryReader reader, bool xnb) {
			DotDialogueLine obj = new DotDialogueLine();

			obj.ResourceText = FmbUtil.ReadObject<string>(reader, xnb);
			obj.Grouped = reader.ReadBoolean();

			return obj;
		}

		public override void Write(BinaryWriter writer, object obj_) {
			DotDialogueLine obj = (DotDialogueLine) obj_;

			FmbUtil.WriteObject(writer, obj.ResourceText);
			writer.Write(obj.Grouped);
		}
	}
}
