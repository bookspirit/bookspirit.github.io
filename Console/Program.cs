using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Console
{
  class Program
  {
    static void Main(string[] args)
    {
      System.Console.WriteLine(ComputeHashPassword("1"));
      System.Console.ReadKey();
    }

    private static string ComputeHashPassword(string password)
    {
      return BinaryToHex(new MD5Cng().ComputeHash(Encoding.UTF8.GetBytes(password)));
    }

    private static string BinaryToHex(byte[] data)
    {
      if (data == null)
      {
        return null;
      }
      var chArray = new char[checked(data.Length * 2)];
      for (int index = 0; index < data.Length; ++index)
      {
        byte num = data[index];
        chArray[2 * index] = NibbleToHex((byte)((uint)num >> 4));
        chArray[2 * index + 1] = NibbleToHex((byte)(num & 15U));
      }
      return new string(chArray);
    }
    
    private static char NibbleToHex(byte nibble)
    {
      return (int)nibble < 10 ? (char)(nibble + 48) : (char)(nibble - 10 + 65);
    }
  }
}
