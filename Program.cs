using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace d3crypt0r
{
    class Program
    {
        private readonly static char[] CharacterDictionary = " !\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~¡¢£¤¥¦§¨©ª«¬­®¯°±²³´µ¶·¸¹º»¼½¾¿ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖ×ØÙÚÛÜÝÞßàáâãäåæçèéêëìíîïðñòóôõö÷øùúûüýþ".ToCharArray();

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Write the text to decrypt:");
                var ToDecrypt = Console.ReadLine();
                Console.WriteLine("Write the key of decryption:");
                var Key = Console.ReadLine();

                var Keys = new byte[6];
                for (var i = 0; i < 6; i++)
                    Keys[i] = (byte)CharacterDictionary.ToList().IndexOf(Key[i]);

                var ByteValues = new byte[ToDecrypt.Length];
                for (var i = 0; i < ByteValues.Length; i++)
                    ByteValues[i] = (byte)CharacterDictionary.ToList().IndexOf(ToDecrypt[i]);

                var Decrypted = "";
                List<string> OtherPosibilities = new List<string>();
                for (var i = 0; i < ByteValues.Length; i += 2)
                {
                    bool One = false;
                    int b = 0;
                    for (var u = 0; u < CharacterDictionary.Length; u++)
                    {
                        for (var k = 0; k < CharacterDictionary.Length; k++)
                        {
                            if (((((u * Keys[0]) + (k * Keys[1])) + Keys[4]) % CharacterDictionary.Length) == ByteValues[i] && ((((u * Keys[2]) + (k * Keys[3])) + Keys[5]) % CharacterDictionary.Length) == ByteValues[i + 1])
                            {
                                if (One)
                                {
                                    if (OtherPosibilities.Count > b)
                                    {
                                        OtherPosibilities[b] += "" + CharacterDictionary[u] + "" + CharacterDictionary[k] + "";
                                        b++;
                                    }
                                    else
                                    {
                                        OtherPosibilities.Add("" + CharacterDictionary[u] + "" + CharacterDictionary[k] + "");
                                    }
                                }
                                else
                                {
                                    One = true;
                                    Decrypted += "" + CharacterDictionary[u] + "" + CharacterDictionary[k] + "";
                                }
                            }
                        }
                    }
                }
                Console.WriteLine("Decrypted: " + Decrypted + (OtherPosibilities.Count > 0 ? "\nOther posibilities:\n" + string.Join("\n", OtherPosibilities.ToArray()) : ""));
            }
        }
    }
}
