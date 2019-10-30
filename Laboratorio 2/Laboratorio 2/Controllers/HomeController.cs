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
		private void BorrarArchivosDeCarpeta(string pathCarpeta)
		{
			//string pathABorrar = Server.MapPath("~/ArchivosTmp/");
			string[] pathsTmp = Directory.GetFiles(pathCarpeta);
			foreach (var item in pathsTmp)
				System.IO.File.Delete(item);
		}

		public ActionResult Index()
		{
			string pathCarpeta = Path.Combine(Server.MapPath("~/"), "Archivos");
			Directory.CreateDirectory(pathCarpeta);

			string pathCarpeta2 = Path.Combine(Server.MapPath("~/"), "ArchivosTmp");
			Directory.CreateDirectory(pathCarpeta2);

			string pathCarpeta3 = Path.Combine(Server.MapPath("~/"), "LlavesRSA");
			Directory.CreateDirectory(pathCarpeta3);

			BorrarArchivosDeCarpeta(Server.MapPath("~/ArchivosTmp/"));

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
			BorrarArchivosDeCarpeta(Server.MapPath("~/ArchivosTmp/"));
			return View();
		}
		[HttpPost]
		public ActionResult ZigZag(HttpPostedFileBase ArchivoEntrada, int clave)
		{
			BorrarArchivosDeCarpeta(Server.MapPath("~/ArchivosTmp/"));
			if (ArchivoEntrada != null && clave > 0)
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
			BorrarArchivosDeCarpeta(Server.MapPath("~/ArchivosTmp/"));
			return View();
		}
		[HttpPost]
		public ActionResult Cesar(HttpPostedFileBase ArchivoEntrada, string llave)
		{
			BorrarArchivosDeCarpeta(Server.MapPath("~/ArchivosTmp/"));
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
			BorrarArchivosDeCarpeta(Server.MapPath("~/ArchivosTmp/"));
			return View();
		}
		[HttpPost]
		public ActionResult SDES(HttpPostedFileBase ArchivoEntrada, string llave)
		{
			BorrarArchivosDeCarpeta(Server.MapPath("~/ArchivosTmp/"));
			bool LlaveValida = true;
			foreach (char item in llave)
			{
				if (item != '0' && item != '1')
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

						string path = Server.MapPath("~/ArchivosTmp/");
						string pathPrueba = path + nombreArchivo[0];
						path = path + ArchivoEntrada.FileName;
						ArchivoEntrada.SaveAs(path);

						string pathMiFichero = Server.MapPath("~/Archivos/");
						pathMiFichero = pathMiFichero + "S-DES.txt";

						H.Cifrado(llave,pathMiFichero, pathPrueba, path);

						ViewBag.Ok = "Proceso completado :)";
						return File(pathPrueba, "scif", (nombreArchivo[0] + ".scif"));
					}
					else if (nombreArchivo[1] == "scif")
					{
						SDES H = new SDES();

						string path = Server.MapPath("~/ArchivosTmp/");
						string pathPrueba = path + nombreArchivo[0];
						path = path + ArchivoEntrada.FileName;
						ArchivoEntrada.SaveAs(path);

						string pathMiFichero = Server.MapPath("~/Archivos/");
						pathMiFichero = pathMiFichero + "S-DES.txt";

						H.Descifrado(llave, pathMiFichero, pathPrueba, path);

						ViewBag.ok = "Proceso completado :)";
						return File(pathPrueba, "txt", (nombreArchivo[0] + ".txt"));
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

		[HttpGet]
		public ActionResult RSA()
		{
			BorrarArchivosDeCarpeta(Server.MapPath("~/ArchivosTmp/"));
			return View();
		}
		[HttpPost]
		public ActionResult RSA(HttpPostedFileBase ArchivoEntrada, HttpPostedFileBase ArchivoLlave)
		{
			BorrarArchivosDeCarpeta(Server.MapPath("~/ArchivosTmp/"));
			if (ArchivoEntrada != null && ArchivoLlave != null)
			{
				string[] nombreArchivo = ArchivoEntrada.FileName.Split('.');
				try
				{
					if (nombreArchivo[1] == "txt")
					{
						RSA H = new RSA();
						
						string path = Server.MapPath("~/ArchivosTmp/");
						string pathRetorno = path + nombreArchivo[0];
						path = path + ArchivoEntrada.FileName;
						ArchivoEntrada.SaveAs(path);

						string pathLlave = Server.MapPath("~/ArchivosTmp/") + ArchivoLlave.FileName;

						ViewBag.Ok = "Proceso completado :)";
						return File(pathRetorno, "rsacif", (nombreArchivo[0] + ".rsacif"));
					}
					else if (nombreArchivo[1] == "rsacif")
					{
						RSA H = new RSA();

						string path = Server.MapPath("~/ArchivosTmp/");
						string pathRetorno = path + nombreArchivo[0];
						path = path + ArchivoEntrada.FileName;
						ArchivoEntrada.SaveAs(path);

						string pathLave = Server.MapPath("~/ArchivosTmp/") + ArchivoLlave.FileName;

						ViewBag.ok = "Proceso completado :)";
						return File(pathRetorno, "txt", (nombreArchivo[0] + ".txt"));
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
				if (ArchivoLlave == null)
					ViewBag.Error = "Debe ingresar un documento con la llave";
			}
			return View();
		}
		[HttpGet]
		public ActionResult LLavesRSA()
		{
			BorrarArchivosDeCarpeta(Server.MapPath("~/ArchivosTmp/"));
			return View();
		}
		[HttpPost]
		public ActionResult LlavesRSA(int P, int Q)
		{
			try
			{
				if (P > 0 && Q > 0)
				{
					BorrarArchivosDeCarpeta(Server.MapPath("~/LlavesRSA/"));
					string pathLlavePublica = Server.MapPath("~/LlavesRSA/") + "Public";
					string pathLlavePrivada = Server.MapPath("~/LlavesRSA/") + "Private";
					List<string> Llaves = new List<string>();
					Llaves.Add(pathLlavePrivada);
					Llaves.Add(pathLlavePublica);

					RSA H = new RSA();
					H.Llaves(P, Q, pathLlavePrivada, pathLlavePublica);
					ViewBag.Ok = "Puede Proceder a descargar las llaves";
					return View(Llaves);
				}
				else
				{
					ViewBag.Error = "Lo valores de 'P' y 'Q' no pueden ser menores o iguales a 0";
					return View();
				}
			}
			catch
			{
				ViewBag.Error = "Debe ingresar los valores de 'P' y 'Q'";
				return View();
			}
		}
		//ActionLink
		public ActionResult DescargarLlavesRSA(string Path)
		{
			string TipoLlave = "";
			if (Path.Substring((Path.Length - 7), 7) == "Private")
				TipoLlave = "Private.Key";
			if (Path.Substring((Path.Length - 6), 6) == "Public")
				TipoLlave = "Public.Key";

			return File(Path, "Key", TipoLlave);
		}
	}
}