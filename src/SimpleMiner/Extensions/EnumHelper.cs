using System;
using System.ComponentModel;

namespace SimpleMiner.Extensions
{
    public static class EnumHelper
    {
        /// <summary>
        /// Obtém o valor existente na description do enum caso exista.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum enumValue)
        {
            var type = enumValue.GetType();

            var memberInfo = type.GetMember(enumValue.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {
                var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return enumValue.ToString();
        }
    }
}
