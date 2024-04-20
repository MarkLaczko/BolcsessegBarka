# Fejlesztői Dokumentáció

## Tartalomjegyzék

1. [API dokumentáció](#api-dokumentáció)
   - [Csoportok, felhasználó regisztrálás, bejelentkezés](#csoportokhoz-és-felhasználó-regisztrálásához-bejelentkezéséhez-szükséges-routeok)
   - [Felhasználó routeok](#felhasználóhoz-kapcsolodó-routeok)
   - [Kurzus routeok](#kurzusokhoz-kapcsolodó-routeok)
   - [Téma routeok](#témához-kapcsolodó-routeok)
1. [Komponensek](#komponensek)

## API Dokumentáció

### Csoportokhoz és felhasználó regisztrálásához, bejelentkezéséhez szükséges routeok:

| Method | URI                 | Name              | Controller      | Action       |
| ------ | ------------------- | ----------------- | --------------- | ------------ |
| POST   | /api/users/register | users.register    | UserController  | store        |
| POST   | /api/users/login    | users.login       | AuthController  | authenticate |
| GET    | /api/groups         | groups.index      | GroupController | index        |
| GET    | /api/groups/{id}    | groups.show       | GroupController | show         |
| POST   | /api/groups         | groups.store      | GroupController | store        |
| PUT    | /api/groups/{id}    | groups.update     | GroupController | update       |
| DELETE | /api/groups/{id}    | groups.destroy    | GroupController | destroy      |
| DELETE | /api/groups         | groups.bulkDelete | GroupController | bulkDelete   |

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

```json
{
  "name": "user",
  "email": "user@gmail.com",
  "password": "user1234",
  "password_confirmation": "user1234"
}
```

Válasz:

```json
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

```json
{
  "name": "user",
  "password": "user1234"
}
```

Válasz:

```json
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

```json
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

```json
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

```json
{
  "name": "Új csoport"
}
```

Válasz:

```json
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

```json
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

```json
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

Csoport törlése. Csak az adminisztrátorok törölhetnek csoportot.

#### Válasz

`204 No Content`, amennyiben sikeres a törlés.

#### Példa

URI:

```
/api/groups/3
```

Válasz:

```json

```

#### Hibakódok

Az alábbi hibakódokat adhatja vissza a végpont:

- `401 Unauthorized`: Hiányzik a Bearer token.
- `403 Forbidden`: A felhasználónak nincs jogosultsága.
- `404 Not Found`: Nincs ilyen rekord az adatbázisban.
- `500 Internal Server Error`: Váratlan hiba történt a szerveren.

### `DELETE /api/groups/`

Több csoport törlése. Csak az adminisztrátorok törölhetnek csoportot.

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

```json
{
  "bulk": [1, 2]
}
```

Válasz:

```json
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

### Felhasználóhoz kapcsolodó routeok:

| Method | URI               | Name             | Controller     | Action     |
| ------ | ----------------- | ---------------- | -------------- | ---------- |
| GET    | /api/users        | users.index      | UserController | index      |
| GET    | /api/users/{id}   | users.show       | UserController | show       |
| POST   | /api/users/delete | users.bulkDelete | UserController | bulkDelete |
| PUT    | /api/users/{id}   | users.update     | UserController | update     |
| DELETE | /api/users/{id}   | users.destroy    | UserController | destroy    |

### `GET /api/users`

Az összes felhasználó lekérése.

#### Válasz

Egy JSON tömböt ad vissza `data` néven, melyben objektumok találhatóak a következőkkel:

- `id`: A felhasználó azonosítója.
- `name`: A felhasználó neve.
- `email`: A felhasználó email címe.
- `is_admin`: A felhasználó adminisztrátor-e.
- `groups`: A felhasználóhoz tartozó csoportok.

#### Példa

URI:

```
/api/users
```

```json
{
  "data": [
    {
      "id": 1,
      "name": "admin",
      "email": "admin@admin.com",
      "is_admin": true,
      "groups": [
        {
          "id": 1,
          "name": "9.a",
          "member": {
            "user_id": 1,
            "group_id": 1,
            "permission": "Tanár"
          }
        }
      ]
    },
    {
      "id": 2,
      "name": "user",
      "email": "user@user.com",
      "is_admin": false,
      "groups": []
    }
  ]
}
```

#### Hibakódok

Az alábbi hibakódokat adhatja vissz a végpont:

- `401 Unauthorized`: Hiányzik a Bearer token.
- `403 Forbidden`: A felhasználónak nincs jogosultsága.
- `500 Internal Server Error`: Váratlan hiba történt a szerveren.

### `GET /api/users/{id}`

Egy felhasználó lekérése azonosító alapján.

### Válasz

Egy JSON obketumot ad vissza `data` néven a következőkkel:

- `id`: A felhasználó azonosítója.
- `name`: A felhasználó neve.
- `email`: A felhasználó email címe.
- `is_admin`: A felhasználó adminisztrátor-e.
- `groups`: A felhasználóhoz tartozó csoportok.

#### Példa

URI:

```
/api/users/1
```

```json
{
  "data": {
    "id": 1,
    "name": "admin",
    "email": "admin@admin.com",
    "is_admin": true,
    "groups": [
      {
        "id": 1,
        "name": "9.a",
        "member": {
          "user_id": 1,
          "group_id": 1,
          "permission": "Tanár"
        }
      }
    ]
  }
}
```

#### Hibakódok

Az alábbi hibakódokat adhatja vissza a végpont:

- `401 Unauthorized`: Hiányzik a Bearer token.
- `403 Forbidden`: A felhasználónak nincs jogosultsága.
- `404 Not Found`: Nincs ilyen rekord az adatbázisban.
- `500 Internal Server Error`: Váratlan hiba történt a szerveren.

### `POST /api/users/delete`

Egyszerre több felhasználó törlése. Csak az adminisztrátorok vehetik igénybe.

#### Törzs

- `userIds`: A felhasználók azonosítója.

#### Válasz

Egy JSON objektumot ad vissza:

- `message`: A törlés sikerességének/sikertelenségének üzenete.

#### Példa

URI:

```
/api/users/delete
```

Törzs:

```json
{
  "userIds": [1, 2]
}
```

Válasz:

```json
{
  "message": "Users deleted successfully"
}
```

#### Hibakódok

Az alábbi hibakódokat adhatja vissza a végpont:

- `401 Unauthorized`: Hiányzik a Bearer token.
- `403 Forbidden`: A felhasználónak nincs jogosultsága.
- `422 Unprocessable Content`: Hiba a törzs adataiban.
- `500 Internal Server Error`: Váratlan hiba történt a szerveren.

### `PUT /api/users/{id}`

Felhasználó szerkesztése. Csak az adminisztrátorok szerkeszthetik.

#### Törzs

- `name`: A felhasználó neve.
- `email`: A felhasználó email címe.

#### Válasz

Egy JSON objektumot ad vissza `data` néven a következőkkel:

- `id`: A felhasználó azonosítója.
- `name`: A felhasználó neve.
- `email`: A felhasználó email címe.
- `is_admin`: A felhasználó adminisztrátor-e.
- `groups`: A felhasználóhoz tartozó csoportok.

#### Példa

URI:

```
/api/users/3
```

Törzs:

```json
{
  "name": "felhasznalo",
  "email": "felhasznalo@felhasznalo.com"
}
```

Válasz:

```json
{
  "data": {
    "id": 3,
    "name": "felhasznalo",
    "email": "felhasznalo@felhasznalo.com",
    "is_admin": false,
    "groups": []
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

### `DELETE /api/users/{id}`

Felhasználó törlése. Csak az adminisztrátorok törölhetnek felhasználót.

#### Válasz

`204 No Content`, amennyiben sikeres a törlés.

#### Példa

URI:

```
/api/users/3
```

Válasz:

```

```

#### Hibakódok

Az alábbi hibakódokat adhatja vissza a végpont:

- `401 Unauthorized`: Hiányzik a Bearer token.
- `403 Forbidden`: A felhasználónak nincs jogosultsága.
- `404 Not Found`: Nincs ilyen rekord az adatbázisban.
- `500 Internal Server Error`: Váratlan hiba történt a szerveren.

### Kurzusokhoz kapcsolodó routeok:

| Method | URI                          | Name                        | Controller       | Action              |
| ------ | ---------------------------- | --------------------------- | ---------------- | ------------------- |
| GET    | /api/courses                 | courses.index               | CourseController | index               |
| GET    | /api/courses/{id}            | courses.show                | CourseController | show                |
| POST   | /api/courses                 | courses.store               | CourseController | store               |
| PUT    | /api/courses/{id}            | courses.update              | CourseController | update              |
| DELETE | /api/courses/{id}            | courses.destroy             | CourseController | destroy             |
| POST   | /api/courses/delete          | courses.bulkDelete          | CourseController | bulkDelete          |
| POST   | /api/courses/{course}/groups | courses.assignGroups        | CourseController | assignGroups        |
| POST   | /api/courses/{course}/topics | courses.assignTopics        | CourseController | assignTopics        |
| GET    | /api/courses/{id}/with-users | courses.showCourseWithUsers | CourseController | showCourseWithUsers |

### `GET /api/courses`

Az összes kurzus lekérése.

#### Válasz

Egy JSON tömböt ad vissza `data` néven, melyben objektumok találhatóak a következőkkel:

- `id`: A kurzus azonosítója.
- `name`: A kurzus neve.
- `image`: A kurzus képe `base64` formátumban.

#### Példa

URI:

```
/api/courses
```

Válasz:

```json
{
  "data": [
    {
      "id": 1,
      "name": "Matematika",
      "image": "iVBORw0KGgoAAAANSUhEUgAAABMC..."
    }
  ]
}
```

#### Hibakódok

Az alábbi hibakódokat adhatja vissza a végpont:

- `401 Unauthorized`: Hiányzik a Bearer token.
- `500 Internal Server Error`: Váratlan hiba történt a szerveren.

### `GET /api/courses/{id}`

Egy kurzus lekérése azonosító alapján.

#### Válasz

Egy JSON tömböt ad vissza `data` néven, melyben objektumok találhatóak a következőkkel:

- `id`: A kurzus azonosítója.
- `name`: A kurzus neve.
- `image`: A kurzus képe `base64` formátumban.
- `topics`: Az adott kurzushoz tartozó témákat adja vissza.

#### Példa

URI:

```
/api/courses/1
```

Válasz:

```json
{
  "data": [
    {
      "id": 1,
      "name": "Matematika",
      "image": "iVBORw0KGgoAAAANSUhEUgAAABMC...",
      "topics": [
        {
          "id": 1,
          "name": "AlmaFa",
          "order": 2
        }
      ]
    }
  ]
}
```

#### Hibakódok

Az alábbi hibakódokat adhatja vissza a végpont:

- `401 Unauthorized`: Hiányzik a Bearer token.
- `404 Not Found`: Nincs ilyen rekord az adatbázisban.
- `500 Internal Server Error`: Váratlan hiba történt a szerveren.

### `POST /api/courses`

Új kurzus létrehozása. Csak az adminisztrátorok hozhatnak létre új kurzust.

#### Törzs

- `name`: A kurzus neve.
- `image`: A kurzus képe.

#### Válasz

Egy JSON objektumot ad vissza `data` néven a következőkkel:

- `id`: A kurzus azonosítója.
- `name`: A kurzus neve.
- `image`: A kurzus képe `base64` formátumban.

#### Példa

URI:

```
/api/courses
```

Törzs:

```json
{
  "name": "Fizika",
  "image": "OIINHUIuinoioiIO234dsf..."
}
```

Válasz:

```json
{
  "data": {
    "id": 2,
    "name": "Fizika",
    "image": "OIINHUIuinoioiIO234dsf..."
  }
}
```

#### Hibakódok

Az alábbi hibakódokat adhatja vissza a végpont:

- `401 Unauthorized`: Hiányzik a Bearer token.
- `403 Forbidden`: A felhasználónak nincs jogosultsága.
- `422 Unprocessable Content`: Hiba a törzs adataiban.
- `500 Internal Server Error`: Váratlan hiba történt a szerveren.

### `PUT /api/courses/{id}`

Kurzus szerkesztése. Csak az adminisztrátorok modósíthatják a kurzust.

#### Törzs

- `name`: A kurzus neve.
- `image`: A kurzus képe `base64` formátumban.

#### Válasz

Egy JSON objektumot ad vissza `data` néven a következőkkel:

- `id`: A kurzus azonosítója.
- `name`: A kurzus neve.
- `image`: A kurzus képe `base64` formátumban.

#### Példa

URI:

```
/api/courses/2
```

Törzs:

```json
{
  "name": "Informatika",
  "image": "odsfgfdngfusdfsduf..."
}
```

Válasz:

```json
{
  "data": {
    "id": 2,
    "name": "Informatika",
    "image": "odsfgfdngfusdfsduf..."
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

### `DELETE /api/courses/{id}`

Kurzus törlése. Csak az adminisztrátorok törölhetnek kurzust.

#### Válasz

`204 No Content`, amennyiben sikeres a törlés.

#### Példa

URI:

```
/api/courses/2
```

Válasz:

```json

```

#### Hibakódok

Az alábbi hibakódokat adhatja vissza a végpont:

- `401 Unauthorized`: Hiányzik a Bearer token.
- `403 Forbidden`: A felhasználónak nincs jogosultsága.
- `404 Not Found`: Nincs ilyen rekord az adatbázisban.
- `500 Internal Server Error`: Váratlan hiba történt a szerveren.

### `POST /api/courses/delete`

Egyszerre több kurzus törlése. Csak az adminisztrátorok vehetik igénybe.

#### Törzs

- `courseIds`: A kurzusok azonosítója.

#### Válasz

Egy JSON objektumot ad vissza:

- `message`: A törlés sikerességének/sikertelenségének üzenete.

#### Példa

URI:

```
/api/courses/delete
```

Törzs:

```json
{
  "courseIds": [3, 4]
}
```

Válasz:

```json
{
  "message": "Courses deleted successfully"
}
```

#### Hibakódok

Az alábbi hibakódokat adhatja vissza a végpont:

- `401 Unauthorized`: Hiányzik a Bearer token.
- `403 Forbidden`: A felhasználónak nincs jogosultsága.
- `422 Unprocessable Content`: Hiba a törzs adataiban.
- `500 Internal Server Error`: Váratlan hiba történt a szerveren.

### `POST /api/courses/{course}/groups`

Kurzushoz való csoportok hozzárendelése. Csak az adminisztrátorok vehetik igénybe.

#### Törzs

- `group_ids`: A csoportok azonosítója.

#### Válasz

Egy JSON objektumot ad vissza a következőkkel:

- `message`: A hozzárendelés sikerességének/sikertelenségének üzenete.
- `course`: A kurzus adatai.
- `groups`: A kurzushoz tartozó csoportok adatai.

#### Példa

URI:

```
/api/courses/1/groups
```

Törzs:

```json
{
  "group_ids": [8, 9]
}
```

Válasz:

```json
{
  {
    "message": "Groups successfully assigned to the course.",
    "course": {
        "id": 1,
        "name": "Matematika",
        "image": "iVBORw0KGgoAAAANSU...",
        "groups": [
            {
                "id": 8,
                "name": "11.b",
                "pivot": {
                    "course_id": 1,
                    "group_id": 8
                }
            },
            {
                "id": 9,
                "name": "11.c",
                "pivot": {
                    "course_id": 1,
                    "group_id": 9
                }
            }
        ]
    }
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

### `POST /api/courses/{course}/topics`

Kurzushoz való témák hozzárendelése. Csak az adminisztrátorok és tanárok vehetik igénybe.

#### Törzs

- `topic_ids`: A témák azonosítója.

#### Válasz

Egy JSON objektumot ad vissza `data` néven a következőkkel:

- `id`: A kurzus azonosítója.
- `name`: A kurzus neve.
- `image`: A kurzus képe `base64` formátumban.
- `topics`: A kurzushoz tartozó témákat adja meg.

#### Példa

URI:

```
/api/courses/1/topics
```

Törzs:

```json
{
  "topic_ids": [1]
}
```

Válasz:

```json
{
  "data": {
    "id": 1,
    "name": "Matematika",
    "image": "iVBORw0KGgoAAAANSUh...",
    "topics": [
      {
        "id": 1,
        "name": "AlmaFa",
        "order": 2
      }
    ]
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

### `GET /api/courses/{id}/with-users`

Az adott kurzushoz tartozó csoport és a csoporthoz tartozó felhasználók lekérése.

#### Válasz

Egy JSON tömböt ad vissza `data` néven, melyben objektumok találhatóak a következőkkel:

- `id`: A csoport azonosítója.
- `name`: A csoport neve.
- `users`: A csoport tagjai.

#### Példa

URI:

```
/api/courses/1/with-users
```

Válasz:

```json
{
  "data": [
    {
      "id": 1,
      "name": "9.a",
      "users": [
        {
          "id": 2,
          "name": "user",
          "email": "user@user.com",
          "is_admin": 0,
          "member": {
            "group_id": 1,
            "user_id": 2,
            "permission": "Tanuló"
          }
        }
      ]
    }
  ]
}
```

#### Hibakódok

Az alábbi hibakódokat adhatja vissza a végpont:

- `401 Unauthorized`: Hiányzik a Bearer token.
- `404 Not Found`: Nincs ilyen rekord az adatbázisban.
- `500 Internal Server Error`: Váratlan hiba történt a szerveren.

### Témához kapcsolodó routeok:

| Method | URI              | Name           | Controller      | Action  |
| ------ | ---------------- | -------------- | --------------- | ------- |
| GET    | /api/topics      | topics.index   | TopicController | index   |
| GET    | /api/topics/{id} | topics.show    | TopicController | show    |
| POST   | /api/topics      | topics.store   | TopicController | store   |
| PUT    | /api/topics/{id} | topics.update  | TopicController | update  |
| DELETE | /api/topics/{id} | topics.destroy | TopicController | destroy |

### `GET /api/topics`

Az összes téma lekérése.

#### Válasz

Egy JSON tömböt ad vissza `data` néven, melyben objektumok találhatóak a következőkkel:

- `id`: A téma azonosítója.
- `name`: A téma neve.
- `order`: A téma sorrendjének száma.

#### Példa

URI:

```
/api/topics
```

Válasz:

```json
{
  "data": [
    {
      "id": 1,
      "name": "Függvények",
      "order": 1
    },
    {
      "id": 2,
      "name": "Algebra",
      "order": 2
    }
  ]
}
```

#### Hibakódok

Az alábbi hibakódokat adhatja vissza a végpont:

- `401 Unauthorized`: Hiányzik a Bearer token.
- `500 Internal Server Error`: Váratlan hiba történt a szerveren.

### `GET /api/topics/{id}`

Egy téma lekérése azonosító alapján.

#### Válasz

Egy JSON objektumot ad vissza `data` néven a következőkkel:

- `id`: A téma azonosítója.
- `name`: A téma neve.
- `order`: A téma sorrendjének száma.

#### Példa

URI:

```
/api/topics/1
```

Válasz:

```json
{
  "data": {
    "id": 1,
    "name": "Függvények",
    "order": 1
  }
}
```

#### Hibakódok

Az alábbi hibakódokat adhatja vissza a végpont:

- `401 Unauthorized`: Hiányzik a Bearer token.
- `404 Not Found`: Nincs ilyen rekord az adatbázisban.
- `500 Internal Server Error`: Váratlan hiba történt a szerveren.

### `POST /api/topics`

Új téma létrehozása. Csak az adminisztrátorok és tanárok hozhatnak létre új témát.

#### Törzs

- `name`: A téma neve.
- `order`: A téma sorrendjének száma.

#### Válasz

Egy JSON objektumot ad vissza `data` néven a következőkkel:

- `id`: A téma azonosítója.
- `name`: A téma neve.
- `order`: A téma sorrendjének száma.

#### Példa

URI:

```
/api/topics
```

Törzs:

```json
{
  "name": "Objektumok",
  "order": 5
}
```

Válasz:

```json
{
  "data": {
    "id": 4,
    "name": "Objektumok",
    "order": 5
  }
}
```

#### Hibakódok

Az alábbi hibakódokat adhatja vissza a végpont:

- `401 Unauthorized`: Hiányzik a Bearer token.
- `403 Forbidden`: A felhasználónak nincs jogosultsága.
- `422 Unprocessable Content`: Hiba a törzs adataiban.
- `500 Internal Server Error`: Váratlan hiba történt a szerveren.

### `PUT /api/topics/{id}` // EZ A PUT LESZ!

Téma szerkesztése. Csak az adminisztrátorok és tanárok szerkeszthetnek témát.

#### Törzs

- `name`: A téma neve.
- `order`: A téma sorrendjének száma.

#### Válasz

Egy JSON objektumot ad vissza `data` néven a következőkkel:

- `id`: A téma azonosítója.
- `name`: A téma neve.
- `order`: A téma sorrendjének száma.

#### Példa

URI:

```
/api/topics/1
```

Törzs:

```json
{
  "name": "Összeadás",
  "order": 10
}
```

Válasz:

```json
{
  "data": {
    "id": 1,
    "name": "Összeadás",
    "order": 10
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

### `DELETE /api/topics/{id}`

Téma törlése. Csak az adminisztrátorok és tanárok törölhetnek témát.

#### Válasz

`204 No Content`, amennyiben sikeres a törlés.

#### Példa

URI:

```
/api/topics/1
```

Válasz:

```json

```

#### Hibakódok

Az alábbi hibakódokat adhatja vissza a végpont:

- `401 Unauthorized`: Hiányzik a Bearer token.
- `403 Forbidden`: A felhasználónak nincs jogosultsága.
- `404 Not Found`: Nincs ilyen rekord az adatbázisban.
- `500 Internal Server Error`: Váratlan hiba történt a szerveren.

## Komponensek

### `BaseDialog`

PrimeVue Dialog felugró ablak.

#### Átvett tulajdonságok:

- `visible`: Logikai tulajdonság, amely meghatározza, mikor látható a felugró ablak.
- `header`: Szöveges tulajdonság, a fejlécben megjelenő szöveg.
- `width`: Szöveges tulajdonság, a felugró ablak szélessége (alapértelmezetten: `50rem`).

#### Példa használatra:

```html
<BaseDialog :visible="visible" header="Felugró ablak" width="60rem">
  <!-- A felugró ablak tartalma -->
</BaseDialog>
```

[PrimeVue Dialog dokumentáció](https://primevue.org/dialog/)
