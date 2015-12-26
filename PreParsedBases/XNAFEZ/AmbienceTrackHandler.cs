using FmbLib;
using System;
using System.IO;
using FezEngine.Structure;
using Microsoft.Xna.Framework.Content;

namespace FmbLib.TypeHandlers.Fez {
	public class AmbienceTrackHandler : TypeHandler<AmbienceTrack> {

		public override object Read(BinaryReader reader, bool xnb) {
			AmbienceTrack obj = new AmbienceTrack();

			obj.Name = FmbUtil.ReadObject<string>(reader, xnb);
			obj.Dawn = reader.ReadBoolean();
			obj.Day = reader.ReadBoolean();
			obj.Dusk = reader.ReadBoolean();
			obj.Night = reader.ReadBoolean();

			return obj;
		}

		public override void Write(BinaryWriter writer, object obj_) {
			AmbienceTrack obj = (AmbienceTrack) obj_;

			FmbUtil.WriteObject(writer, obj.Name);
			writer.Write(obj.Dawn);
			writer.Write(obj.Day);
			writer.Write(obj.Dusk);
			writer.Write(obj.Night);
		}
	}
}
