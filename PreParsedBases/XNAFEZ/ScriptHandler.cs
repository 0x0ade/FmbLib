using FmbLib;
using System;
using System.IO;
using FezEngine.Structure.Scripting;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace FmbLib.TypeHandlers.Fez {
	public class ScriptHandler : TypeHandler<Script> {

		public override object Read(BinaryReader reader, bool xnb) {
			Script obj = new Script();

			obj.Name = reader.ReadString();
			obj.Timeout = FmbUtil.ReadObject<TimeSpan?>(reader, xnb);
			obj.Triggers = FmbUtil.ReadObject<List<ScriptTrigger>>(reader, xnb);
			obj.Conditions = FmbUtil.ReadObject<List<ScriptCondition>>(reader, xnb);
			obj.Actions = FmbUtil.ReadObject<List<ScriptAction>>(reader, xnb);
			obj.OneTime = reader.ReadBoolean();
			obj.Triggerless = reader.ReadBoolean();
			obj.IgnoreEndTriggers = reader.ReadBoolean();
			obj.LevelWideOneTime = reader.ReadBoolean();
			obj.Disabled = reader.ReadBoolean();
			obj.IsWinCondition = reader.ReadBoolean();

			return obj;
		}

		public override void Write(BinaryWriter writer, object obj_) {
			Script obj = (Script) obj_;

			writer.Write(obj.Name);
			FmbUtil.WriteObject(writer, obj.Timeout);
			FmbUtil.WriteObject(writer, obj.Triggers);
			FmbUtil.WriteObject(writer, obj.Conditions);
			FmbUtil.WriteObject(writer, obj.Actions);
			writer.Write(obj.OneTime);
			writer.Write(obj.Triggerless);
			writer.Write(obj.IgnoreEndTriggers);
			writer.Write(obj.LevelWideOneTime);
			writer.Write(obj.Disabled);
			writer.Write(obj.IsWinCondition);
		}
	}
}
