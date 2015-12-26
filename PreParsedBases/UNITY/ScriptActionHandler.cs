using FmbLib;
using System;
using System.IO;
using FezEngine.Structure.Scripting;
using UnityEngine;

namespace FmbLib.TypeHandlers.Fez {
	public class ScriptActionHandler : TypeHandler<ScriptAction> {

		public override object Read(BinaryReader reader, bool xnb) {
			ScriptAction obj = new ScriptAction();

			obj.Object = FmbUtil.ReadObject<Entity>(reader, xnb);
			obj.Operation = reader.ReadString();
			obj.Arguments = FmbUtil.ReadObject<string[]>(reader, xnb);
			obj.Killswitch = reader.ReadBoolean();
			obj.Blocking = reader.ReadBoolean();
			if (!FmbUtil.IsTEST) {
			obj.OnDeserialization();
			}

			return obj;
		}

		public override void Write(BinaryWriter writer, object obj_) {
			ScriptAction obj = (ScriptAction) obj_;

			FmbUtil.WriteObject(writer, obj.Object);
			writer.Write(obj.Operation);
			FmbUtil.WriteObject(writer, obj.Arguments);
			writer.Write(obj.Killswitch);
			writer.Write(obj.Blocking);
			if (!FmbUtil.IsTEST) {
			}
		}
	}
}
