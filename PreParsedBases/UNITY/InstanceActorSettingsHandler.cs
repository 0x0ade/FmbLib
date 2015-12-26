using FmbLib;
using System;
using System.IO;
using FezEngine.Structure;
using UnityEngine;

namespace FmbLib.TypeHandlers.Fez {
	public class InstanceActorSettingsHandler : TypeHandler<InstanceActorSettings> {

		public override object Read(BinaryReader reader, bool xnb) {
			InstanceActorSettings obj = new InstanceActorSettings();

			obj.ContainedTrile = FmbUtil.ReadObject<int?>(reader, xnb);
			obj.SignText = FmbUtil.ReadObject<string>(reader, xnb);
			obj.Sequence = FmbUtil.ReadObject<bool[]>(reader, xnb);
			obj.SequenceSampleName = FmbUtil.ReadObject<string>(reader, xnb);
			obj.SequenceAlternateSampleName = FmbUtil.ReadObject<string>(reader, xnb);
			obj.HostVolume = FmbUtil.ReadObject<int?>(reader, xnb);

			return obj;
		}

		public override void Write(BinaryWriter writer, object obj_) {
			InstanceActorSettings obj = (InstanceActorSettings) obj_;

			FmbUtil.WriteObject(writer, obj.ContainedTrile);
			FmbUtil.WriteObject(writer, obj.SignText);
			FmbUtil.WriteObject(writer, obj.Sequence);
			FmbUtil.WriteObject(writer, obj.SequenceSampleName);
			FmbUtil.WriteObject(writer, obj.SequenceAlternateSampleName);
			FmbUtil.WriteObject(writer, obj.HostVolume);
		}
	}
}
