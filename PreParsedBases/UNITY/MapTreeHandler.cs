using FmbLib;
using System;
using System.IO;
using FezEngine.Structure;
using UnityEngine;

namespace FmbLib.TypeHandlers.Fez {
	public class MapTreeHandler : TypeHandler<MapTree> {

		public override object Read(BinaryReader reader, bool xnb) {
			MapTree obj = new MapTree();

			obj.Root = FmbUtil.ReadObject<MapNode>(reader, xnb);

			return obj;
		}

		public override void Write(BinaryWriter writer, object obj_) {
			MapTree obj = (MapTree) obj_;

			FmbUtil.WriteObject(writer, obj.Root);
		}
	}
}
