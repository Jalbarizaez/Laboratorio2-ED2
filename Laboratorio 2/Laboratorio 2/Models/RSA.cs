using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Web;

namespace Laboratorio_2.Models
{
<<<<<<< HEAD
    public class RSA
    {

    }
=======
	public class RSA
	{
		private void CIF(string pathEscritura, string pathLlave, string pathLectura)
		{
			string lines = File.ReadAllText(pathLlave);
			var llave = lines.Split(',');
			var buffer = new byte[bufferlenght];
			using (var File = new FileStream(pathEscritura, FileMode.OpenOrCreate))
			{
				using (var writer = new BinaryWriter(File))
				{
					using (var file = new FileStream(pathLectura, FileMode.Open))
					{
						using (var reader = new BinaryReader(file, System.Text.Encoding.ASCII))
						{
							while (reader.BaseStream.Position != reader.BaseStream.Length)
							{
								buffer = reader.ReadBytes(bufferlenght);
								var escribir = Ecuacion(buffer, Convert.ToInt32(llave[0]), Convert.ToInt32(llave[1]));
								writer.Write(escribir.ToArray(), 0, escribir.Count);
							}
						}

					}
				}
			}
		}

		public void Llaves(int p, int q, string pathEscritura_Privada, string pathEscritura_Publica)
        {
            int n = p * q;
            int phi = (p - 1) * (q - 1);
            int e = E_(phi, n);
            int d = InversoMultiplicativoMod(phi, e);
            using (var file = new FileStream(pathEscritura_Privada, FileMode.OpenOrCreate))
            {
                using (var writer = new BinaryWriter(file))
                {
                    var Escritura = Encoding.UTF8.GetBytes((d.ToString() + "," + n.ToString()).ToArray());
                    writer.Write(Escritura);
                }
            }
            using (var file = new FileStream(pathEscritura_Publica, FileMode.OpenOrCreate))
            {
                using (var writer = new BinaryWriter(file))
                {
                    var Escritura = Encoding.UTF8.GetBytes((e.ToString() + "," + n.ToString()).ToArray());
                    writer.Write(Escritura);
                }
            }
        }

        private int E_(int fi, int n)
        {
            List<int> FI = new List<int>();
            int residuo_fi = fi;
            for (int i = 2; i <= residuo_fi; i++)
            {
                if (residuo_fi % i == 0)
                {
                    bool agregar = true;
                    foreach (var item in FI)
                    {
                        if (i % item == 0)
                        {
                            agregar = false;
                            break;
                        }
                    }
                    if (agregar)
                    {
                        FI.Add(i);
                        residuo_fi = fi / i;
                    }
                }
            }
            List<int> N = new List<int>();
            int residuo_N = n;
            for (int i = 2; i <= residuo_N; i++)
            {
                if (residuo_N % i == 0)
                {
                    bool agregar = true;
                    foreach (var item in N)
                    {
                        if (i % item == 0)
                        {
                            agregar = false;
                            break;
                        }
                    }
                    if (agregar)
                    {
                        N.Add(i);
                        residuo_N = n / i;
                    }
                }
            }
            bool comparar_fi = true;
            bool comparar_n = true;
            int numero_e = 2;
            while (comparar_fi && comparar_n)
            {
                foreach (var item in N)
                {
                    if (numero_e % item == 0)
                    {
                        comparar_n = false;
                        break;
                    }
                }
                foreach (var item in FI)
                {
                    if (numero_e % item == 0)
                    {
                        comparar_fi = false;
                        break;
                    }
                }
                if (comparar_fi == false || comparar_n == false)
                {
                    numero_e++;
                    comparar_fi = true;
                    comparar_n = true;
                }
                else if (comparar_fi == true && comparar_n == true)
                {
                    comparar_fi = false;
                    comparar_n = false;
                }
            }
            return numero_e;
        }
       
        private int InversoMultiplicativoMod(int Fi, int e, int e2 = 0, int num = 0, int num2 = 1)
		{
			if (e2 == 1)
				return num2;
			else
			{
				if (e2 == 0)
				{
					e2 = e;
					e = Fi;
					num = Fi;
				}

				int aux = e2;
				int r = e / e2;
				e2 = e - (r * e2);
				e = aux;

				int aux2 = num2;
				num2 = num - (r * num2);
				if (num2 < 0)
					num2 = Fi + num2;
				num = aux2;

				return InversoMultiplicativoMod(Fi, e, e2, num, num2);
			}
		}
		
	}
>>>>>>> 17092b5dbe5b557d35cc4ac0f0f94ce532cbdcb9
}