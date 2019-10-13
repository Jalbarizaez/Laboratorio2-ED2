using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Laboratorio_2.Models;

namespace Laboratorio_2.Controllers
{
	public class HomeController : Controller
	{
		private void BorrarArchivosTemporales()
		{
			string pathABorrar = Server.MapPath("~/ArchivosTmp/");
			string[] pathsTmp = Directory.GetFiles(pathABorrar);
			foreach (var item in pathsTmp)
				System.IO.File.Delete(item);
		}

		public ActionResult Index()
		{
			string pathCarpeta = Path.Combine(Server.MapPath("~/"), "Archivos");
			Directory.CreateDirectory(pathCarpeta);

			string pathCarpeta2 = Path.Combine(Server.MapPath("~/"), "ArchivosTmp");
			Directory.CreateDirectory(pathCarpeta2);

			BorrarArchivosTemporales();

			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}

		[HttpGet]
		public ActionResult ZigZag()
		{
			BorrarArchivosTemporales();
			return View();
		}
		[HttpPost]
		public ActionResult ZigZag(HttpPostedFileBase ArchivoEntrada, int clave)
		{
			BorrarArchivosTemporales();
			if (ArchivoEntrada != null)
			{
				string[] nombreArchivo = ArchivoEntrada.FileName.Split('.');
				try
				{
					if (nombreArchivo[1] == "txt")
					{
						ZigZag H = new ZigZag();

						string path = Server.MapPath("~/ArchivosTmp/");
						string pathPrueba = path + nombreArchivo[0];
						path = path + ArchivoEntrada.FileName;
						ArchivoEntrada.SaveAs(path);

						H.Codificar(path, pathPrueba, clave);

						ViewBag.Ok = "Proceso completado :)";
						return File(pathPrueba, "cif", (nombreArchivo[0] + ".cif"));
					}
					else if (nombreArchivo[1] == "cif")
					{
						ZigZag H = new ZigZag();
						string path = Server.MapPath("~/ArchivosTmp/");
						string pathPrueba = path + nombreArchivo[0];
						path = path + ArchivoEntrada.FileName;
						ArchivoEntrada.SaveAs(path);

						H.Decodificar(path, pathPrueba, clave);

						ViewBag.ok = "Proceso completado :)";
						return File(pathPrueba, "txt", (nombreArchivo[0] + ".txt"));
					}
				}
				catch
				{
					ViewBag.Error02 = "Ha ocurrido un error con su archivo";
				}
			}
			else
			{
				ViewBag.Error01 = "No ha ingresado un archivo";
			}
			return View();
		}

		[HttpGet]
		public ActionResult Cesar()
		{
			BorrarArchivosTemporales();
			return View();
		}
		[HttpPost]
		public ActionResult Cesar(HttpPostedFileBase ArchivoEntrada, string llave)
		{
			BorrarArchivosTemporales();
			if (ArchivoEntrada != null)
			{
				string[] nombreArchivo = ArchivoEntrada.FileName.Split('.');
				try
				{
					if (nombreArchivo[1] == "txt")
					{
						Cifrado_Cesar H = new Cifrado_Cesar();

						string path = Server.MapPath("~/ArchivosTmp/");
						string pathPrueba = path + nombreArchivo[0];
						path = path + ArchivoEntrada.FileName;
						ArchivoEntrada.SaveAs(path);

						string pathMiFichero = Server.MapPath("~/Archivos/");
						pathMiFichero = pathMiFichero + "Abecedario.txt";

						H.Cifrar(pathMiFichero, path, llave, pathPrueba);

						ViewBag.Ok = "Proceso completado :)";
						return File(pathPrueba, "cif", (nombreArchivo[0] + ".cif"));
					}
					else if (nombreArchivo[1] == "cif")
					{
						Descifrado_Cesar H = new Descifrado_Cesar();
						string path = Server.MapPath("~/ArchivosTmp/");
						string pathPrueba = path + nombreArchivo[0];
						path = path + ArchivoEntrada.FileName;
						ArchivoEntrada.SaveAs(path);

						string pathMiFichero = Server.MapPath("~/Archivos/");
						pathMiFichero = pathMiFichero + "Abecedario.txt";

						H.Descifrar(pathMiFichero, path, llave, pathPrueba);

						ViewBag.ok = "Proceso completado :)";
						return File(pathPrueba, "txt", (nombreArchivo[0] + ".txt"));
					}
				}
				catch
				{
					ViewBag.Error02 = "Ha ocurrido un error con su archivo";
				}
			}
			else
			{
				ViewBag.Error01 = "No ha ingresado un archivo";
			}
			return View();
		}

		[HttpGet]
		public ActionResult SDES()
		{
			BorrarArchivosTemporales();
			return View();
		}
		[HttpPost]
		public ActionResult SDES(HttpPostedFileBase ArchivoEntrada, string llave)
		{
			BorrarArchivosTemporales();
			bool LlaveValida = true;
			foreach (char item in llave)
			{
				if (item != '0' || item != '1')
					LlaveValida = false;
			}
			if (llave.Length != 10)
				LlaveValida = false;

			if (ArchivoEntrada != null && LlaveValida)
			{
				string[] nombreArchivo = ArchivoEntrada.FileName.Split('.');
				try
				{
					if (nombreArchivo[1] == "txt")
					{
						SDES H = new SDES();

						//string path = Server.MapPath("~/ArchivosTmp/");
						//string pathPrueba = path + nombreArchivo[0];
						//path = path + ArchivoEntrada.FileName;
						//ArchivoEntrada.SaveAs(path);

						//Este va a ser el camino para el archivo de las Permutaciones
						//string pathMiFichero = Server.MapPath("~/Archivos/");
						//pathMiFichero = pathMiFichero + "Abecedario.txt";

						//Permutaciones, Lectura, llave, Escritura 
						//H.Cifrar(pathMiFichero, path, llave, pathPrueba);

						ViewBag.Ok = "Proceso completado :)";
						//return File(pathPrueba, "scif", (nombreArchivo[0] + ".scif"));
					}
					else if (nombreArchivo[1] == "cif")
					{
						//Descifrado_Cesar H = new Descifrado_Cesar();
						//string path = Server.MapPath("~/ArchivosTmp/");
						//string pathPrueba = path + nombreArchivo[0];
						//path = path + ArchivoEntrada.FileName;
						//ArchivoEntrada.SaveAs(path);

						//string pathMiFichero = Server.MapPath("~/Archivos/");
						//pathMiFichero = pathMiFichero + "Abecedario.txt";

						//Permutaciones, Lectura, llave, Escritura
						//H.Descifrar(pathMiFichero, path, llave, pathPrueba);

						ViewBag.ok = "Proceso completado :)";
						//return File(pathPrueba, "txt", (nombreArchivo[0] + ".txt"));
					}
				}
				catch
				{
					ViewBag.Error = "Ha ocurrido un error con su archivo";
				}
			}
			else
			{
				if (ArchivoEntrada == null)
					ViewBag.Error = "No ha ingresado un archivo";
				if (LlaveValida == false)
					ViewBag.Error = "Debe ingresar una llave con solo caracteres '1' o '0'.  Su llave debe poseer 10 caracteres";
			}
			return View();
		}

	}
}