using System;
using System.Globalization;
using System.Reflection;
using Avalonia;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace Harp.LoadCells.Design.Converters;

public class RgbColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        // Handle RgbPayload structs or similar RGB-containing types
        if (value != null)
        {
            var type = value.GetType();

            // Handle RgbPayload type (struct/enum with Red, Green, Blue fields)
            if (type.Name == "RgbPayload" || type.Name.Contains("Rgb"))
            {
                string redName = "Red";
                string greenName = "Green";
                string blueName = "Blue";

                // Try to get the color components using reflection
                var redMember = (MemberInfo?)type.GetProperty(redName) ?? type.GetField(redName);
                var greenMember = (MemberInfo?)type.GetProperty(greenName) ?? type.GetField(greenName);
                var blueMember = (MemberInfo?)type.GetProperty(blueName) ?? type.GetField(blueName);

                if (redMember != null && greenMember != null && blueMember != null)
                {
                    byte r = GetByteValue(redMember, value);
                    byte g = GetByteValue(greenMember, value);
                    byte b = GetByteValue(blueMember, value);

                    return Color.FromRgb(r, g, b);
                }
            }
        }

        return AvaloniaProperty.UnsetValue;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Color color && targetType != null)
        {
            // Create a new instance of the target type (should be RgbPayload or similar)
            var result = Activator.CreateInstance(targetType);
            if (result == null) // Ensure result is not null
            {
                return BindingOperations.DoNothing;
            }

            var type = targetType;

            string redName = "Red";
            string greenName = "Green";
            string blueName = "Blue";

            // Update the appropriate fields
            var redMember = (MemberInfo?)type.GetProperty(redName) ?? type.GetField(redName);
            var greenMember = (MemberInfo?)type.GetProperty(greenName) ?? type.GetField(greenName);
            var blueMember = (MemberInfo?)type.GetProperty(blueName) ?? type.GetField(blueName);

            if (redMember != null)
                SetValue(redMember, result, color.R);

            if (greenMember != null)
                SetValue(greenMember, result, color.G);

            if (blueMember != null)
                SetValue(blueMember, result, color.B);

            return result;
        }

        return BindingOperations.DoNothing;
    }

    private byte GetByteValue(MemberInfo member, object source)
    {
        if (member is PropertyInfo prop)
            return System.Convert.ToByte(prop.GetValue(source));
        else if (member is FieldInfo field)
            return System.Convert.ToByte(field.GetValue(source));
        return 0;
    }

    private void SetValue(MemberInfo member, object target, byte value)
    {
        if (member is PropertyInfo prop)
            prop.SetValue(target, value);
        else if (member is FieldInfo field)
            field.SetValue(target, value);
    }
}
