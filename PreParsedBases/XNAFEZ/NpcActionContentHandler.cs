using FmbLib;
using System;
using System.IO;
using FezEngine.Structure;
using Microsoft.Xna.Framework.Content;

namespace FmbLib.TypeHandlers.Fez {
	public class NpcActionContentHandler : TypeHandler<NpcActionContent> {

		public override object Read(BinaryReader reader, bool xnb) {
			NpcActionContent obj = new NpcActionContent();

			obj.AnimationName = FmbUtil.ReadObject<string>(reader, xnb);
			obj.SoundName = FmbUtil.ReadObject<string>(reader, xnb);

			return obj;
		}

		public override void Write(BinaryWriter writer, object obj_) {
			NpcActionContent obj = (NpcActionContent) obj_;

			FmbUtil.WriteObject(writer, obj.AnimationName);
			FmbUtil.WriteObject(writer, obj.SoundName);
		}
	}
}
