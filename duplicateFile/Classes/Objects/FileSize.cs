using System;

namespace duplicateFile.Classes
{
    public sealed class FileSize : IConvertible
    {
        private long _size;

        public FileSize(long size = 0)
        {
            _size = size;
        }

        public string HumanReadableSize
        {
            get { return sizeToString(_size); }
        }

        public long Size
        {
            get { return _size; }
            set { _size = value; }
        }

        public static implicit operator String(FileSize fs)
        {
            return fs.ToString();
        }

        public static implicit operator Int64(FileSize fs)
        {
            return fs.Size;
        }

        public override string ToString()
        {
            return sizeToString(_size);
        }

        public void Add(long s)
        {
            _size += s;
        }

        private static string sizeToString(long size)
        {
            var unit = new string[] { "o", "Ko", "Mo", "Go", "To", "Po" };

            int i = 0;

            while (size / 1024 > 1 && i < 6)
            {
                i++;
                size = size / 1024;
            }

            return size.ToString("0.## ") + unit[i];
        }

        public TypeCode GetTypeCode()
        {
            return TypeCode.Object;
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            return Convert.ChangeType(Size, conversionType);
        }

        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            return Size > 0;
        }

        string IConvertible.ToString(IFormatProvider provider)
        {
            return HumanReadableSize;
        }

        DateTime IConvertible.ToDateTime(IFormatProvider provider)
        {
            return Convert.ToDateTime(Size);
        }

        Decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            return (Decimal)Size;
        }

        Double IConvertible.ToDouble(IFormatProvider provider)
        {
            return (Double)Size;
        }

        Single IConvertible.ToSingle(IFormatProvider provider)
        {
            return (Single)Size;
        }

        UInt64 IConvertible.ToUInt64(IFormatProvider provider)
        {
            return (UInt64)Size;
        }

        UInt32 IConvertible.ToUInt32(IFormatProvider provider)
        {
            return (UInt32)Size;
        }

        UInt16 IConvertible.ToUInt16(IFormatProvider provider)
        {
            return (UInt16)Size;
        }

        Int64 IConvertible.ToInt64(IFormatProvider provider)
        {
            return Convert.ToInt64(Size);
        }

        Int32 IConvertible.ToInt32(IFormatProvider provider)
        {
            return (Int32)Size;
        }

        Int16 IConvertible.ToInt16(IFormatProvider provider)
        {
            return (Int16)Size;
        }

        Byte IConvertible.ToByte(IFormatProvider provider)
        {
            return Convert.ToByte(Size);
        }

        SByte IConvertible.ToSByte(IFormatProvider provider)
        {
            return Convert.ToSByte(Size);
        }

        Char IConvertible.ToChar(IFormatProvider provider)
        {
            return Convert.ToChar(Size);
        }
    }
}