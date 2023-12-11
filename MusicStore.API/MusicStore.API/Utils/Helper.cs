using System.ComponentModel;
using System.Reflection;

namespace MusicStore.API.Utils;

public class Helper
{
    public static string GetEnumDescription(Enum value)
    {
        FieldInfo field = value.GetType().GetField(value.ToString());

        if (field != null)
        {
            var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));

            if (attribute != null)
            {
                return attribute.Description;
            }
        }

        return value.ToString();
    }
}