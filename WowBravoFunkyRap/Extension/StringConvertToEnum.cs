namespace WowBravoFunkyRap.Extension
{
    public class StringConvertToEnum
    {
        public static TEnum? ConvertToEnum<TEnum>(string value) where TEnum : struct, Enum
        {
            if (Enum.TryParse<TEnum>(value, out var enumValueByName))
            {
                return enumValueByName;
            }

            if (int.TryParse(value, out var intValue) && Enum.IsDefined(typeof(TEnum), intValue))
            {
                return (TEnum)Enum.ToObject(typeof(TEnum), intValue);
            }

            return null;
        }
    }

}
