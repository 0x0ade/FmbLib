using FmbLib;
using System;
using System.IO;
using FezEngine.Structure;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace FmbLib.TypeHandlers.Fez {
	public class PathSegmentHandler : TypeHandler<PathSegment> {

		public override object Read(BinaryReader reader, bool xnb) {
			PathSegment obj = new PathSegment();

			obj.Destination = FmbUtil.ReadObject<Vector3>(reader, xnb, false);
			obj.Duration = FmbUtil.ReadObject<TimeSpan>(reader, xnb);
			obj.WaitTimeOnStart = FmbUtil.ReadObject<TimeSpan>(reader, xnb);
			obj.WaitTimeOnFinish = FmbUtil.ReadObject<TimeSpan>(reader, xnb);
			obj.Acceleration = reader.ReadSingle();
			obj.Deceleration = reader.ReadSingle();
			obj.JitterFactor = reader.ReadSingle();
			obj.Orientation = FmbUtil.ReadObject<Quaternion>(reader, xnb, false);
			if (reader.ReadBoolean()) {
			obj.CustomData = FmbUtil.ReadObject<CameraNodeData>(reader, xnb);
			}

			return obj;
		}

		public override void Write(BinaryWriter writer, object obj_) {
			PathSegment obj = (PathSegment) obj_;

			FmbUtil.GetTypeHandler<Vector3>().Write(writer, obj.Destination);
			FmbUtil.WriteObject(writer, obj.Duration);
			FmbUtil.WriteObject(writer, obj.WaitTimeOnStart);
			FmbUtil.WriteObject(writer, obj.WaitTimeOnFinish);
			writer.Write(obj.Acceleration);
			writer.Write(obj.Deceleration);
			writer.Write(obj.JitterFactor);
			FmbUtil.GetTypeHandler<Quaternion>().Write(writer, obj.Orientation);
			writer.Write(true);
			FmbUtil.WriteObject(writer, obj.CustomData);
		}
	}
}
