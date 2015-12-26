using FmbLib;
using System;
using System.IO;
using FezEngine.Structure.Scripting;
using UnityEngine;

namespace FmbLib.TypeHandlers.Fez {
	public class EntityHandler : TypeHandler<Entity> {

		public override object Read(BinaryReader reader, bool xnb) {
			Entity obj = new Entity();

			obj.Type = reader.ReadString();
			obj.Identifier = FmbUtil.ReadObject<int?>(reader, xnb);

			return obj;
		}

		public override void Write(BinaryWriter writer, object obj_) {
			Entity obj = (Entity) obj_;

			writer.Write(obj.Type);
			FmbUtil.WriteObject(writer, obj.Identifier);
		}
	}
}
