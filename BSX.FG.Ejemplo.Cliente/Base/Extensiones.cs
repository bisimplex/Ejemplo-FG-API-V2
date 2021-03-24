using System;
using System.ComponentModel;

namespace BSX.FG.Ejemplo.Cliente.Base
{
    public static class Extensiones
    {
        /// <summary>
        /// Codifica la cadena en un Base64
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string EncodeBase64(this string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return null;

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        /// <summary>
        /// Obtiene el atributo solicitado
        /// This extension method is broken out so you can use a similar pattern with
        /// other MetaData elements in the future. This is your base method for each.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T GetAttribute<T>(this System.Enum value) where T : Attribute
        {
            var type = value.GetType();
            var memberInfo = type.GetMember(value.ToString());

            if (memberInfo.Length == 0)
                return null;

            var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);

            if (attributes.Length == 0)
                return null;

            return (T)attributes[0];
        }

        /// <summary>
        /// Convierte el enumerado a su nombre asociado en [Description]
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToName(this System.Enum value)
        {
            if (value == null)
                return string.Empty;

            var attribute = value.GetAttribute<DescriptionAttribute>();
            return attribute?.Description ?? value.ToString();
        }
    }
}