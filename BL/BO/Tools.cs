using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public static class Tools
{
    //לא מושלם
    [AttributeUsage(AttributeTargets.Property)]

    class PropertyDisplayAttribute : Attribute
    {
        public String DisplayValue { get; set; }
        public PropertyDisplayAttribute(string displayName)
        {
            DisplayValue = displayName;
        }
    }

    /// <summary>
    /// The method examines each type T at runtime with the help of reflection, and produces a string of all its attributes that were examined at runtime
    /// </summary>
    /// <typeparam name="T">type of the entity</typeparam>
    /// <param name="t">the name of the entity</param>
    /// <returns>A string of all its attributes that were examined at runtime</returns>
    public static string ToStringProperty<T>( this T t)
    {
        string str = "";
        PropertyInfo[] T_properties = t!.GetType().GetProperties();
        foreach (PropertyInfo item in T_properties)
        {
            Type myAttributeType = typeof(PropertyDisplayAttribute);
            object[] itemDisplayAtt = item.GetCustomAttributes(myAttributeType, false);
            if (itemDisplayAtt.Length == 1)
            {
                PropertyDisplayAttribute att = (PropertyDisplayAttribute)itemDisplayAtt[0];
                str += string.Format("\nname: {0,-15} value: {1,-15}",
                att.DisplayValue,
                item.GetValue(t, null));
            }
            else
            {
                str += string.Format("\nname: {0,-15} value: {1,-15}",
                item.Name,
                item.GetValue(t, null));
            }
        }
        return str;
    }

    /// <summary>
    /// Convert from dalagete to type
    /// </summary>
    /// <param name="sourceDelegate">the type</param>
    /// <param name="targetType">the type</param>
    /// <returns>the type after convertion</returns>
    public static Delegate ConvertDelegate(Delegate sourceDelegate, Type targetType)
    {
        return Delegate.CreateDelegate(
                targetType,
                sourceDelegate.Target,
                sourceDelegate.Method);
    }
    //public static string ToStringProperty<T>(this T t)
    //{
    //    string str = "";
    //    PropertyInfo[] T_properties = t!.GetType().GetProperties();
    //    foreach (var item in T_properties)
    //        str += string.Format("\nname: {0,-15} value: {1,-15}",
    //        item.Name,
    //        item.GetValue(t, null));
    //    return str;
    //}
}
