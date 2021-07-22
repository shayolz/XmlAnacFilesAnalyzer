using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using XmlAnacFilesAnalyzer.Domain;

namespace XmlAnacFilesAnalyzer.Services
{
    public class XmlReaderService
    {
        private readonly FileService _fileService;
        public XmlReaderService(FileService fileService)
        {
            _fileService = fileService;
        }

        public int FindNameOnXml(DateTime startingDate, List<Registro> registri, string wordToFind)
        {
            var taskNumber = Configurations.TASKS_NUMBER;
            var totalCount = registri.Count;
            var groupCount = totalCount / taskNumber;

            LogManager.WriteDebugLog("Avvio analisi in " + taskNumber + " gruppi da " + groupCount + " ciascuno.");
            var listTask = new List<Task<int>>();
            for (int i = 1; i <= taskNumber; i++)
            {
                var starting = groupCount * (i - 1);
                var end = groupCount * i;

                var task = FindNameOnXmlAsync(startingDate, registri, wordToFind, starting, end);

                listTask.Add(task);
            }

            try
            {
                Task.WaitAll(listTask.ToArray());
            }
            catch (AggregateException ae)
            {
                foreach (var item in ae.Flatten().InnerExceptions)
                {
                    Console.WriteLine("Errore interno :" + item.Message);
                }
            }

            return listTask.Sum(x => x.Result);
        }


        public async Task<int> FindNameOnXmlAsync(DateTime startingDate, List<Registro> registri, string wordToFind, int from, int to)
        {
            var numeroDocumentiAnalizzati = 0;
            for (int i = from; i < to; i++)
            {
                Registro registro = registri[i];
                string url = registro.url.Trim();

                try
                {
                    if (i % 100 == 0 && i != 0)
                    {
                        LogManager.WriteDebugLog("Analizzati registri da " + from + " a " + i + "...");
                    }

                    if (string.IsNullOrWhiteSpace(registro.url) || !registro.url.EndsWith(".xml"))
                    {
                        LogManager.WriteDebugLog("[FAILED] Url invalid: " + registro.url);
                        continue;
                    }

                    var request = (HttpWebRequest)WebRequest.Create(url);
                    request.Timeout = Configurations.XML_DOCUMENT_TIMEOUT_REQUEST;
                    using (var response = await request.GetResponseAsync())
                    {
                        using (Stream stream = response.GetResponseStream())
                        {
                            var doc = await XDocument.LoadAsync(stream, LoadOptions.None, default(CancellationToken));
                            numeroDocumentiAnalizzati++;

                            if (doc.Document.ToString().Contains(wordToFind))
                            {
                                Console.WriteLine("[Trovato] " + url);
                                numeroDocumentiAnalizzati++;
                                await _fileService.WriteOnFileAsync(startingDate.ToString("yyyy-dd-M-HH-mm-ss"), url);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogManager.WriteDebugLog("[FAILED] Ignored invalid file at url: " + url);
                    LogManager.WriteDebugLog("[FAILED] Reason: " + ex.Message);
                }
            }

            LogManager.WriteDebugLog("Analisi completata per registri da " + from + " a " + to + " con " + numeroDocumentiAnalizzati + " documenti analizzati.");
            return numeroDocumentiAnalizzati;
        }
    }
}
