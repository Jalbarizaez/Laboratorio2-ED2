using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Laboratorio_2.Models
{
	public class RSA
	{
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