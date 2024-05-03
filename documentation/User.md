# Felhasználói Dokumentáció

## Tartalomjegyzék

1.  [Bevezetés](#bevezetés)
2.  [Navigációs sáv](#navigációs-sáv)
3.  [Felhasználó kezelés](#felhasználó-kezelés) (Adminok részére)
4.  [Kurzus kezelés](#kurzus-kezelés) (Adminok részére)
5.  [Kurzusaim oldal](#kurzusaim-oldal)
6.  [Egy adott kurzus oldala](#egy-adott-kurzus-oldala-diákok-számára) (Diákok számára)
7.  [Egy adott kurzus oldala](#egy-adott-kurzus-oldala-tanárok-számára) (Tanárok számára)
8.  [Kvíz létrehozása](#kvíz-létrehozása) (Tanárok számára)
9.  [Kvíz módosítása](#kvíz-módosítása) (Tanárok számára)
10. [Kvíz kitöltése](#kvíz-kitöltése)
11. [Kvíz értékelése](#kvíz-értékelése) (Tanárok számára)
12. [Új feladat létrehozása](#új-feladat-létrehozása) (Tanárok számára)
13. [Feladat módosítása](#feladat-módosítása) (Tanárok számára)
14. [Feladat törlése](#feladat-törlése) (Tanárok számára)
15. [Feladat letöltése](#feladat-letöltése) (Tanárok számára)
16. [Feladat leadása](#feladat-leadása) (Diákok számára)



## Bevezetés

Az oldal segítségével iskolák és egyéb szervezetek tudják oktatási tevékenységeiket lebonyolítani. Egyszerű, felhasználóbarát felülettel tudják csoportokba sorolni a felhasználókat, kurzosokat tudnak létrehozni, leadandó feladatokat, jegyzeteket, kvízeket tudnak létrehozni és kitölteni.

## Navigációs sáv

### Bevezetés:

> A navigációs sáv a felhasználói felület központi eleme, amely lehetővé teszi a felhasználók számára a gyors navigálást a különböző oldalak és funkciók között. A sáv dinamikus tartalmat kínál, amely a felhasználó jogosultságainak függvényében változik.

## Főbb Funkciók és Elhelyezkedés:

1. **Logó és Főoldal**

   - A navigációs sáv bal oldalán található a `logó`, amely a főoldalra mutató linkként is funkcionál. A logóra kattintva bármelyik oldalról visszatérhet a főoldalra.

2. **Navigációs Linkek**

   - A `főoldal`hoz és a `kurzusok`hoz közvetlen linkek találhatók a navigációs sávban, lehetővé téve a gyors átmenetet ezekre az oldalakra.

3. **Adminisztrációs Menü**

   - Amennyiben a felhasználó adminisztrátori jogosultságokkal rendelkezik, egy lenyíló menü is megjelenik, amely további opciókat tartalmaz:

     - **Felhasználók kezelése:** Link a felhasználók adminisztrációs oldalára.
     - **Csoportok kezelése:** Link a csoportok kezeléséhez.
     - **Kurzusok adminisztrációja:** Link a kurzusok adminisztrációs felületére.

4. **Felhasználói Menü**

   - A navigációs sáv jobb oldalán egy felhasználói menü található, amely több beállítást és lehetőséget kínál:
     - **Felhasználói avatár:** Egy karikában megjeleníti a felhasználó monogramját.
     - **Téma Váltása:** A felhasználók váltogathatnak a sötét és világos téma között, a nap ikonra nyomva világos, a hold ikonra kattintva pedig a sötét témát állíthatunk be. A megjelenése eszköz méretétől függő: kismérettől felfele ikonok, kisméret és azalatt pedig szöveg formájában lehet kezelni a témát.
     - **Nyelv Váltása:** Egy kapcsoló segítségével a felhasználók váltogathatnak a `Magyar` és az `Angol` nyelv között.
     - **Profilom:** A felhasználó profiljának szerkesztése. A felhasználó itt jelszót tud változtatni.
     - **Kijelentkezés:** Biztonságosan kilépteti a felhasználót és a bejelentkezési oldalra irányítja.

## Felhasználó kezelés

### Bevezetés:

> Ez a dokumentum részletezi a felhasználókezelő oldal használatát, mely lehetővé teszi új felhasználók regisztrálását, meglévő felhasználók módosítását és törlését, valamint a felhasználói adatok exportálását egy webes alkalmazás keretében.

### Oldalbetöltés és Inicializálás:

- **Betöltési Indikátor**: Amíg az oldal tölt, egy forgó és pulzáló logó jelenik meg.
- **Oldalbetöltés után**: Az adatok betöltődése után megjelenik a felhasználói felület, amely több modulból áll.

### Felhasználói Interfész Elemek:

1. **Felhasználók Listája**

   - A felhasználók adatai táblázatos formában jelennek meg.
   - A táblázat tartalmazza a felhasználók nevét, email címét, admin státuszát, és lehetőséget nyújt a felhasználó módosítására vagy törlésére.

2. **Új Felhasználó Hozzáadása**

   - **Dialogus Ablak**: Új felhasználó hozzáadásához egy formot tartalmazó dialógusablak nyílik meg.
   - **Adatmezők**:
     - `Név` (kötelező, max 255 karakter)
     - `Email` (kötelező, email formátum)
     - `Jelszó` (kötelező, min 8 karakter)
     - `Jelszó megerősítése` (kötelező, egyeznie kell a jelszóval)

3. **Felhasználó Módosítása**

   - **Dialogus Ablak:** A módosítani kívánt felhasználó adatait tartalmazó dialógusablak.
   - **Adatmezők:**
     - Név, Email cím, Jelszó (opcionális, ha megváltoztatásra kerül), Admin státusz (checkbox).

4. **Felhasználó Törlése**

   - Egyes felhasználók kiválasztása után lehetőség nyílik arra, hogy őket egyszerre törölje, azonban szükség esetén lehetőség van minden egyes felhasználót külön-külön is eltávolítani.
   - A tömeges törlés funkciót biztosító gomb az `Új felhasználó` gomb mellett helyezkedik el, és csak akkor válik elérhetővé, ha már van kiválasztott felhasználó. Ez az állapot akkor ismerhető fel, ha a táblázat valamelyik sorában az `X` ikon helyett `✓` látható, jelezve, hogy az adott sor kiválasztásra került.

5. **Adatok Exportálása**

   - A felhasználók adatainak exportálása CSV formátumban.

### Visszajelzések kezelése:

- Sikeres műveletek esetén `zöld` háttérrel jelenik meg egy rövid üzenet, amely tájékoztatja a felhasználót a művelet sikeréről.
- Ha egy művelet nem sikerül, `piros` háttérrel jelenik meg egy hibaüzenet, amely részletezi a probléma okát.

### Funkciók és Műveletek:

- **Új Felhasználó Hozzáadása**

  - A 'Új felhasználó' gombra kattintva nyílik meg az új felhasználó hozzáadásához szükséges dialógusablak.
  - Az űrlap kitöltése és a 'Mentés' gombra kattintás után az adatok elküldésre kerülnek.

- **Felhasználók Módosítása**

  - A módosítani kívánt felhasználó melletti 'Toll' ikonra kattintva nyílik meg a módosítás dialógusablak.
  - Az űrlap módosítása és a 'Mentés' gombra kattintás után a változások elmentésre kerülnek.

- **Felhasználók Törlése**

  - Egy vagy több felhasználó kiválasztása után a 'Törlés' gombra kattintva a felhasználó(k) törlésre kerül(nek).

- **Adatok Exportálása**

  - Az 'Exportálás' gombra kattintva a jelenlegi felhasználói lista exportálásra kerül CSV formátumban.

- **Név szerinti Szűrés**

  - A felhasználók listájában található név oszlopban egy beviteli mező segítségével szűrhetőek a rekordok. A szűrési móddal alapértelmezetten a névvel kezdődő
    rekordok jelennek meg, de ez a mellette található gombbal módosítható.
  - A beviteli mezőben megadott szöveggel kezdődő összes felhasználó neve megjelenik a táblázatban.

- **Email szerinti Szűrés**

  - Az email oszlopban lévő szűrő segítségével tartalmazási alapon lehet szűrni. Ez azt jelenti, hogy minden olyan rekord megjelenik, amely tartalmazza a beviteli mezőbe írt szöveget.
  - A szűrési felétételt a mellette található gombbal lehet módosítani.

### Biztonsági és Validációs Szabályok

- Minden új felhasználó hozzáadása és meglévő felhasználó módosítása során a rendszer ellenőrzi az adatokat a megadott validációs szabályok szerint.
- Jelszavak esetén egyezőségi ellenőrzés történik.

## Kurzus Kezelés

### Bevezetés:

> A kurzuskezelő oldal lehetővé teszi a felhasználók számára, hogy hatékonyan kezeljék a kurzusokkal kapcsolatos tevékenységeiket, beleértve új kurzusok létrehozását, meglévők szerkesztését, törlését, csoportok hozzárendelését kurzusokhoz, valamint adatok exportálását. Ahhoz hogy el tudja érni ezeket a funkciókat navigáljon a fejléc segítségével a Adminisztrációs menűponton keresztül a `Kurzusok Kezelése` oldalra.

### Elérhető fő funkciók:

1. **Kurzus hozzáadása**

   - A kurzus hozzáadásához kattintson az `Új kurzus` gombra. Ekkor megjelenik egy dialógusablak, ahol megadhatja a kurzus nevét és hozzáadhat egy képet.
   - A kurzus neve kötelező mező, és 5-100 karakter közötti hosszúságúnak kell lennie. Ha a kurzus neve nem felel meg ezeknek a kritériumoknak, a rendszer értesíti a felhasználót a szükséges változtatásokról.
   - A képfeltöltés opcionális, és támogatott formátumok: JPEG, PNG, JPG, BMP.

2. **Kurzus szerkesztése**

   - Egy kurzus szerkesztéséhez keresse meg a kurzust a listában, és kattintson a szerkesztés ikonra. Egy új dialógusablak jelenik meg, amely tartalmazza a jelenlegi kurzusadatokat.
   - Itt módosíthatja a kurzus nevét vagy cserélheti a képet, illetve rendelhet hozzá csoportokat. Az új adatokat a `Mentés` gombra kattintva erősítheti meg.

3. **Kurzusok törlése**

   - Egyes kurzusokat közvetlenül törölhet a kurzus melletti `Törlés` ikonra kattintva.
   - Több kurzus egyidejű törléséhez jelölje ki a kívánt kurzusokat a jelölőnégyzetek segítségével, majd kattintson a `Több kurzus törlése` gombra. A `Törlés` gomb akkor válik elérhetővé amint egy darab kurzus kijelölésre került.

4. **Adatok exportálása**

   - A kurzusok listáját CSV formátumban exportálhatja az `Exportálás` gomb segítségével. Az exportált fájl tartalmazni fogja a kurzusok azonosítóját, nevét és a képüket base64 formátumban.

5. **Keresés és szűrés**

   - A kurzusok között gyorsan kereshet név alapján a táblázat feletti keresőmező használatával. A kurzusok listáját a keresési eredmények szerint frissíti a rendszer.
   - A kereső mező mellettt található első gombbal különböző szűrési módokat tudunk beállítani, mint például: Tartalmazás, kezdődés, végződés alapján való szűrés...
   - A kereső mező mellett található második gomb a beállított szűrés típust alaphelyzetbe állítja (az adott betűsorozattal kezdődő szavak alapján fog szűrni), azaz törli a beállított szürőt.

### Visszajelzések kezelése:

- Sikeres műveletek esetén `zöld` háttérrel jelenik meg egy rövid üzenet, amely tájékoztatja a felhasználót a művelet sikeréről.
- Ha egy művelet nem sikerül, `piros` háttérrel jelenik meg egy hibaüzenet, amely részletezi a probléma okát.

## Kurzusaim oldal

### Bevezetés:

> A kurzusaim oldal lehetővé teszi a felhasználók számára, hogy megtekinthessék azokat a kurzusokat, amelyekhez hozzáférésük van. Az oldal egy egyszerű, áttekinthető felületet biztosít, ahol a kurzusok kártya formájában jelennek meg.

### Funkciók és használat:

1. **Kurzusok Betöltése**

   - Amikor az oldal betöltődik, a kurzusok automatikusan betöltődnek a háttérben. Ezt a betöltést egy forgó jelző (spinner) jelzi, amíg az adatok sikeresen be nem töltődnek.
   - A kurzusok betöltése során a felhasználó saját kurzusainak listáját kapja meg, amelyek a felhasználó csoportjaiban szerepelnek.

2. **Kurzus Kártyák**

   - Minden kurzus egy kártyán jelenik meg, amely tartalmazza a kurzus nevét, képét és a hozzá tartozó csoport nevét. Ezenek kivül még tartalmaz egy gombot 'Megtekintés' felirattal amely megnyomásakor átvisz az adott kurzus oldalára.
   - A kurzusokat csoportosítva, több oszlopban jelenítik meg a nagyobb képernyőkön, míg kisebb képernyőkön egymás alá kerülnek a kártyák.

3. **Üzenet, ha nincsenek kurzusok**

- Ha a felhasználónak nincsenek kurzusai, egy központi üzenet jelenik meg, amely tájékoztatja a felhasználót az alábbi üzenettel: "Nincsenek kurzusaid".

## Egy adott kurzus oldala (Diákok számára)

### Bevezetés:

> Egy adott kurzus oldala lehetővé teszi a diákok számára, hogy megtekintsék a kurzusokhoz tartozó témákat és a témához tartozó feladatokat, jegyezeteket. Az oldal egy harmonika szerkezetben jeleníti meg a témákat, ahol minden téma egy külön panelen található, tartalmazva a téma részleteit.

### Témák:

- **Téma Információk:** Minden téma panelje tartalmazza a téma nevét és az alatta hozzáadot jegyzetét, feladatokat.
- **Navigáció a Témák Között:** A téma paneljei kibonthatóak, így a diákok könnyen áttekinthetik a teljes kurzustéma-szerkezetét.

### Jegyzetek:

- **Jegyzetek létrehozása:** Jegyzeteket az adott kurzuson belül található témákhoz tud hozzáadni a tanuló a `Jegyzet hozzáadása` gomb megnyomásával. Itt meg kell adni a jegyzet nevét, annak tartalmát, majd ezek után a `mentés` gombra nyomva hozzáadja az adott témához a jegyzetet.

- **Jegyzet megtekintése:** A már létrehozott jegyzeteket a `Megtekintés` gombra kattintva tudja a felhasználó megtekinteni.

- **Jegyzet szerkesztése:** A `Megtekintés` gombra kattintva nyílik lehetőség a dokumentum elérésére. Alapbeállításban a jegyzet csak olvasható formában jelenik meg. Amennyiben szerkeszteni kívánja a szöveget, először kattintson a `Szerkesztés` gombra, ekkor a dokumentum szerkeszthető állapotba kerül. A szerkesztés befejeztével a `Mentés` gombra kattintva rögzítheti a változtatásokat, és ezzel sikeresen frissíti a jegyzet tartalmát.

- **Jegyzet törlése:** A jegyzet törlése a `Megtekintés` gombra kattintva érhető el. Ha úgy dönt, hogy már nincs szüksége a jegyzetre, egyszerűen kattintson a `Törlés` gombra. Ezt követően megjelenik egy megerősítő párbeszédpanel, amely arra kéri Önt, hogy igazolja vissza a törlés szándékát. Ez a lépés biztosítja, hogy csak akkor törölje a jegyzetet, ha valóban ezt szeretné.

### Visszajelzések kezelése:

- Sikeres műveletek esetén `zöld` háttérrel jelenik meg egy rövid üzenet, amely tájékoztatja a felhasználót a művelet sikeréről.
- Ha egy művelet nem sikerül, `piros` háttérrel jelenik meg egy hibaüzenet, amely részletezi a probléma okát.

## Egy adott kurzus oldala (Tanárok számára)

### Bevezetés:

> Egy adott kurzus oldala lehetővé teszi a tanárok számára, hogy átfogóan kezeljék a kurzusokhoz kapcsolódó témákat. Az oldal különféle funkcionalitásokat kínál, mint például új téma hozzáadása, meglévő téma szerkesztése és törlése. Ezen felül a tanárok hozzáadhatnak feladatokat és jegyzeteket az egyes témákhoz.

### Témák:

- **Téma Információk:** Minden téma panelje tartalmazza a téma nevét és az alatta hozzáadot jegyzetét, feladatokat és a tanár által feltöltött esetleges teszteket. Ezene kivűl szerkesztési valamint törlési lehetőségeket is nyújt a tanároknak.
- **Navigáció a Témák Között:** A téma paneljei kibonthatóak, így a diákok könnyen áttekinthetik a teljes kurzustéma-szerkezetet.

### Új téma hozzáadása

- **Új Téma Gomb:** Az `Új téma` gombra kattintva megnyílik egy dialógusablak, ahol megadható a téma neve és sorrendje.
- **Adatbevitel:** A téma neve kötelező és legfeljebb 60 karakter hosszú lehet. A sorrend egy szám, ami meghatározza a téma elhelyezkedését a listában.
- **Mentés:** A `Mentés` gombra kattintva a rendszer hozzáadja az új témát a kurzushoz.

### Téma szerkesztése

- **Szerkesztés Gomb:** A szerkesztés gombra kattintva egy hasonló dialógusablak jelenik meg, mint az új téma hozzáadásakor.
- **Adatok Frissítése:** A téma adatainak frissítése után a `Mentés` gombra kattintva a rendszer frissíti a téma adatait.

### Téma törlése

- **Törlés Gomb:** A törlés gombra kattintva a rendszer eltávolítja a témát a kurzusból. Ez a művelet végleges, és nem állítható helyre.

### Jegyzetek:

- **Jegyzetek létrehozása:** Jegyzeteket az adott kurzuson belül található témákhoz tud hozzáadni a tanár a `Műveletek` lenyiló listájában található `Új jegyzet` gomb megnyomásával. Itt meg kell adni a jegyzet nevét, annak tartalmát, majd ezek után a `mentés` gombra nyomva hozzáadja az adott témához a jegyzetet.

- **Jegyzet megtekintése:** A már létrehozott jegyzeteket a `Megtekintés` gombra kattintva tudja a tanár megtekinteni.

- **Jegyzet szerkesztése:** A `Megtekintés` gombra kattintva nyílik lehetőség a dokumentum elérésére. Alapbeállításban a jegyzet csak olvasható formában jelenik meg. Amennyiben szerkeszteni kívánja a szöveget, először kattintson a `Szerkesztés` gombra, ekkor a dokumentum szerkeszthető állapotba kerül. A szerkesztés befejeztével a `Mentés` gombra kattintva rögzítheti a változtatásokat, és ezzel sikeresen frissíti a jegyzet tartalmát.

- **Jegyzet törlése:** A jegyzet törlése a `Megtekintés` gombra kattintva érhető el. Ha úgy dönt, hogy már nincs szüksége a jegyzetre, egyszerűen kattintson a `Törlés` gombra. Ezt követően megjelenik egy megerősítő párbeszédpanel, amely arra kéri Önt, hogy igazolja vissza a törlés szándékát. Ez a lépés biztosítja, hogy csak akkor törölje a jegyzetet, ha valóban ezt szeretné.

### Csoportok kezelése:

> A tanár könnyedén módosíthatja a kurzushoz tartozó csoportokat a `Csoportok kezelése` gombra kattintva. Egy felugró ablak jelenik meg, ahol a lefele mutató nyílra kattintva kibontakozik egy panel. Ezen a panelen belül választhatja ki azokat a csoportokat, amelyeket szeretne a kurzushoz hozzárendelni. A kiválasztás után, ha minden beállítással elkészült, zárja be a panelt a jobb felső sarokban lévő `X` ikonra kattintva vagy egyszerűen kattintson a panelen kívülre, hogy bezárja azt. Végül ne felejtse el a `Mentés` gombra kattintani a változtatások rögzítéséhez.

### Visszajelzések kezelése:

- Sikeres műveletek esetén `zöld` háttérrel jelenik meg egy rövid üzenet, amely tájékoztatja a felhasználót a művelet sikeréről.
- Ha egy művelet nem sikerül, `piros` háttérrel jelenik meg egy hibaüzenet, amely részletezi a probléma okát.

### Design és Felhasználói Interakció:

- **Dinamikus Interakció:** A téma hozzáadása, szerkesztése és törlése dinamikusan történik, anélkül, hogy az oldalt újra kellene tölteni.
- **Hozzáférés-szabályozás:** Csak azok a felhasználók férhetnek hozzá a szerkesztési és törlési funkciókhoz, akik rendelkeznek a megfelelő jogosultságokkal (pl. adminisztrátorok vagy tanárok).

## Kvíz létrehozása

### Bevezetés:

> A kvíz létrehozása oldal a kurzus oldaláról, azon belül is egy témából érhető el a `Műveletek`, `Új kvíz` gombra kattintva. 

### Funkciók és használat:

1. **Kvíz neve**

   - Minden kvíz kell név. A név maximum 100 karakter lehet.

2. **Maximum próbálkozások száma**

   - Opcionálisan meg lehet adni, hogy egy felhasználó maximum hányszor tudjon egy kvízt kitölteni.

3. **Kvíz nyitása**

   - Opcionálisan megadható, hogy a kvíz milyen időponttól legyen elérhető.

4. **Kvíz zárása**

   - Opcionálisan megadható, hogy a kvíz milyen időponttól *ne* legyen elérhető.

4. **Időkorlát**

   - Opcionálisan, percben megadható, hogy a kvíz kitöltésére mennyi legyen az időkeret. A kívzt az idő lejárta után az oldal automatikusan elküldi, feltéve, ha az oldal meg van nyitva.

5. **Mentés**

   - A megfelelő adatok bevitele után a `Mentés` gombbal menthetőek az adatok. Ezek után át leszünk irányítva a kvíz szerkesztése oldalra.

## Kvíz szerkesztése

### Bevezetés:

> A kvíz szerkesztésénél megváltoztathazó a kvíz részletei, valamint új feladato lehet hozzáadni. 

### Funkciók és használat:

1. **Kvíz neve**

   - Minden kvíz kell név. A név maximum 100 karakter lehet.

2. **Maximum próbálkozások száma**

   - Opcionálisan meg lehet adni, hogy egy felhasználó maximum hányszor tudjon egy kvízt kitölteni.

3. **Kvíz nyitása**

   - Opcionálisan megadható, hogy a kvíz milyen időponttól legyen elérhető.

4. **Kvíz zárása**

   - Opcionálisan megadható, hogy a kvíz milyen időponttól *ne* legyen elérhető.

4. **Időkorlát**

   - Opcionálisan, percben megadható, hogy a kvíz kitöltésére mennyi legyen az időkeret. A kívzt az idő lejárta után az oldal automatikusan elküldi, feltéve, ha az oldal meg van nyitva.

5. **Mentés**

   - A megfelelő adatok bevitele után a `Mentés` gombbal menthetőek az adatok. Ezek után át leszünk irányítva a kvíz szerkesztése oldalra.

6. **Új feladat hozzáadása**

   - Új feladatot a kék plusz (`+`) gombra kattiva hozzáadható, amikor a feladat hozzáadása oldalra leszünk átirányítva.

7. **Feladatok sorrendje**

   - A feladatok sorrendjét a fel, illetve le nyilakkal lehet változtatni. Az utolsó feladatnak a le nyíl helyett egy felülre nyíl található.

8. **Feladat törlése**

   - Feladatot törölni a mellette található piros kuka gombbal lehet.

### Új feladat

> Minden feladatnak vannak alfeladatai. Ezeket tudja a felhasználó megoldani.

1. **Fejléc**

   - Minden feladatnak kötelező megadni egy rövid fejlécet, ami maximum 100 karakter hosszú lehet.

2. **Szöveg**

   - Minden feladatnak kötelező megadni egy rövid szöveg, ami maximum 255 karakter hosszú lehet.

4. **Alfeladatok**
   
   - Új alfeladatot a kék plusz (`+`) gombra kattiva hozzáadható, amikor az alfeladat a két szövegdoboz alatt fog megjelenni

5. **Alfeladatok sorrendje**

   - Az alfeladatok sorrendjét a fel, illetve le nyilakkal lehet változtatni. Az utolsó alfeladatnak a le nyíl helyett egy felülre nyíl található.

6. **Alfeladat törlése**

   - Alfeladatot törölni a mellette található piros kuka gombbal lehet.

### Új alfeladat / Alfeladat szerkesztése

> Három féle típusú alfeladat van: rövid választ igénylő feladat, több válaszos feladat, valamint esszé feladat.

1. **Alfeladat szövege**

   - Minden alfeldatnak kötelező szöveg. Ezt a szöveget meg lehet formázni, képeket, linkeket beleszúrni.

2. **Alfeladat típusa**

   - Minden feladatnak kötelező egy alfeladat típus. Ez fogja azt meghatározni, hogy milyen típusú választ adhat meg a kitöltő.

3. **Válaszlehetőségek** (több válaszos feladatok)

   - Amennyiben több válaszos feladatról van szó, úgy a válaszlehetőségeket is meg lehet adni. Ezeket Enter lenyomásával lehet elválasztani, majd az `x` gombbal törölni.

4. **Alfeladat pontszáma**

   - Minden alfeladatnak kötelező pontszámának lennie. Ezek lehetnek fél pontosak is, vagy akár lehet, hogy egy feladat nem ér semennyi pontot.

## Kvíz kitöltése

### Bevezetés:

> Kvízt kitölteni a kvíz `Megtekintés` oldalán lehet. Itt a felhasználó láthatja a kvíz részleteit, előző próbálkozásait, a tanár az összes eddigi próbálkozást, valamint itt található a `Kvíz kitöltése` gomb.

### Funkciók és használat:

1. **Kvíz kitöltésének kezdete**

   - A kvíz indítására átkerülünk a próbálkozás oldalára. Amennyiben elnavigálnánk az oldalról, az előző oldalon a `Próbálkozásaim` alatt található `Folytatás` gombbal visszatérhetünk ide. Amennyiben a kvíznek időkorláta van, úgy egy visszaszámláló is elindul az oldal tején, mutatva mennyi időnk van még hátra.

2. **Feladatok kitöltése**

   - A feladatok alfeladtait szöveges mezők, rádiógomobok és hosszú szöveges mezők alkotják, a feladat típusának megfelelően.

3. **Kvíz elküldése**

   - A kvízt elküldeni a zöld `Elküldés` gombbal lehet az oldal alján. Amennyiben időkorlát van beállítva a kvízre, úgy annak lejártával a kvíz automatiksan elküldésre kerül amint be van töltve bármilyen oldal.

4. **Eredmények**

   - A próbálkozás eredményét javítás után a kvíz oldalán lehet megtekinteni.

## Kvíz értékelése

### Bevezetés:

> Kvízt értékelni a kvíz `Megtekintés` oldalán lehet. Itt a tanárok láthatják a kvízhez kapcsolódó összes próbálkozást, valamit az `Értékelés` gombot. Erre nyomva átirányítódunk a kvíz értékelése oldalra.

### Funkciók és használat:

1. **Alfeladat értékelése**

   - Az alfeladatra adott válasz (vagy *Nem válaszolt*) az alfeladat alatt látható. Itt található a feladatra kapható maximum pontszám, illetve adható meg, hogy helyes-e a megoldás, valamint mennyi pontot kapott a kitöltő. A mentés a zöld `Mentés` gombbal lehetséges.

2. **Értékelés**

   - Az értékelés befejezéséhez az oldal alján található `Értékelés` rész a lap alján található. Itt látható az elért pontszám, valamint adható meg szöveges (maximum 10 akrakter hosszú) értékelés. Ennek elmentéséhez a `Mentés` gombra kell kattintani. A `Befejezés` gomb visszavisz a kvíz oldalára.


## Új feladat létrehozása

### Bevezetés:

> Új feladat létrehozása a kurzus oldaláról, azon belül is egy témából érhető el a `Műveletek`, `Új feladat` gombra kattintva. 

### Funkciók és használat:

1. **Feladat címe**

   - Kötelező kitölteni, a maximális hosszúsága 255 karakter lehet. 

2. **Hozzászólás**

   - Nem kötelező kitölteni, a maximális hosszúsága 255 karakter lehet. A diákok számára lehet írni megjegyzést a feladattal kapcsolatban.

3. **Határidő**

   - Kötelező kitölteni, itt lehet beállítani, hogy a diák meddig adhatja le a feladatot.

4. **Feladat/Jegyzet feltöltése diákok számára**

   - Nem kötelező kitölteni, fel tud tölteni egy fájlt amit szeretné hogy a diákok lássanak. 

5. **Mégsem gomb**

   - Ezzel a gombbal lehet elvetni a feladat létrehozását.

6. **Mentés gomb**

   - Ezzel a gombbal lehet elmenteni a feladatot ami annál a témánál fog megjelenni ahol rá lett nyomva a művelet gombra.


## Feladat módosítása

### Bevezetés:

> A feladat módosítása a kurzus oldaláról, a létrehozott feladat kártyán látható ceruza ikonra kattintva érhető el.

### Funkciók és használat:

1. **Feladat címe**

   - Kötelező kitölteni, a maximális hosszúsága 255 karakter lehet. 

2. **Hozzászólás**

   - Nem kötelező kitölteni, a maximális hosszúsága 255 karakter lehet. A diákok számára lehet írni megjegyzést a feladattal kapcsolatban.

3. **Határidő**

   - Kötelező kitölteni, itt lehet módosítani, hogy a diák meddig adhatja le a feladatot.

4. **Feladat/Jegyzet feltöltése diákok számára**

   - Nem kötelező kitölteni, itt lehet hozzáadni vagy módosítani egy fájlt amit szeretné hogy a diákok lássanak. 

5. **Mégsem gomb**

   - Ezzel a gombbal lehet elvetni a feladat módosítását.

6. **Mentés gomb**

   - Ezzel a gombbal lehet elmenteni a módosított feladatot.

## Feladat törlése

> A feladat törlése a kurzus oldaláról, a létrehozott feladat kártyán látható kuka ikonra kattintva érhető el. Miután rányom a gombra egy megerősítő ablakot feldobni.

### Funkciók és használat:

1. **Mégsem**

   - Ezzel a gombbal elveti a törlést és így megmarad a feladat. 

2. **Mentés**

   - Ezzel a gombbal tudja véglegesíteni a törlést és így végleg törli a feladatot.

## Feladat letöltése 

> A feladatot letölteni a kurzus oldaláról, a létrehozott feladat kártyán látható letöltés ikonra kattintva érhető el. Miután rányom a gombra felfog ugrani egy ablak amin megjelennek az ahhoz a témához tartozó összes feladat amit a diákok adtak le.

### Funkciók és használat:

1. **Táblázat**

   - **1. oszlop:** Ebben az oszlopban látható, hogy melyik diák adta le a feladatot. 
   - **2. oszlop:** Ebben az oszlopban látható, a leadott fájl neve.
   - **3. oszlop:** Ebben az oszlopban látható, lehet letölteni a feladatot amit a diák feltöltött.

2. **Összes feladat letöltése**

   - Ezzel a gombbal tudja letölteni az összes diák álltal leadott feladatot ami ahhoz a feladathoz tartozik.

2. **Bezárás**

   - Ezzel a gombbal tudja becsukni a felugró ablakot.

## Feladat leadása

> A feladatot leadni a kurzus oldaláról, a létrehozott feladat kártyán látható `Megtekintés` gombra kattintva lehet.

### Funkciók és használat:

1. **Feladat**

   - Ez a létrehozott feladat címét mutatja.

2. **Kurzus**

   - Ez azt mutatja meg, hogy melyik kurzusba tarozik a feladat.

3. **Határidő**

   - Ez azt mutatja meg, hogy meddig lehet feltölteni a feladatot.

4. **Hozzászólás**

   - Ez csak akkor jelenik meg hogyha a tanár írt a feladathoz megjegyzést.

5. **Feltöltött feladat**

   - Itt látható a tanár álltal feltölött tananyag címe.

 6. **Feladat / jegyzet feltöltése diákok számára.**  

   - A diák itt tudja feltölteni a tanár számára a kész feladatot.

 7. **Feladat letöltése**  

   - A diák le tudja tölteni amit a tanár feltöltött a diákok számára.

8. **Mégsem**  

   - A mégse gombra kattintva vissza tud menni a feladathoz tartozó kurzus oldalára

9. **Mentés**  

   - Feltudja tölteni a feladatot és visszaviszi a kurzus 