using FmbLib;
using System;
using System.IO;
using FezEngine.Structure.Scripting;
using Microsoft.Xna.Framework.Content;

namespace FmbLib.TypeHandlers.Fez {
	public class ScriptTriggerHandler : TypeHandler<ScriptTrigger> {

		public override object Read(BinaryReader reader, bool xnb) {
			ScriptTrigger obj = new ScriptTrigger();

			obj.Object = FmbUtil.ReadObject<Entity>(reader, xnb);
			obj.Event = reader.ReadString();

			return obj;
		}

		public override void Write(BinaryWriter writer, object obj_) {
			ScriptTrigger obj = (ScriptTrigger) obj_;

			FmbUtil.WriteObject(writer, obj.Object);
			writer.Write(obj.Event);
		}
	}
}
