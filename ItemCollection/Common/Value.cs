using System;
using System.Globalization;

namespace Tejas
{	
	public class Null
	{
		#region Constructors
		//================================================================================				
		private Null() {} // non-creatable
		//================================================================================
		#endregion		

		#region Fields
		//================================================================================				
		public static readonly DateTime DateTime = System.DateTime.MinValue;
		public static readonly TimeSpan TimeSpan = System.TimeSpan.MinValue;
		public static readonly byte Byte = System.Byte.MinValue;
		public static readonly short Short = System.Int16.MinValue;
		public static readonly ushort UShort = System.UInt16.MinValue;		
		public static readonly int Integer = System.Int32.MinValue;
		public static readonly uint UInteger = System.UInt32.MinValue;		
		public static readonly long Long = System.Int64.MinValue;
		public static readonly ulong ULong = System.UInt64.MinValue;
		public static readonly decimal Decimal = System.Decimal.MinValue;
		public static readonly float Single = System.Single.NaN;
		public static readonly double Double = System.Double.NaN;
		public static readonly string String = String.Empty;
		public static readonly Guid Guid = System.Guid.Empty;
		public static readonly DBNull DbValue = DBNull.Value;
		//================================================================================		
		#endregion
	}

	public class ValueUtil
	{
		#region Constructors
		//================================================================================				
		private ValueUtil() {} // non-creatable
		//================================================================================
		#endregion		

