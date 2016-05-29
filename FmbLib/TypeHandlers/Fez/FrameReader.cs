using FmbLib;
using System;
using System.IO;
using FezEngine.Content;
using UnityEngine;

namespace FmbLib.TypeHandlers.Fez {
	public class FrameContentHandler : TypeHandler<FrameContent> {

		public override object Read(BinaryReader reader, bool xnb) {
			FrameContent obj = new FrameContent();

			obj.Duration = FmbUtil.ReadObject<TimeSpan>(reader, xnb);
			obj.Rectangle = FmbUtil.ReadObject<Rectangle>(reader, xnb);

			return obj;
		}

		public override void Write(BinaryWriter writer, object obj_) {
			FrameContent obj = (FrameContent) obj_;

			FmbUtil.WriteObject(writer, obj.Duration);
			FmbUtil.WriteObject(writer, obj.Rectangle);
		}
	}
	
	//Remapping as the reader's called FrameReader, not FrameContentReader
	public class FrameHandler : FrameContentHandler {}
}
