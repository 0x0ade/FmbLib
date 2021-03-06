﻿using System;
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
                FmbHelper.Log("Reading Texture2Ds will not be implemented - use external images and read / write manually!");
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

            Texture2D texture = GenTexture(width, height, surfaceFormat, levels);

            #if XNA
            //For XNA, we simply iterate through all levels and set the level data.
            for (int i = 0; i < levels; i++) {
                int levelSize = reader.ReadInt32();
                texture.SetData(i, null, reader.ReadBytes(levelSize), 0, levelSize);
            }
            #elif UNITY
            //Oh, Unity, why do we need to merge all levels into a single blob? RIP loading time.
            if (levels == 1) {
                texture.LoadRawTextureData(Remap(reader.ReadBytes(reader.ReadInt32()), surfaceFormat));
            } else {
                int dataSize = 0;
                
                byte[][] levelDatas = new byte[levels][];
                for (int i = 0; i < levels; i++) {
                    int levelSize = reader.ReadInt32();
                    dataSize += levelSize;
                    levelDatas[i] = Remap(reader.ReadBytes(levelSize), surfaceFormat);
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
            FmbHelper.Log("Writing Texture2Ds will not be implemented - use external images and read / write manually!");
            writer.Write((int) 0); //surfaceFormat
            writer.Write((int) 0); //width
            writer.Write((int) 0); //height
            writer.Write((int) 0); //mips
            //writer.Write((int) 0); //data length
        }
        
        public static Texture2D GenTexture(int width, int height, SurfaceFormat surfaceFormat, int levels) {
            #if XNA
            return new Texture2D(
                FmbUtil.Setup.GraphicsDevice,
                width,
                height,
                levels > 1,
                surfaceFormat
            );
            #elif UNITY
            return new Texture2D(
                width,
                height,
                SurfaceFormatToTextureFormat[(int) surfaceFormat],
                levels > 1,
                FmbUtil.Setup.TexturesLinear
            );
            #endif
        }
        
        public static byte[] Remap(byte[] data, SurfaceFormat format) {
            int[] map = {0, 1, 2};

            switch (format) {
            case SurfaceFormat.Color:
                map = new int[] {3, 0, 1, 2};
                break;
            }

            int size = Size(format);
            int length = data.Length / size;
            byte[] mapped = new byte[size];
            for (int i = 0; i < length; i++) {
                for (int ii = 0; ii < size; ii++) {
                    mapped[ii] = data[i * size + map[ii]];
                }
                for (int ii = 0; ii < size; ii++) {
                    data[i * size + ii] = mapped[ii];
                }
            }
            
            return data;
        }
        
        public static int Size(SurfaceFormat format) {
            switch (format) {
            case SurfaceFormat.Dxt1:
                return 8;
            case SurfaceFormat.Dxt3:
            case SurfaceFormat.Dxt5:
                return 16;
            case SurfaceFormat.Alpha8:
                return 1;
            case SurfaceFormat.Bgr565:
            case SurfaceFormat.Bgra4444:
            case SurfaceFormat.Bgra5551:
            case SurfaceFormat.HalfSingle:
            case SurfaceFormat.NormalizedByte2:
                return 2;
            case SurfaceFormat.Color:
            case SurfaceFormat.Single:
            case SurfaceFormat.Rg32:
            case SurfaceFormat.HalfVector2:
            case SurfaceFormat.NormalizedByte4:
            case SurfaceFormat.Rgba1010102:
                return 4;
            case SurfaceFormat.HalfVector4:
            case SurfaceFormat.Rgba64:
            case SurfaceFormat.Vector2:
                return 8;
            case SurfaceFormat.Vector4:
                return 16;
            default:
                return 0;
            }
        }

        #if UNITY
        public static TextureFormat[] SurfaceFormatToTextureFormat = {
            TextureFormat.ARGB32, //Color
            TextureFormat.RGB565, //Bgr565 //TODO swap R and B in Texture2DHandler for Bgr565 > RGB565
            TextureFormat.ARGB32, //Bgra5551 //TODO Bgra5551 is missing in Unity; Convert data!
            TextureFormat.RGBA4444, //Bgra4444 //TODO swap R and B in Texture2DHandler for Bgra4444 > RGBA4444
            TextureFormat.DXT1, //Dxt1
            TextureFormat.ARGB32, //Dxt3 //TODO Dxt5 is missing in Unity; Convert data!
            TextureFormat.DXT5, //Dxt5
            TextureFormat.ARGB32, //NormalizedByte2 //TODO NormalizedByte2 is missing in Unity; Convert data!
            TextureFormat.ARGB32, //NormalizedByte4 //TODO NormalizedByte4 is missing in Unity; Convert data!
            TextureFormat.ARGB32, //Rgba1010102 //TODO Rgba1010102 is missing in Unity; Convert data!
            TextureFormat.RGHalf, //Rg32
            TextureFormat.RGBAHalf, //Rgba64
            TextureFormat.Alpha8, //Alpha8
            TextureFormat.RFloat, //Single
            TextureFormat.RGFloat, //Vector2
            TextureFormat.RGBAFloat, //Vector4
            TextureFormat.RHalf, //HalfSingle
            TextureFormat.RGHalf, //HalfVector2
            TextureFormat.RGBAHalf, //HalfVector4
            TextureFormat.ARGB32 //HdrBlendable //TODO HdrBlendable is missing in Unity; Convert data!
        };
        #endif
        
    }
}