		#region Methods
		//================================================================================
		public static bool IsNull(DateTime value)	{return value == Null.DateTime;}
		public static bool IsNull(TimeSpan value)	{return value == Null.TimeSpan;}
		public static bool IsNull(byte value)		{return value == Null.Byte;}
		public static bool IsNull(short value)		{return value == Null.Short;}
		public static bool IsNull(int value)		{return value == Null.Integer;}
		public static bool IsNull(long value)		{return value == Null.Long;}
		public static bool IsNull(decimal value)	{return value == Null.Decimal;}
		public static bool IsNull(float value)		{return Single.IsNaN(value);}
		public static bool IsNull(double value)		{return Double.IsNaN(value);}
		public static bool IsNull(string value)		{return (value == null || value.Length == 0);}
		public static bool IsNull(Guid value)		{return value == Null.Guid;}
		//================================================================================
		public static bool IsNumeric(string value) {double result = 0; return IsNumeric(value, out result);}
		public static bool IsNumeric(string value, System.IFormatProvider formatProvider) {double result = 0; return IsNumeric(value, formatProvider, out result);}
		public static bool IsNumeric(string value, System.IFormatProvider formatProvider, System.Globalization.NumberStyles styles) {double result = 0; return IsNumeric(value, formatProvider, styles, out result);}
		public static bool IsNumeric(string value, out double result) {return IsNumeric(value, null, out result);}
		public static bool IsNumeric(string value, System.IFormatProvider formatProvider, out double result) {return IsNumeric(value, formatProvider, NumberStyles.Any, out result);}
		public static bool IsNumeric(string value, System.IFormatProvider formatProvider, System.Globalization.NumberStyles styles, out double result)
		{
			result = 0;
			if (value != null && value.Length > 0)
				return Double.TryParse(value, styles, formatProvider, out result);
			else
				return false;
		}
		//================================================================================
		public static byte ParseByte(string value) {return ParseByte(value, null);}
		public static byte ParseByte(string value, System.IFormatProvider formatProvider) {return ParseByte(value, formatProvider, NumberStyles.Any);}
		public static byte ParseByte(string value, System.IFormatProvider formatProvider, System.Globalization.NumberStyles styles)
		{
			// Parse
			double result = 0;
			if (IsNumeric(value, formatProvider, out result))
				if (Byte.MinValue <= result && result <= Byte.MaxValue)
					return Convert.ToByte(result);

			return Null.Byte;
		}
		//================================================================================
		public static short ParseShort(string value) {return ParseShort(value, null);}
		public static short ParseShort(string value, System.IFormatProvider formatProvider) {return ParseShort(value, formatProvider, NumberStyles.Any);}
		public static short ParseShort(string value, System.IFormatProvider formatProvider, System.Globalization.NumberStyles styles)
		{
			// Parse
			double result = 0;
			if (IsNumeric(value, formatProvider, out result))
				if (Int16.MinValue <= result && result <= Int16.MaxValue)
					return Convert.ToInt16(result);
			
			return Null.Short;
		}
		//================================================================================
		public static ushort ParseUShort(string value) {return ParseUShort(value, null);}
		public static ushort ParseUShort(string value, System.IFormatProvider formatProvider) {return ParseUShort(value, formatProvider, NumberStyles.Any);}
		public static ushort ParseUShort(string value, System.IFormatProvider formatProvider, System.Globalization.NumberStyles styles)
		{
			// Parse
			double result = 0;
			if (IsNumeric(value, formatProvider, out result))
				if (Int16.MinValue <= result && result <= Int16.MaxValue)
					return Convert.ToUInt16(result);
			
			return Null.UShort;
		}
		//================================================================================
		public static int ParseInteger(string value) {return ParseInteger(value, null);}
		public static int ParseInteger(string value, System.IFormatProvider formatProvider) {return ParseInteger(value, formatProvider, NumberStyles.Any);}
		public static int ParseInteger(string value, System.IFormatProvider formatProvider, System.Globalization.NumberStyles styles)
		{
			// Parse
			double result = 0;
			if (IsNumeric(value, formatProvider, out result))
				if (Int32.MinValue <= result && result <= Int32.MaxValue)
					return Convert.ToInt32(result);
			
			return Null.Integer;
		}
		//================================================================================
		public static uint ParseUInteger(string value) {return ParseUInteger(value, null);}
		public static uint ParseUInteger(string value, System.IFormatProvider formatProvider) {return ParseUInteger(value, formatProvider, NumberStyles.Any);}
		public static uint ParseUInteger(string value, System.IFormatProvider formatProvider, System.Globalization.NumberStyles styles)
		{
			// Parse
			double result = 0;
			if (IsNumeric(value, formatProvider, out result))
				if (Int32.MinValue <= result && result <= Int32.MaxValue)
					return Convert.ToUInt32(result);
			
			return Null.UInteger;
		}
		//================================================================================
		public static long ParseLong(string value) {return ParseLong(value, null);}
		public static long ParseLong(string value, System.IFormatProvider formatProvider) {return ParseLong(value, formatProvider, NumberStyles.Any);}
		public static long ParseLong(string value, System.IFormatProvider formatProvider, System.Globalization.NumberStyles styles)
		{
			// Parse
			double result = 0;
			if (IsNumeric(value, formatProvider, out result))
				if (Int64.MinValue <= result && result <= Int64.MaxValue)
					return Convert.ToInt64(result);
			
			return Null.Long;
		}
		//================================================================================
		public static ulong ParseULong(string value) {return ParseULong(value, null);}
		public static ulong ParseULong(string value, System.IFormatProvider formatProvider) {return ParseULong(value, formatProvider, NumberStyles.Any);}
		public static ulong ParseULong(string value, System.IFormatProvider formatProvider, System.Globalization.NumberStyles styles)
		{
			// Parse
			double result = 0;
			if (IsNumeric(value, formatProvider, out result))
				if (Int64.MinValue <= result && result <= Int64.MaxValue)
					return Convert.ToUInt64(result);
			
			return Null.ULong;
		}
		//================================================================================
		public static decimal ParseDecimal(string value) {return ParseDecimal(value, null);}
		public static decimal ParseDecimal(string value, System.IFormatProvider formatProvider) {return ParseDecimal(value, formatProvider, NumberStyles.Any);}
		public static decimal ParseDecimal(string value, System.IFormatProvider formatProvider, System.Globalization.NumberStyles styles)
		{
			// Parse
			double result = 0;
			if (IsNumeric(value, formatProvider, out result))
				if ((double)Decimal.MinValue <= result && result <= (double)Decimal.MaxValue)
					return Convert.ToDecimal(result);

			return Null.Decimal;
		}
		//================================================================================
		public static float ParseSingle(string value) {return ParseSingle(value, null);}
		public static float ParseSingle(string value, System.IFormatProvider formatProvider) {return ParseSingle(value, formatProvider, NumberStyles.Any);}
		public static float ParseSingle(string value, System.IFormatProvider formatProvider, System.Globalization.NumberStyles styles)
		{
			// Parse
			double result = 0;
			if (IsNumeric(value, formatProvider, out result))
				if (Single.MinValue <= result && result <= Single.MaxValue)
					return Convert.ToSingle(result);			
					
			return Null.Single;
		}
		//================================================================================
		public static double ParseDouble(string value) {return ParseDouble(value, null);}
		public static double ParseDouble(string value, System.IFormatProvider formatProvider) {return ParseDouble(value, formatProvider, NumberStyles.Any);}
		public static double ParseDouble(string value, System.IFormatProvider formatProvider, System.Globalization.NumberStyles styles)
		{
			// Parse
			double result = 0;
			if (IsNumeric(value, formatProvider, out result))
				return result;
			else
				return Null.Double;
		}
		//================================================================================
		public static bool ParseBoolean(string value) 
		{
			// Parse
			value = value.ToUpper();
			return (value == "TRUE" || value == "T" || value == "YES" || value == "Y" || value == "ON" 
					|| value == "1" || value == "-1");
		}
		//================================================================================
		public static DateTime ParseDateTime(string value) {return ParseDateTime(value, null as IFormatProvider);} 
		public static DateTime ParseDateTime(string value, System.IFormatProvider formatProvider){return ParseDateTime(value, formatProvider, DateTimeStyles.None);} 
		public static DateTime ParseDateTime(string value, System.IFormatProvider formatProvider, System.Globalization.DateTimeStyles styles)
		{
			// Parse
			if (value != null && value.Length > 0)				
				return DateTime.Parse(value, formatProvider, styles);
			else
				return Null.DateTime;					
		}
		//================================================================================
		public static DateTime ParseDateTime(string value, string format) {return ParseDateTime(value, format, null);}
		public static DateTime ParseDateTime(string value, string format, System.IFormatProvider formatProvider) {return ParseDateTime(value, format, formatProvider, System.Globalization.DateTimeStyles.None);}
		public static DateTime ParseDateTime(string value, string format, System.IFormatProvider formatProvider, System.Globalization.DateTimeStyles styles)
		{
			// Parse
			if (value != null && value.Length > 0)				
				return DateTime.ParseExact(value, format, formatProvider, styles);
			else
				return Null.DateTime;					
		}
		//================================================================================
		public static TimeSpan ParseTimeSpan(string value) 
		{
			// Parse
			if (value != null && value.Length > 0)				
				return TimeSpan.Parse(value);
			else
				return Null.TimeSpan;					
		}
		//================================================================================		
		public static string Format(byte value) {return Format(value, null, null);}
		public static string Format(byte value, string format) {return Format(value, format, null);}
		public static string Format(byte value, System.IFormatProvider formatProvider) {return Format(value, null, formatProvider);}
		public static string Format(byte value, string format, System.IFormatProvider formatProvider)
		{
			if (value != Null.Byte)
				return value.ToString(format, formatProvider);
			else
				return Null.String;
		}		
		//================================================================================
		public static Guid ParseGuid(string value)
		{
			// Parse
			if (value != null && value.Length > 0)				
				return new Guid(value);
			else
				return Null.Guid;					
		}
		//================================================================================		
		public static string Format(short value) {return Format(value, null, null);}
		public static string Format(short value, string format) {return Format(value, format, null);}
		public static string Format(short value, System.IFormatProvider formatProvider) {return Format(value, null, formatProvider);}
		public static string Format(short value, string format, System.IFormatProvider formatProvider)
		{
			if (value != Null.Short)
				return value.ToString(format, formatProvider);
			else
				return Null.String;
		}
		//================================================================================		
		public static string Format(int value) {return Format(value, null, null);}
		public static string Format(int value, string format) {return Format(value, format, null);}
		public static string Format(int value, System.IFormatProvider formatProvider) {return Format(value, null, formatProvider);}
		public static string Format(int value, string format, System.IFormatProvider formatProvider)
		{
			if (value != Null.Integer)
				return value.ToString(format, formatProvider);
			else
				return Null.String;
		}
		//================================================================================		
		public static string Format(long value) {return Format(value, null, null);}
		public static string Format(long value, string format) {return Format(value, format, null);}
		public static string Format(long value, System.IFormatProvider formatProvider) {return Format(value, null, formatProvider);}
		public static string Format(long value, string format, System.IFormatProvider formatProvider)
		{
			if (value != Null.Long)
				return value.ToString(format, formatProvider);
			else
				return Null.String;
		}
		//================================================================================		
		public static string Format(decimal value) {return Format(value, null, null);}
		public static string Format(decimal value, string format) {return Format(value, format, null);}
		public static string Format(decimal value, System.IFormatProvider formatProvider) {return Format(value, null, formatProvider);}
		public static string Format(decimal value, string format, System.IFormatProvider formatProvider)
		{
			if (value != Null.Decimal)
				return value.ToString(format, formatProvider);
			else
				return Null.String;
		}
		//================================================================================		
		public static string Format(float value) {return Format(value, null, null);}
		public static string Format(float value, string format) {return Format(value, format, null);}
		public static string Format(float value, System.IFormatProvider formatProvider) {return Format(value, null, formatProvider);}
		public static string Format(float value, string format, System.IFormatProvider formatProvider)
		{
			if (value != Null.Single)
				return value.ToString(format, formatProvider);
			else
				return Null.String;
		}
		//================================================================================		
		public static string Format(double value) {return Format(value, null, null);}
		public static string Format(double value, string format) {return Format(value, format, null);}
		public static string Format(double value, System.IFormatProvider formatProvider) {return Format(value, null, formatProvider);}
		public static string Format(double value, string format, System.IFormatProvider formatProvider)
		{
			if (value != Null.Double)
				return value.ToString(format, formatProvider);
			else
				return Null.String;
		}
		//================================================================================
		public static string Format(bool value) {return Format(value, null);}
		public static string Format(bool value, System.IFormatProvider formatProvider)
		{
			return value.ToString(formatProvider);
		}
		//================================================================================		
		public static string Format(DateTime value) {return Format(value, null, null);}
		public static string Format(DateTime value, string format) {return Format(value, format, null);}
		public static string Format(DateTime value, System.IFormatProvider formatProvider) {return Format(value, null, formatProvider);}
		public static string Format(DateTime value, string format, System.IFormatProvider formatProvider)
		{
			if (value != Null.DateTime)
				return value.ToString(format, formatProvider);
			else
				return Null.String;
		}
		//================================================================================		
		public static string Format(TimeSpan value)
		{
			if (value != Null.TimeSpan)
				return value.ToString();
			else
				return Null.String;
		}
		//================================================================================
		public static string Format(Guid value) {return Format(value, null, null);}
		public static string Format(Guid value, string format) {return Format(value, format, null);}
		public static string Format(Guid value, System.IFormatProvider formatProvider) {return Format(value, null, formatProvider);}
		public static string Format(Guid value, string format, System.IFormatProvider formatProvider)
		{
			return value.ToString(format, formatProvider);
		}
		//================================================================================
		#endregion
	}
}
