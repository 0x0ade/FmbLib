using System;
using FmbLib;
using System.IO;

using Microsoft.Xna.Framework.Graphics;

#if XNA
using Microsoft.Xna.Framework;
#elif UNITY
using UnityEngine;
#else
#warning FmbLib slim XNA still WIP.
#endif

namespace FmbLib.TypeHandlers.Xna {
    public class Texture2DHandler : TypeHandler<Texture2D> {

        public override object Read(BinaryReader reader, bool xnb) {
            if (!xnb) {
                Console.WriteLine("Reading Texture2Ds will not be implemented - use external images and read / write manually!");
                reader.ReadInt32(); //surfaceFormat
                reader.ReadInt32(); //width
                reader.ReadInt32(); //height
                reader.ReadInt32(); //mips
                //reader.ReadInt32(); //data length
                return null;
            }

            //After I've got feedback from Rioku, we're reading the texture data but (currently) not saving it.

            bool readTexture;
            
            #if XNA
            //XNA requires a graphics device. One should be supplied via FmbUtil.Setup.GraphicsDevice if wanted.
            readTexture = FmbUtil.Setup.GraphicsDevice != null;
            #elif UNITY
            //Unity doesn't need a graphics device and can always read the texture.
            readTexture = true;
            #else
            //XnaSlim will never read internal textures as such. They may return the data in some texture container format, though.
            readTexture = false;
            #endif
            
            if (!readTexture || FmbUtil.IsTEST) {
                reader.ReadInt32(); //surfaceFormat
                reader.ReadInt32(); //width
                reader.ReadInt32(); //height
                int mips = reader.ReadInt32();
                for (int i = 0; i < mips; i++) {
                int dataSize = reader.ReadInt32();
                    reader.BaseStream.Seek(dataSize, SeekOrigin.Current);
                }
                return null;
            }

            SurfaceFormat surfaceFormat = (SurfaceFormat) reader.ReadInt32();
            int width = reader.ReadInt32();
            int height = reader.ReadInt32();
            int levels = reader.ReadInt32();

            //Let's pretend we don't know about DXT1, S3TC nor the other formats to convert.

            Texture2D texture = null;
            #if XNA
            texture = new Texture2D(
                FmbUtil.Setup.GraphicsDevice,
                width,
                height,
                levels > 1,
                surfaceFormat
            );
            #elif UNITY
            texture = new Texture2D(
                width,
                height,
                FmbHelper.SurfaceFormatToTextureFormat[(int) surfaceFormat],
                levels > 1,
                FmbUtil.Setup.TexturesLinear
            );
            #endif

            #if XNA
            //For XNA, we simply iterate through all levels and set the level data.
            for (int i = 0; i < levels; i++) {
                int levelSize = reader.ReadInt32();
                texture.SetData(i, null, reader.ReadBytes(levelSize), 0, levelSize);
            }
            #elif UNITY
            //Oh, Unity, why do we need to merge all levels into a single blob? RIP loading time.
            if (levels == 1) {
                texture.LoadRawTextureData(reader.ReadBytes(reader.ReadInt32()));
            } else {
                int dataSize = 0;
                
                byte[][] levelDatas = new byte[levels][];
                for (int i = 0; i < levels; i++) {
                    int levelSize = reader.ReadInt32();
                    dataSize += levelSize;
                    levelDatas[i] = reader.ReadBytes(levelSize);
                }
                
                byte[] data = new byte[dataSize];
                int offs = 0;
                for (int i = 0; i < levels; i++) {
                    byte[] levelData = levelDatas[i];
                    Array.Copy(levelData, 0, data, offs, levelData.Length);
                }
            }
            
            //updateMipmaps is true by default; makeNoLongerReadable should be false.
            texture.Apply(false, FmbUtil.Setup.TexturesWriteOnly);
            #endif

            return texture;
        }

        public override void Write(BinaryWriter writer, object obj_) {
            Console.WriteLine("Writing Texture2Ds will not be implemented - use external images and read / write manually!");
            writer.Write((int) 0); //surfaceFormat
            writer.Write((int) 0); //width
            writer.Write((int) 0); //height
            writer.Write((int) 0); //mips
            //writer.Write((int) 0); //data length
        }
    }
}
