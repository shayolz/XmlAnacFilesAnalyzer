# XmlAnacFilesAnalyzer
Semplice console application che legge tutti i registri su ANAC e analizza i file xml cercando una parola o una frase.

Finalità del programma:
1. Inserimento di una parola o frase se si vuole cercare all'interno di tutti i documenti XML nel portale https://dati.anticorruzione.it/#/home
2. Una volta inserita la parola o frase, vengono scaricati tutti i registri dal file pubblico json https://dati.anticorruzione.it/data/l190-2021.json
3. Per ogni registro viene analizzato il file XML presente all'interno e viene cercata la parola o frase inserita
4. Viene salvato nella directory del programma un file .txt con tutti i link ai file XML dove è stata trovata la parola o frase
