using System.IO;
using System.Threading.Tasks;

namespace XmlAnacFilesAnalyzer.Services
{
    public class FileService
    {
        public async Task WriteOnFileAsync(string fileName, string text)
        {
            string folderPath = $"{Directory.GetCurrentDirectory()}\\";

            string completePath = folderPath + "Dati-" + fileName + ".txt";

            using (FileStream fs = new FileStream(completePath, FileMode.Append, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(fs))
            {
                await sw.WriteLineAsync(text);
            }
        }
    }
}
