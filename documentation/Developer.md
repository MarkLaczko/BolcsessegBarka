# Fejlesztői Dokumentáció

## Tartalomjegyzék

1. [API dokumentáció](#api-dokumentáció)
1. [Komponensek](#komponensek)

## API Dokumentáció

| Method | URI                 | Name              | Controller      | Action          |
|--------|---------------------|-------------------|-----------------|-----------------|
| POST   | /api/users/register | users.register    | UserController  | store           |
| POST   | /api/users/login    | users.login       | AuthController  | authenticate    |
| GET    | /api/groups         | groups.index      | GroupController | index           |
| GET    | /api/groups/{id}    | groups.show       | GroupController | show            |
| POST   | /api/groups         | groups.store      | GroupController | store           |
| PUT    | /api/groups/{id}    | groups.update     | GroupController | update          |
| DELETE | /api/groups/{id}    | groups.destroy    | GroupController | destroy         |
| DELETE | /api/groups         | groups.bulkDelete | GroupController | bulkDelete      |

### `POST /api/users/register`

Regisztrálás a weboldalra.

#### Törzs

- `name`: A felhasználó egyedi neve.
- `email`: A felhasználó email címe.
- `password`: A felhasználó jelszava. Minimum 8, maximum 255 karakter hosszú.
- `password_confirmation`: A felhasználó jelszavának megerősítése. Meg kell egyeznie a `password` paraméter értékével.

#### Válasz

Egy JSON objektumot ad vissza `data` néven a következőkkel:

- `message`: Üzenet a sikerességről.
- `errors`: Ha hiba keletkezett, egy objektumban visszaadja a hibákat.

#### Példa

URI:
```
/api/users/register
```

Törzs:
``` json
{
    "name": "user",
    "email": "user@gmail.com",
    "password": "user1234",
    "password_confirmation": "user1234"
}
```

Válasz:
``` json
{
    "data": {
        "message": "Successful registration"
    }
}
```

#### Hibakódok

Az alábbi hibakódokat adhatja vissza a végpont:

- `422 Unprocessable Content`: Hiba a törzs adataiban.
- `500 Internal Server Error`: Váratlan hiba történt a szerveren.

### `POST /api/users/login`

Bejelentkezés a weboldalra, siker esetén egy Bearer tokent ad vissza.

#### Törzs

- `email`: A felhasználó email címe.
- `password`: A felhasználó jelszava.

#### Válasz

Egy JSON objektumot ad vissza `data` néven a következőkkel:

- `token`: Ha sikeres a bejelentkezés, akkor visszaadja a felhasználó token-jét.
- `message`: Ha sikertelen a bejelentkezés, akkor üzenetet ad erről vissza.
- `errors`: Ha hiba keletkezett, egy objektumban visszaadja a hibákat.

#### Példa

URI:
```
/api/users/login
```

Törzs:
``` json
{
    "name": "user",
    "password": "user1234",
}
```

Válasz:
``` json
{
    "data": {
        "token": "1|SFR8dEMktYkKqStwBn18t8cq4Wq7G63h8P6w2nui6929d493"
    }
}
```

#### Hibakódok

Az alábbi hibakódokat adhatja vissza a végpont:

- `401 Unauthorized`: Sikertelen a belépés.
- `422 Unprocessable Content`: Hiba a törzs adataiban.
- `500 Internal Server Error`: Váratlan hiba történt a szerveren.

### `GET /api/groups`

Az összes csoport lekérése.

#### Válasz

Egy JSON tömböt ad vissza `data` néven, melyben objektumok találhatóak a következőkkel:

- `id`: A csoport azonosítója.
- `name`: A csoport neve.
- `users`: A csoport tagjai.
- `courses`: A csoporthoz tartozó kurzusok.
- `errors`: Ha hiba keletkezett, egy objektumban visszaadja a hibákat.

#### Példa

URI:
```
/api/groups
```

Válasz:
``` json
{
    "data": [
        {
            "id": 1,
            "name": "Csoport 1",
            "users": [
                {
                    "id": 1,
                    "name": "Felhasználó",
                    "email": "user@email.com",
                    "is_admin": 0,
                    "member": {
                        "group_id": 1,
                        "user_id": 1,
                        "permission": "Tanuló"
                    }
                }
            ],
            "courses": [
                {
                    "id": 1,
                    "name": "Történelem",
                    "image": "iVBORa[...]Jggg=="
                }
            ]
        },
        {
            "id": 2,
            "name": "Csoport 2",
            "users": [
                {
                    "id": 2,
                    "name": "Tanár",
                    "email": "teacher@email.com",
                    "is_admin": 1,
                    "member": {
                        "group_id": 2,
                        "user_id": 2,
                        "permission": "Tanár"
                    }
                }
            ],
            "courses": [
                {
                    "id": 2,
                    "name": "Matematika",
                    "image": "ErjK[...]JklER"
                }
            ]
        }
    ]
}
```

#### Hibakódok

Az alábbi hibakódokat adhatja vissza a végpont:

- `401 Unauthorized`: Hiányzik a Bearer token.
- `500 Internal Server Error`: Váratlan hiba történt a szerveren.

### `GET /api/groups/{id}`

Egy csoport lekérése azonosító alapján.

#### Válasz

Egy JSON objektumot ad vissza `data` néven a következőkkel:

- `id`: A csoport azonosítója.
- `name`: A csoport neve.
- `users`: A csoport tagjai.
- `courses`: A csoporthoz tartozó kurzusok.
- `errors`: Ha hiba keletkezett, egy objektumban visszaadja a hibákat.

#### Példa

URI:
```
/api/groups/1
```

Válasz:
``` json
{
    "data": {
        "id": 1,
        "name": "Csoport 1",
        "users": [
            {
                "id": 1,
                "name": "Felhasználó",
                "email": "user@email.com",
                "is_admin": 0,
                "member": {
                    "group_id": 1,
                    "user_id": 1,
                    "permission": "Tanár"
                }
            }
        ],
        "courses": [
            {
                "id": 1,
                "name": "Történelem",
                "image": "iVBORa[...]Jggg=="
            }
        ]
    }
}
```

#### Hibakódok

Az alábbi hibakódokat adhatja vissza a végpont:

- `401 Unauthorized`: Hiányzik a Bearer token.
- `404 Not Found`: Nincs ilyen rekord az adatbázisban.
- `500 Internal Server Error`: Váratlan hiba történt a szerveren.

### `POST /api/groups`

Új csoport létrehozása. Csak az adminisztrátorok hozhatnak létre új csoportot.

#### Törzs

- `name`: A csoport neve.

#### Válasz

Egy JSON objektumot ad vissza `data` néven a következőkkel:

- `id`: A csoport azonosítója.
- `name`: A csoport neve.
- `users`: A csoport tagjai.
- `courses`: A csoporthoz tartozó kurzusok.
- `errors`: Ha hiba keletkezett, egy objektumban visszaadja a hibákat.

#### Példa

URI:
```
/api/groups
```

Törzs:
``` json
{
    "name": "Új csoport",
}
```

Válasz:
``` json
{
    "data": {
        "id": 3,
        "name": "Új csoport",
        "users": [],
        "courses": []
    }
}
```

#### Hibakódok

Az alábbi hibakódokat adhatja vissza a végpont:

- `401 Unauthorized`: Hiányzik a Bearer token.
- `403 Forbidden`: A felhasználónak nincs jogosultsága.
- `422 Unprocessable Content`: Hiba a törzs adataiban.
- `500 Internal Server Error`: Váratlan hiba történt a szerveren.

### `PUT /api/groups/{id}`

Csoport szerkesztése. Csak az adminisztrátorok szerkeszthetnek csoportot.

#### Törzs

- `name`: A csoport neve.
- `selectedUsers`: Tömb az összes felhasználókval, aki a csoportba tartozik. A tömb elemeinek kötelező az `id` mező.

#### Válasz

Egy JSON objektumot ad vissza `data` néven a következőkkel:

- `id`: A csoport azonosítója.
- `name`: A csoport neve.
- `users`: A csoport tagjai, ahol az `id` a felhasználó azonosítóját, a `permission` a felhasználó jogosultsági szintjét ("Tanár vagy "Tanuló") adja meg.
- `courses`: A csoporthoz tartozó kurzusok.
- `errors`: Ha hiba keletkezett, egy objektumban visszaadja a hibákat.

#### Példa

URI:
```
/api/groups/3
```

Törzs:
``` json
{
    "name": "Új csoport név",
    "selectedUsers": [
        {
            "id": 2
            "permission": "Tanár"
        }
    ]
}
```

Válasz:
``` json
{
    "data": {
        "id": 3,
        "name": "Új csoport név",
        "users": [
            {
                "id": 1,
                "name": "Felhasználó",
                "email": "user@email.com",
                "is_admin": 0,
                "member": {
                    "group_id": 1,
                    "user_id": 1,
                    "permission": "Tanár"
                }
            }
        ],
        "courses": []
    }
}
```

#### Hibakódok

Az alábbi hibakódokat adhatja vissza a végpont:

- `401 Unauthorized`: Hiányzik a Bearer token.
- `403 Forbidden`: A felhasználónak nincs jogosultsága.
- `404 Not Found`: Nincs ilyen rekord az adatbázisban.
- `422 Unprocessable Content`: Hiba a törzs adataiban.
- `500 Internal Server Error`: Váratlan hiba történt a szerveren.

### `DELETE /api/groups/{id}`

Csoport törlése. Csak az adminisztrátorok szerkeszthetnek csoportot.

#### Válasz

`204 No Content`, amennyiben sikeres a törlés.

#### Példa

URI:
```
/api/groups/3
```

Válasz:
``` json
```

#### Hibakódok

Az alábbi hibakódokat adhatja vissza a végpont:

- `401 Unauthorized`: Hiányzik a Bearer token.
- `403 Forbidden`: A felhasználónak nincs jogosultsága.
- `404 Not Found`: Nincs ilyen rekord az adatbázisban.
- `500 Internal Server Error`: Váratlan hiba történt a szerveren.

### `DELETE /api/groups/`

Több csoport törlése. Csak az adminisztrátorok szerkeszthetnek csoportot.

#### Törzs

- `bulk`: Tömb az összes csoport azonosítójával, amit törölni kell.

#### Válasz

Egy JSON objektumot ad vissza `data` néven a következőkkel:

- `message`: Üzenet a sikerességről.
- `errors`: Ha hiba keletkezett, egy objektumban visszaadja a hibákat.

#### Példa

URI:
```
/api/groups
```

Törzs:
``` json
{
    "bulk": [
        1,
        2
    ]
}
```

Válasz:
``` json
{
    "data": {
        "id": 3,
        "name": "Új csoport név",
        "users": [
            {
                "id": 1,
                "name": "Felhasználó",
                "email": "user@email.com",
                "is_admin": 0,
                "member": {
                    "group_id": 1,
                    "user_id": 1,
                    "permission": "Tanár"
                }
            }
        ],
        "courses": [
            "message": "Groups deleted successfully"
        ]
    }
}
```

#### Hibakódok

Az alábbi hibakódokat adhatja vissza a végpont:

- `401 Unauthorized`: Hiányzik a Bearer token.
- `403 Forbidden`: A felhasználónak nincs jogosultsága.
- `500 Internal Server Error`: Váratlan hiba történt a szerveren.

## Komponensek

### `BaseDialog`

PrimeVue Dialog felugró ablak.

#### Átvett tulajdonságok:
- `visible`: Logikai tulajdonság, amely meghatározza, mikor látható a felugró ablak.
- `header`: Szöveges tulajdonság, a fejlécben megjelenő szöveg.
- `width`: Szöveges tulajdonság, a felugró ablak szélessége (alapértelmezetten: `50rem`).

#### Példa használatra:

``` html
<BaseDialog :visible="visible" header="Felugró ablak" width="60rem">
    <!-- A felugró ablak tartalma -->
</BaseDialog>
```

[PrimeVue Dialog dokumentáció](https://primevue.org/dialog/)