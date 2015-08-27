using System;
using FmbLib;
using System.IO;

#if XNA
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#else
#warning FmbLib slim XNA still WIP.
#endif

namespace FmbLib.TypeHandlers.Xna {
    public class Texture2DHandler : TypeHandler<Texture2D> {

        public override object Read(BinaryReader reader, bool xnb) {
            if (!xnb) {
                throw new NotImplementedException("Reading Texture2Ds will not be implemented - use external images and read / write manually!");
            }

            //Internal textures will not be loaded, just skipped
            int surfaceFormat = reader.ReadInt32();
            int width = reader.ReadInt32();
            int height = reader.ReadInt32();
            int mips = reader.ReadInt32();
            for (int i = 0; i < mips; i++) {
                int dataSize = reader.ReadInt32();
                reader.BaseStream.Seek(dataSize, SeekOrigin.Current);
            }

            return null;
        }

        public override void Write(BinaryWriter writer, object obj_) {
            throw new NotImplementedException("Writing Texture2Ds will not be implemented - use external images and read / write manually!");
        }
    }
}
