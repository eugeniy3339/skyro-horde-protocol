1. Vytvorim novy scriptable object, kam dam vsetkych enemakov co maju pocas tej vlny ist.
2. Pull posle factory scriptu: spawni mi enemaka. Factory script enemaka spawne a nastavi vsetke jeho hodnoty.
3. UI sleduje scriptable object, lebo to cislo vlny setupujeme v nom, WavesManager ho moze mat ine. Find nepouzivame, lebo ak nahodou budeme mat viac ako 1 spawner na scene, tak ono to moze najst iny spawner, ako ten, ktory potrebujeme.
4. Menenie hodnot scriptable objectov cez skripty. Potom tam mas nepekne cislo.