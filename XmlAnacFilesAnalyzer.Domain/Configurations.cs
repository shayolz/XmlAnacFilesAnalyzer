namespace XmlAnacFilesAnalyzer.Domain
{
    public static class Configurations
    {
        /// <summary>
        /// Numero di task per leggere i 19.000 documenti xml
        /// Testato con 50 task, la banda media è sui 200 mbit/s e tempistiche tra i 5-10 minuti
        /// </summary>
        public const int TASKS_NUMBER = 10;

        /// <summary>
        /// Timeout per richiesta documento xml (potrebbero dare un 404)
        /// </summary>
        public const int XML_DOCUMENT_TIMEOUT_REQUEST = 6000;

        /// <summary>
        /// Url di download per il file json
        /// </summary>
        public const string ANAC_URL_JSON = "https://dati.anticorruzione.it/data/l190-2021.json";

        /// <summary>
        /// Modalità debug per visualizzare più log
        /// </summary>
        public const bool DEBUG = false;
    }

}