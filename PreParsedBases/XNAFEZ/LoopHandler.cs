using FmbLib;
using System;
using System.IO;
using FezEngine.Structure;
using Microsoft.Xna.Framework.Content;

namespace FmbLib.TypeHandlers.Fez {
	public class LoopHandler : TypeHandler<Loop> {

		public override object Read(BinaryReader reader, bool xnb) {
			Loop obj = new Loop();

			obj.Duration = reader.ReadInt32();
			obj.LoopTimesFrom = reader.ReadInt32();
			obj.LoopTimesTo = reader.ReadInt32();
			obj.Name = reader.ReadString();
			obj.TriggerFrom = reader.ReadInt32();
			obj.TriggerTo = reader.ReadInt32();
			obj.Delay = reader.ReadInt32();
			obj.Night = reader.ReadBoolean();
			obj.Day = reader.ReadBoolean();
			obj.Dusk = reader.ReadBoolean();
			obj.Dawn = reader.ReadBoolean();
			obj.FractionalTime = reader.ReadBoolean();
			obj.OneAtATime = reader.ReadBoolean();
			obj.CutOffTail = reader.ReadBoolean();

			return obj;
		}

		public override void Write(BinaryWriter writer, object obj_) {
			Loop obj = (Loop) obj_;

			writer.Write(obj.Duration);
			writer.Write(obj.LoopTimesFrom);
			writer.Write(obj.LoopTimesTo);
			writer.Write(obj.Name);
			writer.Write(obj.TriggerFrom);
			writer.Write(obj.TriggerTo);
			writer.Write(obj.Delay);
			writer.Write(obj.Night);
			writer.Write(obj.Day);
			writer.Write(obj.Dusk);
			writer.Write(obj.Dawn);
			writer.Write(obj.FractionalTime);
			writer.Write(obj.OneAtATime);
			writer.Write(obj.CutOffTail);
		}
	}
}
