using MVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Services
{
    public class MakeFileDoc
    {
        public static async Task Wirte(IDictionary<string, List<string>> data, List<string> teamForService, List<string> columnsForServices, string nameService, Address cityService)
        {
            var header = data["OS"];
            var keys = data["KEYS"];
            List<List<string>> allData = new List<List<string>>();
            var indexService = 0;
            foreach (var key in keys)
            {
                List<string> justDataForUse = new List<string>();
                List<string> line = data[key];
                int city = 0, cityInHeader = 0;

                foreach (var column in columnsForServices)
                {
                    justDataForUse.Add(line[int.Parse(column)]);
                    city = justDataForUse.IndexOf(cityService.City.ToUpper());
                    cityInHeader = justDataForUse.IndexOf("CIDADE");
                    if (indexService <= 0)
                        indexService = justDataForUse.IndexOf(nameService);
                }

                var typeService = indexService > 0 ? justDataForUse[indexService] : "";
                if (city > 0 && typeService.ToUpper().Equals(nameService, StringComparison.InvariantCultureIgnoreCase) || cityInHeader > 0)
                    allData.Add(justDataForUse);
            }

            //string a = "a", b = "a";
            //var t = a.Equals(b, StringComparison.InvariantCultureIgnoreCase);
            allData.RemoveAt(0);
            var divisionServicesForTeams = allData.Count / teamForService.Count;
            decimal restDivisionOfServicesForTeams = allData.Count % teamForService.Count;
            List<string> lastData = new List<string>();
            if (!(allData.Count % teamForService.Count == 0))
            {
                lastData = allData.ToList().LastOrDefault();
                allData.Remove(lastData);
            }


            var stringBuilder = new StringBuilder();
            if (allData.Count > 0)
            {
                stringBuilder.Append($@"               ROTA TRABALHO - {DateTime.Now.ToString("dd/MM/yyyy")}           

");
                
                int indexAux = 0, count = 0;
                stringBuilder.AppendLine($@"Time:{teamForService[indexAux]}

");
                for (int index = 0; index < allData.Count; index++)
                {
                    
                    if (count == divisionServicesForTeams && restDivisionOfServicesForTeams == 0 || count > divisionServicesForTeams && restDivisionOfServicesForTeams > 0)
                    {
                        indexAux++;
                        stringBuilder.AppendLine(@$"/\ ========================================= /\
");
                        stringBuilder.Append($@"Time: {teamForService[indexAux]}
                                        
");
                        count = 0;
                    }
                    
                    var lineData = allData[index];
                    for (int i = 0; i < columnsForServices.Count; i++)
                    {
                        var stringForCreateStringBuilder =  BuildingNewString(header, lineData, i).ToString();
                        stringBuilder.Append(stringForCreateStringBuilder);
                    }
                    stringBuilder.AppendLine($@"
----------
");
                    count++;
                }
                if(lastData.Count > 0 && !(teamForService.Count % 2 == 0))
                {
                    stringBuilder.AppendLine(@$"/\ ========================================= /\
");
                    stringBuilder.Append($@"Time: {teamForService.LastOrDefault()}
                                        
");
                    for (int i = 0; i < columnsForServices.Count ; i++)
                    {
                        var stringForCreateStringBuilder = BuildingNewString(header, lastData, i).ToString();
                        stringBuilder.Append(stringForCreateStringBuilder);
                    }
                }
                else if (lastData.Count > 0)
                {
                    for (int i = 0; i < columnsForServices.Count; i++)
                    {

                        var stringForCreateStringBuilder =  BuildingNewString(header, lastData, i).ToString();
                        stringBuilder.Append(stringForCreateStringBuilder);
                    }
                }
            }

            string date = DateTime.Now.ToString("dd/MM/yyyy");
            string hour = DateTime.Now.ToString("hh-mm-ss");
            string dateTime = date.Replace("/", "") + hour.Replace("-", "");

            string fileName = $@"D:\Teste\File{dateTime}.docx";

            using (StreamWriter sw = new StreamWriter(fileName))
            {
   
                sw.Write(stringBuilder.ToString());
            }
        }

        public static string BuildingNewString(List<string> header, List<string> lineData, int i)
        {
            var valueDataHeaderFilteredFormatted = UpdateStringWithSpecialCharacters(header[i]);
            var valueDataForBuilderStringFormatted = UpdateStringWithSpecialCharacters(lineData[i]);
            return $@"
{valueDataHeaderFilteredFormatted}: {valueDataForBuilderStringFormatted}";
        }

        public static string UpdateStringWithSpecialCharacters(string str)
        {
            /** Troca os caracteres acentuados por não acentuados **/
            string[] acentos = new string[] { "ç", "Ç", "á", "é", "í", "ó", "ú", "ý", "Á", "É", "Í", "Ó", "Ú", "Ý", "à", "è", "ì", "ò", "ù", "À", "È", "Ì", "Ò", "Ù", "ã", "õ", "ñ", "ä", "ë", "ï", "ö", "ü", "ÿ", "Ä", "Ë", "Ï", "Ö", "Ü", "Ã", "Õ", "Ñ", "â", "ê", "î", "ô", "û", "Â", "Ê", "Î", "Ô", "Û" };
            string[] semAcento = new string[] { "c", "C", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "Y", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U", "a", "o", "n", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "A", "O", "N", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U" };

            for (int i = 0; i < acentos.Length; i++)
            {
                str = str.Replace(acentos[i], semAcento[i]);
            }
            /** Troca os caracteres especiais da string por "" **/
            string[] caracteresEspeciais = { "\\.", ",", "-", ":", "\\(", "\\)", "ª", "\\|", "\\\\", "°" };

            for (int i = 0; i < caracteresEspeciais.Length; i++)
            {
                str = str.Replace(caracteresEspeciais[i], "");
            }

            /** Troca os espaços no início por "" **/
            str = str.Replace("^\\s+", "");
            /** Troca os espaços no início por "" **/
            str = str.Replace("\\s+$", "");
            /** Troca os espaços duplicados, tabulações e etc por " " **/
            str = str.Replace("\\s+", " ");
            return str;
        }
    }
}
