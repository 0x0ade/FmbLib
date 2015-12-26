using FmbLib;
using System;
using System.IO;
using FezEngine.Content;
using UnityEngine;

namespace FmbLib.TypeHandlers.Fez {
	public class FrameHandler : TypeHandler<Frame> {

		public override object Read(BinaryReader reader, bool xnb) {
			Frame obj = new Frame();

			obj.Duration = FmbUtil.ReadObject<TimeSpan>(reader, xnb);
			obj.Rectangle = FmbUtil.ReadObject<Rectangle>(reader, xnb);

			return obj;
		}

		public override void Write(BinaryWriter writer, object obj_) {
			Frame obj = (Frame) obj_;

			FmbUtil.WriteObject(writer, obj.Duration);
			FmbUtil.WriteObject(writer, obj.Rectangle);
		}
	}
}
