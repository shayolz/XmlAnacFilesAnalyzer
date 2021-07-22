using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading;
using XmlAnacFilesAnalyzer.Domain;

namespace XmlAnacFilesAnalyzer.Services
{
    public class RegistriService
    {
        /**
         * Sito web ANAC https://dati.anticorruzione.it/#/l190
         * Alcune volte l'URL per il downlaod json da errore 404 (possibile sovraccarico del server), quindi riproviamo ogni secondo finché non risponde 200
         */
        public List<Registro> GetAllRegistri()
        {
            Console.WriteLine("Inizio download registri...");

            while (true)
            {
                try
                {
                    using (var webClient = new WebClient())
                    {
                        webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
                        var jsonData = webClient.DownloadString(Configurations.ANAC_URL_JSON);
                        var result = JsonSerializer.Deserialize<List<Registro>>(jsonData);

                        Console.WriteLine(result.Count + " registri trovati.");
                        return result;
                    }
                }
                catch (Exception ex) when (ex.Message.Contains("(404)"))
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("Ricerca in corso...");
                }
            }
        }
    }
}
