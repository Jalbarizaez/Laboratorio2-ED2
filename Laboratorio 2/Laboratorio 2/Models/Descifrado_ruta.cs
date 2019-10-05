using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace Laboratorio_2.Models
{
    public class Descifrado_ruta
    {
        private char[,] matriz;
        private const int bufferlenght = 1500;
        private long tamaño_archivo { get; set; }

        public void Descifrado(int clave, string path_archivo, string path_escritura, int direccion)
        {
            Crear_Matriz(clave, path_archivo, direccion, path_escritura);

        }
        private void Crear_Matriz(int clave, string path_archivo, int direccion, string path_escritura)
        {
            int cantidad = 0;

            //Se llena el diccionario con los valores iniciales
            using (var file = new FileStream(path_archivo, FileMode.Open))
            {
                using (var reader = new BinaryReader(file))
                {
                    while (reader.BaseStream.Position != reader.BaseStream.Length)
                    {

                        var buffer = reader.ReadChars(bufferlenght);
                        tamaño_archivo = buffer.Count(x => x != '\0');
                        matriz = new char[clave, Convert.ToInt16(Math.Ceiling(Convert.ToDecimal(tamaño_archivo) / Convert.ToDecimal(clave)))];
                        if (direccion == 1)
                        {//Horario
                            int mov_x = matriz.GetLength(1);
                            int mov_y = matriz.GetLength(0);
                            int filas = 0;
                            int columnas = 0;
                            int filas_n = matriz.GetLength(1);
                            int columnas_n = matriz.GetLength(0);

                            int escribir = matriz.GetLength(0) * matriz.GetLength(1);
                            while (cantidad < escribir)
                            {
                                for (int i = columnas; i < mov_y - 1; i++)
                                {
                                    matriz[i, columnas] = buffer[cantidad];
                                    cantidad++;
                                }
                                columnas++; ;
                                mov_y--;
                                for (int i = filas; i < mov_x - 1; i++)
                                {
                                    matriz[mov_y, i] = buffer[cantidad];
                                    cantidad++;
                                }
                                filas++;
                                mov_x--;

                                for (int i = columnas_n - 1; i > filas - 1; i--)
                                {
                                    matriz[i, filas_n - 1] = buffer[cantidad];
                                    cantidad++;
                                }
                                columnas_n--;

                                for (int i = filas_n - 1; i > filas - 1; i--)
                                {
                                    matriz[columnas - 1, i] = buffer[cantidad];
                                    cantidad++;
                                }
                                filas_n--;

                            }
                        }
                        else
                        {//AntiHorario
                            int mov_x = matriz.GetLength(1);
                            int mov_y = matriz.GetLength(0);
                            int filas = 0;
                            int columnas = 0;
                            int filas_n = matriz.GetLength(1);
                            int columnas_n = matriz.GetLength(0);

                            int escribir = matriz.GetLength(0) * matriz.GetLength(1);
                            while (cantidad < escribir)
                            {
                                for (int i = filas; i < mov_x - 1; i++)
                                {
                                    matriz[filas, i] = buffer[cantidad];
                                    cantidad++;
                                }
                                filas++; ;
                                mov_x--;
                                for (int i = columnas; i < mov_y - 1; i++)
                                {
                                    matriz[i, mov_x] = buffer[cantidad];
                                    cantidad++;
                                }
                                columnas++;
                                mov_y--;

                                for (int i = filas_n - 1; i > filas - 1; i--)
                                {
                                    matriz[columnas_n - 1, i] = buffer[cantidad];
                                    cantidad++;
                                }

                                columnas_n--;
                                for (int i = columnas_n - 1; i > columnas - 1; i--)
                                {
                                    matriz[i, filas - 1] = buffer[cantidad];
                                    cantidad++;
                                }
                                filas_n--;
                            }
                        }
                        buffer = new char[bufferlenght];
                        cantidad = 0;
                        Escribir_Matriz(direccion, path_escritura);
                    }

                }
            }
        }
        private void Escribir_Matriz(int direccion, string path_Escritura)
        {
            var escritura = new char[bufferlenght];
            int cantidad = 0;

            using (var writer = new FileStream(path_Escritura, FileMode.Append))
            {
                if (direccion == 1)
                {
                    for (int i = 0; i < matriz.GetLength(0); i++)
                    {
                        for (int j = 0; j < matriz.GetLength(1); j++)
                        {
                            if (cantidad < matriz.GetLength(0) * matriz.GetLength(1))
                            {
                                escritura[cantidad] = matriz[i, j];
                                cantidad++;
                            }

                        }
                    }
                }
                else
                {

                    for (int i = 0; i < matriz.GetLength(1); i++)
                    {
                        for (int j = 0; j < matriz.GetLength(0); j++)
                        {
                            if (cantidad < matriz.GetLength(0) * matriz.GetLength(1))
                            {
                                escritura[cantidad] = matriz[j, i];
                                cantidad++;
                            }

                        }
                    }
                }


                var escritor = Encoding.UTF8.GetBytes(escritura);
                writer.Write(escritor, 0, escritor.Length);
                escritura = new char[bufferlenght];
            }
        }
    }
}