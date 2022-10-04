# CubesOnCircle
Aplikacja treningowa Unity.

Główne założenia:
1. Spawn'owanie określonej ilości cube'ów.
2. Cube'y są spawn'owane na elipsie o zadanych parametrach i są zorientowane na zewnątrz.
3. Cube’y są równomiernie rozłożone. Czyli 5 cube’ów wyprodukuje pięciokąt.
4. W momencie naciśnięcia spacji wszystkie cube’y dostają losową prędkość i w losowym kierunku.
5. Cube’y które się zderzą ze sobą bądź wylecą poza ekran są niszczone. Czyli docelowo zniszczone zostaną
wszystkie.
6. Kalkulacja fizyki jest autorska (kolizja kół – czyli odległości środków), a więc Unity liczy lot cube’ów
ale już nie to który na który wpadł. Ponowne wciśniecie spacji nie robi nic. Esc zamyka aplikacje.
7. W prawym górnym rogu label pokazujący ile cube’ów jest jeszcze na ekranie.
8. Parametry działania aplikacji (ilość cube’ów, parametry figury) są w Scriptible Objecie.
9. UI (cała jedna labelka) jest na odzielnej Scenie a kod z nią związany jest w odzielnym assembly.
10. Unity Phisics używamy do nadania obiektom prędkości, ale już nie do sprawdzania czy cube zderzył się
z innym lub wyleciał poza ekran. Do wyliczania zderzeń zakładamy że kwadrat jest kołem (wiem że bez
sensu ale to jest aplikacja treningowa).
