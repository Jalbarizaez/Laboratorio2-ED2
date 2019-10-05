using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Laboratorio_2.Models
{
    public class Descifrado_Cesar
    {
		private static Dictionary<char, char> Tabla_Caracteres;
		private List<char> Creacion_clave;
		private List<char> Abecedario;
        private const int bufferlength = 750;

        public void Descifrar(string path_archivo, string path_texto, string clave, string path_Escritura)
        {
			Tabla_Caracteres = new Dictionary<char, char>();
			Creacion_clave = new List<char>();
			Abecedario = new List<char>();

			Crear_diccionario(clave, path_archivo);
            Escribir_Descifrado(path_texto, path_Escritura);
        }
        private void Escribir_Descifrado(string path_texto, string path_Escritura)
        {
            var escritura = new char[bufferlength];
            int i = 0;
            var buffer = new char[bufferlength];
            using (var writer = new FileStream(path_Escritura, FileMode.Append))
            {
                using (var file = new FileStream(path_texto, FileMode.Open))
                {
                    using (var reader = new BinaryReader(file))
                    {

                        while (reader.BaseStream.Position != reader.BaseStream.Length)
                        {
                            buffer = reader.ReadChars(bufferlength);
                            foreach (var item in buffer)
                            {
                                if (Tabla_Caracteres.ContainsKey(item))
                                {
                                    escritura[i] = (Tabla_Caracteres[item]);
                                    i++;
                                }
                                else
                                {
                                    escritura[i] = item;
                                    i++;
                                }
                            }
                            i = 0;
                            var escritor = Encoding.UTF8.GetBytes(escritura);
                            writer.Write(escritor, 0, escritor.Length);
                            escritura = new char[bufferlength];
                        }


                    }
                }
            }
			Tabla_Caracteres = new Dictionary<char, char>();
			Creacion_clave = new List<char>();
			Abecedario = new List<char>();
		}

        private void Crear_diccionario(string clave, string path_archivo)
        {

            var buffer = new char[bufferlength];
            //Se llena el diccionario con los valores iniciales
            using (var file = new FileStream(path_archivo, FileMode.Open))
            {
                using (var reader = new BinaryReader(file))
                {
                    while (reader.BaseStream.Position != reader.BaseStream.Length)
                    {
                        buffer = reader.ReadChars(bufferlength);
                        foreach (var item in buffer)
                        {
                            Abecedario.Add(item);

                        }
                    }
                }

            }
            char[] Clave = clave.ToCharArray();
            for (int i = 0; i < Clave.Length; i++)
            {
                if (!Creacion_clave.Contains(Clave[i]) && Abecedario.Contains(Clave[i]))
                {
                    Creacion_clave.Add(Clave[i]);
                }
            }
            foreach (var item in Abecedario)
            {
                if (!Creacion_clave.Contains(item))
                {
                    Creacion_clave.Add(item);
                }
            }
            var key = Abecedario.ToArray();
            var value = Creacion_clave.ToArray();
            for (int i = 0; i < Abecedario.Count; i++)
            {
                Tabla_Caracteres.Add(value[i], key[i]);
            }

        }
    }
}