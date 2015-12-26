using FmbLib;
using System;
using System.IO;
using FezEngine.Structure;
using UnityEngine;
using System.Collections.Generic;

namespace FmbLib.TypeHandlers.Fez {
	public class TrackedSongHandler : TypeHandler<TrackedSong> {

		public override object Read(BinaryReader reader, bool xnb) {
			TrackedSong obj = new TrackedSong();

			obj.Loops = FmbUtil.ReadObject<List<Loop>>(reader, xnb);
			obj.Name = reader.ReadString();
			obj.Tempo = reader.ReadInt32();
			obj.TimeSignature = reader.ReadInt32();
			obj.Notes = FmbUtil.ReadObject<ShardNotes[]>(reader, xnb);
			obj.AssembleChord = FmbUtil.ReadObject<AssembleChords>(reader, xnb);
			obj.RandomOrdering = reader.ReadBoolean();
			obj.CustomOrdering = FmbUtil.ReadObject<int[]>(reader, xnb);

			return obj;
		}

		public override void Write(BinaryWriter writer, object obj_) {
			TrackedSong obj = (TrackedSong) obj_;

			FmbUtil.WriteObject(writer, obj.Loops);
			writer.Write(obj.Name);
			writer.Write(obj.Tempo);
			writer.Write(obj.TimeSignature);
			FmbUtil.WriteObject(writer, obj.Notes);
			FmbUtil.WriteObject(writer, obj.AssembleChord);
			writer.Write(obj.RandomOrdering);
			FmbUtil.WriteObject(writer, obj.CustomOrdering);
		}
	}
}
