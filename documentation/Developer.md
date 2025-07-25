# Fejlesztői Dokumentáció

## Tartalomjegyzék

1. [Indítás](#indítás)
   - [Beépített felhasználók](#beépített-felhasználók)
   - [Adatbázis](#adatbázis)
   - [Tesztelési jegyzőkönyv](#tesztelési-jegyzőkönyv)
2. [API dokumentáció](#api-dokumentáció)
   - [Csoportok, felhasználó regisztrálás, bejelentkezés](#csoportokhoz-és-felhasználó-regisztrálásához-bejelentkezéséhez-szükséges-routeok)
   - [Felhasználó routeok](#felhasználóhoz-kapcsolodó-routeok)
   - [Kurzus routeok](#kurzusokhoz-kapcsolodó-routeok)
   - [Téma routeok](#témához-kapcsolodó-routeok)
   - [Kvíz routeok](#kvízekhez-kapcsolodó-routeok)
   - [Jegyzet routeok](#jegyzethez-kapcsolodó-routeok)
   - [Feladat routeok](#feladathoz-kapcsolodó-routeok)
3. [Komponensek](#komponensek)
   - [BaseDialog](#basedialog)
   - [BaseSpinner](#basespinner)
   - [BasePaginator](#basepaginator)
   - [BaseLearningMaterialCard](#baselearningmaterialcard)
   - [BaseCourseCard](#basecoursecard)
   - [BaseAssignmentCard](#baseassignmentcard)
   - [ScrollBackToTopButton](#scrollbacktotopbutton)
4. [Layoutok](#layoutok)
   - [BaseNavbar](#basenavbar)
   - [BaseLayout](#baselayout)
5. [Oldalak](#oldalak)
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
   - [AssignmentPage](#assignmentpage)
6. [Nyelvi beállítások](#nyelvi-beállítások)
7. [Tárolók](#tárolók)
   - [LanguageStore](#languagestore)
   - [ThemeStore](#themestore)
   - [TopicStore](#topicstore)
   - [GroupStore](#groupstore)
   - [QuizStore](#quizstore)
   - [CourseStore](#coursestore)
   - [NoteStore](#notestore)
8. [Őrök (guardok)](#őrök-guardok)
   - [AdminGuard](#adminguard) 
   - [AttemptAccessGuard](#attemptaccessguard) 
   - [AttemptMarkGuard](#attemptmarkguard) 
   - [AuthGuard](#authguard) 
   - [QuizAccessGuard](#quizaccessguard) 
   - [QuizCreationGuard](#quizcreationguard) 
   - [HTTP Interceptor](#http-interceptor)
9. [Erőforrások](#erőforrások)
   - [Képek](#images-képek)
   - [Stílusok](#styles-stíluslapok)
10. [Csomagok](#felhasznált-csomagok)
11. [Források](#felhasznált-források)

## Indítás

A projekt elindításához futassa le a `start.sh` fájlt.

### Beépített felhasználók

#### Admin felhasználó

- Felhasználónév: `admin`
- E-mail cím: `admin@admin.com`
- Jelszó: `admin123`

#### Felhasználó

- Felhasználónév: `user`
- E-mail cím: `user@user.com`
- Jelszó: `user1234`

### Adatbázis

Az adatbázis EK diagramja [itt](https://dbdiagram.io/d/BolcsessegBarka-65f97220ae072629ce6400fe) érhető el.

![EK diagram](/documentation/er_diagram.png)

### Tesztelési jegyzőkönyv

A teszteléi jegyzőkönyv [itt](https://docs.google.com/spreadsheets/d/11gIBbxXhjW49-Xj5zJ5saSNCxc-tVdxybsBv-37Cc1k/edit?usp=sharing) érhető el.

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
- `groups`: Az adott kurzushoz tartozó csoportokat foglalja magába, ezen belül az adott csoporthoz tartozó felhasználókat is visszaadja.
- `created_by`: Az adott kurzust létrehozó felhasználó azonosítója.

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
      "name": "Történelem",
      "image": "KSDJASD...",
      "groups": [
        {
          "id": 1,
          "name": "9.a",
          "users": [
            {
              "id": 1,
              "name": "admin",
              "email": "admin@admin.com",
              "is_admin": 1,
              "member": {
                "group_id": 1,
                "user_id": 1,
                "permission": "Tanár"
              }
            },
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
      ],
      "created_by": null
    },
    {
      "id": 2,
      "name": "Matematika",
      "image": "LFHK3...",
      "groups": [
        {
          "id": 2,
          "name": "9.b",
          "users": [
            {
              "id": 2,
              "name": "user",
              "email": "user@user.com",
              "is_admin": 0,
              "member": {
                "group_id": 2,
                "user_id": 2,
                "permission": "Tanuló"
              }
            }
          ]
        }
      ],
      "created_by": null
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

Egy JSON objektumot ad vissza `data` néven, melyben objektumok és tömbök találhatóak a következőkkel:

- `id`: A kurzus azonosítója.
- `name`: A kurzus neve.
- `image`: A kurzus képe `base64` formátumban.
- `topics`: A kurzushoz tartozó téma.
- `assignment`: Az adott topichoz tartozó feladat.
- `quizzes`: Az adott topichoz tartozó kvízeket adja vissza.
- `groups`: Az adott kurzushoz tartozó csoportokat azon belül pedig a felhasználókat adja meg.
- `created_by`: Az adott kurzust létrehozó felhasználó azonosítója.

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
    "name": "Történelem",
    "image": "KSDJASD...",
    "topics": [
      {
        "id": 1,
        "name": "Történelmi korszakok",
        "assignment": [
          {
            "id": 1,
            "task_name": "Ókori civilizációk kutatása",
            "comment": "Az ókori Egyiptom, Görögország és Róma társadalmi szerkezete és kultúrája.",
            "deadline": "2024-05-18 12:34:00",
            "course": {
              "id": 1,
              "name": "Történelem",
              "image": "KSDJASD...",
              "created_by": null
            },
            "teacher_task_name": null,
            "student_task": []
          },
          {
            "id": 2,
            "task_name": "Középkori Európa elemzése",
            "comment": "Elemzés a középkori Európa politikai és gazdasági viszonyairól.",
            "deadline": "2024-05-25 12:34:00",
            "course": {
              "id": 1,
              "name": "Történelem",
              "image": "KSDJASD...",
              "created_by": null
            },
            "teacher_task_name": null,
            "student_task": []
          },
          {
            "id": 3,
            "task_name": "Modern korszak fordulópontjai",
            "comment": "A jelentős történelmi fordulópontok hatása a modern társadalomra.",
            "deadline": "2024-06-01 12:34:00",
            "course": {
              "id": 1,
              "name": "Történelem",
              "image": "KSDJASD...",
              "created_by": null
            },
            "teacher_task_name": null,
            "student_task": []
          }
        ],
        "quizzes": [
          {
            "id": 1,
            "name": "2023 május emelt",
            "max_attempts": 5,
            "opens": 1714739646,
            "closes": 1717850046,
            "time": 60,
            "number_of_tasks": 2
          }
        ]
      }
    ],
    "groups": [
      {
        "id": 1,
        "name": "9.a",
        "users": [
          {
            "id": 1,
            "name": "admin",
            "email": "admin@admin.com",
            "is_admin": 1,
            "member": {
              "group_id": 1,
              "user_id": 1,
              "permission": "Tanár"
            }
          },
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
    ],
    "created_by": null
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
- `created_by`: Az adott kurzust létrehozó felhasználó azonosítója.

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
    "image": "OIINHUIuinoioiIO234dsf...",
    "created_by": 1
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
- `created_by`: Az adott kurzust létrehozó felhasználó azonosítója.

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
    "image": "odsfgfdngfusdfsduf...",
    "created_by": 1
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
- `created_by`: Az adott kurzust létrehozó felhasználó azonosítója.

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
        "created_by": 1,
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
- `assignment`: Az adott témához tartozó feladatokat adja meg.
- `quizzes`: Az adott témához tartozó kvízeket adja vissza.
- `created_by`: Az adott kurzust létrehozó felhasználó azonosítója.

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
    "name": "Történelem",
    "image": "asdasdasdad...",
    "topics": [
      {
        "id": 1,
        "name": "Történelmi korszakok",
        "assignment": [
          {
            "id": 1,
            "task_name": "Ókori civilizációk kutatása",
            "comment": "Az ókori Egyiptom, Görögország és Róma társadalmi szerkezete és kultúrája.",
            "deadline": "2024-05-18 12:51:00",
            "course": {
              "id": 1,
              "name": "Történelem",
              "image": "asdasdasdad...",
              "created_by": null
            },
            "teacher_task_name": null,
            "student_task": []
          },
          {
            "id": 2,
            "task_name": "Középkori Európa elemzése",
            "comment": "Elemzés a középkori Európa politikai és gazdasági viszonyairól.",
            "deadline": "2024-05-25 12:51:00",
            "course": {
              "id": 1,
              "name": "Történelem",
              "image": "asdasdasdad...",
              "created_by": null
            },
            "teacher_task_name": null,
            "student_task": []
          },
          {
            "id": 3,
            "task_name": "Modern korszak fordulópontjai",
            "comment": "A jelentős történelmi fordulópontok hatása a modern társadalomra.",
            "deadline": "2024-06-01 12:51:00",
            "course": {
              "id": 1,
              "name": "Történelem",
              "image": "asdasdasdad...",
              "created_by": null
            },
            "teacher_task_name": null,
            "student_task": []
          }
        ],
        "quizzes": [
          {
            "id": 1,
            "name": "2023 május emelt",
            "max_attempts": 5,
            "opens": 1714740710,
            "closes": 1717851110,
            "time": 60,
            "number_of_tasks": 2
          }
        ]
      }
    ],
    "created_by": 1
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
- `assignment`: A témához tartozó feladatok.
- `quizzes`: A témához tartozó kvízek.

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
      "name": "Történelmi korszakok",
      "assignment": [
        {
          "id": 1,
          "task_name": "Feladat",
          "comment": "Ez egy hozzászólás.",
          "deadline": "2024-04-29 11:24:00",
          "grade": null,
          "course": {
            "id": 1,
            "name": "Történelem",
            "image": "tortenelem.png",
            "created_by": null
          },
          "teacher_task_name": "undefined",
          "student_task": []
        }
      ],
      "quizzes": [
        {
          "id": 1,
          "name": "A kora újkor",
          "max_attempts": null,
          "opens": null,
          "closes": null,
          "time": null,
          "number_of_tasks": 1
        }
      ]
    },
    {
      "id": 4,
      "name": "Téma2",
      "assignment": [],
      "quizzes": []
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
- `assignment`: A témához tartozó feladatok.
- `quizzes`: A témához tartozó kvízek.

#### Példa

URI:

```
/api/topics/4
```

Válasz:

```json
{
  "data": {
    "id": 4,
    "name": "Téma2",
    "assignment": [],
    "quizzes": []
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

#### Válasz

Egy JSON objektumot ad vissza `data` néven a következőkkel:

- `id`: A téma azonosítója.
- `name`: A téma neve.

#### Példa

URI:

```
/api/topics
```

Törzs:

```json
{
  "name": "Objektumok"
}
```

Válasz:

```json
{
  "data": {
    "id": 4,
    "name": "Objektumok"
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

Téma szerkesztése.

#### Törzs

- `name`: A téma neve.

#### Válasz

Egy JSON objektumot ad vissza `data` néven a következőkkel:

- `id`: A téma azonosítója.
- `name`: A téma neve.
- `assignment`: A témához tartozó feladatok.
- `quizzes`: A témához tartozó kvízek.

#### Példa

URI:

```
/api/topics/5
```

Törzs:

```json
{
  "name": "Új Téma"
}
```

Válasz:

```json
{
  "data": {
    "id": 5,
    "name": "Új Téma",
    "assignment": [],
    "quizzes": []
  }
}
```

#### Hibakódok

Az alábbi hibakódokat adhatja vissza a végpont:

- `401 Unauthorized`: Hiányzik a Bearer token.
- `404 Not Found`: Nincs ilyen rekord az adatbázisban.
- `403 Forbidden`: A felhasználónak nincs jogosultsága.
- `422 Unprocessable Content`: Hiba a törzs adataiban.
- `500 Internal Server Error`: Váratlan hiba történt a szerveren.

### `DELETE /api/topics/{id}`

Téma törlése.

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
- `404 Not Found`: Nincs ilyen rekord az adatbázisban.
- `403 Forbidden`: A felhasználónak nincs jogosultsága.
- `500 Internal Server Error`: Váratlan hiba történt a szerveren.

### Kvízekhez kapcsolodó routeok:

| Method | URI                         | Name             | Controller        | Action  |
| ------ | --------------------------- | ---------------- | ----------------- | ------- |
| GET    | /api/quizzes                | quizzes.index    | QuizController    | index   |
| GET    | /api/quizzes/{id}           | quizzes.show     | QuizController    | show    |
| POST   | /api/quizzes                | quizzes.store    | QuizController    | store   |
| PUT    | /api/quizzes/{id}           | quizzes.update   | QuizController    | update  |
| DELETE | /api/quizzes/{id}           | quizzes.destory  | QuizController    | destroy |
| GET    | /api/quizzes/{id}/tasks     | tasks.index      | TaskController    | index   |
| GET    | /api/quizzes/{id}/tasks/ids | tasks.taskIds    | TaskController    | taskIds |
| GET    | /api/tasks/{id}             | tasks.show       | TaskController    | show    |
| POST   | /api/tasks                  | tasks.store      | TaskController    | store   |
| PUT    | /api/tasks/{id}             | tasks.update     | TaskController    | update  |
| DELETE | /api/tasks/{id}             | tasks.destory    | TaskController    | destroy |
| DELETE | /api/subtasks/{id}          | subtasks.destroy | SubtaskController | destroy |

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
      "header": "Ez egy példa feladat.",
      "text": "A források és ismeretei alapján válaszoljon a kérdésekre!",
      "subtasks": [
        {
          "id": 1,
          "order": 0,
          "question": "<p><b>Adja meg, mikor volt a rendszerváltás!</b></p>",
          "options": null,
          "type": "short_answer",
          "marks": 0.5
        },
        {
          "id": 2,
          "order": 1,
          "question": "<p><b>Mikor volt a szkizma?</b></p>",
          "options": null,
          "type": "short_answer",
          "marks": 1
        }
      ]
    },
    {
      "id": 2,
      "order": 1,
      "header": "A feladat egy másik példa.",
      "text": "Oldja meg a feladatokat a források és ismeretei alapján!",
      "subtasks": [
        {
          "id": 3,
          "order": 0,
          "question": "<p><b>Fogalmazza meg saját szavaival a reformáció egyik fő kiváltó okát!</b></p>",
          "options": null,
          "type": "short_answer",
          "marks": 1
        },
        {
          "id": 4,
          "order": 1,
          "question": "<b>b) Milyen államformát tartott követendőnek az Emberi és Polgári Jogok Nyilatkozata? Jelölje ki!</b>",
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
  "data": [1, 2]
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
    "header": "Ez egy példa feladat.",
    "text": "A források és ismeretei alapján válaszoljon a kérdésekre!",
    "subtasks": [
      {
        "id": 1,
        "order": 0,
        "question": "<p><b>Adja meg, mikor volt a rendszerváltás!</b></p>",
        "options": null,
        "type": "short_answer",
        "marks": 0.5
      },
      {
        "id": 2,
        "order": 1,
        "question": "<p><b>Mikor volt a szkizma?</b></p>",
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
- `500 Internal Server Error`: Váratlan hiba történt a szerveren.

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
  "id": 1,
  "order": 0,
  "header": "Ez egy példa feladat.",
  "text": "A források és ismeretei alapján válaszoljon a kérdésekre!",
  "subtasks": [
    {
      "id": 1,
      "order": 0,
      "question": "<p><b>Adja meg, mikor volt a rendszerváltás!</b></p>",
      "options": null,
      "type": "short_answer",
      "marks": 0.5
    },
    {
      "id": 2,
      "order": 1,
      "question": "<p><b>Mikor volt a szkizma?</b></p>",
      "options": null,
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
    "id": 1,
    "order": 0,
    "header": "Ez egy példa feladat.",
    "text": "A források és ismeretei alapján válaszoljon a kérdésekre!",
    "subtasks": [
      {
        "id": 1,
        "order": 0,
        "question": "<p><b>Adja meg, mikor volt a rendszerváltás!</b></p>",
        "options": null,
        "type": "short_answer",
        "marks": 0.5
      },
      {
        "id": 2,
        "order": 1,
        "question": "<p><b>Mikor volt a szkizma?</b></p>",
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
  "id": 1,
  "order": 0,
  "header": "Ez egy példa feladat.",
  "text": "A források és ismeretei alapján válaszoljon a kérdésekre!",
  "subtasks": [
    {
      "id": 1,
      "order": 0,
      "question": "<p><b>Adja meg, mikor volt a rendszerváltás!</b></p>",
      "options": null,
      "type": "short_answer",
      "marks": 0.5
    },
    {
      "id": 2,
      "order": 1,
      "question": "<p><b>Mikor volt a szkizma?</b></p>",
      "options": null,
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
    "id": 1,
    "order": 0,
    "header": "Ez egy példa feladat.",
    "text": "A források és ismeretei alapján válaszoljon a kérdésekre!",
    "subtasks": [
      {
        "id": 1,
        "order": 0,
        "question": "<p><b>Adja meg, mikor volt a rendszerváltás!</b></p>",
        "options": null,
        "type": "short_answer",
        "marks": 0.5
      },
      {
        "id": 2,
        "order": 1,
        "question": "<p><b>Mikor volt a szkizma?</b></p>",
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

### Jegyzethez kapcsolodó routeok:

| Method | URI                  | Name                  | Controller     | Action          |
| ------ | -------------------- | --------------------- | -------------- | --------------- |
| GET    | /api/notes           | notes.index           | NoteController | index           |
| GET    | /api/notes/{id}      | notes.show            | NoteController | show            |
| POST   | /api/notes           | notes.store           | NoteController | store           |
| PUT    | /api/notes/{id}      | notes.update          | NoteController | update          |
| DELETE | /api/notes/{id}      | notes.destroy         | NoteController | destroy         |
| GET    | /api/getCurrentNotes | notes.getCurrentNotes | NoteController | getCurrentNotes |
| GET    | /api/getTeacherNotes | notes.getTeacherNotes | NoteController | getTeacherNotes |

### `GET /api/notes`

Az összes jegyzet lekérése.

#### Válasz

Egy JSON tömböt ad vissza `data` néven, melyben objektumok találhatóak a következőkkel:

- `id`: A jegyzet azonosítója.
- `title`: A jegyzet címe.
- `text`: A jegyzet szövege.
- `user_id`: A jegyzetet létrehozó felhasználó azonosítója.
- `topic_id`: Annak a témának az azonosítója amelyikhez az adott jegyzet tartozik.
- `role`: Jelzi, hogy a jegyzetet egy tanár vagy tanuló készítette.

#### Példa

URI:

```
/api/notes
```

Válasz:

```json
{
  "data": [
    {
      "id": 1,
      "title": "Jegyzet1",
      "text": "Ez egy teszt jegyzet.",
      "user_id": 1,
      "topic_id": 1,
      "role": "Tanár"
    },
    {
      "id": 1,
      "title": "Jegyzet2",
      "text": "Ez is egy teszt jegyzet.",
      "user_id": 2,
      "topic_id": 1,
      "role": "Diák"
    }
  ]
}
```

#### Hibakódok

Az alábbi hibakódokat adhatja vissza a végpont:

- `401 Unauthorized`: Hiányzik a Bearer token.
- `500 Internal Server Error`: Váratlan hiba történt a szerveren.

### `GET /api/notes/{id}`

Egy jegyzet lekérése azonosító alapján.

#### Válasz

Egy JSON objektumot ad vissza `data` néven a következőkkel:

- `id`: A jegyzet azonosítója.
- `title`: A jegyzet címe.
- `text`: A jegyzet szövege.
- `user_id`: A jegyzetet létrehozó felhasználó azonosítója.
- `topic_id`: Annak a témának az azonosítója amelyikhez az adott jegyzet tartozik.
- `role`: Jelzi, hogy a jegyzetet egy tanár vagy tanuló készítette.

#### Példa

URI:

```
/api/notes/1
```

Válasz:

```json
{
  "data": {
    "id": 1,
    "title": "Jegyzet1",
    "text": "Ez egy teszt jegyzet.",
    "user_id": 1,
    "topic_id": 1,
    "role": "Tanár"
  }
}
```

#### Hibakódok

Az alábbi hibakódokat adhatja vissza a végpont:

- `401 Unauthorized`: Hiányzik a Bearer token.
- `404 Not Found`: Nincs ilyen rekord az adatbázisban.
- `500 Internal Server Error`: Váratlan hiba történt a szerveren.

### `POST /api/notes`

Új Jegyzet létrehozása.

#### Törzs

- `title`: A jegyzet címe.
- `text`: A jegyzet szövege.
- `topic_id`: Azon téma azonosítója, amelyhez a jegyzetet kapcsolni kívánjuk.
- `user_id`: A jegyzetet létrehozó felhasználó azonosítója.
- `role`: Jelzi, hogy a jegyzetet egy tanár vagy tanuló készítette.

#### Válasz

Egy JSON objektumot ad vissza `data` néven a következőkkel:

- `id`: A jegyzet azonosítója.
- `title`: A jegyzet címe.
- `text`: A jegyzet szövege.
- `user_id`: A jegyzetet létrehozó felhasználó azonosítója.
- `topic_id`: Azon téma azonosítója, amelyhez a jegyzetet kapcsolni kívánjuk.
- `role`: Jelzi, hogy a jegyzetet egy tanár vagy tanuló készítette.

#### Példa

URI:

```
/api/notes
```

Törzs:

```json
{
  "title": "Jegyzetem",
  "text": "Jegyzet",
  "topic_id": 1,
  "user_id": 1,
  "role": "Tanuló"
}
```

Válasz:

```json
{
  "data": {
    "id": 3,
    "title": "Jegyzetem",
    "text": "Jegyzet",
    "user_id": 1,
    "topic_id": 1,
    "role": "Tanuló"
  }
}
```

#### Hibakódok

Az alábbi hibakódokat adhatja vissza a végpont:

- `401 Unauthorized`: Hiányzik a Bearer token.
- `422 Unprocessable Content`: Hiba a törzs adataiban.
- `500 Internal Server Error`: Váratlan hiba történt a szerveren.

### `PUT /api/notes/{id}`

Jegyzet szerkesztése. Csak a jegyzet eredeti készítője jogosult annak módosítására.

#### Törzs

- `title`: A jegyzet címe.
- `text`: A jegyzet szövege.

#### Válasz

Egy JSON objektumot ad vissza `data` néven a következőkkel:

- `id`: A jegyzet azonosítója.
- `title`: A jegyzet címe.
- `text`: A jegyzet szövege.
- `user_id`: A jegyzetet létrehozó felhasználó azonosítója.

#### Példa

URI:

```
/api/notes/3
```

Törzs:

```json
{
  "title": "2.Jegyzetem",
  "text": "Ez a második Jegyzetem."
}
```

Válasz:

```json
{
  "data": {
    "id": 3,
    "title": "2.Jegyzetem",
    "text": "Ez a második Jegyzetem.",
    "user_id": 1,
    "topic_id": 1,
    "role": "Tanuló"
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

### `DELETE /api/notes/{id}`

Jegyzet törlése. Csak a jegyzet eredeti készítője jogosult annak törlésére.

#### Válasz

`204 No Content`, amennyiben sikeres a törlés.

#### Példa

URI:

```
/api/notes/3
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

### `GET /api/getCurrentNotes`

Visszaadja a tanuló által létrehozott, tanár által közzétett jegyzeteket azok alapján, hogy melyik kurzushoz és csoporthoz tartozik.

#### Válasz

Egy JSON tömböt ad vissza `data` néven, melyben objektumok találhatóak a következőkkel:

- `id`: A jegyzet azonosítója.
- `title`: A jegyzet címe.
- `text`: A jegyzet szövege.
- `user_id`: A jegyzetet létrehozó felhasználó azonosítója.
- `topic_id`: Annak a témának az azonosítója amelyikhez az adott jegyzet tartozik.
- `role`: Jelzi, hogy a jegyzetet egy tanár vagy tanuló készítette.

#### Példa

URI:

```
/api/getCurrentNotes
```

Válasz:

```json
{
  "data": [
    {
      "id": 1,
      "title": "Ókori civilizációk",
      "text": "Egyiptomi Birodalom, Görög Poliszok, Római Köztársaság",
      "user_id": 1,
      "topic_id": 1,
      "role": "Tanár"
    },
    {
      "id": 2,
      "title": "Középkori Európa",
      "text": "A Karoling Királyság, A Keresztes Háborúk, A Fekete Halál",
      "user_id": 2,
      "topic_id": 1,
      "role": "Tanuló"
    }
  ]
}
```

#### Hibakódok

Az alábbi hibakódokat adhatja vissza a végpont:

- `401 Unauthorized`: Hiányzik a Bearer token.
- `500 Internal Server Error`: Váratlan hiba történt a szerveren.

### `GET /api/getTeacherNotes`

Visszaadja az összes tanári jegyzetet aszerint, hogy az adott felhasználó részese-e a megfelelő kurzusnak, csoportnak.

#### Válasz

Egy JSON tömböt ad vissza `data` néven, melyben objektumok találhatóak a következőkkel:

- `id`: A jegyzet azonosítója.
- `title`: A jegyzet címe.
- `text`: A jegyzet szövege.
- `updated_at`: A legutolsó frissítés óta eltelt idő.
- `course_name`: A jegyzetet tartalmazó kurzus neve.
- `course_image`: A jegyzetet tartalmazó kurzus képe.

#### Példa

URI:

```
/api/getTeacherNotes
```

Válasz:

```json
{
  "data": [
    {
      "id": 2,
      "title": "Középkori Európa",
      "text": "A Karoling Királyság, A Keresztes Háborúk, A Fekete Halál",
      "updated_at": "2024-05-01 14:51:50",
      "course_name": "Történelem",
      "course_image": "asdasdasdad..."
    },
    {
      "id": 1,
      "title": "Ókori civilizációk",
      "text": "Egyiptomi Birodalom, Görög Poliszok, Római Köztársaság",
      "updated_at": "2024-04-29 14:51:50",
      "course_name": "Történelem",
      "course_image": "asdasdasdad..."
    }
  ]
}
```

#### Hibakódok

Az alábbi hibakódokat adhatja vissza a végpont:

- `401 Unauthorized`: Hiányzik a Bearer token.
- `500 Internal Server Error`: Váratlan hiba történt a szerveren.

### Feladathoz kapcsolodó routeok:

| Method | URI                        | Name                              | Controller           | Action                |
| ------ | -------------------------- | --------------------------------- | -------------------- | --------------------- |
| GET    | /api/getCurrentAssignments | assignments.getCurrentAssignments | AssignmentController | getCurrentAssignments |

### `GET /api/getCurrentAssignments`

Visszaadja az összes tanár által kitett feladatot, aszerint hogy az adott felhasználó részese-e a megfelelő kurzusnak, csoportnak és a diák még nem adta le a feladatot, illetve a beadási határidő még nem járt le.

#### Válasz

Egy JSON tömböt ad vissza `data` néven, melyben objektumok találhatóak a következőkkel:

- `id`: A feladat azonosítója.
- `task_name`: A feladat neve.
- `deadline`: A feladat lejárati dátuma.
- `course_image`: A feladatot tartalmazó kurzus képe.
- `student_assignment`: Itt jelenik meg a diák által benyújtott feladat.

#### Példa

URI:

```
/api/getCurrentAssignments
```

Válasz:

```json
{
  "data": [
    {
      "id": 1,
      "task_name": "Ókori civilizációk kutatása",
      "deadline": "2024.05.18 12:51:00",
      "course_image": "asdasdasdad...",
      "student_assignment": []
    },
    {
      "id": 2,
      "task_name": "Középkori Európa elemzése",
      "deadline": "2024.05.25 14:51:00",
      "course_image": "asdasdasdad...",
      "student_assignment": []
    }
  ]
}
```

#### Hibakódok

Az alábbi hibakódokat adhatja vissza a végpont:

- `401 Unauthorized`: Hiányzik a Bearer token.
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

- **Képoszlop:** Megjeleníti a tananyag képét, amely adaptív méretű és kitölti a rendelkezésre álló területet.
- **Szöveges oszlop:** Megjeleníti a kurzus nevét és a tananyag címét, valamint azt hogy a tananyag mikor lett legutóbb frissítve/kiadva.
- **Gomboszlop:** Tartalmaz egy gombot, amely lehetőséget ad a tananyag részletes megtekintésére.

### Használat:

A komponens a `learningMaterial` objektum prop-ot várja, amely tartalmazza a tananyaghoz kapcsolódó adatokat, mint a kép, a kurzus neve, a tananyag címe, és a frissítési dátum.

### Példa a komponens használatára egy szülőkomponensben:

```html
<template>
  <div>
    <BaseLearningMaterialCard
      :learningMaterial="{
        course_image: 'course-image.jpg',
        course_name: 'Web Development',
        title: 'Introduction to Vue.js',
        updated_at: '2023-04-01'
      }"
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

### Komponens tulajdonságok:

- `learningMaterial`: Egy objektum, amely tartalmazza a tananyaghoz kapcsolódó adatokat:
  - `course_image`: A kurzus képének útvonala Base64 formátumban.
  - `course_name`: A kurzus neve.
  - `title`: A tananyag címe.
  - `updated_at`: A tananyag frissítési dátuma.

### Számított tulajdonságok:

- **messages**: A nyelvi üzeneteket kezeli a Pinia állapotkezelőn keresztül, így biztosítva, hogy a komponens nyelvi adaptációja dinamikusan történjen.

### Metódusok

- `formatDate(dateStr)`: Megformázza a dátumot **YYYY.MM.DD** formátumban.
- `getImageSrc(base64Data)`: Át konvertálja a `Base64` adatot kép URL-lé.
- `openDialog(material)`: Egy dialógusablakot nyit meg a tananyag részleteivel.

### Stílusok:

> A komponens stílusai biztosítják, hogy a kártya minden eszközön jól nézzen ki. Az `img-fluid`, `object-fit-cover` és `responsive-rounded` osztályok gondoskodnak a képek reszponzív és esztétikus megjelenéséről.

### `BaseCourseCard`

### Beveztés:

> A BaseCourseCard egy reszponzív, vizuálisan vonzó kártya komponens, amely bemutatja a kurzusok alapvető információit, beleértve a képét, címét és a csoporttagságait. A komponens továbbá biztosít egy közvetlen útválasztási linket, amely lehetővé teszi a felhasználók számára, hogy közvetlenül a kurzus részleteire navigáljanak.

### Használat:

> A komponens a `title`, `image`, `courseId`, és `groups` propokat fogadja, amelyek a kurzus alapvető adatait definiálják. Ezek a propok lehetővé teszik a komponens széleskörű alkalmazhatóságát és könnyű integrálását különböző felületeken.

### Példa:

```html
<template>
  <div>
    <BaseCourseCard
      title="Webfejlesztés"
      image="encodedImageString"
      courseId="102"
      :groups="[{ id: 1, name: 'Haladó Tanfolyamok' }]"
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

- **Kártya:** Használja a Bootstrap `card` osztályát a vizuális keret biztosításához.
- **Kép:** A kurzushoz tartozó képet jeleníti meg `Base64` kódolásban, ami egyszerűsíti az adatátvitelt.
- **Szöveges Tartalom:** Középre igazított módon megjeleníti a kurzus címét és a csoportok neveit.
- **Navigációs Link:** Egy `RouterLink` komponens segítségével lehetővé teszi a felhasználók számára, hogy közvetlenül a kurzus részletes oldalára navigáljanak.

### Komponens tulajdonságok:

- **title:** A kurzus címe.
- **image:** A kurzus képének `Base64` kódolt stringje.
- **courseId:** A kurzus azonosítója.
- **groups:** A kurzushoz tartozó csoportok listája. Mindegyik csoport objektum tartalmaz egy **id** és **name** kulcsot.

### Számított tulajdonságok:

- **messages**: A nyelvi üzenetek kezelése a Pinia állapotkezelő segítségével történik, amely lehetővé teszi a komponens dinamikus nyelvi adaptációját.

### Stílusok:

> A `scoped` stílusok használata biztosítja, hogy a komponens megjelenése konzisztens maradjon anélkül, hogy befolyásolná a többi komponens stílusát. A képek és szövegek úgy vannak formázva, hogy minden eszközön jól nézzenek ki.

### `BaseAssignmentCard`

### Áttekintés:

> A `BaseAssignmentCard` egy reszponzív kártya komponens, amely megjeleníti a feladatok alapvető információit, mint például a címét, a képét, és a beadási határidőt. Ezen kívül tartalmaz egy gombot, amely a felhasználót közvetlenül az adott feladat részletező oldalára irányítja.

### Használat:

> A komponens egy `assignment` objektum prop-ot fogad, amely tartalmazza a feladat alapvető adatait. Ez lehetővé teszi a feladatok könnyedén és hatékonyan való megjelenítését a felhasználók számára.

### Példa:

```html
<template>
  <div>
    <BaseAssignmentCard
      :assignment="{
        task_name: 'Programozási Alapok',
        course_image: 'programozas_kep.png',
        deadline: '2023-12-15',
        id: 102
      }"
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

- **Kártya Fejléc:** Tartalmazza a feladat címét (`task_name`), amely középre van igazítva.
- **Kártya Törzs:** Itt jelenik meg a feladat képe (`course_image`), amely szintén központi helyet kap. Alatta egy gomb található, amely a feladat részleteire mutató oldalra vezet.
- **Kártya Lábléc:** Megjeleníti a feladat beadási határidejét (`deadline`), amely ki van emelve, hogy felhívja a figyelmet a határidő fontosságára.

### Komponens tulajdonságok:

- **assignment:** Egy objektum, amely tartalmazza a feladat adatait, mint a cím (`task_name`), a kép (`course_image`), a beadási határidő (`deadline`) és az egyedi azonosító (`id`).

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

### `ScrollBackToTopButton`

### Bemutatás:

> A `ScrollBackToTopButton` létrehoz egy gombot aminek megnyomásával képes az oldal aljárról az oldal tetejére vinni a felhasználót.

### Használat:

> A `ScrollBackToTopButton` komponenst be kell importálnunk a script részben és utána a komponenst már be is illeszthetjük a `template` közé.

#### Példa

```html
<template>
  <ScrollBackToTopButton />
</template>

<script>
  import ScrollBackToTopButton from "@layouts/ScrollBackToTopButton.vue";

  export default {
    components: {
      ScrollBackToTopButton,
    },
  };
</script>
```

## Oldalak:

### `HomePage`

> A `HomePage` komponens a webalkalmazás kezdőlapját jelenti, amely dinamikusan jeleníti meg a felhasználó számára releváns tartalmakat, mint például feladatokat, tananyagokat. A komponens integrálja többek között az `BaseLayout`, `BaseSpinner`, `BaseAssignmentCard`, `BaseLearningMaterialCard`, és `BasePaginator` komponenseket a felhasználói felület egyszerű és interaktív kialakításához.

### Komponens Struktúra

**Template Részletezése:**

- **BaseLayout:** Ez a komponens biztosítja az oldal alapvető elrendezését.
- **BaseSpinner:** Egy töltő animáció, amely akkor jelenik meg, amikor az oldal adatai még betöltés alatt állnak.
- **BaseDialog:** Egy párbeszédablak, amelyet akkor jelenítünk meg, amikor egy tananyag jegyzetének megtekintése szükséges.
- **Üdvözlő Üzenetek:** Üdvözlő üzenet, amely a felhasználó nevét is megjeleníti, valamint külön szekciókat határoz meg a feladatoknak és tananyagoknak.
- **BaseAssignmentCard & BaseLearningMaterialCard:** Ezek a kártyák az egyes feladatokat és tananyagokat mutatják be részletesen.
- **Paginator:** Lapozó komponens, amely lehetővé teszi a feladatok lapozását.

**Script Részletezése:**

- **Belső Állapotok:** Tartalmazza a feladatok listáját, a tananyagokat, az aktuális oldalszámot, az animáció irányát, a betöltés állapotát, a jegyzet láthatóságát, és a megtekintés alatt áló tananyag adatait.
- **Módszerek és Számított Tulajdonságok:** A `onPageChanged` metódus kezeli az oldalszám változását, a `paginatedAssignments` kiszámítja az aktuális oldalon megjelenő feladatokat, és a `totalPages` meghatározza az összes oldal számát. Az `handleMaterialDialog` metódus kezeli a tananyagokhoz kapcsolódó párbeszédablak megjelenítését.
- **Életciklus metódus (Mounted):** A `mounted` életciklus metódusban lekérdezzük a felhasználói adatokat, a feladatokat, és a tananyagokat, majd beállítjuk a `loading` állapotot `false`-ra a betöltés befejezése után.

### Stílusok (Styles)

A stílusok részletesen kezelik az animációkat és a médialekérdezéseket, biztosítva, hogy a felhasználói felület reszponzív és vizuálisan vonzó legyen minden eszközön.

- **Animációk**: Definiált animációk a feladatkártyák számára, amelyek jobbra vagy balra csúsztatással történő átmenetet valósítanak meg.
- **Médialekérdezések**: Különböző képernyőméretekhez igazodó stílusok, amelyek biztosítják a tartalom megfelelő megjelenítését a különböző eszközökön.

### `CoursesPage`

> A `CoursesPage` komponens egy kulcsfontosságú oldal a webalkalmazásban, amely lehetővé teszi a felhasználók számára, hogy megtekinthessék és hozzáférjenek az összes elérhető kurzushoz. Ez az oldal dinamikusan jeleníti meg a kurzusokat `BaseCourseCard` komponensek formájában, amelyek részletes információkat tartalmaznak az egyes kurzusokról.

### Komponens Struktúra

#### **Template Áttekintés:**

- **BaseLayout:** Ez az elrendezési keret biztosítja az oldal konzisztens kinézetét és struktúráját az alkalmazáson belül.
- **BaseSpinner:** Egy töltésjelző animáció, amely megjelenik, míg a kurzusok betöltődnek.
- **Dinamikus Kurzuskártyák:** A kurzusokat a `BaseCourseCard` komponensek mutatják be dinamikusan, amelyek tartalmazzák a kurzusok nevét, képét, azonosítóját, és a hozzájuk kapcsolódó csoportokat.

### Script Részletek

- **Belső Állapotok:**
  - **courses:** Egy tömb, amely a felhasználó számára elérhető kurzusokat tárolja.
  - **loading:** Egy logikai változó, amely a betöltés állapotát jelzi.
- **Metódusok:**
  - **getCourses():** Ez a metódus lekéri az API-tól a kurzusokat, figyelembe véve a felhasználóhoz kapcsolódó csoporttagságokat, és dinamikusan tölti fel a `courses` tömböt a hozzáférhető kurzusokkal.

### Stílusok és Animációk

A komponens CSS szabályai a `BaseLayout`-ból és a `BaseCourseCard`-ból származnak, biztosítva a konzisztens kinézetet és a reszponzív dizájnt.

### Integráció a Pinia Tárolókkal

A `CoursesPage` komponens integrálja a `languageStore`-t a lokalizált üzenetek kezelésére, valamint a `userStore`-t és `groupStore`-t a felhasználói adatok és csoportinformációk kezelésére. Ezek a tárolók biztosítják, hogy a kurzusinformációk mindig naprakészek és relevánsak legyenek a felhasználó számára.

### `CoursePage`

> A _CoursePage_ komponens egy dinamikus felületet biztosít, ahol a diákok és tanárok kurzus-specifikus tevékenységeket végezhetnek, mint például témák kezelése, jegyzetek és feladatok létrehozása, valamint feladatok szerkesztése és letöltése. Ez a komponens több újrafelhasználható komponenst integrál, mint például `BaseSpinner`, `BaseDialog`, és `BaseLayout`, és kezeli az állapotokat és viselkedéseket az alkalmazásban.

### Komponens Struktúra

#### **Template Áttekintés:**

- **BaseLayout:** Ez a fő elrendezési keret a lap számára, biztosítva az alkalmazáson belüli konzisztens stílust és szerkezetet.
- **BaseSpinner:** Betöltési animációt jelenít meg, amíg az adatok be nem töltődnek.
- **BaseDialogok:** Több párbeszédpanel példány használata az új témák hozzáadásához, témák szerkesztéséhez, csoportok kezeléséhez, új feladatok létrehozásához, jegyzetek kezeléséhez, kvízek létrehozásához és kezeléséhez.

### Script Részletek

#### **Belső Állapotok:**

- **courses:** A kurzus adatokat tároló objektum.
- **topics:** A kurzushoz tartozó témák listája.
- **loading:** Betöltés állapotának jelzése.
- **member:** A jelenlévő felhasználó jogosultságát adja meg.
- **Dialogus ablakok láthatóság:** Ezek az állapotok határozzák meg hogy melyik dialogus ablak milyen feltétel alapján legyen látható.
- **isNoteReadonly:** Azt határozza meg a jegyzetek esetében hogy egy jegyzet csak olvasható vagy írható is.
- **currentGroups:** A `MultiSelect` komponensben kijelölt csoportok kerülnek bele.
- **title és text:** A jegyzet létrehozásához használatos állapotok.
- **currentTitle és currentText:** A már meglévő jegyzetek betöltésére szolgáló állapotok.
- **currentlyModifyingNote:** Az éppen módosítás alatt álló jegyzet kerül bele.

#### **Metódusok és Eseménykezelők:**

#### Téma

- A témakezelő funkciók (`addTopic`, `editTopic`, `putTopic`, `deleteTopic`) csak a tanárok és az adminisztrátorok számára érhetők el, biztosítva az adatok integritását és a jogosultságok szerinti hozzáférést.
- A `toggleTopic` függvény felelős az `activeTopicId` állapot frissítéséért, ami lehetővé teszi számunkra, hogy nyomon kövessük, melyik témához kívánunk például új jegyzetet hozzáadni.

#### Jegyzet

- A jegyzetkezelő funkciók (`deleteNote`, `updateNote`, `saveNote`,`openCurrentNote`) minden felhasználó számára elérhetőek, lehetővé téve számukra, hogy egyszerűen és hatékonyan kezeljék saját jegyzeteiket.
- A `getAllNotesForUser`, `getCurrentNotes` függvények, a bejelentkezett felhasználóhoz mérten adja vissza a jegyzeteket.

#### Feladat

- A feladatkezelő funkciók (`postNewAssignment`, `openUpdateAssignment`, `filteredStudentAssignment`,`updateAssignment`, `deleteAssignment`,`downloadAssignment`) a `tanárok` és az `adminok` számára elérhetőek, lehetővé téve számukra, hogy egyszerűen és hatékonyan kezeljék a feladatokat.

#### Csoportok

- A `saveGroups` függvénnyel tudjuk a csoportokat hozzárendelni az adott kurzushoz.

#### Felhasználói jogosultságok

- A `findUserDetails` metódus feladata, hogy az adott felhasználóhoz tartozó jogosultságokat azonosítsa egy meghatározott kurzus és csoport kontextusában. Ez a metódus segít megállapítani, hogy a felhasználónak milyen hozzáférési szintje van a kurzushoz és annak tartalmához, beleértve a témákat, feladatokat és egyéb kapcsolódó anyagokat.

#### **Állapotok és Adatkezelés:**

- #### Pinia Tárolók Integrációja:

- `userStore`, `courseStore`, `groupStore`, `topicStore`, `noteStore` használata az adatok és állapotok kezelésére.
- `themeStore`, `languageStore` használata a nyelvi adatok és stílusok kezeléséhez.

#### **Jogosultságok Kezelése:**

- A `userStore` segítségével ellenőrizhető, hogy a jelenlegi felhasználó rendelkezik-e a szükséges adminisztrátori jogosultsággal.

### `CourseManagementPage`

> A `CourseManagementPage` komponens a kurzuskezelési feladatokat támogatja, lehetővé téve a kurzusok létrehozását, módosítását, törlését és exportálását. A komponens felhasználóbarát kezelőfelületet biztosít a kurzusok kezeléséhez, széles körű funkciókat kínálva az oktatási intézmények és tréning szolgáltatók számára. Az adott oldalt csak a megfelelő jogosultsági szinttel lehet elérni.

### Komponens Struktúra

#### **Template Áttekintés:**

- **BaseLayout:** Az oldal alapvető elrendezési keretét biztosítja, amely konzisztens felhasználói felületet nyújt.
- **BaseSpinner:** Egy töltésjelző, amely akkor jelenik meg, amikor az oldal adatokat tölt be.
- **BaseDialog:** Modális ablakok, amelyeket új kurzus hozzáadásához vagy meglévő kurzus módosításához használnak.
- **BaseToast:** Üzeneteket jelenít meg a kurzusműveletek eredményéről, mint például sikeres létrehozás vagy törlés.
- **BaseConfirmDialog:** Törlési folyamatok során jelenik meg, egy felugró ablak, ami megkérdezi a felhasználót, hogy valóban szeretné-e törölni az adott kurzust.

#### **Dinamikus Funkciók:**

- **DataTable:** Egy táblázat, amely a rendelkezésre álló kurzusok listáját jeleníti meg, lehetőséget nyújtva a kiválasztott kurzusok módosítására vagy törlésére.
- **Toolbar:** Eszköztár, amely gombokat tartalmaz új kurzus létrehozásához, több kurzus törléséhez és exportáláshoz.

### Script Részletek

#### **Belső Állapotok:**

- **courses:** Tömb, amely a rendelkezésre álló kurzusokat tartalmazza.
- **loading:** Egy boolean típusú változó, amely jelzi, hogy az oldal betöltése folyamatban van-e.
- **addCourseDialogVisible, modifyCourseDialogVisible:** Logikai változók, amelyek az új kurzus hozzáadása és meglévő kurzus módosítása dialógusablakok láthatóságát szabályozzák.
- **filters:** A `primeVue` által használatos szűrési feltételt tároló állapot.
- **selectedCourses:** A több kurzus törlése esetén való kiválasztás során kerülnek bele a kurzusok.
- **currentlyModifyingCourse:** A jelenleg modósítás alatt áló kurzus kerül bele.

#### **Metódusok:**

- **getCourses, postCourses, deleteCourse, deleteMultipleCourses, updateCourse:** Funkciók, amelyek az adott kurzusokkal kapcsolatos API hívásokat kezelik.
- **handleFileChange:** Kezeli a fájlkiválasztó eseményeket a kurzus képének frissítéséhez.
- **groupsNoUselessData:** Ez a függvény adja át a `MultiSelect` komponensnek az elérhető csoportokat mindeféle felesleges adat nélkül.
- **handleFileChange:** A `handleFileChange` metódus a fájlválasztóból kiválasztott képfájl tartalmát olvassa be és tárolja el `base64` kódolt formátumban a `currentlyModifyingCourse.image` tulajdonságban.
- **selectAllCourses:** A `selectAllCourses` metódus kiválasztja vagy törli a kiválasztást minden kurzusról a listából, attól függően, hogy jelenleg minden kurzus kiválasztva van-e vagy sem.
- **exportCSV:** Az `exportCSV` metódus a `DataTable` komponens `exportCSV` funkcióját hívja meg, hogy exportálja az aktuális kurzuslistát CSV fájl formátumban.

### Stílusok és Animációk

CSS szabályok és animációk vannak definiálva a komponenshez, hogy javítsák a felhasználói élményt, mint például áttűnések és eltolódások a dialógusablakok és toast üzenetek megjelenítésekor.

### Integráció a Pinia Tárolókkal

A komponens integrálja a `userStore`, `groupStore`, `courseStore`, `languageStore` és `themeStore` tárolókat a felhasználói adatok, kurzusok, csoportok és nyelvi és téma beállítások kezeléséhez.

### Összefoglalás

A `CourseManagementPage` komponens egy komplex, de könnyen kezelhető felületet kínál a kurzusok kezelésére. Az integrált adatkezelési funkciók, a felhasználóbarát interfész és a rugalmas konfiguráció lehetővé teszi, hogy az oktatási intézmények hatékonyan kezeljék kurzusaikat. Ez a komponens elengedhetetlen része lehet minden olyan oktatási platformnak, ahol a kurzusok centralizált kezelése szükséges.

### `UserManagementPage`

> A `UserManagementPage` egy központi kezelőfelület, amely lehetővé teszi a felhasználók kezelését egy oktatási környezetben. A komponens segítségével az adminisztrátorok hozzáadhatnak, módosíthatnak, és törölhetnek felhasználókat, valamint beállíthatják azok adminisztratív jogosultságait.

### Komponens Struktúra

#### **Template Áttekintés:**

- **BaseLayout:** Az oldal alapvető elrendezési keretét biztosítja.
- **BaseSpinner:** Egy töltésjelző, amely a felhasználói adatok betöltése közben jelenik meg.
- **BaseDialog:** Dialógusablakok új felhasználó hozzáadásához és meglévő felhasználók módosításához.
- **BaseToast:** Üzenetek megjelenítése a felhasználói műveletek eredményéről.
- **Toolbar és DataTable:** Eszköztár és táblázat a felhasználók kezelésére, új felhasználó hozzáadásához, törléséhez és exportáláshoz.
- **BaseConfirmDialog:** Törlési folyamatok során jelenik meg, egy felugró ablak, ami megkérdezi a felhasználót, hogy valóban szeretné-e törölni az adott felhasználót.

### Script Részletek

#### **Belső Állapotok:**

- **users:** Tömb, amely a felhasználókat tartalmazza.
- **loading:** Boolean típusú változó, amely jelzi, hogy az oldal betöltése folyamatban van-e.
- **addUserDialogVisible, modifyUserDialogVisible:** Logikai változók, amelyek az új felhasználó hozzáadása és meglévő felhasználó módosítása dialógusablakok láthatóságát szabályozzák.
- **filters:** A `primeVue` által használatos szűrési feltételt tároló állapot.
- **selectedUsers:** A több felhasználó törlése esetén való kiválasztás során kerülnek bele a felhasználók.
- **currentlyModifyingUser:** A jelenleg modósítás alatt áló felhasználó kerül bele.
- **checked:** A `checked` állapot a felhasználó adminisztrátori státuszát jelzi, amely a rádiógomb segítségével szinkronizálva van a felhasználói adatmodell `is_admin` mezőjével.

#### **Metódusok:**

- **getUsers, postUser, deleteUser, deleteMultipleUsers, updateUser:** Funkciók, amelyek az adott felhasználókkal kapcsolatos API hívásokat kezelik.
- **selectAllUsers:** A `selectAllUsers` metódus kiválasztja vagy törli a kiválasztást minden felhasználóról a listából, attól függően, hogy jelenleg minden felhasználó kiválasztva van-e vagy sem.
- **exportCSV:** Az `exportCSV` metódus a `DataTable` komponens `exportCSV` funkcióját hívja meg, hogy exportálja az aktuális felhasználólistát CSV fájl formátumban.

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
- **messages**: A felhasználó által kiválaszott nyelvhez tartozó a `languageStore`-ból.

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

### `AssignmentPage`

A `AssignmentPage` oldal lehetővé teszi a diákok számára, hogy leadjanak feladatokat a tanárok számára.

## Komponens Struktúra

### Template Áttekintés:

- **BaseLayout:** Az oldal alapvető elrendezési keretét biztosítja.
- **BaseSpinner:** Egy töltésjelző, amely a felhasználói adatok betöltése közben jelenik meg.
- **BaseToast:** Üzenetek megjelenítése a felhasználói műveletek eredményéről.

### Script Részletek:

#### Belső Állapotok:

- **assignments:** Az Id alapján lekért feladatot tárolja.
- **isDeadlineReached:** Boolean változó, mely jelzi, hogy elérte-e a határidőt a feladat.
- **deadlineDate:** A feladat határideje.
- **loading:** Boolean változó, ami jelzi, hogy az oldal betöltése folyamatban van-e.

#### Metódusok:

- **downloadAssignment(assignmentId):** Letölti a feladathoz tartozó fájlt.
- **getAssignments():** Lekéri a szerkesztendő feladatot.
- **postNewStudentAssignment(data):** Elküldi az új tanulói feladatot a szerverre.
- **checkDeadline():** Ellenőrzi, hogy elértük-e a határidőt a feladatokhoz.

## Integráció a Pinia Tárolókkal

A komponens integrálja a `languageStore`, `userStore` és `themeStore` tárolókat a felhasználói adatok és témabeállítások kezeléséhez.

## Stílusok és Animációk

CSS szabályok és animációk vannak definiálva a komponenshez, hogy javítsák a felhasználói élményt.

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

> A `LanguageStore` a Pinia keretrendszerrel készült központi állapotkezelő tároló, amely az alkalmazás nyelvi beállításait és üzeneteit kezeli. Lehetővé teszi a dinamikus nyelvváltást, és az állapotot tartósítja, hogy a felhasználó preferenciái megmaradjanak az oldal újratöltésekor vagy a böngésző újraindításakor.

### **Tároló felépítése:**

### Állapot (state)

A tároló állapota a következőket tartalmazza:

- `language`: A jelenleg aktív nyelv kódja, alapértelmezés szerint "HU" (magyar).

#### Példa

```js
state() {
  return {
    language: "HU"
  };
}
```

### Műveletek (actions)

A tároló műveletei a következő funkciót tartalmazzák:

- `switchLanguage()`: Ez a metódus vált a két nyelv (magyar és angol) között. Az állapotban tárolt `language` értékét módosítja, majd a megfelelő nyelvi fájlt állítja be a `messages` getterrel.

```js
actions: {
  switchLanguage() {
    this.language = this.language === "HU" ? "EN" : "HU";
  }
}
```

### Getterek (getters)

A getterek dinamikusan biztosítják az aktuális nyelvhez tartozó üzeneteket.

- `messages`: A jelenlegi nyelvhez tartozó üzenetfájl tartalmát adja vissza.

```js
getters: {
  messages() {
    if (this.language == "HU") {
      return hu;
    }
    return en;
  }
}
```

### Állapot Tartósítása (Persistence)

> A `persist` tulajdonság beállításával a tároló állapota automatikusan megőrződik a böngésző lokális tárolójában vagy egy másik, konfigurálható tárolási helyen. Ez biztosítja, hogy a felhasználó által beállított nyelvi preferenciák megmaradjanak az oldal újratöltései és a böngésző újraindításai után is.

#### Használata:

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
    ...mapState(groupStore, ["groups"]),
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

## `CourseStore`

> A `CourseStore` egy Pinia állapotkezelő tároló, amely a kurzusok kezelését végzi. Ez a tároló felelős a kurzusok adatainak lekérdezéséért, frissítéséért és törléséért, valamint az új kurzusok létrehozásáért.

### Tároló Funkciói

#### **Műveletek (Actions):**

- **getCourses():** Lekéri az összes kurzus részletes adatait.
- **getCourse(id):** Lekéri egy specifikus kurzus részletes adatait azonosító alapján.
- **postCourse(data):** Új kurzus létrehozására szolgál, ahol a data tartalmazza a tanfolyam adatait.
- **putCourse(id, data):** Meglévő kurzus adatainak módosítását végzi az adott azonosító alapján.
- **destroyCourse(id):** Törli a megadott azonosítójú kurzust.

### Hitelesítés Kezelése

Minden API kérés során az aktuális felhasználó hitelesítési tokenjét csatoljuk a kérés fejlécéhez, ami biztosítja az adatok védelmét és az engedélyezett hozzáférést.

### Állapot Tartósítása

A tároló állapotát nem tartósítjuk, mivel a tanfolyamok dinamikus adatok, amelyek gyakran frissülnek. A frissesség biztosítása érdekében minden oldalbetöltéskor újra lekérjük őket.

### Használati Példa

A tároló használata egy Vue komponensben:

```js
import { mapActions } from "pinia";
import { courseStore } from "@stores/CourseStore";

export default {
  methods: {
    ...mapActions(courseStore, ["getCourse"]),
  },
  async mounted() {
    const course = await this.getCourse(1);
    console.log(course);
  },
};
```

## `NoteStore`

> A `NoteStore` egy Pinia állapotkezelő tároló, amely a jegyzetek kezelését végzi. Ez a tároló felelős például a jegyzet adatok lekérdezéséért, frissítéséért és törléséért, valamint az új jegyzetek létrehozásáért.

### Tároló Funkciói

#### **Műveletek (Actions):**

- **getNotes():** Lekéri az összes jegyzet részletes adatait.
- **getNote(id):** Lekéri egy specifikus jegyzet részletes adatait azonosító alapján.
- **postNote(data):** Új jegyzet létrehozására szolgál, ahol a data tartalmazza a jegyzet adatait.
- **putNote(id, data):** Meglévő jegyzet adatainak módosítását végzi az adott azonosító alapján.
- **destroyNote(id):** Törli a megadott azonosítójú jegyzetet.
- **getCurrentNotes():** Visszaadja a tanuló által létrehozott, tanár által közzétett jegyzeteket azok alapján, hogy melyik kurzushoz és csoporthoz tartozik.
- **getTeacherNotes():** Visszaadja az összes tanári jegyzetet aszerint, hogy az adott felhasználó részese-e a megfelelő kurzusnak, csoportnak.

### Hitelesítés Kezelése

Minden API kérés során az aktuális felhasználó hitelesítési tokenjét csatoljuk a kérés fejlécéhez, ami biztosítja az adatok védelmét és az engedélyezett hozzáférést.

### Állapot Tartósítása

A tároló állapotát nem tartósítjuk, mivel a jegyzetek dinamikus adatok, amelyek gyakran frissülnek. A frissesség biztosítása érdekében minden oldalbetöltéskor újra lekérjük őket.

### Használati Példa

A tároló használata egy Vue komponensben:

```js
import { mapActions } from "pinia";
import { noteStore } from "@stores/NoteStore";

export default {
  methods: {
    ...mapActions(noteStore, ["getNote"]),
  },
  async mounted() {
    const note = await this.getNote(1);
    console.log(note);
  },
};
```

## Őrök (guardok)

### `AdminGuard`

#### Funkció:
> A `AdminGuard` egy útvonalőrző (route guard) funkció, amely biztosítja, hogy csak az adminisztrátori jogosultsággal rendelkező felhasználók férhessenek hozzá bizonyos útvonalakhoz az alkalmazásban. Ha a felhasználónak nincs adminisztrátori jogosultsága, akkor a requiresAdmin metával ellátott útvonalakhoz történő hozzáférést korlátozza, és átirányítja a felhasználót egy meghatározott alapértelmezett útvonalra.

#### Importált Modulok:

- `userStore` a `@stores/UserStore.mjs `modulból: Ez a modul szolgál az aktuális felhasználó információinak lekérésére és kezelésére.

#### Paraméterek:

- **to:** A következő útvonal objektuma, ahová a felhasználó navigálni szeretne.
- **from:** A jelenlegi útvonal objektuma, ahonnan a felhasználó érkezik.
- **next:** A továbbhaladást irányító függvény, amellyel eldönthető, hogy a felhasználó átirányításra kerül-e vagy folytatja az aktuális útvonalat.

#### Működés:

1. A `userStore` segítségével ellenőrzi, hogy az aktuális felhasználó rendelkezik-e adminisztrátori jogosultsággal az `isAdmin` változó alapján.
2. Ha a felhasználó adminisztrátor:
    - Tovább engedi a felhasználót a következő útvonalra (`next()`).
3. Ha a felhasználó nem adminisztrátor:
    - Ha az új útvonal (`to`) rendelkezik a `requiresAdmin` metával:
      - Átirányítja a felhasználót a `home` útvonalra (`next({ name: 'home' })`).
    - Ha az új útvonal nem tartalmazza a `requiresAdmin` metát:
      - Tovább engedi a felhasználót a cél útvonalra (`next()`).

### `AttemptAccessGuard`

#### Funkció: 
> A `AttemptAccessGuard` egy útvonalőrző funkció, amely biztosítja, hogy csak az adott kísérlethez (attempt) jogosultsággal rendelkező felhasználók férhessenek hozzá a vonatkozó útvonalakhoz. Az `attemptStore` segítségével ellenőrzi, hogy a felhasználó jogosult-e a kísérlet megtekintésére vagy szerkesztésére.

#### Importált Modulok:

- `userStore` a `@stores/UserStore.mjs modulból`: Az aktuális felhasználó hitelesítési és jogosultsági állapotának lekérésére szolgál.
- `attemptStore` a `@stores/AttemptStore.mjs` modulból: A felhasználó kísérleti jogosultságainak lekérésére szolgál.

#### Segédfüggvények:

- `getPerms:` Ellenőrzi, hogy a felhasználó hozzáféréssel rendelkezik-e az aktuálisan megtekintett kísérlethez.
1. Az aktuális URL-ből (path) kinyeri a kísérlet azonosítóját (ID).
2. Ha az URL minta nem felel meg a kísérletekhez vezető útvonalnak, vagy nem a megfelelő alútvonalat próbálja elérni (pl. "mark"), akkor `false`-t ad vissza.
3. A felhasználó által hozzáférhető kísérleteket (attempts) lekérdezi az `attemptStore` segítségével.
4. Megkeresi, hogy az adott kísérlet (ID) benne van-e a felhasználó elérhető kísérletei között.
.5 Ha a kísérlet megtalálható, visszatér `true` értékkel, ellenkező esetben `false`-sal.

#### Paraméterek:

- **to:** A következő útvonal objektuma, ahová a felhasználó navigálni szeretne.
- **from:** A jelenlegi útvonal objektuma, ahonnan a felhasználó érkezik.
- **next:** A továbbhaladást irányító függvény, amely meghatározza, hogy a felhasználó átirányításra kerül-e vagy folytatja az aktuális útvonalat.

#### Működés:

1. Ellenőrzi, hogy a felhasználó hitelesített-e (`userStore().isAuthenticated`) és hogy jogosult-e a megtekintett kísérlethez (`getPerms`).
2. Ha mindkét feltétel teljesül:
    - Tovább engedi a felhasználót a következő útvonalra (`next()`).
3. Ha bármelyik feltétel nem teljesül:
    - Ha az új útvonal (`to`) rendelkezik a `attemptAccessGuard` metával:
      - Átirányítja a felhasználót a home útvonalra (`next({ name: 'home' })`).
    - Ha az új útvonal nem tartalmazza az `attemptAccessGuard` metát:
      - Tovább engedi a felhasználót a cél útvonalra (`next()`).

### `AttemptMarkGuard`

#### Funkció: 
> Az `AttemptMarkGuard` egy útvonalőrző funkció, amely biztosítja, hogy csak a tanári jogosultsággal rendelkező felhasználók férhessenek hozzá az adott kísérlet pontozási (mark) útvonalaihoz. Az `attemptStore` és a `userStore` modulok segítségével ellenőrzi, hogy a felhasználó jogosult-e a pontozási műveletekhez.

#### Importált Modulok:

- `userStore` a `@stores/UserStore.mjs` modulból: Az aktuális felhasználó hitelesítési és jogosultsági állapotának lekérésére szolgál.
- `attemptStore` a `@stores/AttemptStore.mjs` modulból: Az adott kísérlet csoportjainak és felhasználóinak kezelésére szolgál.

#### Segédfüggvény:

- ``getPerms``: Ellenőrzi, hogy a felhasználó rendelkezik-e tanári jogosultsággal az adott kísérletben.
1. Az aktuális URL-ből (path) kinyeri a kísérlet azonosítóját (ID).
2. Ha az URL minta nem felel meg a kísérletek pontozási útvonalának (pl. "mark"), akkor `false`-t ad vissza.
3. Lekéri az adott kísérlethez tartozó csoportok felhasználóit az `attemptStore` segítségével (`getAttemptGroups`).
4. Keresést végez az aktuális felhasználó azonosítója alapján az adott csoportokban.
5. Ellenőrzi, hogy a felhasználónak van-e tanári (Teacher) jogosultsága a csoportok bármelyikében.
6. Ha a felhasználónak tanári jogosultsága van, `true` értéket ad vissza, ellenkező esetben `false`-t.

#### Paraméterek:

- **to:** A következő útvonal objektuma, ahová a felhasználó navigálni szeretne.
- **from:** A jelenlegi útvonal objektuma, ahonnan a felhasználó érkezik.
- **next:** A továbbhaladást irányító függvény, amely meghatározza, hogy a felhasználó átirányításra kerül-e vagy folytatja az aktuális útvonalat.

#### Működés:

1. Ellenőrzi, hogy a felhasználó hitelesített-e (`userStore().isAuthenticated`) és rendelkezik-e tanári jogosultsággal a kísérlet pontozási útvonalához (getPerms).
2. Ha mindkét feltétel teljesül:
    - Tovább engedi a felhasználót a következő útvonalra (`next()`).
3. Ha bármelyik feltétel nem teljesül:
    - Ha az új útvonal (`to`) rendelkezik az `attemptMarkGuard` metával:
      - Átirányítja a felhasználót a `home` útvonalra (`next({ name: 'home' })`).
    - Ha az új útvonal nem tartalmazza az `attemptMarkGuard` metát:
      - Tovább engedi a felhasználót a cél útvonalra (`next()`).

### `AuthGuard`

#### Funkció: 
> Az `AuthGuard` egy útvonalőrző funkció, amely ellenőrzi, hogy a felhasználó hitelesített-e, és ennek megfelelően irányítja az alkalmazáson belüli navigációt. Az olyan útvonalak esetében, amelyekhez hitelesítés szükséges, biztosítja, hogy csak bejelentkezett felhasználók férjenek hozzá, míg az olyan útvonalakhoz, mint például a `login` vagy `register`, átirányítja a már hitelesített felhasználókat egy másik oldalra.

#### Importált Modulok:

- `userStore` a `@stores/UserStore.mjs` modulból: Az aktuális felhasználó hitelesítési és jogosultsági állapotának lekérésére szolgál.

#### Paraméterek:

- **to:** A következő útvonal objektuma, ahová a felhasználó navigálni szeretne.
- **from:** A jelenlegi útvonal objektuma, ahonnan a felhasználó érkezik.
- **next:** A továbbhaladást irányító függvény, amely meghatározza, hogy a felhasználó átirányításra kerül-e vagy folytatja az aktuális útvonalat.

#### Működés:

1. Ha a felhasználó hitelesített (`userStore().isAuthenticated`):
    - Ellenőrzi, hogy az új útvonal (`to`) a `login` vagy `register` oldalra irányul-e.
    - Ha igen, átirányítja a felhasználót a home oldalra (`next({ name: 'home' })`).
    - Egyébként tovább engedi a felhasználót a cél útvonalra (`next()`).
2. Ha a felhasználó nem hitelesített:
    - Ha az új útvonal (`to`) rendelkezik a `requiresAuth` metával:
      - Átirányítja a felhasználót a `login` oldalra (`next({ name: 'login' })`).
    - Ha az új útvonal nem tartalmazza a `requiresAuth` metát:
        - Tovább engedi a felhasználót a cél útvonalra (`next()`).

### `QuizAccessGuard`

#### Funkció: 
> A `QuizAccessGuard` egy útvonalőrző funkció, amely ellenőrzi, hogy a felhasználó jogosult-e egy adott kvízhez való hozzáféréshez. Az `quizStore` és a `userStore` modulok segítségével határozza meg a felhasználó jogosultságait, és biztosítja, hogy csak tanárok és diákok érjék el a kívánt útvonalakat.

#### Importált Modulok:

- `userStore` a `@stores/UserStore.mjs` modulból: Az aktuális felhasználó hitelesítési és jogosultsági állapotának lekérésére szolgál.
- `quizStore` a `@stores/QuizStore.mjs` modulból: Az adott kvíz részleteinek és csoportjainak lekérésére szolgál.

#### Segédfüggvény:

- `getPerms`: Ellenőrzi, hogy a felhasználó rendelkezik-e megfelelő jogosultsággal az adott kvíz megtekintéséhez vagy szerkesztéséhez.
1. Az aktuális URL-ből (`path`) kinyeri a kvíz azonosítóját (ID).
2. Ha az URL minta nem egyezik a kvíz útvonalaival vagy az "edit" alútvonallal, akkor `false`-t ad vissza.
3. Lekéri a kvízhez tartozó csoportok felhasználóit a `quizStore` segítségével (`getQuiz`).
4. Megkeresi az aktuális felhasználót az adott csoportokban.
5. Ellenőrzi, hogy a felhasználónak van-e "Tanár" vagy "Tanuló" jogosultsága.
6. Ha a felhasználó tanár vagy tanuló, true értéket ad vissza, ellenkező esetben `false`-t.

#### Paraméterek:

- **to:** A következő útvonal objektuma, ahová a felhasználó navigálni szeretne.
- **from:** A jelenlegi útvonal objektuma, ahonnan a felhasználó érkezik.
- **next:** A továbbhaladást irányító függvény, amely meghatározza, hogy a felhasználó átirányításra kerül-e vagy folytatja az aktuális útvonalat.

#### Működés:

1. Ellenőrzi, hogy a felhasználó hitelesített-e (`userStore().isAuthenticated`) és hogy rendelkezik-e tanári vagy tanulói jogosultsággal az adott kvízhez (`getPerms`).
2. Ha mindkét feltétel teljesül:
    - Tovább engedi a felhasználót a következő útvonalra (`next()`).
3. Ha bármelyik feltétel nem teljesül:
    - Ha az új útvonal (`to`) rendelkezik a `quizAccessGuard` metával:
      - Átirányítja a felhasználót a home oldalra (`next({ name: 'home' })`).
    - Ha az új útvonal nem tartalmazza a `quizAccessGuard` metát:
      - Tovább engedi a felhasználót a cél útvonalra (`next()`).

### `QuizCreationGuard`

### Funkció: 
> A `QuizCreationGuard` egy útvonalőrző funkció, amely ellenőrzi, hogy a felhasználó rendelkezik-e tanári jogosultsággal egy adott kurzuson belül kvíz létrehozásához vagy szerkesztéséhez. Az `userStore`, a `courseStore` és a `quizStore` segítségével ellenőrzi a hozzáférési jogosultságokat.

#### Importált Modulok:

- `userStore` a `@stores/UserStore.mjs` modulból: Az aktuális felhasználó - hitelesítési és jogosultsági állapotának lekérésére szolgál.
- `courseStore` a `@stores/CourseStore.mjs` modulból: Az adott kurzus részleteinek és csoportjainak lekérésére szolgál.
- `quizStore` a `@stores/QuizStore.mjs` modulból: Az adott kvíz részleteinek és csoportjainak lekérésére szolgál.

#### Segédfüggvény:

- `getCourse`: Ellenőrzi, hogy a felhasználó tanári jogosultsággal rendelkezik-e az adott kurzusban vagy kvízben.
1. Az aktuális URL-ből (`path`) kinyeri a kurzus vagy kvíz azonosítóját (ID).
2. Ha az URL minta nem tartalmazza a kvíz vagy kurzus szerkesztési vagy létrehozási alútvonalakat (pl. "edit", "create-quiz"), akkor `false`-t ad vissza.
3. Ha az URL a kvíz szerkesztéséhez kapcsolódik (`quiz`):
    - Lekéri a kvízhez tartozó kurzus csoportjait a `quizStore` segítségével (`getQuiz`).
4. Ha az URL a kurzus létrehozásához kapcsolódik (`create-quiz`):
    - Lekéri a kurzus csoportjait a `courseStore` segítségével (`getCourse`).
5. Megkeresi az aktuális felhasználót az adott csoportokban.
6. Ellenőrzi, hogy a felhasználónak van-e tanári jogosultsága ("Tanár").
7. Ha a felhasználó tanár, true értéket ad vissza, ellenkező esetben `false`-t.

#### Paraméterek:

- **to:** A következő útvonal objektuma, ahová a felhasználó navigálni szeretne.
- **from:** A jelenlegi útvonal objektuma, ahonnan a felhasználó érkezik.
- **next:** A továbbhaladást irányító függvény, amely meghatározza, hogy a felhasználó átirányításra kerül-e vagy folytatja az aktuális útvonalat.

#### Működés:

1. Ellenőrzi, hogy a felhasználó hitelesített-e (`userStore().isAuthenticated`) és rendelkezik-e tanári jogosultsággal a kvíz létrehozásához vagy szerkesztéséhez (`getCourse`).
2. Ha mindkét feltétel teljesül:
    - Tovább engedi a felhasználót a következő útvonalra (`next()`).
3. Ha bármelyik feltétel nem teljesül:
    - Ha az új útvonal (`to`) rendelkezik a `quizCreationGuard` metával:
      - Átirányítja a felhasználót a home oldalra (`next({ name: 'home' })`).
    - Ha az új útvonal nem tartalmazza a `quizCreationGuard` metát:
      - Tovább engedi a felhasználót a cél útvonalra (`next()`).

### `HTTP Interceptor`

#### Funkció: 
> A `http.interceptors.response.use` egy HTTP interceptor, amely figyeli az API-válaszokat, hogy megfelelően kezelje az esetleges hibakódokat. Ha az API-hívás során egy adott hibakódot, például `401 Unauthorized` kap, automatikusan kijelentkezteti a felhasználót, és átirányítja a bejelentkezési oldalra.

#### Működés:

1. **Pozitív válasz:** Ha a válasz sikeres, visszaadja a `response` objektumot, lehetővé téve az adatok felhasználását.
2. **Hibás válasz:** Ha hiba történik a kérések során, a következőket teszi:
    - Ellenőrzi, hogy a válasz státuszkódja `401 Unauthorized`-e.
    - Ha a státuszkód `401`:
      - Lekéri a `userStore` aktuális állapotát.
      - Meghívja a `logout` függvényt a felhasználó kijelentkeztetéséhez.
      - Átirányítja a felhasználót a bejelentkezési oldalra (`router.push("/login")`).

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

## Felhasznált csomagok

- [PrimeVue](https://primevue.org)
- [Quill](https://www.npmjs.com/package/quill)
- [PrimeIcons](https://www.npmjs.com/package/primeicons)
- [Vue Devtools](https://www.npmjs.com/package/vite-plugin-vue-devtools)
- [Vue Router](https://router.vuejs.org)
- [Bootstrap](https://getbootstrap.com/docs/5.3/getting-started/introduction/)
- [Axios](https://axios-http.com/docs/intro)
- [FormKit](https://formkit.com)
- [Pinia](https://pinia.vuejs.org)
- [Font Awesome](https://fontawesome.com)

## Felhasznált források

Oktatási Hivatal, A megjelenés éve: 2023, TÖRTÉNELEM EMELT SZINTŰ ÍRÁSBELI VIZSGA. [PDF] URL: https://dload-oktatas.educatio.hu/erettsegi/feladatok_2023tavasz_emelt/e_tort_23maj_fl.pdf Megtekintve/Letöltve: 2024. 04. 30.
