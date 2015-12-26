using FmbLib;
using System;
using System.IO;
using FezEngine;
using FezEngine.Structure;
using UnityEngine;

namespace FmbLib.TypeHandlers.Fez {
	public class MapNodeConnectionHandler : TypeHandler<MapNodeConnection> {

		public override object Read(BinaryReader reader, bool xnb) {
			MapNodeConnection obj = new MapNodeConnection();

			obj.Face = FmbUtil.ReadObject<FaceOrientation>(reader, xnb);
			obj.Node = FmbUtil.ReadObject<MapNode>(reader, xnb);
			obj.BranchOversize = reader.ReadSingle();

			return obj;
		}

		public override void Write(BinaryWriter writer, object obj_) {
			MapNodeConnection obj = (MapNodeConnection) obj_;

			FmbUtil.WriteObject(writer, obj.Face);
			FmbUtil.WriteObject(writer, obj.Node);
			writer.Write(obj.BranchOversize);
		}
	}
}
