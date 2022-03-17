using System.Linq;

namespace UsefulScripts.Other
{
    public class UIFromInt
    {
        private readonly string _value;

        public UIFromInt(params int[] ints)
        {
            var str = ints.Aggregate("", (current, i) => current + i);
            //Debug.Log(str);
            //var bytes = SevenZipHelper.Compress(ByteConvert.SerializeObject(str));
            _value = str; //Encoding.Default.GetString(bytes.Where(x => x != 0).ToArray());;
        }

        public static implicit operator string(UIFromInt field)
        {
            return field._value;
        }
    }
}