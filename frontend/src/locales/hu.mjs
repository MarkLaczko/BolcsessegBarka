export default {
  pages: {
    loginPage: {
      title: "Bejelentkezés",
      emailPlaceholder: "E-mail",
      passwordPlaceholder: "Jelszó",
      loginButtonText: "Bejelentkezés",
      accountText: "Még nincs fiókja?",
      loginFailed: "Sikertelen belépés!"
    },
    homePage: {
      title: "Üdvözöllek",
      taskTitle: "Elkészítendő feladatok",
      materialsTitle: "Legutóbbi tananyagok",
    },
    coursesPage: {
      title: "Kurzusok",
    },
    userManagementPage: {
      title: "Felhasználók kezelése",
      newUser: " Új felhasználó",
      deleteUser: " Törlés",
      exportButton: " Exportálás",
      namePlaceholder: "Név...",
      idText: "Azonosító",
      nameText: "Név",
      adminText: "Adminisztrátor",
      modifyText: "Módosítás",
      deleteText: "Törlés",
      newUserDialog: {
        title: "Felhasználó hozzáadása",
        nameLabel: "Név",
        emailLabel: "Email",
        passwordLabel: "Jelszó",
        confirmPasswordLabel: "Jelszó megerősítése",
        saveButton: "Mentés",
        cancelButton: "Mégsem",
        validationMessages: {
          nameRequired: "A felhasználónév kitöltése kötelező.",
          emailRequired: "Az email kitöltése kötelező.",
          passwordRequired: "A jelszó kitöltése kötelező.",
          confirmPasswordRequired: "A jelszó megerősítés kitöltése kötelező.",
          matchAllValidationMessage:
            "Sajnáljuk, nem minden mezőt töltöttek ki helyesen.",
          nameLength:
            "A felhasználónévnek kevesebbnek kell lennie, mint 255 karakter.",
          validEmail: "Adjon meg egy érvényes email címet.",
          passwordLength:
            "Legalább 8, maximum 255 karakter hosszú lehet a jelszó.",
          confirmPasswordLength:
            "Legalább 8, maximum 255 karakter hosszú lehet a jelszó megerősítés.",
          confirmPasswordConfirm: "A jelszavak nem egyeznek.",
        },
      },
      editUserDialog: {
        title: "Módosítása:",
        nameLabel: "Név",
        emailLabel: "Email",
        passwordLabel: "Jelszó",
        confirmPasswordLabel: "Jelszó megerősítése",
        isAdmin: "Admin-E",
        saveButton: "Mentés",
        cancelButton: "Mégsem",
        validationMessages: {
          nameRequired: "A felhasználónév kitöltése kötelező.",
          emailRequired: "Az email kitöltése kötelező.",
          matchAllValidationMessage:
            "Sajnáljuk, nem minden mezőt töltöttek ki helyesen.",
          nameLength:
            "A felhasználónévnek kevesebbnek kell lennie, mint 255 karakter.",
          validEmail: "Adjon meg egy érvényes email címet.",
          passwordLength:
            "Legalább 8, maximum 255 karakter hosszú lehet a jelszó.",
          confirmPasswordLength:
            "Legalább 8, maximum 255 karakter hosszú lehet a jelszó megerősítés.",
          confirmPasswordConfirm: "A jelszavak nem egyeznek.",
        },
      },
      toastMessages: {
        successfullyCreatedUser: "Felhasználó hozzáadása sikeres volt!",
        failedToCreateUser: "Felhasználó hozzáadása sikertelen volt!",
        successfullyDeletedUser: "Felhasználó törlése sikeres volt!",
        failedToDeleteUser: "Felhasználó törlése sikertelen volt!",
        successfullyDeletedMultipleUsers:
          "Felhasználó(k) törlése sikeres volt!",
        failedToDeleteMultipleUsers: "Felhasználó(k) törlése sikertelen volt!",
        successfullyUpdatedUser: "Felhasználó módosítása sikeres volt!",
        failedToUpdateUser: "Felhasználó módosítása sikertelen volt!",
      },
    },
    groupManagementPage: {
      title: "Csoportok kezelése",
      newGroup: " Új csoport",
      deleteGroup: " Törlés",
      exportButton: " Exportálás",
      namePlaceholder: "Név...",
      idText: "Azonosító",
      nameText: "Név",
      member: "tag",
      course: "kurzus",
      memberText: "Tagok száma",
      courseText: "Kurzusok száma",
      modifyText: "Módosítás",
      deleteText: "Törlés",
      newGroupDialog: {
        title: "Csoport hozzáadása",
        nameLabel: "Név",
        saveButton: "Mentés",
        cancelButton: "Mégsem",
        validationMessages: {
          nameRequired: "A csoport neve mező kitöltése kötelező.",
          nameLength:
            "A felhasználónévnek kevesebbnek kell lennie, mint 100 karakter.",
          matchAllValidationMessage:
            "Sajnáljuk, nem minden mezőt töltöttek ki helyesen.",
        },
      },
      editGroupDialog: {
        title: "Módosítása:",
        idLabel: "Azonosító",
        nameLabel: "Név",
        emailLabel: "Email",
        permissionLabel: "Jogosultság",
        namePlaceholder: "Név...",
        emailPlaceholder: "Email...",
        saveButton: "Mentés",
        cancelButton: "Mégsem",
        validationMessages: {
          matchAllValidationMessage:
            "Sajnáljuk, nem minden mezőt töltöttek ki helyesen.",
          nameRequired: "A csoport neve mező kitöltése kötelező.",
          nameLength:
            "A csoport nevének kevesebbnek kell lennie, mint 100 karakter.",
        },
      },
      toastMessages: {
        successfullyCreatedGroup: "Csoport hozzáadása sikeres volt!",
        failedToCreateGroup: "Csoport hozzáadása sikertelen volt!",
        successfullyDeletedGroup: "Csoport törlése sikeres volt!",
        failedToDeleteGroup: "Csoport törlése sikertelen volt!",
        successfullyDeletedMultipleGroups: "Csoport(ok) törlése sikeres volt!",
        failedToDeleteMultipleGroups: "Csoport(ok) törlése sikertelen volt!",
        successfullyUpdatedGroup: "Csoport módosítása sikeres volt!",
        failedToUpdateGroup: "Csoport módosítása sikertelen volt!",
      },
    },
    courseManagementPage: {
      title: "Kurzusok kezelése",
      newUser: " Új kurzus",
      deleteUser: " Törlés",
      exportButton: " Exportálás",
      namePlaceholder: "Név...",
      idText: "Azonosító",
      nameText: "Név",
      imageText: "Kép",
      modifyText: "Módosítás",
      deleteText: "Törlés",
      newCourseDialog: {
        title: "Kurzus hozzáadása",
        nameLabel: "Kurzus neve",
        saveButton: "Mentés",
        cancelButton: "Mégsem",
        validationMessages: {
          nameRequired: "A kurzus nevének kitöltése kötelező.",
          nameLength:
            "A kurzus nevének kevesebbnek kell lennie, mint 255 karakter.",
          imageRequired: "A kép kitöltése kötelező.",
          fileUpload: "Kép feltöltése",
          matchAllValidationMessage:
            "Sajnáljuk, nem minden mezőt töltöttek ki helyesen.",
        },
      },
      editCourseDialog: {
        title: "Módosítása:",
        idLabel: "Azonosító",
        nameLabel: "Név",
        emailLabel: "Email",
        permissionLabel: "Jogosultság",
        namePlaceholder: "Név...",
        emailPlaceholder: "Email...",
        saveButton: "Mentés",
        cancelButton: "Mégsem",
        validationMessages: {
          matchAllValidationMessage:
            "Sajnáljuk, nem minden mezőt töltöttek ki helyesen.",
          fileUpload: "Kép hozzáadása",
          nameLength:
            "A kurzus nevének kevesebbnek kell lennie, mint 255 karakter.",
        },
      },
      toastMessages: {
        successfullyCreatedCourse: "Kurzus hozzáadása sikeres volt!",
        failedToCreateCourse: "Kurzus hozzáadása sikertelen volt!",
        successfullyDeletedCourse: "Kurzus törlése sikeres volt!",
        failedToDeleteCourse: "Kurzus törlése sikertelen volt!",
        successfullyDeletedMultipleCourses: "Kurzus(ok) törlése sikeres volt!",
        failedToDeleteMultipleCourses: "Kurzus(ok) törlése sikertelen volt!",
        successfullyUpdatedCourse: "Kurzus módosítása sikeres volt!",
        failedToUpdateCourse: "Kurzus módosítása sikertelen volt!",
      },
    },
    assignmentPage: {
      dragFileText: "Húzza ide a fájlokat a feltöltéshez",
      cancelButton: "Mégsem",
    },
    notePage: {
      title: "Jegyzet létrehozása",
      notesNameText: "Jegyzet neve:",
      cancelButton: "Mégsem",
      saveButton: "Mentés",
    },
  },
  layout: {
    header: {
      home: "Főoldal",
      courses: "Kurzusaim",
      admin: "Adminisztráció",
      userAdministration: "Felhasználói kezelése",
      groupAdministration: "Csoportok kezelése",
      courseAdministration: "Kurzusok kezelése",
      logout: "Kijelentkezés",
    },
    footer: {},
  },
  components: {
    BaseCourseCard: {
      viewButton: "Megtekintés",
    },
    BaseAssignmentCard: {
      buttonTitle: "Feladat leadása",
      deadline: "Leadási határidő",
    },
    BaseLearningMaterialCard: {
      courseTitle: "Kurzus",
      viewButton: "Megtekintés",
    },
  },
};
