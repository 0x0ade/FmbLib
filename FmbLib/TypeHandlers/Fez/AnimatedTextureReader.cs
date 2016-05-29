using FmbLib;
using System;
using System.IO;
using FezEngine.Content;
using FezEngine.Structure;
using FezEngine.Tools;
using System.Collections.Generic;
using FmbLib.TypeHandlers.Xna;

using Microsoft.Xna.Framework.Graphics;

#if XNA
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
#elif UNITY
using UnityEngine;
#else
#warning FmbLib slim XNA still WIP.
#endif

namespace FmbLib.TypeHandlers.Fez {
	public class AnimatedTextureHandler : TypeHandler<AnimatedTexture> {

		public override object Read(BinaryReader reader, bool xnb) {
			AnimatedTexture obj = new AnimatedTexture();

			int width = reader.ReadInt32();
			int height = reader.ReadInt32();
			obj.FrameWidth = reader.ReadInt32();
			obj.FrameHeight = reader.ReadInt32();
			
			int dataSize = reader.ReadInt32();
			Texture2D texture = Texture2DHandler.GenTexture(width, height, SurfaceFormat.Color, 1);
#if XNA
            //For XNA, we simply pass the data for the only level.
			texture.SetData(0, null, reader.ReadBytes(dataSize), 0, dataSize);
#elif UNITY
            //Oh, Unity, wh~ oh, just one level. Continue on.
			texture.LoadRawTextureData(Texture2DHandler.Remap(reader.ReadBytes(dataSize), SurfaceFormat.Color));
            
            //updateMipmaps is true by default; makeNoLongerReadable should be false.
            texture.Apply(false, FmbUtil.Setup.TexturesWriteOnly);
#endif
			
			obj.Texture = texture;
			
			FrameContent[] list = FmbUtil.ReadObject<List<FrameContent>>(reader, xnb).ToArray();
			obj.Offsets = FmbHelper.Select<FrameContent, Rectangle>(list, delegate(FrameContent x) { return x.Rectangle; });
			obj.Timing = new AnimationTiming(0, list.Length - 1, FmbHelper.Select<FrameContent, float>(list, delegate(FrameContent x) { return (float) x.Duration.TotalSeconds; }));
			obj.PotOffset = new Vector2((float) (FmbHelper.NextPowerOfTwo(obj.FrameWidth) - obj.FrameWidth), (float) (FmbHelper.NextPowerOfTwo(obj.FrameHeight) - obj.FrameHeight));

			return obj;
		}

		public override void Write(BinaryWriter writer, object obj_) {
			AnimatedTexture obj = (AnimatedTexture) obj_;

			writer.Write((int) -1);
			writer.Write((int) -1);
			writer.Write(obj.FrameWidth);
			writer.Write(obj.FrameHeight);
			writer.Write((int) 0);
			FmbHelper.Log("TODO: ANIMATED TEXTURE WRITER");
			FmbHelper.Log("TODO: ANIMATED TEXTURE WRITER");
			FmbHelper.Log("TODO: ANIMATED TEXTURE WRITER");
		}
	}
}
