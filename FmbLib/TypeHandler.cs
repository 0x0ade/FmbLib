using System;
using System.IO;

namespace FmbLib {
    public abstract class TypeHandler {

        public string ReaderName;
        public Type Type;

        public TypeHandler() {
        }

        public TypeHandler(string readerName) {
            ReaderName = readerName;
        }

        public TypeHandler(Type type) {
            Type = type;
        }

        public abstract object Read(BinaryReader reader, bool xnb);
        public virtual T Read<T>(BinaryReader reader, bool xnb) {
            return (T) Read(reader, xnb);
        }

        public abstract void Write(BinaryWriter writer, object obj_);
    }

    public abstract class TypeHandler<T> : TypeHandler {

        public TypeHandler()
            : base(typeof(T)) {
        }

    }
}

