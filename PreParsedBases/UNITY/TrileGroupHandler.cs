using FmbLib;
using System;
using System.IO;
using FezEngine.Structure;
using UnityEngine;
using System.Collections.Generic;

namespace FmbLib.TypeHandlers.Fez {
	public class TrileGroupHandler : TypeHandler<TrileGroup> {

		public override object Read(BinaryReader reader, bool xnb) {
			TrileGroup obj = new TrileGroup();

			obj.Triles = FmbUtil.ReadObject<List<TrileInstance>>(reader, xnb);
			obj.Path = FmbUtil.ReadObject<MovementPath>(reader, xnb);
			obj.Heavy = reader.ReadBoolean();
			obj.ActorType = FmbUtil.ReadObject<ActorType>(reader, xnb);
			obj.GeyserOffset = reader.ReadSingle();
			obj.GeyserPauseFor = reader.ReadSingle();
			obj.GeyserLiftFor = reader.ReadSingle();
			obj.GeyserApexHeight = reader.ReadSingle();
			obj.SpinCenter = FmbUtil.ReadObject<Vector3>(reader, xnb, false);
			obj.SpinClockwise = reader.ReadBoolean();
			obj.SpinFrequency = reader.ReadSingle();
			obj.SpinNeedsTriggering = reader.ReadBoolean();
			obj.Spin180Degrees = reader.ReadBoolean();
			obj.FallOnRotate = reader.ReadBoolean();
			obj.SpinOffset = reader.ReadSingle();
			obj.AssociatedSound = FmbUtil.ReadObject<string>(reader, xnb);

			return obj;
		}

		public override void Write(BinaryWriter writer, object obj_) {
			TrileGroup obj = (TrileGroup) obj_;

			FmbUtil.WriteObject(writer, obj.Triles);
			FmbUtil.WriteObject(writer, obj.Path);
			writer.Write(obj.Heavy);
			FmbUtil.WriteObject(writer, obj.ActorType);
			writer.Write(obj.GeyserOffset);
			writer.Write(obj.GeyserPauseFor);
			writer.Write(obj.GeyserLiftFor);
			writer.Write(obj.GeyserApexHeight);
			FmbUtil.GetTypeHandler<Vector3>().Write(writer, obj.SpinCenter);
			writer.Write(obj.SpinClockwise);
			writer.Write(obj.SpinFrequency);
			writer.Write(obj.SpinNeedsTriggering);
			writer.Write(obj.Spin180Degrees);
			writer.Write(obj.FallOnRotate);
			writer.Write(obj.SpinOffset);
			FmbUtil.WriteObject(writer, obj.AssociatedSound);
		}
	}
}
