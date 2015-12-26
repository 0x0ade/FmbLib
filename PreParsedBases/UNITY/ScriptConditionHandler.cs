using FmbLib;
using System;
using System.IO;
using FezEngine.Structure.Scripting;
using UnityEngine;

namespace FmbLib.TypeHandlers.Fez {
	public class ScriptConditionHandler : TypeHandler<ScriptCondition> {

		public override object Read(BinaryReader reader, bool xnb) {
			ScriptCondition obj = new ScriptCondition();

			obj.Object = FmbUtil.ReadObject<Entity>(reader, xnb);
			obj.Operator = FmbUtil.ReadObject<ComparisonOperator>(reader, xnb);
			obj.Property = reader.ReadString();
			obj.Value = reader.ReadString();
			obj.OnDeserialization();

			return obj;
		}

		public override void Write(BinaryWriter writer, object obj_) {
			ScriptCondition obj = (ScriptCondition) obj_;

			FmbUtil.WriteObject(writer, obj.Object);
			FmbUtil.WriteObject(writer, obj.Operator);
			writer.Write(obj.Property);
			writer.Write(obj.Value);
		}
	}
}
