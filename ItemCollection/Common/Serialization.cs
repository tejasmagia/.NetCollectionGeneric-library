using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Tejas.Serialization
{
	public class SerializationUtil
	{
		#region Constructors
		//================================================================================				
		private SerializationUtil() {} // non-creatable
		//================================================================================
		#endregion		
		
		#region Methods
		//================================================================================
		public static byte[] BinarySerializeObject(object dataObject)
		{
			using (System.IO.MemoryStream stream = new MemoryStream())
			{
				System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new BinaryFormatter();
				formatter.Serialize(stream, dataObject);
				stream.Position = 0;
				//
				return stream.ToArray();
			}
		}
		//================================================================================
		public static object BinaryDeserializeObject(byte[] bytes)
		{
			using (System.IO.MemoryStream stream = new MemoryStream(bytes))
			{
				stream.Position = 0;
				System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new BinaryFormatter();
				//
				return formatter.Deserialize(stream);
			}
		}		
		//================================================================================
		public static object BinaryCloneObject(object dataObject)
		{
			return BinaryDeserializeObject(BinarySerializeObject(dataObject));
		}
		//================================================================================
		#endregion		
	}	
}
