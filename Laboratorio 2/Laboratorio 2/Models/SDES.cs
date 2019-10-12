using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace Laboratorio_2.Models
{
    public class SDES
    {
        private List<int> ip = new List<int>();
        private List<int> ip_1 = new List<int>();
        private List<int> ep = new List<int>();
        private List<int> p10 = new List<int>();
        private List<int> p8 = new List<int>();
        private List<int> p4 = new List<int>();
        private List<int> ls1 = new List<int>();
        private List<int> ls2 = new List<int>();
        private void Read_Permutations(string path)
        {
            string[] lines = File.ReadAllLines(path);
            for (int i = 0; i < lines.Length; i++)
            {
                string[] line_split = lines[i].Split(':');
                string[] order = line_split[1].Split(',');
                if (i == 0)
                {
                    foreach (var item in order)
                    {
                        p10.Add(Convert.ToInt16(item));
                    }
                }
                if (i == 1)
                {
                    foreach (var item in order)
                    {
                        p8.Add(Convert.ToInt16(item));
                    }
                }
                if (i == 2)
                {
                    foreach (var item in order)
                    {
                        p4.Add(Convert.ToInt16(item));
                    }
                }
                if (i == 3)
                {
                    foreach (var item in order)
                    {
                        ep.Add(Convert.ToInt16(item));
                    }
                }
                if (i == 4)
                {
                    foreach (var item in order)
                    {
                        ip.Add(Convert.ToInt16(item));
                    }
                }
                if (i == 5)
                {
                    foreach (var item in order)
                    {
                        ip_1.Add(Convert.ToInt16(item));
                    }
                }
            }
        }

        private string[] Keys(string key)
        {
            string[] keys = new string[2];
            var first_permutation = P10(key);
            var Left_Shift_1 = LS1(first_permutation);
            var second_permutation = P8(Left_Shift_1[0] + Left_Shift_1[1]);
            keys[0] = second_permutation;
            var Left_Shift_2 = LS2(Left_Shift_1[0] + Left_Shift_1[1]);
            var last_permutation = P8(Left_Shift_2[0] + Left_Shift_2[1]);
            keys[1] = last_permutation;
            return keys;
        }
        public void Cifrado(string bits, string path_read, string path_write)
        {
            Read_Permutations(path_read);
            string[] keys = Keys(bits);
        }

        private string P10(string bits)
        {
            var bit = bits.ToCharArray();
            string final = "";
            foreach (var item in p10)
            {
                final += bits[item];
            }
            return final;
        }
        private string[] LS1(string bits)
        {
            var bit = bits.ToCharArray();
            string[] final = new string[2];
            for (int i = 1; i < 5; i++)
            {
                final[0] += bit[i];
            }
            final[0] += bit[0];
            for (int i = 6; i < 10; i++)
            {
                final[1] += bit[i];
            }
            final[1] += bit[5];
            return final;
        }
        private string P8(string bits)
        {
            var bit = bits.ToCharArray();
            string final = "";
            foreach (var item in p8)
            {
                final += bits[item];
            }
            return final;
        }
        private string[] LS2(string bits)
        {
            var bit = bits.ToCharArray();
            string[] final = new string[2];
            for (int i = 2; i < 5; i++)
            {
                final[0] += bit[i];
            }
            final[0] += bit[0];
            final[0] += bit[1];
            for (int i = 7; i < 10; i++)
            {
                final[1] += bit[i];
            }
            final[1] += bit[5];
            final[1] += bit[6];
            return final;
        }
    }
}