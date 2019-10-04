using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace Laboratorio_2.Models
{
	public class ZigZag
	{
		int bufferLenght = 500;
		List<char>[] Filas;

		private int CalculoDeM(int clave, int cantCaracteres)
		{
			int n = clave - 2;
			int arriva = cantCaracteres + 1 + (2 * n);
			int abajo = 2 * (1 + n);
			if (n < 0)
				return 0;
			else
			{
				int m = arriva / abajo;
				return m;
			}
		}

		public void Codificar(string pathLectura, string pathEscritura, int clave)
		{
			Filas = new List<char>[clave];
			for (int i = 0; i < clave; i++)
			{
				Filas[i] = new List<char>();
			}
			var buffer = new char[bufferLenght];
			int cantCaracteres = 0;
			char separador = '|';
			int separadorN = 0;
			using (var file = new FileStream(pathLectura, FileMode.Open))
			{
				using (var reader = new BinaryReader(file))
				{
					while (reader.BaseStream.Position != reader.BaseStream.Length)
					{
						buffer = reader.ReadChars(bufferLenght);
						foreach (var item in buffer)
						{
							cantCaracteres++;
							if (item == '|' && separadorN == 0)
							{
								separador = 'ÿ';
								separador++;
							}
							if (item == 'ÿ' && separador == 1)
								separador = 'ß';
						}
					}
				}
			}

			int fila = 0;
			bool bajando = true;
			using (var file = new FileStream(pathLectura, FileMode.Open))
			{
				using (var reader = new BinaryReader(file))
				{
					while (reader.BaseStream.Position != reader.BaseStream.Length)
					{
						buffer = reader.ReadChars(bufferLenght);
						foreach (var item in buffer)
						{
							Filas[fila].Add(Convert.ToChar(item));

							if (clave != 1)
							{
								if (fila == 0)
									bajando = true;
								if (fila == (clave - 1))
									bajando = false;

								if (bajando == true)
									fila++;
								else
									fila--;
							}
						}
					}
				}
			}

			bool rellenar = true;
			if (clave == 1)
				rellenar = false;
			while (rellenar)
			{
				if (bajando == true && fila == 1)
				{
					rellenar = false;
				}
				else
				{
					Filas[fila].Add(separador);

					if (fila == 0)
						bajando = true;
					if (fila == (clave - 1))
						bajando = false;
					if (bajando == true)
						fila++;
					else
						fila--;
				}
			}

			var escritura = new byte[bufferLenght];
			using (var file = new FileStream(pathEscritura, FileMode.Create))
			{
				using (var writer = new BinaryWriter(file))
				{
					foreach (var item in Filas)
					{
						foreach (var item2 in item)
						{
							writer.Write(Convert.ToChar(item2));
						}
					}
				}
			}
		}

		public void Decodificar(string pathLectura, string pathEscritura, int clave)
		{
			Filas = new List<char>[clave];
			for (int i = 0; i < clave; i++)
			{
				Filas[i] = new List<char>();
			}
			var buffer = new char[bufferLenght];
			int cantCaracteres = 0;
			char separador = '|';
			int separadorN = 0;
			using (var file = new FileStream(pathLectura, FileMode.Open))
			{
				using (var reader = new BinaryReader(file))
				{
					while (reader.BaseStream.Position != reader.BaseStream.Length)
					{
						buffer = reader.ReadChars(bufferLenght);
						foreach (var item in buffer)
						{
							cantCaracteres++;
							if (item == '|' && separadorN == 0)
							{
								separador = 'ÿ';
								separador++;
							}
							if (item == 'ÿ' && separador == 1)
								separador = 'ß';
						}
					}
				}
			}

			int fila = 0;
			int m = CalculoDeM(clave, cantCaracteres);
			int mIntermedio = 2 * (m - 1);
			using (var file = new FileStream(pathLectura, FileMode.Open))
			{
				using (var reader = new BinaryReader(file))
				{
					while (reader.BaseStream.Position != reader.BaseStream.Length)
					{
						buffer = reader.ReadChars(bufferLenght);
						foreach (var item in buffer)
						{
							if (clave != 1)
							{
								if (fila == 0)
								{
									if (Filas[fila].Count() < m)
										Filas[fila].Add(item);
									else
									{
										fila++;
										Filas[fila].Add(item);
									}
								}
								else if (fila == (clave - 1))
								{
									Filas[fila].Add(item);
								}
								else
								{
									if (Filas[fila].Count() < mIntermedio)
									{
										Filas[fila].Add(item);
									}
									else
									{
										fila++;
										Filas[fila].Add(item);
									}
								}
							}
							else
								Filas[fila].Add(item);
						}
					}
				}
			}

			bool bajando = true;
			fila = 0;
			//var escritrua = new byte[bufferLenght];
			using (var file = new FileStream(pathEscritura, FileMode.Create))
			{
				using (var writer = new BinaryWriter(file))
				{
					while (cantCaracteres != 0)
					{
						try
						{
							if (Filas[fila][0] == separador)
								Filas[fila].RemoveRange(0, 1);
							else
							{
								writer.Write(Filas[fila][0]);
								Filas[fila].RemoveRange(0, 1);
							}
						}
						catch { }

						if (clave != 1)
						{
							if (fila == 0)
								bajando = true;
							if (fila == (clave - 1))
								bajando = false;

							if (bajando == true)
								fila++;
							else
								fila--;
						}
						cantCaracteres--;
					}
				}
			}
		}
	}
}