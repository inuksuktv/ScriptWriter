﻿using System;
using System.ComponentModel;
using System.Globalization;

namespace ScriptWriter {
    public class MyHexConverter : TypeConverter {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            string strValue = (string)value;
            if (strValue != null) return byte.Parse(strValue, NumberStyles.HexNumber);
            return base.ConvertFrom(context, culture, value);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(string);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (value is byte b) { return b.ToString("X"); }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
