using System;
using FmbLib;
using System.IO;

#if !UNITY
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
#else
using UnityEngine;
#endif

namespace FmbLib.TypeHandlers.Xna {
    public class MatrixHandler : TypeHandler<Matrix> {

        public override object Read(BinaryReader reader, bool xnb) {
            return new Matrix(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
        }

        public override void Write(BinaryWriter writer, object obj_) {
            throw new NotImplementedException("Writing Matrix not implemented yet!");
        }
    }
}
