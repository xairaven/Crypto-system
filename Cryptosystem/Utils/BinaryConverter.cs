using System;
using System.Text;

namespace Cryptosystem.Utils;

public static class BinaryConverter
{
    public static string StringToBinary(string input, int bits)
    {
        var sb = new StringBuilder();
        
        foreach (var c in input)
        {
            var num = (int) c;
            var binary = Convert.ToString(num, 2)
                .PadLeft(bits, '0');
            sb.Append(binary);
        }
        
        return sb.ToString();
    }

    public static string BinaryToString(string binary, int bits)
    {
        var sb = new StringBuilder();
        
        for (int i = 0; i < binary.Length; i += bits)
        {
            if (i == binary.Length - (binary.Length % bits)) break;
            
            var binaryNumber = binary.Substring(i, bits);

            var number = Convert.ToInt32(binaryNumber, 2);
            
            // fix - padding may take way more bits than 7
            if (number == 0) continue;
            
            sb.Append((char) number);
        }

        return sb.ToString();
    }
}