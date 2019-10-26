using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Laboratorio_2.Models
{
	public class RSA
	{
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
        //Para llamarla solo tenes que enviarle Fi(n) y e.  Ejemplo:
        //Fi(n) = 20      e = 7
        //InversoMultiplicativoMod(20, 7)
        //eso da de resultado "d"
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
}