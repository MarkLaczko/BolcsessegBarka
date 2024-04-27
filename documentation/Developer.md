# Fejlesztői Dokumentáció

## Tartalomjegyzék

1. [API dokumentáció](#api-dokumentáció)
   - [Csoportok, felhasználó regisztrálás, bejelentkezés](#csoportokhoz-és-felhasználó-regisztrálásához-bejelentkezéséhez-szükséges-routeok)
   - [Felhasználó routeok](#felhasználóhoz-kapcsolodó-routeok)
   - [Kurzus routeok](#kurzusokhoz-kapcsolodó-routeok)
   - [Téma routeok](#témához-kapcsolodó-routeok)
   - [Kvíz routeok](#kívzekhez-kapcsolodó-routeok)
2. [Komponensek](#komponensek)
   - [BaseDialog](#basedialog)
   - [BaseSpinner](#basespinner)
   - [BaseLearningMaterialCard](#baselearningmaterialcard)
   - [BaseCourseCard](#basecoursecard)
   - [BaseAssignmentCard](#baseassignmentcard)
3. [Layoutok](#layoutok)
   - [BaseNavbar](#basenavbar)
   - [BaseLayout](#baselayout)
4. [Oldalak](#oldalak)
   - [HomePage](#homepage)
   - [CoursesPage](#coursespage)
   - [CoursePage](#coursepage)
   - [CourseManagementPage](#coursemanagementpage)
   - [UserManagementPage](#usermanagementpage)
   - [GroupManagementPage](#groupmanagementpage)
   - [CreateQuizPage](#createquizpage)
   - [EditQuizPage](#qditquizpage)
   - [CreateTaskPage](#createtaskpage)
   - [EditTaskPage](#qdittaskpage)
5. [Nyelvi beállítások](#nyelvi-beállítások)
6. [Tárolók](#tárolók)
   - [LanguageStore](#languagestore)
   - [ThemeStore](#themestore)
   - [TopicStore](#topicstore)
   - [GroupStore](#groupstore)
   - [QuizStore](#quizstore)
7. [Erőforrások](#erőforrások)
   - [Képek](#images-képek)
   - [Stílusok](#styles-stíluslapok)

## API Dokumentáció

### Csoportokhoz és felhasználó regisztrálásához, bejelentkezéséhez szükséges routeok:

| Method | URI                 | Name              | Controller      | Action       |
| ------ | ------------------- | ----------------- | --------------- | ------------ |
| POST   | /api/users/register | users.register    | UserController  | store        |
| POST   | /api/users/login    | users.login       | AuthController  | authenticate |
| PUT    | /api/users/profile  | profile.edit      | UserController  | editProfile  |
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

### `PUT /api/users/profile`

Felhasználó profil (jelszó) szerkesztése.

#### Törzs

- `old_password`: A felhasználó jelenlegi jelszava.
- `password`: A felhasználó új jelszava. Minimum 8, maximum 255 karakter hosszú.
- `password_confirmation`: A felhasználó jelszavának megerősítése. Meg kell egyeznie a `password` paraméter értékével.

#### Válasz

Egy JSON objektumot ad vissza `data` néven a következőkkel:

- `message`: Üzenet a sikerességről.
- `errors`: Ha hiba keletkezett, egy objektumban visszaadja a hibákat.

#### Példa

URI:

```
/api/users/profile
```

Törzs:

```json
{
  "old_password": "password123",
  "password": "newpassword132",
  "password_confirmation": "newpassword123"
}
```

Válasz:

```json
{
  "data": {
    "message": "Profile updated"
  }
}
```

#### Hibakódok

Az alábbi hibakódokat adhatja vissza a végpont:

- `401 Unauthorized`: Hiányzik a Bearer token.
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
- `topics`: Az adott kurzushoz tartozó témákat adja vissza.
  - `assignments`: Az adott témához tartozó beadandók tömbje.
  - `notes`: Az adott témához tartozó jegyzetek tömbje.
  - `quizzes`: Az adott témához tartozó kvízek tömbje.

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
      "image": "iVBORw0KGgoAAAANSUhEUgAAABMC...",
      "topics": [
        {
          "id": 1,
          "name": "AlmaFa",
          "order": 2,
          "assignments": [],
          "notes": [],
          "quizzes": [],
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

### `GET /api/courses/{id}`

Egy kurzus lekérése azonosító alapján.

#### Válasz

Egy JSON tömböt ad vissza `data` néven, melyben objektumok találhatóak a következőkkel:

- `id`: A kurzus azonosítója.
- `name`: A kurzus neve.
- `image`: A kurzus képe `base64` formátumban.
- `topics`: Az adott kurzushoz tartozó témákat adja vissza.
  - `assignments`: Az adott témához tartozó beadandók tömbje.
  - `notes`: Az adott témához tartozó jegyzetek tömbje.
  - `quizzes`: Az adott témához tartozó kvízek tömbje.

#### Példa

URI:

```
/api/courses/1
```

Válasz:

```json
{
  "data": {
    "id": 1,
    "name": "Matematika",
    "image": "iVBORw0KGgoAAAANSUhEUgAAABMC...",
    "topics": [
      {
        "id": 1,
        "name": "AlmaFa",
        "order": 2,
        "assignments": [],
        "notes": [],
        "quizzes": [],
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

### `PUT /api/topics/{id}`

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

### Kvízekhez kapcsolodó routeok:

| Method | URI                         | Name              | Controller        | Action   |
|--------|-----------------------------|-------------------|-------------------|----------|
| GET    | /api/quizzes                | quizzes.index     | QuizController    | index    |
| GET    | /api/quizzes/{id}           | quizzes.show      | QuizController    | show     |
| POST   | /api/quizzes                | quizzes.store     | QuizController    | store    |
| PUT    | /api/quizzes/{id}           | quizzes.update    | QuizController    | update   |
| DELETE | /api/quizzes/{id}           | quizzes.destory   | QuizController    | destroy  |
| GET    | /api/quizzes/{id}/tasks     | tasks.index       | TaskController    | index    |
| GET    | /api/quizzes/{id}/tasks/ids | tasks.taskIds     | TaskController    | taskIds  |
| GET    | /api/tasks/{id}             | tasks.show        | TaskController    | show     |
| GET    | /api/tasks/{id}/solution    | tasks.solution    | TaskController    | solution |
| POST   | /api/tasks                  | tasks.store       | TaskController    | store    |
| PUT    | /api/tasks/{id}             | tasks.update      | TaskController    | update   |
| DELETE | /api/tasks/{id}             | tasks.destory     | TaskController    | destroy  |
| GET    | /api/subtasks/{id}/solution | subtasks.solution | SubtaskController | solution |
| DELETE | /api/subtasks/{id}          | subtasks.destroy  | SubtaskController | destroy  |

### `GET /api/quizzes`

Az összes kvíz lekérése.

#### Válasz

Egy JSON tömböt ad vissza `data` néven, melyben objektumok találhatóak a következőkkel:

- `id`: A kvíz azonosítója.
- `name`: A kvíz neve.
- `max_attempts`: A kívz kitöltésének maximum száma. Ha nincs megadva, akkor `null`.
- `opens`: A kvíz nyitása. Unix időbélyeg. Ha nincs megadva, akkor `null`.
- `closes`: A kvíz zárása. Unix időbélyeg. Ha nincs megadva, akkor `null`.
- `time`: A kvíz kitöltésének időkorlátja. Ha nincs megadva, akkor `null`.

#### Példa

URI:

```
/api/quizzes
```

Válasz:

```json
{
  "data": [
    {
      "id": 1,
      "name": "A kora újkor",
      "max_attempts": 1,
      "opens": null,
      "closes": null,
      "time": null,
      "number_of_tasks": 2
    },
    {
      "id": 2,
      "name": "A hidegháború",
      "max_attempts": 1,
      "opens": 1704063600,
      "closes": 1766357999,
      "time": 60,
      "number_of_tasks": 2
    }
  ]
}
```

#### Hibakódok

Az alábbi hibakódokat adhatja vissza a végpont:

- `401 Unauthorized`: Hiányzik a Bearer token.
- `403 Forbidden`: A felhasználónak nincs jogosultsága.
- `500 Internal Server Error`: Váratlan hiba történt a szerveren.

### `GET /api/quizzes/{id}`

Egy kvíz lekérése azonosító alapján.

#### Válasz

Egy JSON objektumot ad vissza `data` néven, melyben objektumok találhatóak a következőkkel:

- `id`: A kvíz azonosítója.
- `name`: A kvíz neve.
- `max_attempts`: A kívz kitöltésének maximum száma. Ha nincs megadva, akkor `null`.
- `opens`: A kvíz nyitása. Unix időbélyeg. Ha nincs megadva, akkor `null`.
- `closes`: A kvíz zárása. Unix időbélyeg. Ha nincs megadva, akkor `null`.
- `time`: A kvíz kitöltésének időkorlátja. Ha nincs megadva, akkor `null`.
- `topic`: A téma, amibe a kvíz tartozik.
  - `course`: A kurzus, amibe a kvízhez tartozó téma kapcsolódik.

#### Példa

URI:

```
/api/quizzes/1
```

Válasz:

```json
{
  "data": {
    "id": 1,
    "name": "A kora újkor",
    "max_attempts": 1,
    "opens": null,
    "closes": null,
    "time": null,
    "number_of_tasks": 2,
    "topic": {
      "id": 1,
      "name": "Történelmi korszakok",
      "order": 1,
      "course": {
        "id": 1,
        "name": "Történelem",
        "image": "/9j/4[...]",
        "created_by": null
      }
    }
  }
}
```

#### Hibakódok

Az alábbi hibakódokat adhatja vissza a végpont:

- `401 Unauthorized`: Hiányzik a Bearer token.
- `403 Forbidden`: A felhasználónak nincs jogosultsága.
- `404 Not Found`: Nincs ilyen rekord az adatbázisban.
- `500 Internal Server Error`: Váratlan hiba történt a szerveren.

### `POST /api/quizzes`

Új kvíz létrehozása. Csak az adminisztrátorok és tanárok hozhatnak létre új kvízt.

#### Törzs

- `name`: A kvíz neve.
- `topic_id`: A téma azonosítója, amihez a kvíz tartozik.
- `max_attempts`: A kívz kitöltésének maximum száma. (nem kötelező)
- `opens`: A kvíz nyitása. Unix időbélyeg. (nem kötelező)
- `closes`: A kvíz zárása. Unix időbélyeg. (nem kötelező)
- `time`: A kvíz kitöltésének időkorlátja. (nem kötelező)

#### Válasz

Egy JSON objektumot ad vissza `data` néven, melyben objektumok találhatóak a következőkkel:

- `id`: A kvíz azonosítója.
- `name`: A kvíz neve.
- `max_attempts`: A kívz kitöltésének maximum száma. Ha nincs megadva, akkor `null`.
- `opens`: A kvíz nyitása. Unix időbélyeg. Ha nincs megadva, akkor `null`.
- `closes`: A kvíz zárása. Unix időbélyeg. Ha nincs megadva, akkor `null`.
- `time`: A kvíz kitöltésének időkorlátja. Ha nincs megadva, akkor `null`.

#### Példa

URI:

```
/api/quizzes
```

Törzs:

```json
{
  "topic_id": 1,
  "name": "Dolgozat",
  "max_attempts": 1,
  "opens": 1704099600,
  "closes": 1704102300,
  "time": 45
}
```

Válasz:

```json
{
  "data": {
    "id": 2,
    "name": "Dolgozat",
    "max_attempts": 1,
    "opens": 1704099600,
    "closes": 1704102300,
    "time": 45,
    "number_of_tasks": 2
  }
}
```

#### Hibakódok

Az alábbi hibakódokat adhatja vissza a végpont:

- `401 Unauthorized`: Hiányzik a Bearer token.
- `403 Forbidden`: A felhasználónak nincs jogosultsága.
- `422 Unprocessable Content`: Hiba a törzs adataiban.
- `500 Internal Server Error`: Váratlan hiba történt a szerveren.

### `PUT /api/quizzes/{id}`

Kvíz szerkesztése. Csak az adminisztrátorok és tanárok szerkeszthetnek témát.

#### Törzs

- `name`: A kvíz neve.
- `topic_id`: A téma azonosítója, amihez a kvíz tartozik.
- `max_attempts`: A kívz kitöltésének maximum száma. (nem kötelező)
- `opens`: A kvíz nyitása. Unix időbélyeg. (nem kötelező)
- `closes`: A kvíz zárása. Unix időbélyeg. (nem kötelező)
- `time`: A kvíz kitöltésének időkorlátja. (nem kötelező)

#### Válasz

Egy JSON objektumot ad vissza `data` néven a következőkkel:

- `id`: A kvíz azonosítója.
- `name`: A kvíz neve.
- `max_attempts`: A kívz kitöltésének maximum száma. Ha nincs megadva, akkor `null`.
- `opens`: A kvíz nyitása. Unix időbélyeg. Ha nincs megadva, akkor `null`.
- `closes`: A kvíz zárása. Unix időbélyeg. Ha nincs megadva, akkor `null`.
- `time`: A kvíz kitöltésének időkorlátja. Ha nincs megadva, akkor `null`.

#### Példa

URI:

```
/api/quizzes/1
```

Törzs:

```json
{
  "topic_id": 1,
  "name": "Érettségi felkészülés",
  "max_attempts": 1,
  "opens": 1704099600,
  "closes": 1704102300,
  "time": 45
}
```

Válasz:

```json
{
  "data": {
    "id": 2,
    "name": "Érettségi felkészülés",
    "max_attempts": 1,
    "opens": 1735722000,
    "closes": 1735724700,
    "time": 45,
    "number_of_tasks": 2
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

Kvíz törlése. Csak az adminisztrátorok és tanárok törölhetnek témát.

#### Válasz

`204 No Content`, amennyiben sikeres a törlés.

#### Példa

URI:

```
/api/quizzes/1
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

### `GET /quizzes/{id}/tasks`

Egy adott kvízhez tartozó összes feladat lekérése. Csak adminisztrátorok, tanárok és jelenleg a kvízt kitöltő diákok kérhetnek le adatot.

#### Válasz

Egy JSON tömböt ad vissza `data` néven, melyben objektumok találhatóak a következőkkel:

- `id`: A feladat azonosítója.
- `order`: A feladat sorrendje.
- `header`: A feladat bevezető szövege.
- `text`: A feladat szövege.
- `subtasks`: A feladat alfeladatai (tömb).
  - `id`: Az alfeladat azonosítója.
  - `order`: Az alfeladat sorszáma.
  - `question`: Az alfeladat szövege.
  - `options`: Tömb az alfeladat válaszlehetőségeivel. Ha nincs megadva, akkor `null`.
  - `type`: Az alfeladat típusa. (`short_answer`, `multiple_choice`, `essay`)
  - `marks`: Az alfeladatért járó pont.

#### Példa

URI:

```
/api/quizzes/1/tasks
```

Válasz:

```json
{
  "data": [
    {
      "id": 1,
      "order": 0,
      "header": "A feladat a nagy földrajzi felfedezésekkel kapcsolatos",
      "text": "A források és ismeretei alapján válaszoljon a kérdésekre!",
      "subtasks": [
        {
          "id": 1,
          "order": 0,
          "question": "<p><b>a) A tordesillasi szerződés megkötését milyen nagy fontosságú történelmi esemény előzte meg 1492-ben?</b></p>",
          "options": null,
          "type": "short_answer",
          "marks": 0.5
        },
        {
          "id": 2,
          "order": 1,
          "question": "<p><b>Ki volt az a felfedező, aki elsőként jutott el Indiába Afrika megkerülésével? Melyik királyság szolgálatában állt?</b></p>",
          "options": null,
          "type": "short_answer",
          "marks": 1
        }
      ]
    },
    {
      "id": 2,
      "order": 1,
      "header": "A feladat a kora újkori egyházakkal kapcsolatos.",
      "text": "Oldja meg a feladatokat a források és ismeretei alapján!",
      "subtasks": [
        {
          "id": 3,
          "order": 0,
          "question": "<p>A) [...]<br>B) [...]<br>C) [...]</p><br><b>a) Melyik az a két forrás, amely tartalmát tekintve a reformáció elindulásának okát fogalmazza meg? Adja meg a források betűjelét!</b>",
          "options": null,
          "type": "short_answer",
          "marks": 1
        },
        {
          "id": 4,
          "order": 1,
          "question": "<b>b) Milyen államformát tart követendőnek a C) betűjelű forrás? Karikázza be a megfelelőt!</b>",
          "options": "[\"királyság\", \"köztársaság\", \"monarchia\"]",
          "type": "multiple_choice",
          "marks": 0.5
        }
      ]
    }
  ]
}
```

#### Hibakódok

Az alábbi hibakódokat adhatja vissza a végpont:

- `401 Unauthorized`: Hiányzik a Bearer token.
- `403 Forbidden`: A felhasználónak nincs jogosultsága.
- `404 Not Found`: Nincs ilyen rekord (kvíz) az adatbázisban.
- `500 Internal Server Error`: Váratlan hiba történt a szerveren.

### `GET /quizzes/{id}/tasks/ids`

Egy adott kvízhez tartozó összes feladat azonosítójának lekérése. Csak adminisztrátorok, tanárok és jelenleg a kvízt kitöltő diákok kérhetnek le adatot.

#### Válasz

Egy JSON tömböt ad vissza `data` néven, melyben a feladatok azonosítói találhatóak.

#### Példa

URI:

```
/api/quizzes/1/tasks/ids
```

Válasz:

```json
{
  "data": [
    1,
    2
  ]
}
```

#### Hibakódok

Az alábbi hibakódokat adhatja vissza a végpont:

- `401 Unauthorized`: Hiányzik a Bearer token.
- `403 Forbidden`: A felhasználónak nincs jogosultsága.
- `404 Not Found`: Nincs ilyen rekord (kvíz) az adatbázisban.
- `500 Internal Server Error`: Váratlan hiba történt a szerveren.

### `GET /api/tasks/{id}`

Egy feladat lekérése azonosító alapján.

#### Válasz

Egy JSON objektumot ad vissza `data` néven a következőkkel:

- `id`: A feladat azonosítója.
- `order`: A feladat sorrendje.
- `header`: A feladat bevezető szövege.
- `text`: A feladat szövege.
- `subtasks`: A feladat alfeladatai (tömb).
  - `id`: Az alfeladat azonosítója.
  - `order`: Az alfeladat sorszáma.
  - `question`: Az alfeladat szövege.
  - `options`: Tömb az alfeladat válaszlehetőségeivel. Ha nincs megadva, akkor `null`.
  - `type`: Az alfeladat típusa. (`short_answer`, `multiple_choice`, `essay`)
  - `marks`: Az alfeladatért járó pont.

#### Példa

URI:

```
/api/tasks/1
```

Válasz:

```json
{
  "data": {
    "id": 1,
    "order": 0,
    "header": "A feladat a nagy földrajzi felfedezésekkel kapcsolatos",
    "text": "A források és ismeretei alapján válaszoljon a kérdésekre!",
    "subtasks": [
      {
        "id": 1,
        "order": 0,
        "question": "<p><b>a) A tordesillasi szerződés megkötését milyen nagy fontosságú történelmi esemény előzte meg 1492-ben?</b></p>",
        "options": null,
        "type": "short_answer",
        "marks": 0.5
      },
      {
        "id": 2,
        "order": 1,
        "question": "<p><b>Ki volt az a felfedező, aki elsőként jutott el Indiába Afrika megkerülésével? Melyik királyság szolgálatában állt?</b></p>",
        "options": null,
        "type": "short_answer",
        "marks": 1
      },
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

### `GET /api/tasks/{id}/solution`

Egy feladat alfeladatainak megoldásának lekérése azonosító alapján. Csak adminisztrátok és tanárok kérhetik le.

#### Válasz

Egy JSON tömböt ad vissza `data` néven a következőkkel:

- `id`: Az alfeladat azonosítója.
- `solution`: Tömb az alfeladat meogoldásaival. Ha nincs megadva, akkor `null`.

#### Példa

URI:

```
/api/tasks/1/solution
```

Válasz:

```json
{
  "data": [
    {
      "id": 1,
      "solution": [
        "Amerika felfedezése",
        "Kolumbusz Kristóf eljutott Amerikába"
      ]
    },
    {
      "id": 2,
      "solution": [
        "Portugália"
      ]
    },
  ]
}
```

#### Hibakódok

Az alábbi hibakódokat adhatja vissza a végpont:

- `401 Unauthorized`: Hiányzik a Bearer token.
- `403 Forbidden`: A felhasználónak nincs jogosultsága.
- `404 Not Found`: Nincs ilyen rekord az adatbázisban.
- `500 Internal Server Error`: Váratlan hiba történt a szerveren.

### `POST /api/tasks`

Új feladat létrehozása. Csak az adminisztrátorok és tanárok hozhatnak létre új feladatot. A feladatokhoz tartozó alfeladatokat is itt kell megadni.

#### Törzs

- `quiz_id`: A kbíz azonosítója, amihez a feladat tartozik.
- `order`: A feladat sorrendje.
- `header`: A feladat bevezető szövege.
- `text`: A feladat szövege.
- `subtasks`: A feladat alfeladatai (tömb).
  - `order`: Az alfeladat sorszáma.
  - `question`: Az alfeladat szövege.
  - `options`: Tömb az alfeladat opcióival szövegként (csak `multiple_choice` kérdések).
  - `solution`: Tömb az alfeladat megoldásaival szövegként (csak `short_asnwer` és `multiple_choice` kérdések).
  - `type`: Az alfeladat típusa. (`short_answer`, `multiple_choice`, `essay`)
  - `marks`: Az alfeladatért járó pont.

#### Válasz

Egy JSON objektumot ad vissza `data` néven a következőkkel:

- `id`: A feladat azonosítója.
- `order`: A feladat sorrendje.
- `header`: A feladat bevezető szövege.
- `text`: A feladat szövege.
- `subtasks`: A feladat alfeladatai (tömb).
  - `id`: Az alfeladat azonosítója.
  - `order`: Az alfeladat sorszáma.
  - `question`: Az alfeladat szövege.
  - `options`: Tömb az alfeladat válaszlehetőségeivel. Ha nincs megadva, akkor `null`.
  - `type`: Az alfeladat típusa. (`short_answer`, `multiple_choice`, `essay`)
  - `marks`: Az alfeladatért járó pont.

#### Példa

URI:

```
/api/tasks
```

Törzs:

```json
{
  "quiz_id": 1,
  "order": 2,
  "header": "A feladat Az Emberi és Polgári Jogok Nyilatkozatához kapcsolódik",
  "text": "Oldja meg a feladatokat saját ismeretei és a források segítségével!",
  "subtasks": [
    {
      "order": 0,
      "question": "<p><b>a) Milyen elvre utal az alábbi részlet az Emberi és Polgári Jogok Nyilatkozatából?</b><br>„III. Minden szuverenitás elve természeténél fogva a nemzetben lakozik; sem testület, sem egyén nem gyakorolhat hatalmat, ha (az) nem határozottan tőle ered.”</p>",
      "solution": [
        "népfelség",
        "népszuverenitás"
      ],
      "type": "short_answer",
      "marks": 0.5
    },
    {
      "order": 1,
      "question": "<p><b>Határozza meg, hogy melyik ország alapító dokumentumát tartják az Emberi és Polgári Jogok Nyilatkozatának alapjának?</b></p>",
      "solution": [
        "Franciaország"
      ],
      "type": "short_answer",
      "marks": 1
    }
  ]
}
```

Válasz:

```json
{
  "data": {
    "id": 3,
    "order": 2,
    "header": "A feladat Az Emberi és Polgári Jogok Nyilatkozatához kapcsolódik",
    "text": "Oldja meg a feladatokat saját ismeretei és a források segítségével!",
    "subtasks": [
      {
        "id": 23,
        "order": 0,
        "question": "<p><b>a) Milyen elvre utal az alábbi részlet az Emberi és Polgári Jogok Nyilatkozatából?</b><br>„III. Minden szuverenitás elve természeténél fogva a nemzetben lakozik; sem testület, sem egyén nem gyakorolhat hatalmat, ha (az) nem határozottan tőle ered.”</p>",
        "options": null,
        "type": "short_answer",
        "marks": 0.5
      },
      {
        "id": 24,
        "order": 1,
        "question": "<p><b>Határozza meg, hogy melyik ország alapító dokumentumát tartják az Emberi és Polgári Jogok Nyilatkozatának alapjának?</b></p>",
        "options": null,
        "type": "short_answer",
        "marks": 1
      }
    ]
  }
}
```

#### Hibakódok

Az alábbi hibakódokat adhatja vissza a végpont:

- `401 Unauthorized`: Hiányzik a Bearer token.
- `403 Forbidden`: A felhasználónak nincs jogosultsága.
- `422 Unprocessable Content`: Hiba a törzs adataiban.
- `500 Internal Server Error`: Váratlan hiba történt a szerveren.

### `PUT /api/tasks/{id}`

Feladat szerkesztése. Csak az adminisztrátorok és tanárok szerkeszthetnek témát. A feladatokhoz tartozó alfeladatokat is itt kell megadni/szerkeszteni. Ha egy alfeladathoz tartozik `id`, akkor frissíti az adott alfeladatot, ha nincs, akkor új alfeladatot hoz létre.

#### Törzs

- `quiz_id`: A kbíz azonosítója, amihez a feladat tartozik.
- `order`: A feladat sorrendje.
- `header`: A feladat bevezető szövege.
- `text`: A feladat szövege.
- `subtasks`: A feladat alfeladatai (tömb).
  - `id`: Az alfeladat azonosítója. Ha nincs megadva, új alfeladat jön létre.
  - `order`: Az alfeladat sorszáma.
  - `question`: Az alfeladat szövege.
  - `options`: Tömb az alfeladat opcióival szövegként (csak `multiple_choice` kérdések).
  - `solution`: Tömb az alfeladat megoldásaival szövegként (csak `short_asnwer` és `multiple_choice` kérdések).
  - `type`: Az alfeladat típusa. (`short_answer`, `multiple_choice`, `essay`)
  - `marks`: Az alfeladatért járó pont.

#### Válasz

Egy JSON objektumot ad vissza `data` néven a következőkkel:

- `id`: A feladat azonosítója.
- `order`: A feladat sorrendje.
- `header`: A feladat bevezető szövege.
- `text`: A feladat szövege.
- `subtasks`: A feladat alfeladatai (tömb).
  - `id`: Az alfeladat azonosítója.
  - `order`: Az alfeladat sorszáma.
  - `question`: Az alfeladat szövege.
  - `options`: Tömb az alfeladat válaszlehetőségeivel Ha nincs megadva, akkor `null`.
  - `type`: Az alfeladat típusa. (`short_answer`, `multiple_choice`, `essay`)
  - `marks`: Az alfeladatért járó pont.

#### Példa

URI:

```
/api/tasks/1
```

Törzs:

```json
{
  "quiz_id": 1,
  "order": 2,
  "header": "A feladat Az Emberi és Polgári Jogok Nyilatkozatához kapcsolódik.",
  "text": "A források és ismeretei alapján oldja meg a feladatokat!",
  "subtasks": [
    {
      "id": 23,
      "order": 0,
      "question": "<p><b>a) Milyen elvre utal az alábbi részlet az Emberi és Polgári Jogok Nyilatkozatából?</b> <br>„III. Minden szuverenitás elve természeténél fogva a nemzetben lakozik; sem testület, sem egyén nem gyakorolhat hatalmat, ha (az) nem határozottan tőle ered.”</p>",
      "solution": [
        "népfelség",
        "népszuverenitás"
      ],
      "type": "short_answer",
      "marks": 0.5
    },
    {
      "order": 1,
      "question": "<p><b>Határozza meg, hogy melyik ország alapító dokumentumát tartják az Emberi és Polgári Jogok Nyilatkozatának alapjának?</b></p>",
      "solution": [
        "Francia Királyság"
      ],
      "type": "short_answer",
      "marks": 1
    }
  ]
}
```

Válasz:

```json
{
  "data": {
    "id": 3,
    "order": 2,
    "header": "A feladat Az Emberi és Polgári Jogok Nyilatkozatához kapcsolódik",
    "text": "Oldja meg a feladatokat saját ismeretei és a források segítségével!",
    "subtasks": [
      {
        "id": 23,
        "order": 0,
        "question": "<p><b>a) Milyen elvre utal az alábbi részlet az Emberi és Polgári Jogok Nyilatkozatából?</b> <br>„III. Minden szuverenitás elve természeténél fogva a nemzetben lakozik; sem testület, sem egyén nem gyakorolhat hatalmat, ha (az) nem határozottan tőle ered.”</p>",
        "options": null,
        "type": "short_answer",
        "marks": 0.5
      },
      {
        "id": 24,
        "order": 1,
        "question": "<p><b>Határozza meg, hogy melyik ország alapító dokumentumát tartják az Emberi és Polgári Jogok Nyilatkozatának alapjának?</b></p>",
        "options": null,
        "type": "short_answer",
        "marks": 1
      },
      {
        "id": 25,
        "order": 1,
        "question": "<p><b>Határozza meg, hogy melyik ország alapító dokumentumát tartják az Emberi és Polgári Jogok Nyilatkozatának alapjának?</b></p>",
        "options": null,
        "type": "short_answer",
        "marks": 1
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

### `DELETE /api/tasks/{id}`

Feladat törlése. Az összes kapcsolódó alfeladatot is törli. Csak az adminisztrátorok és tanárok törölhetnek témát.

#### Válasz

`204 No Content`, amennyiben sikeres a törlés.

#### Példa

URI:

```
/api/tasks/1
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

### `GET /api/subtasks/{id}/solution`

Egy alfeladat alfeladatainak megoldásának lekérése azonosító alapján. Csak adminisztrátok és tanárok kérhetik le.

#### Válasz

Egy JSON objektumot ad vissza `data` néven a következőkkel:

- `id`: Az alfeladat azonosítója.
- `solution`: Tömb az alfeladat meogoldásaival. Ha nincs megadva, akkor `null`.

#### Példa

URI:

```
/api/subtasks/1/solution
```

Válasz:

```json
{
  "data": {
    "id": 1,
    "solution": [
      "Amerika felfedezése",
      "Kolumbusz Kristóf eljutott Amerikába"
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

### `DELETE /api/subtasks/{id}`

Alfeladat törlése. Csak az adminisztrátorok és tanárok törölhetnek témát.

#### Válasz

`204 No Content`, amennyiben sikeres a törlés.

#### Példa

URI:

```
/api/subtasks/1
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

## Layoutok

### `BaseNavbar`

### Áttekintés:

> A `BaseNavbar` egy reszponzív navigációs sávot valósít meg. A komponens Bootstrap alapú, támogatja a sötét témát, és több funkcióval rendelkezik, mint például dinamikus útvonalak kezelése, nyelvváltás, témaváltás és felhasználói interakciók. Az adminisztratív menüpontok megjelenítése feltételes, és csak adminisztrátori jogosultsággal rendelkező felhasználók számára érhetők el.

### Használat:

> A komponens különböző részei a `RouterLink` segítségével kapcsolódnak a Vue Router által kezelt útvonalakhoz, ami lehetővé teszi a felhasználók számára, hogy könnyen navigáljanak az alkalmazás különböző oldalai között.

#### Példa

```html
<template>
  <div>
    <BaseNavbar />
  </div>
</template>

<script>
  import BaseNavbar from "@components/BaseNavbar.vue";

  export default {
    components: {
      BaseNavbar,
    },
  };
</script>
```

### Template Struktúra:

- **Navigációs Sáv**: A navbar osztályt használja Bootstrap-ből, és egy logót, menügombokat, felhasználói beállításokat, és adminisztratív linkeket tartalmaz.
- **RouterLink**: Dinamikus útvonalakat és a Vue Router funkcióit használja a navigáláshoz.
- **Felhasználói és Nyelvi Beállítások**: `Toggle` gombok a témaváltáshoz és a nyelvváltáshoz, valamint a kijelentkezés opció is megtalálható rajta.

### Script részletei:

- **Pinia Store Integráció**: A `userStore`, `themeStore`, és `languageStore` által biztosított állapotok és műveletek használata.
- **Metódusok**:
  - `logoutUser()`: Kezeli a felhasználó kijelentkezését és átirányítja a bejelentkezési oldalra.
  - `toggleTheme()`: Váltás a sötét és világos téma között.
  - `switchLanguage()`: Nyelvváltást kezeli.

### Számított tulajdonságok:

- **Felhasználói Adatok és Beállítások**: `currentUserData`, `avatarText`, `isDarkMode`, `language`, és `messages` állapotok kezelése és megjelenítése.

### `BaseLayout`

### Bemutatás:

> A `BaseLayout` az alkalmazás általános szerkezetének meghatározására használtuk. A komponens egy fejlécet tartalmaz, amelybe a [`BaseNavbar`](#basenavbar) navigációs sáv komponens van integrálva, és egy fő tartalmi területet (`main`), amely dinamikusan tölthető be más komponensekkel vagy tartalommal az alkalmazás különböző részeiről.

### Használat:

> A `BaseLayout` komponens a `BaseNavbar`-t és a `slot`-ot használja, hogy flexibilis, újrahasználható elrendezést biztosítson az alkalmazás számára. A `slot` lehetővé teszi, hogy különböző tartalmakat helyezzünk be a fő tartalmi területre, attól függően, hogy melyik oldalon vagyunk az alkalmazásban.

#### Példa

```html
<template>
  <BaseLayout>
    <div class="custom-content">Üdvözöljük a mi Vue.js alkalmazásunkban!</div>
  </BaseLayout>
</template>

<script>
  import BaseLayout from "@layouts/BaseLayout.vue";

  export default {
    components: {
      BaseLayout,
    },
  };
</script>
```

### Template Struktúra:

- **Fejléc (`header`)**: Tartalmazza a `BaseNavbar` komponenst, ami az alkalmazás navigációs sávját kezeli.
- **Fő tartalmi terület (`main`)**: Egy `container` osztállyal rendelkező `main` elem, amely a dinamikus tartalmak befogadására szolgál az alkalmazás különböző részeiről.

### Script részletei:

- **Komponensek:**
  - `BaseNavbar`: A navigációs sáv komponens, amelyet a fejlécben használunk.

## Oldalak:

### `HomePage`

> A Főoldal komponens a webalkalmazás kezdőlapját jelenti, amely dinamikusan jeleníti meg a felhasználó számára releváns tartalmakat, mint például feladatokat és tananyagokat. A komponens integrálja az `BaseLayout`, `BaseSpinner`, `BaseAssignmentCard`, `BaseLearningMaterialCard`, és `BasePaginator` komponenseket a felhasználói felület egyszerű és interaktív kialakításához.

### Komponens Struktúra

**Template Részletezése:**

- **BaseLayout**: Ez a komponens biztosítja az oldal alapvető elrendezését.
- **BaseSpinner**: Egy töltő animáció, amely akkor jelenik meg, amikor az oldal adatai még betöltés alatt állnak.
- **H1 & H2 Címek**: Üdvözlő üzenet, amely a felhasználó nevét is megjeleníti, valamint külön szekciókat határoz meg a feladatoknak és tananyagoknak.
- **BaseAssignmentCard & BaseLearningMaterialCard**: Ezek a kártyák az egyes feladatokat és tananyagokat mutatják be részletesen.
- **Paginator**: Lapozó komponens, amely lehetővé teszi a feladatok lapozását.

**Script Részletezése:**

- **Belső Állapotok**: Tartalmazza a feladatok listáját, az aktuális oldalszámot, az animáció irányát, és a betöltés állapotát.
- **Módszerek (Methods) és Számított Tulajdonságok (Computed)**: Az `onPageChanged` metódus kezeli az oldalszám változását, a `paginatedAssignments` kiszámítja az aktuális oldalon megjelenő feladatokat, és a `totalPages` meghatározza az összes oldal számát.
- **Életciklus metódus (Mounted)**: A `mounted` életciklus metódusban a `getUser` metódust hívjuk meg a felhasználói adatok lekérésére, és beállítjuk a `loading` állapotot `false`-ra a betöltés befejezése után.

### Stílusok (Styles)

A stílusok részletesen kezelik az animációkat és a médialekérdezéseket, biztosítva, hogy a felhasználói felület reszponzív és vizuálisan vonzó legyen minden eszközön.

- **Animációk**: Definiált animációk a feladatkártyák számára, amelyek jobbra vagy balra csúsztatással történő átmenetet valósítanak meg.
- **Médialekérdezések**: Különböző képernyőméretekhez igazodó stílusok, amelyek biztosítják a tartalom megfelelő megjelenítését a különböző eszközökön.

### `CoursesPage`

> A `CoursesPage` komponens egy kulcsfontosságú oldal, amely lehetővé teszi a felhasználók számára, hogy megtekinthessék az összes elérhető kurzust, amelyekhez hozzáférhetnek. Ez az oldal dinamikusan jeleníti meg a kurzusokat `BaseCourseCard` komponensek formájában, amelyek a kurzusok alapvető információit tartalmazzák.

### Komponens Struktúra

#### **Template Áttekintés:**

- **BaseLayout:** Ez az elrendezési keret biztosítja az oldal konzisztens kinézetét és struktúráját az alkalmazáson belül.
- **BaseSpinner:** Egy töltésjelző animáció, amely megjelenik, amíg a kurzusok töltődnek.
- **Dinamikus Tartalom Megjelenítése:** A kurzusokat dinamikusan jeleníti meg, amennyiben vannak elérhető kurzusok. Ha nincsenek kurzusok, egy üzenet jelenik meg, amely tájékoztatja a felhasználót erről.

**Dinamikus Kurzuskártyák:** Az egyes kurzusok részleteit [`BaseCourseCard`](#basecoursecard) komponensek mutatják be, amelyek tartalmazzák a kurzus nevét, képét, és egyéb releváns adatokat.

### Script Részletek

#### **Belső Állapotok:**

- **courses:** Egy tömb, amely a felhasználó számára elérhető kurzusokat tárolja.
- **loading:** Egy logikai változó, amely a betöltés állapotát jelzi.

#### **Metódusok:**

- `getCourses()`: Ez a metódus lekéri a kurzusokat az API-tól, és dinamikusan tölti fel a `courses` tömböt a felhasználó által elérhető kurzusokkal.

### Stílusok és Animációk

A komponens CSS szabályai a `BaseLayout`-ból és a `BaseCourseCard`-ból származnak, biztosítva a konzisztens kinézetet és a reszponzív dizájnt.

### Integráció a Pinia Tárolókkal

A `CoursesPage` komponens integrálja a `languageStore`-t a lokalizált üzenetek kezelésére, valamint a `userStore`-t és `groupStore`-t a felhasználói adatok és csoportinformációk kezelésére. Ezek a tárolók biztosítják, hogy a kurzusinformációk mindig naprakészek és relevánsak legyenek a felhasználó számára.

### `CoursePage`

> A `CoursePage` komponens egy dinamikus felületet biztosít, ahol a diákok kurzus-specifikus témákat tekinthetnek meg, a tanárok és adminisztrátorok pedig témákat szerkeszthetnek és kezelhetnek. Ez a komponens több újrafelhasználható komponenst integrál, és kezeli az állapotokat és viselkedéseket, beleértve a témák létrehozását, frissítését és törlését.

### Komponens Struktúra

#### **Template Áttekintés:**

- **BaseLayout:** Ez a fő elrendezési keret a lap számára, biztosítva az alkalmazáson belüli konzisztens stílust és szerkezetet.
- **BaseSpinner:** Betöltési animációt jelenít meg, amíg az adatok be nem töltődnek.
- **BaseDialogok:** Két példányban használják – egy új téma hozzáadásához és egy meglévő téma szerkesztéséhez. Mindkettő `FormKit`-et használ az űrlapkezeléshez és validáláshoz.

**Dinamikus Tartalom Megjelenítése:** Feltételesen jeleníti meg a tartalmat a betöltés állapotától függően, és lehetőséget biztosít témák hozzáadására, szerkesztésére és törlésére, kizárólag a tanárok és az adminisztrátorok számára.

### Script Részletek

#### **Belső Állapotok:**

- A kurzussal kapcsolatos információkat és a témaadatokat helyileg tárolják a komponens adatfunkciójában.
- A párbeszédablakok láthatósági zászlói (`newTopicDialogVisible`, `editTopicDialogVisible`) szabályozzák a témakezelő modális párbeszédablakok megjelenítését.

#### **Metódusok:**

- A témakezelő funkciók (`addTopic`, `editTopic`, `deleteTopic`) csak a tanárok és az adminisztrátorok számára érhetők el, biztosítva az adatok integritását és a jogosultságok szerinti hozzáférést.

#### **Számított Tulajdonságok és Jogosultságok Kezelése:**

- A `userStore` segítségével ellenőrizhető, hogy a jelenlegi felhasználó rendelkezik-e a szükséges adminisztrátori vagy tanári jogosultságokkal.

### Stílusok és Animációk

#### **CSS Animációk:**

- Átmeneti effektusokat definiál a témalistában lévő bejegyzések számára, hogy javítsák a felhasználói élményt, amikor témákat adnak hozzá vagy távolítanak el a listáról.

### Integráció a Pinia Tárolókkal

> A komponens széles körben használja a Pinia tárolókat (`userStore`, `courseStore`, `groupStore`, `topicStore`, `languageStore`) a kurzus és a témák kezeléséhez, az adatok lekéréséhez és a szerveroldali erőforrások frissítéséhez. Ez az architektúra elősegíti az alkalmazás karbantarthatóságát és skálázhatóságát azáltal, hogy elválasztja az üzleti logikát a felhasználói felülettől.

### `CourseManagementPage`

> A `CourseManagementPage` komponens a kurzuskezelési feladatokat támogatja, lehetővé téve a kurzusok létrehozását, módosítását, törlését és exportálását. A komponens felhasználóbarát kezelőfelületet biztosít a kurzusok kezeléséhez, széles körű funkciókat kínálva az oktatási intézmények és tréning szolgáltatók számára. Az adott oldalt csak a megfelelő jogosultsági szinttel lehet elérni.

### Komponens Struktúra

#### **Template Áttekintés:**

- **BaseLayout:** Az oldal alapvető elrendezési keretét biztosítja, amely konzisztens felhasználói felületet nyújt.
- **BaseSpinner:** Egy töltésjelző, amely akkor jelenik meg, amikor az oldal adatokat tölt be.
- **Dialog:** Modális ablakok, amelyeket új kurzus hozzáadásához vagy meglévő kurzus módosításához használnak.
- **Toast:** Üzeneteket jelenít meg a kurzusműveletek eredményéről, mint például sikeres létrehozás vagy törlés.

#### **Dinamikus Funkciók:**

- **DataTable:** Egy táblázat, amely a rendelkezésre álló kurzusok listáját jeleníti meg, lehetőséget nyújtva a kiválasztott kurzusok módosítására vagy törlésére.
- **Toolbar:** Eszköztár, amely gombokat tartalmaz új kurzus létrehozásához, több kurzus törléséhez és exportáláshoz.

### Script Részletek

#### **Belső Állapotok:**

- **courses:** Tömb, amely a rendelkezésre álló kurzusokat tartalmazza.
- **loading:** Egy boolean típusú változó, amely jelzi, hogy az oldal betöltése folyamatban van-e.
- **addCourseDialogVisible, modifyCourseDialogVisible:** Logikai változók, amelyek az új kurzus hozzáadása és meglévő kurzus módosítása dialógusablakok láthatóságát szabályozzák.

#### **Metódusok:**

- **getCourses, postCourses, deleteCourse, updateCourse:** Funkciók, amelyek az adott kurzusokkal kapcsolatos API hívásokat kezelik.
- **handleFileChange:** Kezeli a fájlkiválasztó eseményeket a kurzus képének frissítéséhez.

### Stílusok és Animációk

CSS szabályok és animációk vannak definiálva a komponenshez, hogy javítsák a felhasználói élményt, mint például áttűnések és eltolódások a dialógusablakok és toast üzenetek megjelenítésekor.

### Integráció a Pinia Tárolókkal

A komponens integrálja a `userStore`, `groupStore`, `courseStore`, `languageStore` és `themeStore` tárolókat a felhasználói adatok, kurzusok, csoportok és nyelvi és téma beállítások kezeléséhez.

### Összefoglalás

A `CourseManagementPage` komponens egy komplex, de könnyen kezelhető felületet kínál a kurzusok kezelésére. Az integrált adatkezelési funkciók, a felhasználóbarát interfész és a rugalmas konfiguráció lehetővé teszi, hogy az oktatási intézmények hatékonyan kezeljék kurzusaikat. Ez a komponens elengedhetetlen része lehet minden olyan oktatási platformnak, ahol a kurzusok centralizált kezelése szükséges.

### `UserManagementPage`

> A `UserManagementPage` egy központi kezelőfelület, amely lehetővé teszi a felhasználók kezelését egy oktatási környezetben. A komponens segítségével az adminisztrátorok hozzáadhatnak, módosíthatnak és törölhetnek felhasználókat, valamint beállíthatják azok adminisztratív jogosultságait.

### Komponens Struktúra

#### **Template Áttekintés:**

- **BaseLayout:** Ez az oldal alapvető elrendezési keretét biztosítja.
- **BaseSpinner:** Egy töltésjelző, amely a felhasználói adatok betöltése közben jelenik meg.
- **Dialog:** Dialógusablakok új felhasználó hozzáadásához és meglévő felhasználók módosításához.
- **Toast:** Üzenetek megjelenítése a felhasználói műveletek eredményéről.

#### **Dinamikus Tartalom:**

- **DataTable:** Egy táblázat, amely a felhasználókat listázza, lehetőséget nyújtva a kiválasztott felhasználók módosítására vagy törlésére.
- **Toolbar:** Eszköztár, amely gombokat tartalmaz új felhasználó létrehozásához, több felhasználó törléséhez és exportáláshoz.

### Script Részletek

#### **Belső Állapotok:**

- **users:** Tömb, amely a felhasználókat tartalmazza.
- **loading:** Boolean típusú változó, amely jelzi, hogy az oldal betöltése folyamatban van-e.
- **addUserDialogVisible, modifyUserDialogVisible:** Logikai változók, amelyek az új felhasználó hozzáadása és meglévő felhasználó módosítása dialógusablakok láthatóságát szabályozzák.

#### **Metódusok:**

- **getUsers, postUser, deleteUser, updateUser:** Funkciók, amelyek az adott felhasználókkal kapcsolatos API hívásokat kezelik.

### Stílusok és Animációk

CSS szabályok és animációk vannak definiálva a komponenshez, hogy javítsák a felhasználói élményt, mint például áttűnések és eltolódások a dialógusablakok és toast üzenetek megjelenítésekor.

### Integráció a Pinia Tárolókkal

A komponens integrálja a `userStore`, `themeStore`, és `languageStore` tárolókat a felhasználói adatok, témabeállítások és nyelvi beállítások kezeléséhez.

### Összefoglalás

> A `UserManagementPage` komponens egy esszenciális eszköz minden olyan rendszer számára, ahol a felhasználói adatok kezelése központi jelentőséggel bír. Az integrált adatkezelési funkciók, a felhasználóbarát interfész és a rugalmas konfiguráció lehetővé teszi, hogy a rendszergazdák hatékonyan kezeljék a felhasználói adatbázist. Ez a komponens kritikus szerepet játszik a felhasználói jogosultságok és hozzáférés-vezérlés kezelésében, biztosítva a rendszer biztonságát és hatékonyságát.

### `GroupManagementPage`

> A `GroupManagementPage` egy központi kezelőfelület, amely lehetővé teszi a csoportok kezelését egy oktatási környezetben. Az oldal segítségével az adminisztrátorok hozzáadhatnak, módosíthatnak és törölhetnek csoportokat, valamint beállíthatják az ahhoz a csoporthoz tartozó felhasználókat illetve jogosultságaikat.

### Komponens Struktúra

#### **Template Áttekintés:**

- **BaseLayout:** Ez az oldal alapvető elrendezési keretét biztosítja.
- **BaseSpinner:** Egy töltésjelző, amely a felhasználói adatok betöltése közben jelenik meg.
- **Dialog:** Dialógusablakok új felhasználó hozzáadásához és meglévő felhasználók módosításához.
- **Toast:** Üzenetek megjelenítése a felhasználói műveletek eredményéről.

#### **Dinamikus Tartalom:**

- **DataTable:** Egy táblázat, amely a felhasználókat listázza, lehetőséget nyújtva a kiválasztott felhasználók módosítására vagy törlésére.
- **Toolbar:** Eszköztár, amely gombokat tartalmaz új felhasználó létrehozásához, több felhasználó törléséhez és exportáláshoz.

### Script Részletek

#### **Belső Állapotok:**

- **selectedGroups**: A kiválasztott csoportok listája.
- **users:** Tömb, amely a felhasználókat tartalmazza.
- **loading:** Boolean típusú változó, amely jelzi, hogy az oldal betöltése folyamatban van-e.
- **addGroupDialogVisible, modifyGroupDialogVisible:** Logikai változók, amelyek az új csoport hozzáadása és meglévő csoport módosítása dialógusablakok láthatóságát szabályozzák.
- **currentlyModifyingGroup**: A jelenleg szerkesztett csoport adatait tárolja.
- **token**: A felhasználót azonosító tokenje a `userStore`-ból.
- **groups**: A csoportok listája a `groupStore`-ból.
- **isDarkMode**: A felhasználó által kiválaszott világos/sötét téma a `themeStore`-ból.
- **messages**: A felhasználó által kiválaszott nyelvhez tartozó  a `languageStore`-ból.

#### **Metódusok:**

- **getGroups, getGroup, postGroup, updateGroup, deleteGroup, bulkDeleteGroups**: Függvények, amelyek a csoportokkal kapcsolatos API hívásokat kezelik.
- **selectAllGroups**: Az összes csoport kijelölése.
- **getUsers**: Az összes felhasználó lekérése.
- **deleteMultipleGroups**: Több csoport törlése.
- **addGroup**: Új csoport hozzáadása.
- **sendUpdateGroup**: Csoport frissítése.
- **selectUser**: Felhasználó kijelölése a csoport módosítása oldalon.
- **unSelectUser**: Felhasználó kijelölésének megszűntetése a csoport módosítása oldalon.
- **openModifyWindow**: A csoport módosítása ablak megnyitása.
- **addPermissionFieldToAllGroups**: Hozzáad az összes csoport felhasználójához egy `permission` tulajdonságot.

### Stílusok és Animációk

CSS szabályok és animációk vannak definiálva a komponenshez, hogy javítsák a felhasználói élményt, mint például áttűnések és eltolódások a dialógusablakok és toast üzenetek megjelenítésekor.

### Integráció a Pinia Tárolókkal

A komponens integrálja a `groupStore`, `userStore`, `themeStore`, és `languageStore` tárolókat a felhasználói adatok, témabeállítások és nyelvi beállítások kezeléséhez.

### Összefoglalás

> A `GroupManagementPage` komponens egy szükséges eszköz minden olyan rendszer számára, ahol a csoportok adatainak kezelése központi jelentőséggel bír. Az integrált adatkezelési funkciók, a felhasználóbarát interfész és a rugalmas konfiguráció lehetővé teszi, hogy a rendszergazdák hatékonyan kezeljék a csoportokat.

### `CreateQuizPage`

> A `CreateQuizPage` lehetővé teszi tanárok számára, hogy új kvízeket hozzanak létre.

### Komponens Struktúra

#### **Template Áttekintés:**

- **BaseLayout:** Ez az oldal alapvető elrendezési keretét biztosítja.
- **BaseSpinner:** Egy töltésjelző, amely a felhasználói adatok betöltése közben jelenik meg.
- **Toast:** Üzenetek megjelenítése a felhasználói műveletek eredményéről.

### Script Részletek

#### **Belső Állapotok:**

- **course**: Az a kurzus, ahova a felhasználó a kvízt hozzá kívánja adni.
- **topic:** Az a téma, ahova a felhasználó a kvízt hozzá kívánja adni. Ha az URL-ben lévő téma azonosítója nincs benne a kurzusban, akkor a felahsználó választhat.
- **loading:** Boolean típusú változó, amely jelzi, hogy az oldal betöltése folyamatban van-e.
- **form**: Az űrlap tartalma.

#### **Metódusok:**

- **getCourse**: A kurzus adatainak lekérése.
- **submitForm**: Az űralp adatainak elküldése.

### `EditQuizPage`

> Az `EditQuizPage` lehetővé teszi tanárok számára, hogy meglévő kvízeket szerkeszenek.

### Komponens Struktúra

#### **Template Áttekintés:**

- **BaseLayout:** Ez az oldal alapvető elrendezési keretét biztosítja.
- **BaseSpinner:** Egy töltésjelző, amely a felhasználói adatok betöltése közben jelenik meg.
- **Toast:** Üzenetek megjelenítése a felhasználói műveletek eredményéről.
- **BaseConfirmDialog:** 

### Script Részletek

#### **Belső Állapotok:**

- **quiz**: A kvíz adatai.
- **tasks:** A kívzhez tartozó feladatok.
- **loading:** Boolean típusú változó, amely jelzi, hogy az oldal betöltése folyamatban van-e.

#### **Metódusok:**

- **getQuiz**: A kvíz adatainak lekérése.
- **putForm**: Az űralp adatainak elküldése.
- **moveItem**: A feladatok sorrdenjének változtatása.
- **confirmDeleteTask**: Feladat törlése.

### Stílusok és Animációk

CSS szabályok és animációk vannak definiálva a komponenshez, hogy javítsák a felhasználói élményt, mint például áttűnések és eltolódások a dialógusablakok és toast üzenetek megjelenítésekor.

### Integráció a Pinia Tárolókkal

A komponens integrálja a `userStore`, `quizStore`, `themeStore`, és `languageStore` tárolókat a felhasználói adatok, témabeállítások és nyelvi beállítások kezeléséhez.

### `CreateTaskPage`

> A `CreateTaskPage` lehetővé teszi tanárok számára, hogy kvízekhez új feladatokat adjanak hozzá.

### Komponens Struktúra

#### **Template Áttekintés:**

- **BaseLayout:** Ez az oldal alapvető elrendezési keretét biztosítja.
- **BaseSpinner:** Egy töltésjelző, amely a felhasználói adatok betöltése közben jelenik meg.
- **Toast:** Üzenetek megjelenítése a felhasználói műveletek eredményéről.
- **BaseConfirmDialog:** Megerősítő felugró ablak.

### Script Részletek

#### **Belső Állapotok:**

- **quiz**: A kvíz adatai.
- **form:** Az űrlap tartalma feladatokkal.
- **loading:** Boolean típusú változó, amely jelzi, hogy az oldal betöltése folyamatban van-e.

#### **Metódusok:**

- **getQuiz**: A kvíz adatainak lekérése.
- **moveItem**: Az alfeladatok sorrdenjének változtatása.
- **addEmptySubtask**: Új üres alfeladat hozzáadása.
- **postTask**: Feladat mentése.
- **confirmDeleteSubtask**: Alfeladat törlése.

### Stílusok és Animációk

CSS szabályok és animációk vannak definiálva a komponenshez, hogy javítsák a felhasználói élményt, mint például áttűnések és eltolódások a dialógusablakok és toast üzenetek megjelenítésekor.

### Integráció a Pinia Tárolókkal

A komponens integrálja a `userStore`, `quizStore`, `themeStore`, és `languageStore` tárolókat a felhasználói adatok, témabeállítások és nyelvi beállítások kezeléséhez.

### `EditTaskPage`

> Az `EditTaskPage` lehetővé teszi tanárok számára, hogy kvízekhez már meglébő feladatokat szerkesszenek.

### Komponens Struktúra

#### **Template Áttekintés:**

- **BaseLayout:** Ez az oldal alapvető elrendezési keretét biztosítja.
- **BaseSpinner:** Egy töltésjelző, amely a felhasználói adatok betöltése közben jelenik meg.
- **Toast:** Üzenetek megjelenítése a felhasználói műveletek eredményéről.
- **BaseConfirmDialog:** Megerősítő felugró ablak.

### Script Részletek

#### **Belső Állapotok:**

- **quiz**: A kvíz adatai.
- **form:** Az űrlap tartalma feladatokkal.
- **loading:** Boolean típusú változó, amely jelzi, hogy az oldal betöltése folyamatban van-e.

#### **Metódusok:**

- **getQuiz**: A kvíz adatainak lekérése.
- **moveItem**: Az alfeladatok sorrdenjének változtatása.
- **addEmptySubtask**: Új üres alfeladat hozzáadása.
- **putTask**: Feladat mentése.
- **confirmDeleteSubtask**: Alfeladat törlése.
- **navigateToEditQuizPage**: Visszanavigálás a kvíz szerkesztése oldalra.

### Stílusok és Animációk

CSS szabályok és animációk vannak definiálva a komponenshez, hogy javítsák a felhasználói élményt, mint például áttűnések és eltolódások a dialógusablakok és toast üzenetek megjelenítésekor.

### Integráció a Pinia Tárolókkal

A komponens integrálja a `userStore`, `quizStore`, `themeStore`, és `languageStore` tárolókat a felhasználói adatok, témabeállítások és nyelvi beállítások kezeléséhez.

## Nyelvi beállítások:

> A `messages` tömb a projektünkben a többnyelvűség égköve, amely a különböző nyelvű szövegeket tárolja, és lehetővé teszi a nyelvi adaptáció dinamikus kezelését. A szövegek két külön fájlban, `hu.mjs` és `en.mjs` nevű modulokban vannak eltárolva, amelyek a magyar és angol nyelvű tartalmakat tartalmazzák. Ezeket a modulokat a `LanguageStore` kezeli, ami biztosítja, hogy a megfelelő nyelvi tartalom az aktuális felhasználói beállításoknak megfelelően jelenjen meg.

### Integráció a Projektbe:

Az alábbiakban bemutatjuk, hogyan integrálható a `LanguageStore` a Vue.js alkalmazásba, és hogyan használható a `messages` tömb a komponensekben:

#### 1. **Nyelvi Modulok Importálása és Kezelése**

Először is be kell importálni a `languageStore`-t és a Pinia által használt `mapState` nevű funkciót a `script` résznél, majd a számított tulajdonságokhoz fel kell venni az alábbi módon:

```html
<script>
  import { mapState } from "pinia";
  import { languageStore } from "@stores/LanguageStore.mjs";
  export default {
    computed: {
      ...mapState(languageStore, ["messages"]),
    },
  };
</script>
```

#### 2. **`messages` Tömb Használata komponensekben**

A komponensek `template` részénél egyszerűen hivatkozhatunk a `LanguageStore` által biztosított `messages` tömbre a szövegek megjelenítéséhez:

```html
<template>
  <div class="example">{{ messages.pages.examplePage.title }}</div>
</template>
```

### Felhasználási Javaslatok

- **Modularitás**: Tartsuk a nyelvi fájlokat jól strukturáltan és könnyen karbantarthatónak, külön modulokban a nyelvek szerint.
- **Karbantartás**: Rendszeresen ellenőrizzük és frissítsük a nyelvi fájlokat, hogy azok naprakészek legyenek az alkalmazás változásaival.
- **Tesztelés**: Győződjünk meg róla, hogy minden nyelvi változat helyesen működik, és a szövegek helyesek minden támogatott nyelven.

> Ez a megközelítés biztosítja az alkalmazás skálázhatóságát és könnyű kezelhetőségét több nyelven, és lehetővé teszi a felhasználói élmény javítását a nyelvi preferenciák dinamikus támogatásával.

## Tárolók

### `LanguageStore`

### Bevezetés:

> A `LanguageStore` egy központi állapotkezelő tároló a Pinia keretrendszer használatával, amely kezeli a nyelvi beállításokat és az alkalmazásszintű üzeneteket (messages). Ez a tároló lehetővé teszi a dinamikus nyelvváltást, és támogatja az állapot tartósítását, hogy a felhasználói preferenciák átmenetek és újraindítások után is megmaradjanak.

### **Tároló felépítése:**

### Állapot (state)

A tároló állapota a következőket tartalmazza:

- `language`: A jelenleg aktív nyelv kódja, alapértelmezés szerint "HU" (magyar).
- `messages`: Az aktív nyelvhez tartozó üzeneteket tartalmazza, kezdetben a magyar nyelvi fájl (`hu`) adataival töltődik be.

#### Példa

```js
state() {
  return {
    language: "HU",
    messages: hu,
  };
}
```

### Műveletek (actions)

A tároló műveletei a következő funkciót tartalmazzák:

- `switchLanguage()`: Ez a metódus vált a két nyelv (magyar és angol) között. A nyelvváltáskor az állapotban tárolt `language` és `messages` is frissül, így a felületen azonnal megjelennek a megfelelő nyelvű szövegek.

```js
actions: {
  switchLanguage() {
    this.language = this.language === "HU" ? "EN" : "HU";
    this.messages = this.language === "HU" ? hu : en;
  },
}
```

### Állapot Tartósítása (Persistence)

> A `persist` tulajdonság beállításával a tároló állapota automatikusan megőrződik a böngésző lokális tárolójában vagy egy másik, konfigurálható tárolási helyen. Ez biztosítja, hogy a felhasználó által beállított nyelvi preferenciák megmaradjanak az oldal újratöltései és a böngésző újraindításai után is.

#### Hazsnálata:

```js
persist: true;
```

### Integráció és Használat

> A `LanguageStore` tároló integrálása és használata egyszerűen megvalósítható. Az alábbiakban egy példát mutatunk be, hogy hogyan használható a tároló a komponensekben a nyelvváltás kezelésére:

```html
<template>
  <h1>{{ messages.welcome }}</h1>
  <button @click="switchLanguage">Nyelv váltás</button>
</template>

<script>
  import { mapActions, mapState } from "pinia";
  import { languageStore } from "@stores/LanguageStore.mjs";

  export default {
    methods: {
      ...mapActions(languageStore, ["switchLanguage"]),
    },
    computed: {
      ...mapState(languageStore, ["language", "messages"]),
    },
  };
</script>
```

### `ThemeStore`

> A `ThemeStore` egy állapotkezelő tároló a Pinia keretrendszeren belül, amely a témaváltás kezeléséért felelős az alábbi alkalmazásban. Ez a tároló lehetővé teszi a felhasználó számára, hogy váltson a sötét és világos mód között, valamint az állapotot tartósan megőrzi a böngészőben.

### **Tároló felépítése:**

### Állapot (State)

A tároló állapota a következőt tartalmazza:

- `isDarkMode`: Egy logikai érték, amely jelzi, hogy az alkalmazás jelenleg sötét módban van-e. Kezdeti értéke false, ami azt jelenti, hogy alapértelmezés szerint a világos mód aktív.

#### Példa

```js
state() {
  return {
    isDarkMode: false,
  };
}
```

### Műveletek (Actions)

> A tároló műveletei között szerepel a `toggleTheme()` metódus, ami vált a sötét és világos mód között. Ennek hatására a DOM dokumentum gyökérelemére (`document.documentElement`) kerül egy attribútum, amely a témát szabályozza:

```js
actions: {
  toggleTheme() {
    this.isDarkMode = !this.isDarkMode;

    if (this.isDarkMode) {
      document.documentElement.setAttribute("data-theme", "dark");
    } else {
      document.documentElement.removeAttribute("data-theme");
    }
  },
}
```

### Állapot Tartósítása (Persistence)

> A `persist` tulajdonság beállításával a tároló állapota automatikusan megőrződik a böngésző lokális tárolójában vagy egy másik, konfigurálható tárolási helyen. Ez biztosítja, hogy a felhasználó által beállított témaválasztás megmaradjon az oldal újratöltései és a böngésző újraindításai után is.

#### Hazsnálata:

```js
persist: true;
```

### Integráció és Használat

> A `ThemeStore` integrálása és használata egyszerűen megvalósítható. Itt van egy példa arra, hogyan lehet a `ThemeStore`-t integrálni és használni a komponensekben:

```html
<template>
  <button class="example" @click="toggleTheme">Téma váltás</button>
  <p>Jelenlegi téma: {{ isDarkMode ? 'Sötét' : 'Világos' }}</p>
</template>

<script>
  import { mapActions, mapState } from "pinia";
  import { themeStore } from "@stores/ThemeStore.mjs";
  export default {
    methods: {
      toggleTheme() {
        themeStore().toggleTheme();
      },
    },
    computed: {
      ...mapState(themeStore, ["isDarkMode"]),
    },
  };
</script>
```

### `TopicStore`

> A `TopicStore` egy Pinia állapotkezelő tároló, amely a témák (topics) kezelését végzi. Ez a tároló felelős a témaadatok lekérdezéséért, frissítéséért és törléséért, valamint az új témák létrehozásáért.

### Tároló Funkciói

#### **Műveletek (Actions):**

- **getTopics():** Lekéri az összes téma listáját a szerverről.
- **getTopic(id):** Lekéri egy specifikus téma részletes adatait azonosító alapján.
- **postTopic(data):** Új téma létrehozására szolgál, ahol a data tartalmazza a téma adatait.
- **putTopic(id, data):** Meglévő téma adatainak módosítását végzi az adott azonosító alapján.
- **destroyTopic(id):** Törli a megadott azonosítójú témát.

### Hitelesítés Kezelése

Minden API kérés során az aktuális felhasználó hitelesítési tokenjét csatoljuk a kérés fejlécéhez, ami biztosítja az adatok védelmét és az engedélyezett hozzáférést.

### Állapot Tartósítása

A tároló állapotát nem tartósítjuk, mivel a témák dinamikus adatok, amelyek gyakran frissülnek. A frissesség biztosítása érdekében minden oldalbetöltéskor újra lekérjük őket.

### Használati Példa

A tároló használata egy Vue komponensben:

```js
import { mapActions } from "pinia";
import { topicStore } from "@stores/TopicStore";

export default {
  methods: {
    ...mapActions(topicStore, ["getTopics"]),
  },
  async mounted() {
    const topics = await this.getTopics();
    console.log(topics);
  },
};
```

Ez a példa bemutatja, hogyan lehet lekérni és kiíratni a konzolra az összes témát a `TopicStore` segítségével, amikor a komponens betöltődik.

### `GroupStore`

> A `GroupStore` egy Pinia állapotkezelő tároló, amely a csoportok (groups) kezelését végzi. Ez a tároló felelős a csoportok adtaiainak lekérdezéséért, frissítéséért és törléséért, valamint az új csoportok létrehozásáért.

### Tároló Funkciói

#### **Állapotok (States):**

- **groups** Az összes csoport adatai.

#### **Műveletek (Actions):**

- **getGroups():** Lekéri az összes csoport listáját a szerverről.
- **getGroup(id):** Lekéri egy specifikus csoport részletes adatait azonosító alapján.
- **postGroup(data):** Új csoport létrehozására szolgál, ahol a data tartalmazza a csoport adatait.
- **putGroup(id, data):** Meglévő csoport adatainak módosítását végzi az adott azonosító alapján.
- **destroyGroup(id):** Törli a megadott azonosítójú csoportot.

### Hitelesítés Kezelése

Minden API kérés során az aktuális felhasználó hitelesítési tokenjét csatoljuk a kérés fejlécéhez, ami biztosítja az adatok védelmét és az engedélyezett hozzáférést.

### Állapot Tartósítása

A tároló állapotát nem tartósítjuk, mivel a témák dinamikus adatok, amelyek gyakran frissülnek. A frissesség biztosítása érdekében minden oldalbetöltéskor újra lekérjük őket.

### Használati Példa

A tároló használata egy Vue komponensben:

```js
import { mapState, mapActions } from "pinia";
import { groupStore } from "@stores/GroupStore";

export default {
  computed: {
    ...mapState(groupStore, ['groups'])
  },
  methods: {
    ...mapActions(groupStore, ["getGroups"]),
  },
  async mounted() {
    await this.getGroups();
    console.log(this.groups);
  },
};
```

### `QuizStore`

> A `QuizStore` egy Pinia állapotkezelő tároló, amely a kvízek kezelését végzi. Ez a tároló felelős a kvízek adtaiainak lekérdezéséért, frissítéséért és törléséért, valamint az új kvízek létrehozásáért.

### Tároló Funkciói

#### **Műveletek (Actions):**

- **getQuiz(id):** Lekéri egy specifikus kvíz részletes adatait azonosító alapján.
- **postQuiz(data):** Új kvíz létrehozására szolgál, ahol a data tartalmazza a kvíz adatait.
- **putQuiz(id, data):** Meglévő kvíz adatainak módosítását végzi az adott azonosító alapján.
- **destroyQuiz(id):** Törli a megadott azonosítójú kvízt.

### Hitelesítés Kezelése

Minden API kérés során az aktuális felhasználó hitelesítési tokenjét csatoljuk a kérés fejlécéhez, ami biztosítja az adatok védelmét és az engedélyezett hozzáférést.

### Állapot Tartósítása

A tároló állapotát nem tartósítjuk, mivel a témák dinamikus adatok, amelyek gyakran frissülnek. A frissesség biztosítása érdekében minden oldalbetöltéskor újra lekérjük őket.

### Használati Példa

A tároló használata egy Vue komponensben:

```js
import { mapActions } from "pinia";
import { quizStore } from "@stores/QuizStore";

export default {
  methods: {
    ...mapActions(quizStore, ["getQuiz"]),
  },
  async mounted() {
    const quiz = await this.getQuiz(1);
    console.log(quiz);
  },
};
```

## Erőforrások

### `Images` (képek)

> Ezek a képek esztétikai célokat szolgálnak, és javítják az oldal vizuális megjelenését.

### `Styles` (Stíluslapok)

> A projektben két fontos stíluslap fájl, az `_Variables.scss` és az `app.scss` található, amelyek központi szerepet játszanak az alkalmazás vizuális megjelenésének kezelésében. Ezek a fájlok az SCSS előfeldolgozó nyelvet használják, amely lehetővé teszi változók, függvények és más újrafelhasználható kódok definiálását a CSS hatékonyságának növelése érdekében.

#### \_Variables.scss

> Ez a fájl tartalmazza az összes globálisan használt színváltozót és egyéb stílusdefiníciókat, amelyek segítenek az alkalmazás konzisztens kinézetének fenntartásában. A változók nevei intuitívek, és jól leírják a tárolt szín vagy stílus jellegét. Példák a fájlban definiált változókra:

```scss
$purple: #8f00ff;
$red: #ff2500;
$orange: #ff8700;
$gray-100: #f8f9fa;
$gray-900: #212529;
...
$white: #ffffff;
$black: #000000;
```

Ezek a változók más stíluslapokban is felhasználhatók a konzisztencia és az egyszerű karbantartás érdekében.

#### App.scss

> Az `App.scss` a fő stíluslap, amely az `_Variables.scss`-ből importált változókat használja fel, továbbá beilleszti a Bootstrap és a PrimeIcons CSS fájlokat, amelyek az alkalmazás alapvető stílusát és ikonjait szolgáltatják. Ezen kívül tartalmaz specifikus stílusdefiníciókat az alkalmazás egyedi elemeihez, mint például:

- Alapértelmezett stílusok, mint a teljes magasságú (`height: 100vh`) és flexbox beállítások.
- Animációk, mint a forgó-pulzáló effekt (`rotating-pulsating`).
- Médialekérdezések a görgetősáv stílusának beállításához nagyobb képernyőméretek esetén.
- Sötét mód stílusok, amelyek a `:root` szabályon keresztül a teljes alkalmazásra kiterjedően érvényesülnek.

A fájlban definiált CSS szabályok széles skálája lehetővé teszi a felhasználói felület finomhangolását és az alkalmazás vizuális megjelenésének teljes körű testreszabását.
