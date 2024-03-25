# Vue alap | 2024.3

> Az alap tartalmaz minden olyan csomagot amely vizsgához szükséges.

## Tartalom

- [Tartalom](#content)
- [Node és a Vite kezelése](#vite)
- [Mappaszerkezet](#directory)
- [Tesztelés](#testing)

## Node és a Vite kezelése 

### Telepítés

Első indítás alkalmával:

```bash
npm install
```

Egyéb csomagok telepítése:

```bash
npm install <csomag_neve>
```

Fejlesztői csomagok telepítése:

```bash
npm install --save-dev <csomag_neve>
```

### Fejlesztői szerver indítás

A fejlesztői szervert a következő paranccsal tudod elindítani. Ezt követően a jelzett linken éred el a szervert.

```bash
npm run dev
```

### Közzétenni kívánt verzió előállítása

A következő parancs futtatásával egy olyan mappát állít elő a Vite, amit fel tudsz tölteni egy statikus tárhelyre (Pl. [Vercel](https://vercel.com/), [Netlify](https://www.netlify.com/)), ezzel elérhetővé téve az elkészült oldaladat. Az elkészült fájlokat a `dist` könyvtárban találod.

```bash
npm run build
```

Ahhoz, hogy ellenőrizni tudd, hogy helyesen működik az alkalmazásod, a következő paranccsal egy olyan szervert tudsz elindítani, ami az elkészült fájlokat mutatja meg neked. Ekkor már fejlesztői eszközök nem fognak működni az oldalon.

```bash
npm run preview
```