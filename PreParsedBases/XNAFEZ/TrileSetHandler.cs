using FmbLib;
using System;
using System.IO;
using FezEngine.Structure;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace FmbLib.TypeHandlers.Fez {
	public class TrileSetHandler : TypeHandler<TrileSet> {

		public override object Read(BinaryReader reader, bool xnb) {
			TrileSet obj = new TrileSet();

			obj.Name = reader.ReadString();
			obj.Triles = FmbUtil.ReadObject<Dictionary<int, Trile>>(reader, xnb);
			obj.TextureAtlas = FmbUtil.ReadObject<Texture2D>(reader, xnb);
			obj.OnDeserialization();

			return obj;
		}

		public override void Write(BinaryWriter writer, object obj_) {
			TrileSet obj = (TrileSet) obj_;

			writer.Write(obj.Name);
			FmbUtil.WriteObject(writer, obj.Triles);
			FmbUtil.WriteObject(writer, obj.TextureAtlas);
		}
	}
}
