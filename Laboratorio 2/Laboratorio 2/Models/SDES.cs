using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace Laboratorio_2.Models
{
	public class SDES
	{
        private const int bufferLenght = 1000;
        private int[] ip = new int[8];
        private int[] ip_1 = new int[8];
        private int[] ep = new int[8];
        private int[] p10 = new int[10];
        private int[] p8 = new int[8];
        private int[] p4 = new int[4];
        private string[][] S0 = new string[4][];
		private string[][] S1 = new string[4][];

        private void Read_Permutations(string path)
        {
            string[] lines = File.ReadAllLines(path);
            for (int i = 0; i < lines.Length; i++)
            {
                string[] line_split = lines[i].Split(':');
                string[] order = line_split[1].Split(',');
                if (i == 0)
                {
                    int count = 0;
                    foreach (var item in order)
                    {
                        p10[count] = (Convert.ToInt16(item));
                        count++;
                    }
                }
                if (i == 1)
                {
                    int count = 0;
                    foreach (var item in order)
                    {
                        p8[count] = (Convert.ToInt16(item));
                        count++;
                    }
                }
                if (i == 2)
                {
                    int count = 0;
                    foreach (var item in order)
                    {
                        p4[count] = (Convert.ToInt16(item));
                        count++;
                    }
                }
                if (i == 3)
                {
                    int count = 0;
                    foreach (var item in order)
                    {
                        ep[count] = (Convert.ToInt16(item));
                        count++;
                    }
                }
                if (i == 4)
                {
                    int count = 0;
                    foreach (var item in order)
                    {
                        ip[count] = (Convert.ToInt16(item));
                        count++;
                    }
                }
                if (i == 5)
                {
                    int count = 0;
                    foreach (var item in order)
                    {

                        ip_1[count] = (Convert.ToInt16(item));
                        count++;
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

        private byte CIF(byte item, string k1, string k2)
        {
            var bits = Convert.ToString(item, 2);
            var byte_ = bits.PadLeft(8, '0');
            var first_permutation = IP(byte_);
            var second_permutation = EP(first_permutation[1]);
            var xor = XOR(k1, second_permutation);
            var third_permutation = SBox(xor);
            var xor2 = XOR(P4(third_permutation), first_permutation[0]);
            var four_permutation = EP(xor2);
            var xor3 = XOR(k2, four_permutation);
            var five_permutation = SBox(xor3);
            var xor4 = XOR(P4(five_permutation), first_permutation[1]);
            var six_permutation = xor4 + xor2;
            var final = IP_1(six_permutation);
            return Convert.ToByte((final[0] + final[1]), 2);
        }

        private string EP(string bits)
        {
            var bit = bits.ToCharArray();
            string final = "";
            foreach (var item in ep)
            {
                final += bits[item];
            }
            return final;
        }

        public void Cifrado(string bits, string path_read_S, string path_write, string path_read)
        {
            Read_Permutations(path_read_S);
            string[] keys = Keys(bits);
            Write_bytes(keys[0], keys[1], path_read, path_write);
        }

        public void Descifrado(string bits, string path_read_S, string path_write, string path_read)
        {
            Read_Permutations(path_read_S);
            string[] keys = Keys(bits);
            Write_bytes(keys[1], keys[0], path_read, path_write);
        }

        private void Write_bytes(string k1, string k2, string path_read, string path_write)
        {
            int count = 0;
            var write = new byte[bufferLenght];
            var buffer = new byte[bufferLenght];
            using (var File = new FileStream(path_write, FileMode.OpenOrCreate))
            {
                using (var writer = new BinaryWriter(File))
                {
                    using (var file = new FileStream(path_read, FileMode.Open))
                    {
                        using (var reader = new BinaryReader(file, System.Text.Encoding.ASCII))
                        {
                            while (reader.BaseStream.Position != reader.BaseStream.Length)
                            {
                                buffer = reader.ReadBytes(bufferLenght);
                                foreach (var item in buffer)
                                {
                                    write[count] = CIF(item, k1, k2);
                                    count++;
                                }
                                writer.Write(write, 0, count);
                                count = 0;
                                write = new byte[bufferLenght];
                            }
                        }

                    }
                }
            }
        }

        private string[] IP_1(string bits)
        {
            var bit = bits.ToCharArray();
            string[] final = new string[2];
            int count = 0;
            foreach (var item in ip_1)
            {
                if (count < 4)
                {
                    final[0] += bits[item];
                    count++;
                }
                else
                {
                    final[1] += bits[item];
                }
            }
            return final;
        }

        private string[] IP(string bits)
        {
            var bit = bits.ToCharArray();
            string[] final = new string[2];
            int count = 0;
            foreach (var item in ip)
            {
                if (count < 4)
                {
                    final[0] += bits[item];
                    count++;
                }
                else
                {
                    final[1] += bits[item];
                }
            }
            return final;
        }

        private string P4(string bits)
        {
            var bit = bits.ToCharArray();
            string final = "";
            foreach (var item in p4)
            {
                final += bits[item];
            }
            return final;
        }

        private string XOR(string bits1, string bits2)
        {
            var bit1 = bits1.ToCharArray();
            var bit2 = bits2.ToCharArray();
            string final = "";
            for (int i = 0; i < bit1.Length; i++)
            {
                if (bit1[i] == bit2[i])
                {
                    final += "0";
                }
                else
                {
                    final += "1";
                }
            }
            return final;
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

		private string SBox(string bits)
		{
			S0[0] = new string[4] { "01", "00", "11", "10" };
			S0[1] = new string[4] { "11", "10", "01", "00" };
			S0[2] = new string[4] { "00", "10", "01", "11" };
			S0[3] = new string[4] { "11", "01", "11", "10" };

			S1[0] = new string[4] { "00", "01", "10", "11" };
			S1[1] = new string[4] { "10", "00", "01", "11" };
			S1[2] = new string[4] { "11", "00", "01", "00" };
			S1[3] = new string[4] { "10", "01", "00", "11" };

			char[] bit = bits.ToCharArray();

			int F0 = IndiceSBox(bit[0].ToString() + bit[3].ToString());
			int C0 = IndiceSBox(bit[1].ToString() + bit[2].ToString());
			int F1 = IndiceSBox(bit[4].ToString() + bit[7].ToString());
			int C1 = IndiceSBox(bit[5].ToString() + bit[6].ToString());

			return S0[F0][C0] + S1[F1][C1];
		}

		private int IndiceSBox(string cadena)
		{
			if (cadena == "00")
				return 0;
			if (cadena == "01")
				return 1;
			if (cadena == "10")
				return 2;
			if (cadena == "11")
				return 3;
			else
				return 0;
		}
	}
}