﻿using System;
using System.ComponentModel;

namespace PersianTools.Jalali
{
    public class PersianDateConverter : TypeConverter
    {
        // Overrides the CanConvertFrom method of TypeConverter.
        // The ITypeDescriptorContext interface provides the context for the
        // conversion. Typically, this interface is used at design time to 
        // provide information about the design-time container.
        public override bool CanConvertFrom(ITypeDescriptorContext context,
            Type sourceType)
        {
            return sourceType == typeof(string) ||
                   sourceType == typeof(DateTime) ||
                   base.CanConvertFrom(context, sourceType);
        }

        // Overrides the ConvertFrom method of TypeConverter.
        public override object ConvertFrom(ITypeDescriptorContext context,
            System.Globalization.CultureInfo culture, object value)
        {
            if (value is string)
            {
                return PersianDate.Parse(value as string);
            }
            if (value is DateTime)
            {
                return new PersianDate((DateTime) value);
            }
            return base.ConvertFrom(context, culture, value);
        }

        // Overrides the ConvertTo method of TypeConverter.
        public override object ConvertTo(ITypeDescriptorContext context,
            System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof (string))
            {
                PersianDate pd = (PersianDate) value;
                return pd.ToString();
            }
            if (destinationType == typeof (DateTime))
            {
                PersianDate pd = (PersianDate) value;
                return pd.ToDateTime();
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
