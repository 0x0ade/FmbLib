using FmbLib;
using System;
using System.IO;
using FezEngine.Structure;
using UnityEngine;
using System.Collections.Generic;

namespace FmbLib.TypeHandlers.Fez {
	public class WinConditionsHandler : TypeHandler<WinConditions> {

		public override object Read(BinaryReader reader, bool xnb) {
			WinConditions obj = new WinConditions();

			obj.ChestCount = reader.ReadInt32();
			obj.LockedDoorCount = reader.ReadInt32();
			obj.UnlockedDoorCount = reader.ReadInt32();
			obj.ScriptIds = FmbUtil.ReadObject<List<int>>(reader, xnb);
			obj.CubeShardCount = reader.ReadInt32();
			obj.OtherCollectibleCount = reader.ReadInt32();
			obj.SplitUpCount = reader.ReadInt32();
			obj.SecretCount = reader.ReadInt32();

			return obj;
		}

		public override void Write(BinaryWriter writer, object obj_) {
			WinConditions obj = (WinConditions) obj_;

			writer.Write(obj.ChestCount);
			writer.Write(obj.LockedDoorCount);
			writer.Write(obj.UnlockedDoorCount);
			FmbUtil.WriteObject(writer, obj.ScriptIds);
			writer.Write(obj.CubeShardCount);
			writer.Write(obj.OtherCollectibleCount);
			writer.Write(obj.SplitUpCount);
			writer.Write(obj.SecretCount);
		}
	}
}
