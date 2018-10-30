# Lab 01
## Duszek
![Ghost game](https://github.com/Natsyu/GrafikaRuchoma/blob/master/ReadmeGifs/lab_01-duszek.gif)
#### Treść
```
Planszę gry stanowi zadane w postaci pliku graficznego tło. 
Tło powinno zajmować cały dostępny obszar okna. 
Nowy duszek pojawia się na ekranie co określony przez programistę czas równy 0,5s i 
pozostaje na nim przez czas losowany z zakresu (0 - 2s). 
Po upływnie tego czasu duszek znika z ekranu. 
Jeżeli użytkownik, za pomocą wskaźnika myszy kliknie w obszar rysowania duszka (sprite'a), 
zostanie naliczony punkt za trafienie – duszek pozostanie na ekranie w postaci zmodyfikowanej („zbity”) 
oraz zostanie naliczony punkt za trafienie (patrz górna belka okna gry). 
Liczba „zbitych” przez gracza duszków ma być wypisywana w belce tytułowej okna. 
Wymagania szczegółowe:
•	naciśnięcie klawisza Escape kończy program
•	użytkownik ma mieć możliwość zmiany rozmiaru okna
•	aktualny stan liczby zdobytych punktów wyświetlany jest w postaci tekstu – 
                                      w belce tytułowej okna (jak na rysunku)
•	należy zapewnić losową zmianę pozycji nowego duszka (biorąc pod uwagę rozmiary okna) 
                            oraz czas jego wyświetlania na ekranie w przedziale 0 - 2s
•	oryginalny obraz sprite'a (ghost.png oraz ghost-foot.png) należy przeskalować programowo
        aby uzyskać efekt jak na rysunku (w tym przypadku, wymiary duszka wynoszą 50x50 px)
•	Zabrania się jakichkolwiek modyfikacji zadanych plików graficznych
```


## Liczby
![numbers](https://github.com/Natsyu/GrafikaRuchoma/blob/master/ReadmeGifs/lab_01-liczby.gif)
#### Treść
<p align="center">
  <img src="https://i.imgur.com/C8LOWIX.png" alt="Size Limit example">
</p>

# Lab 02
## Pong
![pong](https://github.com/Natsyu/GrafikaRuchoma/blob/master/ReadmeGifs/lab_02-pong.gif)
#### Treść
```
•	naciśnięcie [Escape] kończy program,
•	zakłada się brak możliwości zmiany rozmiaru okna gry,
•	klawisz [Spacja] rozpoczyna rozgrywkę,
•	wektor prędkości piłki ma być modyfikowany w sposób zależny od obszaru uderzenia w rakietę (uwzględnia się przy tym podział paletki na 5 sektorów),
•	aktualny wynik punktowy ma być wyświetlany w górnej, środkowej części ekranu (jak na rysunku),
•	sterowanie rakietami odbywa się za pomocą klawiatury, przy czym:
•	Gracz1: góra - [Q], dół - [A].
•	Gracz2: góra - [P], dół - [L],
•	podczas detekcji kolizji piłki z krawędziami ekranu oraz rakietami należy uwzględnić margines (offset) wynoszący 10px
•	należy zaimplementować animcję sprite'a piłki (zgodnie z arkuszem animacji piłki), uwzględniając przy tym kontrolę prędkości animacji, z 
•	możliwością jej modyfikacji przez programistę, np.każda klatka wyświetlana przez 16ms,
•	kolizja piłki z pionową krawędzią ekranu ma zakończyć się animacją pojedyńczej sekwencji klatek, zgodnie z arkuszem animacji eksplozji,
•	kolizja piłki z rakietą powinna skutkować wyraźnym (300ms) "podświetleniem" sprite'a danej rakiety.
```
