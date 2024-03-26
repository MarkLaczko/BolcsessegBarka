# Fejlesztői Dokumentáció

## Tartalomjegyzék

1. [API dokumentáció](#api-dokumentáció)

## API Dokumentáció

| Method | URI                 | Name           | Controller     | Action       |
|--------|---------------------|----------------|----------------|--------------|
| POST   | /api/users/register | users.register | UserController | store        |
| POST   | /api/users/login    | users.login    | AuthController | authenticate |

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