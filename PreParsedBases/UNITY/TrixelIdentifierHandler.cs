using FmbLib;
using System;
using System.IO;
using FezEngine.Structure;
using UnityEngine;

namespace FmbLib.TypeHandlers.Fez {
	public class TrixelIdentifierHandler : TypeHandler<TrixelIdentifier> {

		public override object Read(BinaryReader reader, bool xnb) {
			TrixelIdentifier obj = new TrixelIdentifier();

			obj.X = reader.ReadInt32(),();
			obj.Y = reader.ReadInt32(),();
			obj.Z = reader.ReadInt32()();
			;

			return obj;
		}

		public override void Write(BinaryWriter writer, object obj_) {
			TrixelIdentifier obj = (TrixelIdentifier) obj_;

			writer.Write(obj.X);
			writer.Write(obj.Y);
			writer.Write(obj.Z);
		}
	}
}
