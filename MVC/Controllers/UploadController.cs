using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Services;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    public class UploadController : Controller
    {
        private static ICollection<string> services = new List<string>(); 
        private static ICollection<string> firstLine = new List<string>();
        private static string serviceName;
        private static string cityId;

        // This action renders the form
        public ActionResult Upload()
        {
            return View();
        }

        // This action handles the form POST and the upload
       
        public ActionResult UploadFile()
        {
            var files = HttpContext.Request.Form.Files;

            if(files.Count > 0)
            {
                services = new List<string>();
                firstLine = new List<string>();
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using ExcelPackage package = new ExcelPackage(files[0].OpenReadStream());
                ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();
                //receber linhas e colunas
                int rowsOfFile = worksheet.Dimension.Rows;
                int columnsOfFile = worksheet.Dimension.Columns;
                //criando instancias de listas e dicionarios
                List<string> list = new List<string>();
                IDictionary<string, List<string>> routes = new Dictionary<string, List<string>>();
                //referencia cep
                int columnCep = 0;
                int columnServices = 0;
                for (int rows = 1; rows < rowsOfFile; rows++)
                {
                    list = new List<string>();
                    for (int columns = 1; columns < columnsOfFile; columns++)
                    {
                        //recebe conteudo da linhado excel
                        var conteudo = worksheet.Cells[rows, columns].Value == null ? "" : worksheet.Cells[rows, columns].Value;
                        if (conteudo == null && columns == columnsOfFile - 1)
                            break;
                        if (conteudo.ToString().ToUpper() == "CEP")
                            columnCep = columns;

                        if (conteudo.ToString().ToUpper() == "SERVIÇO")
                            columnServices = columns;
                        list.Add(conteudo.ToString());
                        //Console.WriteLine($"{conteudo}");
                    }
                    if (worksheet.Cells[rows, columnCep].Value == null || worksheet.Cells[rows, columnCep].Value.ToString() == "")
                        break;
                    routes.Add(worksheet.Cells[rows, columnCep].Value.ToString(), list);
                }
                for (int i = 2; i < rowsOfFile; i++)
                {
                    int count = 0;
                    if (worksheet.Cells[i, columnServices].Value == null)
                        break;
                    var resultOfExcel = worksheet.Cells[i, columnServices].Value.ToString();
                    foreach (var service in services)
                    {
                        if (service == resultOfExcel)
                            count = 1;

                    }
                    if(count == 0)
                        services.Add(worksheet.Cells[i, columnServices].Value.ToString());
                }
                var value = routes.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
                firstLine = value["CEP"].ToList();
                return RedirectToAction(nameof(OperationAddress));
            }
            return RedirectToAction(nameof(Upload));
        
        }

        public async Task<IActionResult> OperationAddress()
        {
            IEnumerable<Address> address = await AddressServices.GetAll();

            ViewBag.Address = address;
            ViewBag.Services = services;

            return View();
        }

        public IActionResult GetTeamsByCity()
        {
            serviceName = Request.Form["serviceName"].ToString();
            cityId = Request.Form["AddressOperationTeams"].ToString();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Teams> teams = await TeamsServices.GetTeamsByCity(cityId);

            ViewBag.Teams = teams;
            ViewBag.FirstLine = firstLine;

            return View();
        }


    }
}