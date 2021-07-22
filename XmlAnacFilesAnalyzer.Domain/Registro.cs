namespace XmlAnacFilesAnalyzer.Domain
{
    /* Example response json
    {
	    "identificativoPEC": "opec292.20200210090607.22397.154.1.62@pec.aruba.it",
	    "codiceFiscale": "02311370585",
	    "ragioneSociale": "COMUNE DI ARTENA",
	    "url": "http://www.halleyweb.com/c058011/zf/index.php/dataset/appalti-2019.xml",
	    "dataUltimoTentativoAccessoUrl": "2021-02-17T09:26:15.623+0000",
	    "esitoUltimoTentativoAccessoUrl": "successo"
    },
    */
    public class Registro
    {
        public string identificativoPEC { get; set; }
        public string codiceFiscale { get; set; }
        public string ragioneSociale { get; set; }
        public string url { get; set; }
        public string dataUltimoTentativoAccessoUrl { get; set; }
        public string esitoUltimoTentativoAccessoUrl { get; set; }

    }

}