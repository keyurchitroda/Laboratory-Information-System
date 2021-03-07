using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace LIS.UI.Helper
{
    public static class CommanFunction
    {
        public static String ByteArrayToHexString(byte[] input)
        {
            StringBuilder builder = new StringBuilder();
            foreach (byte b in input)
                builder.Append(string.Format("{0:X2}", b));
            return builder.ToString();
        }

        public static IEnumerable<byte> HexStringToByteArray(String input)
        {
            for (int i = 0; i <= input.Length - 2; i += 2)
                yield return Convert.ToByte(input.Substring(i, 2), 16);
        }
    }
}