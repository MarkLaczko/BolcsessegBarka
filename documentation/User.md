# Felhasználói Dokumentáció

## Tartalomjegyzék

1. [Navigációs sáv](#navigációs-sáv)
2. [Felhasználó kezelés](#felhasználó-kezelés) (Adminok részére)
3. [Kurzus kezelés](#kurzus-kezelés) (Adminok részére)
4. [Kurzusaim oldal](#kurzusaim-oldal)
5. [Egy adott kurzus oldala](#egy-adott-kurzus-oldala-diákok-számára) (Diákok számára)
6. [Egy adott kurzus oldala](#egy-adott-kurzus-oldala-tanárok-számára) (Tanárok számára)

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

   - Egyes felhasználók kiválasztása után lehetőség van őket egyszerre törölni.

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

   - A kurzus hozzáadásához kattintson az "Új kurzus" gombra. Ekkor megjelenik egy dialógusablak, ahol megadhatja a kurzus nevét és hozzáadhat egy képet.
   - A kurzus neve kötelező mező, és 5-100 karakter közötti hosszúságúnak kell lennie. Ha a kurzus neve nem felel meg ezeknek a kritériumoknak, a rendszer értesíti a felhasználót a szükséges változtatásokról.
   - A képfeltöltés opcionális, és támogatott formátumok: JPEG, PNG, JPG, BMP.

2. **Kurzus szerkesztése**

   - Egy kurzus szerkesztéséhez keresse meg a kurzust a listában, és kattintson a szerkesztés ikonra. Egy új dialógusablak jelenik meg, amely tartalmazza a jelenlegi kurzusadatokat.
   - Itt módosíthatja a kurzus nevét vagy cserélheti a képet, illetve rendelhet hozzá csoportokat. Az új adatokat a "Mentés" gombra kattintva erősítheti meg.

3. **Kurzusok törlése**

   - Egyes kurzusokat közvetlenül törölhet a kurzus melletti "Törlés" ikonra kattintva.
   - Több kurzus egyidejű törléséhez jelölje ki a kívánt kurzusokat a jelölőnégyzetek segítségével, majd kattintson a "Több kurzus törlése" gombra.

4. **Adatok exportálása**

   - A kurzusok listáját CSV formátumban exportálhatja az "Exportálás" gomb segítségével. Az exportált fájl tartalmazni fogja a kurzusok azonosítóját, nevét és a képüket base64 formátumban.

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

> A kurzus témák kezelése oldal lehetővé teszi a diákok számára, hogy megtekintsék a kurzusokhoz tartozó témákat. Az oldal egy harmonika szerkezetben jeleníti meg a témákat, ahol minden téma egy külön panelen található, tartalmazva a téma részleteit.

### Témák:

- **Téma Információk:** Minden téma panelje tartalmazza a téma nevét és az alatta hozzáadot jegyzetét, feladatokat és a tanár által feltöltött esetleges teszteket.
- **Navigáció a Témák Között:** A téma paneljei kibonthatóak, így a diákok könnyen áttekinthetik a teljes kurzustéma-szerkezetet.

## Egy adott kurzus oldala (Tanárok számára)

### Bevezetés:

> A kurzus témák kezelése oldal lehetővé teszi a tanárok számára, hogy kezeljék a kurzusokhoz kapcsolódó témákat, beleértve új téma hozzáadását, meglévő téma szerkesztését és törlését.

### Témák:

- **Téma Információk:** Minden téma panelje tartalmazza a téma nevét és az alatta hozzáadot jegyzetét, feladatokat és a tanár által feltöltött esetleges teszteket. Ezene kivűl szerkesztési valamint törlési lehetőségeket is nyújt a tanároknak.
- **Navigáció a Témák Között:** A téma paneljei kibonthatóak, így a diákok könnyen áttekinthetik a teljes kurzustéma-szerkezetet.

### Új téma hozzáadása:

- **Új Téma Gomb:** Az "Új téma" gombra kattintva megnyílik egy dialógusablak, ahol megadható a téma neve és sorrendje.
- **Adatbevitel:** A téma neve kötelező és legfeljebb 60 karakter hosszú lehet. A sorrend egy szám, ami meghatározza a téma elhelyezkedését a listában.
- **Mentés:** A "Mentés" gombra kattintva a rendszer hozzáadja az új témát a kurzushoz.

### Téma szerkesztése

- **Szerkesztés Gomb:** A szerkesztés gombra kattintva egy hasonló dialógusablak jelenik meg, mint az új téma hozzáadásakor.
- **Adatok Frissítése:** A téma adatainak frissítése után a "Mentés" gombra kattintva a rendszer frissíti a téma adatait.

### Téma törlése

- **Törlés Gomb:** A törlés gombra kattintva a rendszer eltávolítja a témát a kurzusból. Ez a művelet végleges, és nem állítható helyre.

### Visszajelzések kezelése:

- Sikeres műveletek esetén `zöld` háttérrel jelenik meg egy rövid üzenet, amely tájékoztatja a felhasználót a művelet sikeréről.
- Ha egy művelet nem sikerül, `piros` háttérrel jelenik meg egy hibaüzenet, amely részletezi a probléma okát.

### Design és Felhasználói Interakció:

- **Dinamikus Interakció:** A téma hozzáadása, szerkesztése és törlése dinamikusan történik, anélkül, hogy az oldalt újra kellene tölteni.
- **Hozzáférés-szabályozás:** Csak azok a felhasználók férhetnek hozzá a szerkesztési és törlési funkciókhoz, akik rendelkeznek a megfelelő jogosultságokkal (pl. adminisztrátorok vagy tanárok).
