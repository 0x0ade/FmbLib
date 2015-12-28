using FmbLib;
using System;
using System.IO;
using FezEngine.Content;
using FezEngine.Structure;
using FezEngine.Tools;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace FmbLib.TypeHandlers.Fez {
	public class AnimatedTextureHandler : TypeHandler<AnimatedTexture> {

		public override object Read(BinaryReader reader, bool xnb) {
			AnimatedTexture obj = new AnimatedTexture();

			reader.ReadInt32();
			reader.ReadInt32();
			obj.FrameWidth = reader.ReadInt32();
			obj.FrameHeight = reader.ReadInt32();
			reader.BaseStream.Seek(reader.ReadInt32(), SeekOrigin.Current);
			List<FrameContent> list = FmbUtil.ReadObject<List<FrameContent>>(reader, xnb);
			Offsets Enumerable.ToArray<Rectangle>(Enumerable.Select<FrameContent, Rectangle>((IEnumerable<FrameContent>) list, (Func<FrameContent, Rectangle>) (x => x.Rectangle)));
			Timing new AnimationTiming(0, list.Count - 1, Enumerable.ToArray<float>(Enumerable.Select<FrameContent, float>((IEnumerable<FrameContent>) list, (Func<FrameContent, float>) (x => (float) x.Duration.TotalSeconds))));
			PotOffset new Vector2((float) (FezMath.NextPowerOfTwo((double) FrameWidth) - FrameWidth), (float) (FezMath.NextPowerOfTwo((double) FrameHeight) - FrameHeight));

			return obj;
		}

		public override void Write(BinaryWriter writer, object obj_) {
			AnimatedTexture obj = (AnimatedTexture) obj_;

			writer.Write((int) -1);
			writer.Write((int) -1);
			writer.Write(obj.FrameWidth);
			writer.Write(obj.FrameHeight);
			writer.Write((int) 0);
			Console.WriteLine("TODO: ANIMATED TEXTURE WRITER");
			Console.WriteLine("TODO: ANIMATED TEXTURE WRITER");
			Console.WriteLine("TODO: ANIMATED TEXTURE WRITER");
		}
	}
}
TEXTURE WRITER");
		}
	}
}
