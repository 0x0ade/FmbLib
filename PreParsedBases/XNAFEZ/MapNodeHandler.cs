using FmbLib;
using System;
using System.IO;
using FezEngine;
using FezEngine.Structure;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace FmbLib.TypeHandlers.Fez {
	public class MapNodeHandler : TypeHandler<MapNode> {

		public override object Read(BinaryReader reader, bool xnb) {
			MapNode obj = new MapNode();

			obj.LevelName = reader.ReadString();
			obj.Connections = FmbUtil.ReadObject<List<MapNode.Connection>>(reader, xnb);
			obj.NodeType = FmbUtil.ReadObject<LevelNodeType>(reader, xnb);
			obj.Conditions = FmbUtil.ReadObject<WinConditions>(reader, xnb);
			obj.HasLesserGate = reader.ReadBoolean();
			obj.HasWarpGate = reader.ReadBoolean();

			return obj;
		}

		public override void Write(BinaryWriter writer, object obj_) {
			MapNode obj = (MapNode) obj_;

			writer.Write(obj.LevelName);
			FmbUtil.WriteObject(writer, obj.Connections);
			FmbUtil.WriteObject(writer, obj.NodeType);
			FmbUtil.WriteObject(writer, obj.Conditions);
			writer.Write(obj.HasLesserGate);
			writer.Write(obj.HasWarpGate);
		}
	}
}
