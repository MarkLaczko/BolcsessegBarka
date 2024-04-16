# Felhasználói Dokumentáció

## Tartalomjegyzék

1. [Felhasználó kezelés](#felhasználó-kezelés)

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
    rekordok jelennek meg (STARTS_WITH), de ez a mellette található gombbal módosítható.
  - A beviteli mezőben megadott szöveggel kezdődő összes felhasználó neve megjelenik a táblázatban.

- **Email szerinti Szűrés**

  - Az email oszlopban lévő szűrő segítségével tartalmazási alapon lehet szűrni. Ez azt jelenti, hogy minden olyan rekord megjelenik, amely tartalmazza a beviteli mezőbe írt szöveget (CONTAINS).
  - A szűrési felétételt a mellette található gombbal lehet módosítani.

### Biztonsági és Validációs Szabályok

- Minden új felhasználó hozzáadása és meglévő felhasználó módosítása során a rendszer ellenőrzi az adatokat a megadott validációs szabályok szerint.
- Jelszavak esetén egyezőségi ellenőrzés történik.
