using FmbLib;
using System;
using System.IO;
using FezEngine.Structure;
using Microsoft.Xna.Framework.Content;

namespace FmbLib.TypeHandlers.Fez {
	public class SpeechLineHandler : TypeHandler<SpeechLine> {

		public override object Read(BinaryReader reader, bool xnb) {
			SpeechLine obj = new SpeechLine();

			obj.Text = FmbUtil.ReadObject<string>(reader, xnb);
			obj.OverrideContent = FmbUtil.ReadObject<NpcActionContent>(reader, xnb);

			return obj;
		}

		public override void Write(BinaryWriter writer, object obj_) {
			SpeechLine obj = (SpeechLine) obj_;

			FmbUtil.WriteObject(writer, obj.Text);
			FmbUtil.WriteObject(writer, obj.OverrideContent);
		}
	}
}
