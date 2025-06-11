using System;
using System.Globalization;
using System.Reflection;
using Avalonia;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace Harp.LoadCells.Design.Converters;

public class RgbChannelConverter : IValueConverter
{
    // The RGB channel to extract (0 or 1)
    public int Channel { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return AvaloniaProperty.UnsetValue;

        try
        {
            var type = value.GetType();

            // Extract fields for the specified channel (0 or 1)
            string redName = $"Red{Channel}";
            string greenName = $"Green{Channel}";
            string blueName = $"Blue{Channel}";

            // Try to get the color components using reflection
            var redMember = (MemberInfo?)type.GetProperty(redName) ?? type.GetField(redName);
            var greenMember = (MemberInfo?)type.GetProperty(greenName) ?? type.GetField(greenName);
            var blueMember = (MemberInfo?)type.GetProperty(blueName) ?? type.GetField(blueName);

            if (redMember != null && greenMember != null && blueMember != null)
            {
                byte r = ExtractByteValue(redMember, value);
                byte g = ExtractByteValue(greenMember, value);
                byte b = ExtractByteValue(blueMember, value);

                return Color.FromRgb(r, g, b);
            }
        }
        catch
        {
            // Fall back to default color on error
            return Colors.Gray;
        }

        return AvaloniaProperty.UnsetValue;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is not Color color)
            return BindingOperations.DoNothing;

        try
        {
            // Create a new instance or get the existing one
            var instance = parameter as object ?? Activator.CreateInstance(targetType);
            if (instance == null) // Ensure instance is not null
            {
                return BindingOperations.DoNothing;
            }
            var type = targetType;

            // Determine field names for this channel
            string redName = $"Red{Channel}";
            string greenName = $"Green{Channel}";
            string blueName = $"Blue{Channel}";

            // Update the appropriate fields
            var redMember = (MemberInfo?)type.GetProperty(redName) ?? type.GetField(redName);
            var greenMember = (MemberInfo?)type.GetProperty(greenName) ?? type.GetField(greenName);
            var blueMember = (MemberInfo?)type.GetProperty(blueName) ?? type.GetField(blueName);

            if (redMember != null)
                SetValue(redMember, instance, color.R);

            if (greenMember != null)
                SetValue(greenMember, instance, color.G);

            if (blueMember != null)
                SetValue(blueMember, instance, color.B);

            return instance;
        }
        catch
        {
            return BindingOperations.DoNothing;
        }
    }

    private byte ExtractByteValue(MemberInfo member, object source)
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