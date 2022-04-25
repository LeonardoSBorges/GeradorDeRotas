using MVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Services
{
    public class MakeFileDoc
    {
        public static async Task Wirte(IDictionary<string, List<string>> data, List<string> teamForService, List<string> columnsForServices, List<string> nameService, Address cityService)
        {
            var header = data["OS"];
            var keys = data["KEYS"];
            List<List<string>> allData = new List<List<string>>();
            foreach (var key in keys)
            {
                List<string> justDataForUse = new List<string>();
                List<string> line = data[key];
                int city = 0, cityInHeader = 0;

                foreach (var column in columnsForServices)
                {
                    justDataForUse.Add(line[int.Parse(column)]);
                    city = justDataForUse.IndexOf(cityService.City);
                    cityInHeader = justDataForUse.IndexOf("CIDADE");

                }

                if(city > 0 || cityInHeader > 0)
                    allData.Add(justDataForUse);
            }

            var sb = new StringBuilder();
            var listForHeaderFiltered = allData[0];
;
            if (listForHeaderFiltered.Count > 0)
            {
                for (int countColumnsHeader = 1; countColumnsHeader < allData.Count; countColumnsHeader++)
                {

                    sb.Append($@"               ROTA TRABALHO - {DateTime.Now.ToString("dd/MM/yyyy")}           

");
                    var dataForBulderString = allData[countColumnsHeader];
                    for (int index = 0; index < columnsForServices.Count; index++)
                    {
                        var valueListForHeaderFilteredFormatted = await UpdateStringWithSpecialCharacters(listForHeaderFiltered[index]);
                        var valueDataForBuilderStringFormatted = await UpdateStringWithSpecialCharacters(dataForBulderString[index]);
                        sb.AppendLine($@"{valueListForHeaderFilteredFormatted}: {valueDataForBuilderStringFormatted}");
                    }


                    sb.AppendLine($@"\/------------------------------------------------------\/");

                }

                string date = DateTime.Now.ToString("dd/MM/yyyy");
                string hour = DateTime.Now.ToString("hh-mm-ss");
                string dateTime = date.Replace("/", "") + hour.Replace("-", "");

                string fileName = $@"D:\Teste\File{dateTime}.docx";

                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    sw.Write(sb.ToString());
                }
            }
        }


        public static async Task<string> UpdateStringWithSpecialCharacters(string str)
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
