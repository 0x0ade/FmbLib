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


			return obj;
		}

		public override void Write(BinaryWriter writer, object obj_) {
			AnimatedTexture obj = (AnimatedTexture) obj_;

		}
	}
}
