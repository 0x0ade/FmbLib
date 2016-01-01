using FmbLib;
using System;
using System.IO;
using FezEngine.Structure;
using UnityEngine;
using System.Collections.Generic;

namespace FmbLib.TypeHandlers.Fez {
	public class NpcInstanceHandler : TypeHandler<NpcInstance> {

		public override object Read(BinaryReader reader, bool xnb) {
			NpcInstance obj = new NpcInstance();

			obj.Name = reader.ReadString();
			obj.Position = FmbUtil.ReadObject<Vector3>(reader, xnb, false);
			obj.DestinationOffset = FmbUtil.ReadObject<Vector3>(reader, xnb, false);
			obj.WalkSpeed = reader.ReadSingle();
			obj.RandomizeSpeech = reader.ReadBoolean();
			obj.SayFirstSpeechLineOnce = reader.ReadBoolean();
			obj.AvoidsGomez = reader.ReadBoolean();
			obj.ActorType = FmbUtil.ReadObject<ActorType>(reader, xnb);
			obj.Speech = FmbUtil.ReadObject<List<SpeechLine>>(reader, xnb);
			obj.Actions = FmbUtil.ReadObject<Dictionary<NpcAction, NpcActionContent>>(reader, xnb);

			return obj;
		}

		public override void Write(BinaryWriter writer, object obj_) {
			NpcInstance obj = (NpcInstance) obj_;

			writer.Write(obj.Name);
			FmbUtil.GetTypeHandler<Vector3>().Write(writer, obj.Position);
			FmbUtil.GetTypeHandler<Vector3>().Write(writer, obj.DestinationOffset);
			writer.Write(obj.WalkSpeed);
			writer.Write(obj.RandomizeSpeech);
			writer.Write(obj.SayFirstSpeechLineOnce);
			writer.Write(obj.AvoidsGomez);
			FmbUtil.WriteObject(writer, obj.ActorType);
			FmbUtil.WriteObject(writer, obj.Speech);
			FmbUtil.WriteObject(writer, obj.Actions);
		}
	}
}
