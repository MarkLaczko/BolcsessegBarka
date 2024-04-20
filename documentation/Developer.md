# Fejlesztői Dokumentáció

## Tartalomjegyzék

1. [API dokumentáció](#api-dokumentáció)
   - [Csoportok, felhasználó regisztrálás, bejelentkezés](#csoportokhoz-és-felhasználó-regisztrálásához-bejelentkezéséhez-szükséges-routeok)
   - [Felhasználó routeok](#felhasználóhoz-kapcsolodó-routeok)
   - [Kurzus routeok](#kurzusokhoz-kapcsolodó-routeok)
   - [Téma routeok](#témához-kapcsolodó-routeok)
1. [Komponensek](#komponensek)
   - [BaseDialog](#basedialog)
   - [BaseSpinner](#basespinner)
   - [BaseLearningMaterialCard](#baselearningmaterialcard)
   - [BaseCourseCard](#basecoursecard)
   - [BaseAssignmentCard](#baseassignmentcard)

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

### `BaseSpinner`

### Áttekintés:

> A `BaseSpinner.vue` egy forgó és pulzáló logót jelenít meg a töltési folyamat során. A komponens központosított elrendezésű, és Bootstrap stílusosztályokat használ a megjelenés vezérlésére.

### Használat:

> A komponens használata egyszerű. Csupán be kell illeszteni a szülőkomponens template részébe, és a `loading` prop-on keresztül szabályozható, hogy mikor jelenjen meg.

#### Példa:

```html
<template>
  <div>
    <BaseSpinner :loading="isLoading" />
  </div>
</template>

<script>
  import BaseSpinner from "@components/BaseSpinner.vue";

  export default {
    components: {
      BaseSpinner,
    },
    data() {
      return {
        isLoading: true,
      };
    },
  };
</script>
```

### Template részletei:

> A template-ben definiált `div` elem a `.d-flex` és `.justify-content-center` class-ok segítségével középre helyezi a spinner-t. A forgó logó egy `<img>` elem, amely a `rotating-pulsating` class segítségével animált.

### Komponens tulajdonságok:

- loading:
  - Típus: `Boolean`
  - Kötelező: Nem
  - Leírás: A prop értéke határozza meg, hogy a spinner látható-e. Ha `true`, akkor a spinner aktív.

### `BasePaginator`

### Áttekintés:

> A BasePaginator oldalszámok dinamikus megjelenítéséért felelős. Ez a komponens lehetővé teszi a felhasználó számára, hogy navigáljon az oldalak között, beleértve az elsőre, az előzőre, a következőre és az utolsóra ugrást.

### Használat:

> A komponens a `totalPages` és `currentPage` prop-okat várja, amelyek meghatározzák az összes oldal számát és az aktuális oldal számát. A komponens automatikusan frissíti a navigációs linkeket az aktuális oldal állapotának megfelelően.

#### Példa a komponens integrálására egy szülőkomponensben:

```html
<template>
  <div>
    <BasePaginator
      :totalPages="100"
      :currentPage="5"
      @page-changed="handlePageChange"
    />
  </div>
</template>

<script>
  import BasePaginator from "@components/BasePaginator.vue";

  export default {
    components: {
      BasePaginator,
    },
    methods: {
      handlePageChange(newPage) {
        console.log("Page changed to:", newPage);
      },
    },
  };
</script>
```

### Template részletei:

> A template egy `ul` elemet tartalmaz `pagination` és `justify-content-center` class-okkal, amelyek az oldalszámokat középre helyezik. A navigációs gombok az első, előző, következő és utolsó oldalra ugrásra szolgálnak, és mind letilthatók, ha a jelenlegi oldal az intervallum határain belül van.

### Komponens tulajdonságok:

- `totalPages`: A lapozható oldalak teljes száma.
- `currentPage`: Az aktuálisan megjelenített oldal száma.

### Számítitott tulajdonságok:

- `visiblePages`: Számított tulajdonság, amely az aktuálisan látható oldalszámok listáját generálja, figyelembe véve az aktuális oldalt és az összes oldal számát.

### Metódusok:

- `goToPage(page)`: Egy metódus, amely megváltoztatja az aktuális oldalt. Ha a megadott oldalszám érvényes, eseményt bocsát ki a szülőkomponens számára, jelezve az oldalváltást.

### Stílusok:

> A komponens saját `style` szekciót tartalmaz, amely biztosítja, hogy a linkek félkövér betűtípussal jelenjenek meg, és interaktív háttérszínnel rendelkezzenek ha az egeret fölé visszük.

### `BaseLearningMaterialCard`

### Áttekintés:

> A `BaseLearningMaterialCard` egy kártya jellegű komponens, amely különböző tananyagokat, kurzusokat és azok megjelenési dátumait egy könnyen értelmezhető formában jeleníti meg.

### Komponens leírás:

> A `BaseLearningMaterialCard` komponens egy reszponzív kártyát hoz létre, amely tartalmaz egy képet, a tananyag címét, a kurzus nevét és a megjelenési dátumot. A komponens a Pinia állapotkezelőt használja a nyelvi üzenetek dinamikus kezelésére.

### Használat:

A komponens az `image`, `course`, `learningMaterial`, és `releaseData` prop-okat várja, amelyek az anyag vizuális és szöveges adatait adják meg.

### Példa a komponens használatára egy szülőkomponensben:

```html
<template>
  <div>
    <BaseLearningMaterialCard
      image="course-image.jpg"
      course="Web Development"
      learningMaterial="Introduction to Vue.js"
      releaseData="2023-04-01"
    />
  </div>
</template>

<script>
  import BaseLearningMaterialCard from "@components/BaseLearningMaterialCard.vue";

  export default {
    components: {
      BaseLearningMaterialCard,
    },
  };
</script>
```

### Template részletei:

A `template`-ben egy Bootstrap `card` szerkezetet használunk, amely három oszlopra van osztva:

- **Képoszlop**: Itt jelenik meg a tananyag képe, amely reszponzív és kitölti a rendelkezésre álló területet.
- **Szöveges oszlop**: Itt található a tananyag és a kurzus címe, valamint a megjelenési dátum.
- **Gomboszlop**: Egy gomb, amely lehetőséget ad a tananyag részletes megtekintésére.

### Komponens tulajdonságok:

- `image`: A tananyag képének elérési útja.
- `course`: A kurzus neve.
- `learningMaterial`: A tananyag címe.
- `releaseData`: A tananyag megjelenési dátuma.

### Számított tulajdonságok:

- **messages**: A nyelvi üzeneteket kezeli a Pinia állapotkezelőn keresztül, így biztosítva, hogy a komponens nyelvi adaptációja dinamikusan történjen.

### Stílusok:

> A komponens stílusai biztosítják, hogy a kártya jól nézzen ki minden eszközön. A `img-fluid`, `rounded-start`, és `object-fit-cover` osztályok gondoskodnak arról, hogy a képek reszponzívak és esztétikusak legyenek.

### `BaseCourseCard`

### Beveztés:

> A `BaseCourseCard` ideális arra, hogy rövid összefoglalót nyújtson egy kurzusról, beleértve annak képét, címét és csoporttagságát. A komponens továbbá egy közvetlen útválasztási linket is biztosít a kurzus részletes megtekintéséhez.

### Használat:

> A komponens a `title`, `image`, `courseId`, és `groupName` propokat fogadja, melyek a kurzus alapvető adatait határozzák meg. Ezek a propok lehetővé teszik a komponens széleskörű alkalmazhatóságát és könnyű integrálását különböző felületeken.

### Példa:

```html
<template>
  <div>
    <BaseCourseCard
      title="Webfejlesztés"
      image="encodedImageString"
      courseId="102"
      groupName="Haladó Tanfolyamok"
    />
  </div>
</template>

<script>
  import BaseCourseCard from "@components/BaseCourseCard.vue";

  export default {
    components: {
      BaseCourseCard,
    },
  };
</script>
```

### Template struktúra:

- **Kártya**: A Bootstrap `card` osztályát használja a vizuális keret biztosításához.
- **Kép:** A kurzushoz tartozó képet Base64 kódolásban jeleníti meg, ami az adatátvitelt egyszerűsíti.
- **Szöveges Tartalom:** A kurzus címe és a csoport neve a kártya testében (`card-body`) jelenik meg, középre igazítva.
- **Navigációs Link:** Egy `RouterLink` komponens, amely lehetővé teszi a felhasználó számára, hogy közvetlenül a kurzus részletes oldalára navigáljon.

### Komponens tulajdonságok:

- **title:** A kurzus címe.
- **image:** A kurzus képének Base64 kódolt stringje.
- **courseId:** A kurzus azonosítója.
- **groupName:** A kurzushoz tartozó csoport neve.

### Számított tulajdonságok:

- **messages**: A nyelvi üzenetek kezelése a Pinia állapotkezelő segítségével történik, amely lehetővé teszi a komponens dinamikus nyelvi adaptációját.

### Stílusok:

> A `scoped` stílusok használata biztosítja, hogy a komponens megjelenése konzisztens maradjon anélkül, hogy befolyásolná a többi komponens stílusát. A képek és szövegek úgy vannak formázva, hogy minden eszközön jól nézzenek ki.

### `BaseAssignmentCard`

### Áttekintés:

> A `BaseAssignmentCard` komponens egy kártya formátumban jeleníti meg a feladatok alapvető információit, mint például a címét, a feladat képét, és a beadási határidőt. Ezen kívül tartalmaz egy gombot, amely a felhasználót az adott feladat oldalára irányítja.

### Használat:

> A komponens a `title`, `image`, és `deadline` prop-okat fogadja. Ezekkel a paraméterekkel a feladatokat könnyedén és hatékonyan jeleníthetjük meg a felhasználók számára. A komponens a Pinia állapotkezelőt használja a nyelvi üzenetek dinamikus kezelésére.

### Példa:

```html
<template>
  <div>
    <BaseAssignmentCard
      title="Programozási Alapok"
      image="programozas_kep.png"
      deadline="2023-12-15"
    />
  </div>
</template>

<script>
  import BaseAssignmentCard from "@components/BaseAssignmentCard.vue";

  export default {
    components: {
      BaseAssignmentCard,
    },
  };
</script>
```

### Template Struktúra:

- **Kártya Fejléc:** Tartalmazza a feladat címét, amely középre van igazítva.
- **Kártya Törzs:** Itt jelenik meg a feladat képe, amely szintén központi helyet kap. Alatta található egy gomb, amely a feladat részleteire mutató oldalra vezet.
- **Kártya Lábléc:** Megjeleníti a feladat beadási határidejét, amely kivan emelve, hogy felhívja a figyelmet a határidő fontosságára.

### Komponens tulajdonságok:

- `title`: A feladat címe.
- `image`: A feladat képének URL-címe.
- `deadline`: A feladat beadási határideje.

### Számított tulajdonságok:

- **messages:** A nyelvi üzenetek kezelése a Pinia állapotkezelő segítségével, amely lehetővé teszi a komponens nyelvi adaptációját.

### Stílusok:

> A kártya stílusai a Bootstrap keretrendszer elemekre épülnek, így biztosítva a reszponzív és esztétikailag vonzó megjelenést minden készüléken. A `card`, `img-fluid`, és `rounded` osztályokat használjuk a vizuális elemek formázására.
