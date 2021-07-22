using System;
using System.Threading.Tasks;
using XmlAnacFilesAnalyzer.Services;

namespace XmlAnacFilesAnalyzer
{
    /*
     * Finalità del programma:
     * 1. Inserimento di una parola o frase se si vuole cercare all'interno di tutti i documenti XML nel portale https://dati.anticorruzione.it/#/home
     * 2. Una volta inserita la parola o frase, vengono scaricati tutti i registri dal file pubblico json https://dati.anticorruzione.it/data/l190-2021.json
     * 3. Per ogni registro viene analizzato il file XML presente all'interno e viene cercata la parola o frase inserita.
     * 4. Viene salvato nella directory del programma un file .txt con tutti i link ai file XML dove è stata trovata la parola o frase
     */
    static class Program
    {
        static void Main(string[] args)
        {
            // TODO: implement factory?
            var fileService = new FileService();
            var registriService = new RegistriService();
            var xmlReaderService = new XmlReaderService(fileService);
            var startingDate = DateTime.Now;

            Console.WriteLine("Avvio del programma " + startingDate + "\n");
            Console.WriteLine("I documenti vengono scaricati dal sito https://dati.anticorruzione.it/#/home \n");

            Console.WriteLine("Inserire il nome della parola che si vuole cercare nei documenti:");
            var parolaDaCercare = Console.ReadLine();
            Console.WriteLine("Frase inserita '" + parolaDaCercare + "'\n");

            if (string.IsNullOrWhiteSpace(parolaDaCercare))
            {
                throw new InvalidOperationException("Chiusura del programma.. parola da cercare non valida.");
            }

            var registri = registriService.GetAllRegistri();

            Console.WriteLine("Inizio lettura dei files XML, questa operazione può richiedere alcuni minuti (10-30)");
            var matchFound = xmlReaderService.FindNameOnXml(startingDate, registri, parolaDaCercare);
            Console.WriteLine("[COMPLETATO]: " + matchFound + " documenti sono stati trovati e analizzati alle " + DateTime.Now);
            Console.WriteLine("E' possibile visualizzare il risultato nel file .txt generato.");
            Console.ReadLine();
        }
    }
}
