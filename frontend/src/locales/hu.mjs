export default {
  pages: {
    registerPage: {
      title: "Regisztráció",
      submitButton: "Regisztráció",
      accountText: "Már van fiókja?",
      namePlaceholder: "Név",
      emailPlaceholder: "E-mail",
      passwordPlaceholder: "Jelszó",
      passwordConfirmPlaceholder: "Jelszó megerősítés",
      validationMessages: {
        matchAllValidationMessage:
          "Sajnáljuk, nem minden mezőt töltöttek ki helyesen.",
        nameRequired: "A felhasználónév kitöltése kötelező.",
        nameLength:
          "A felhasználónévnek kevesebbnek kell lennie, mint 255 karakter.",
        emailRequired: "Az email kitöltése kötelező.",
        validEmail: "Adjon meg egy érvényes email címet.",
        passwordRequired: "A jelszó kitöltése kötelező.",
        passwordLength:
          "Legalább 8, maximum 255 karakter hosszú lehet a jelszó.",
        confirmPasswordRequired: "A jelszó megerősítés kitöltése kötelező.",
        confirmPasswordLength:
          "Legalább 8, maximum 255 karakter hosszú lehet a jelszó megerősítés.",
        confirmPasswordConfirm: "A jelszavak nem egyeznek.",
      },
      registerFailed: "Sikertelen regisztráció!",
      registerSucceeded: "Sikeres regisztráció!",
    },
    loginPage: {
      title: "Bejelentkezés",
      emailPlaceholder: "E-mail",
      passwordPlaceholder: "Jelszó",
      loginButtonText: "Bejelentkezés",
      accountText: "Még nincs fiókja?",
      loginFailed: "Sikertelen belépés!",
    },
    homePage: {
      title: "Üdvözöllek",
      taskTitle: "Elkészítendő feladatok",
      materialsTitle: "Legutóbbi tananyagok",
      noMaterialsTitle: "Nincsenek tananyagok!", 
      noAssignmentsTitle: "Nincsenek feladataid!",
      viewNoteDialog: {
        title: "Jegyzet megtekintése",
        noteName: "Jegyzet címe:",
        cancelButton: "Vissza"
      }
    },
    coursesPage: {
      title: "Kurzusok",
      youDontHaveCoursesText: "Nincsenek kurzusaid!",
    },
    coursePage: {
      newTopicButton: "Új téma",
      deleteButton: "Törlés",
      editButton: "Szerkesztés",
      viewButton: "Megtekintés",
      cancelButton: "Mégsem",
      teacherText: "Tanári",
      groupTreatmentButton: "Csoportok kezelése",
      confirmDialogs: {
        messageTopic: "Biztos ki akarja törölni ezt a témát?",
        messageAssignment: "Biztos ki akarja törölni ezt a feladatot?",
        messageNote: "Biztos ki akarja törölni ezt a jegyzetet?",
        rejectLabel: "Mégsem",
        acceptLabel: "Törlés"
      },
      updateAssignmentDialog: {
        uploadedFile: "Feltöltött fájl:",
        noUploadedFile: "Nincs feltöltött feladat",
        successfullyModifyAssignment: "Sikeresen módosítottad a feladatot!",
        failedToModifyAssignment: "Sikertelen volt a feladat módosítása!"
      },
      downloadAssignmentDialog: {
        title: "Leadott feladatok megtekintése",
        noAssignment: "Nincs leadott feladat!",
        student: "Diák",
        task: "Feladat",
        download: "Letöltés",
        downloadAllTasks: "Összes feladat letöltése",
        close: "Bezárás"
      },
      newTopicDialog: {
        title: "Új téma felvétele",
        nameLabel: "Név",
        saveButton: "Mentés",
        cancelButton: "Mégsem",
        validationMessages: {
          nameRequired: "A téma nevének kiválasztása kötelező.",
          nameLength:
            "A téma nevének kevesebbnek kell lennie, mint 60 karakter.",
          matchAllValidationMessage:
            "Sajnáljuk, nem minden mezőt töltöttek ki helyesen.",
        },
      },
      editTopicDialog: {
        title: "Téma modósítása",
        nameLabel: "Név",
        saveButton: "Mentés",
        cancelButton: "Mégsem",
        validationMessages: {
          nameLength:
            "A téma nevének kevesebbnek kell lennie, mint 60 karakter.",
          matchAllValidationMessage:
            "Sajnáljuk, nem minden mezőt töltöttek ki helyesen.",
        },
      },
      deleteTopic: {
        toastMessages: {
          successfullyDeletedTopic: "A téme törlése sikeres volt!",
          failedToDeleteTopic: "A téme törlése sikertelen volt!"
        }
      },
      addTopic: {
        toastMessages: {
          successfulyCreatedTopic: "A téma létrehozása sikeres volt!",
          failedToCreateTopic: "A téma létrehozása sikertelen volt!",
        }
      },
      editTopic: {
        toastMessages: {
          successfullyUpdatedTopic: "A téma modósítása sikeres volt!",
          failedToUpdateTopic: "A téma modósítása sikertelen volt!",
        }
      },
      groupTreatmentDialog: {
        title: "Csoportok kezelése",
        multiSelectPlaceholder: "Csoportok kiválasztása",
        cancelButton: "Mégsem",
        saveButton: "Mentés"
      },
      saveGroups: {
        toastMessages: {
          successfullyUpdatedGroups: "A csoportok modósítása sikeres volt!",
          failedToUpdateGroups: "A csoportok modósítása sikertelen volt!",
        }
      },
      newNoteDialog: {
        title: "Jegyzet létrehozása",
        notesNameText: "Jegyzet neve:",
        cancelButton: "Mégsem",
        saveButton: "Mentés"
      },
      currentNoteDialog: {
        title: "Jegyzet modósítása",
        notesNameText: "Jegyzet neve:",
        cancelButton: "Mégsem",
        saveButton: "Mentés",
        editButton: "Szerkesztés",
        deleteButton: "Törlés"
      },
      viewNoteDialog: {
        title: "Jegyzet megtekintése",
        noteName: "Jegyzet címe:",
        cancelButton: "Vissza",
      },
      accordionText: {
          actions: "Műveletek",
          newAssignment: "Új feladat",
          newNote: "Új jegyzet",
          newQuiz: "Új kvíz",
          edit: "Szerkesztés",
          delete: "Törlés",
          createNoteButton: "Jegyzet létrehozása"
      },
      note: {
        name: "Jegyzet",
        text: "Jegyzet címe:",
        viewButton: "Megtekintés",
        toastMessages: {
          successfullyCreatedNote: "Jegyzet elmentése sikeres volt!",
          failedToCreateNote: "Jegyzet elmentése sikertelen volt!",
          successfullyUpdatedNote: "Jegyzet frissítése sikeres volt!",
          failedToUpdateNote: "Jegyzet frissítése sikertelen volt!",
          successfullyDeletedNote: "Jegyzet törlése sikeres volt!",
          failedToDeleteNote: "Jegyzet törlése sikertelen volt!"
        }
      },
      quiz: {
        name: "Kvíz",
        viewButton: "Megtekintés",
        opens: "Nyílik",
        closes: "Zárul",
        time: "Időkorlát",
        attempts: "Maximum próbálkozások",
        minutes: "perc"
      },
      deleteAssignment: {
        toastMessages: {
          successfullyDeletedAssignment: "A feladat törlése sikeres volt!",
          failedToDeleteAssignment: "A feladat törlése sikertelen volt!",
        }
      }
    },
    markQuizPage: {
      attemptOf: "próbálkozása",
      started: "Kezdte",
      finsihed: "Befejezte",
      late: "Késve leadva",
      maxMarks: "Maximum pontszám",
      marks: "Elért pontszám",
      save: "Mentés",
      noAnswer: "Nincs válasz",
      correctAnswer: "Helyes válasz",
      overallMarks: "Elért pontszám összesen",
      grade: "Értékelés",
      finish: "Befejezés",
      toastMessages: {
        updatedMark: "Pontszám frissítve!",
        failedUpdateMark: "A pontszám frissítése sikertelen!",
        graded: "Értékelve!",
        failedToGrade: "Az értékelés elküldése sikertelen!"
      }
    },
    attemptPage: {
      removeOption: "Törlés",
    },
    quizPage: {
      opens: "Nyílik",
      closes: "Zárul",
      time: "Időkorlát",
      attempts: "Maximum próbálkozások",
      minutes: "perc",
      userAttempts: "Próbálkozások",
      user: "Felhasználó",
      begin: "Kezdte",
      finished: "Végzett",
      marks: "Pontszám",
      grade: "Érdemjegy",
      evaluate: "Értékelés",
      startAttempt: "Próbálkozás indítása",
      ownAttempts: "Saját próbálkozásaim",
      beginAttemptDialog: {
        begin: "Kvíz indítása",
        time: "Felhasználható idő",
        minutes: "perc",
        confirm: "Biztos ki akarja tölteni ezt a kvízt?",
        attemptOnce: "Ezt a kvízt csak egyszer lehet kitölteni.",
        attemptMultipleStart: "Ezt a kvízt",
        attemptMultipleMiddle: "akárhány",
        attemptMultipleEnd: "alkalommal lehet kitölteni",
        cancel: "Mégse",
        start: "Kezdés",
      }
    },
    createQuizPage: {
      title: "Kvíz létrehozása",
      topic: "Téma",
      name: "Név",
      attempts: "Max. próbálkozások száma",
      opens: "Kvíz nyitása",
      closes: "Kvíz zárása",
      time: "Időkorlát (perc)",
      submit: "Mentés",
      validationMessages: {
        matchAllValidationMessage: "Sajnáljuk, nem minden mezőt töltöttek ki helyesen.",
        nameLength: "A név maximum 100 karakter hosszú lehet.",
        nameRequired: "A név kitöltése kötelező.",
        attemptsMin: "A próbálkozások száma nem lehet kevesebb, mitn 0.",
        timeMin: "Az időkorlát nem lehet kevesebb, mitn 0.",
      },
      toastMessages: {
        unexpectedError: "Váratlan hiba a kvíz létrehozásakor!",
      }
    },
    editQuizPage: {
      title: "szerkesztése",
      name: "Név",
      attempts: "Max. próbálkozások száma",
      opens: "Kvíz nyitása",
      closes: "Kvíz zárása",
      time: "Időkorlát (perc)",
      submit: "Mentés",
      tasks: "Feladatok",
      validationMessages: {
        matchAllValidationMessage: "Sajnáljuk, nem minden mezőt töltöttek ki helyesen.",
        nameLength: "A név maximum 100 karakter hosszú lehet.",
        nameRequired: "A név kitöltése kötelező.",
        attemptsMin: "A próbálkozások száma nem lehet kevesebb, mitn 0.",
        timeMin: "Az időkorlát nem lehet kevesebb, mitn 0.",
      },
      toastMessages: {
        updateSuccess: "Kvíz frissítve!",
        updateUnexpectedError: "Váratlan hiba a kvíz frissítésekor!",
        changeOrderUnexpectedError: "Váratlan hiba a feladatok sorrendjének módosításakor!",
        deleteSuccess: "Sikeres törlés!",
        deleteUnexpectedError: "Váratlan hiba a törléskor!"
      },
      confirmDialogs: {
        deleteTask: "Biztos ki akarja törölni ezt a feladatot és az összes hozzá tartozó alfeladatot?",
        cancel: "Mégse",
        confirm: "Törlés"
      }
    },
    createTaskPage: {
      title: "Feladat hozzáadása",
      header: "Fejléc",
      text: "Rövid szöveg",
      subtaskQuestion: "Alfeladat szövege",
      subtaskType: "Alfeladat típusa",
      subtaskOptions: "Válaszlehetőségek",
      subtaskOptionsInstructions: "Kezdje el gépelni a válaszlehetőségeket! Az Enter billentyűvel tudja hozzáadni a válaszokat.",
      subtaskSolution: "Helyes válaszok",
      subtaskSolutionInstructions: "Kezdje el gépelni a helyes válaszokat! Az Enter billentyűvel tudja hozzáadni a helyes válaszokat.",
      subtaskMarks: "Alfeladat pontszáma",
      cancel: "Mégse",
      submit: "Mentés",
      typeOptions: {
        shortAnswer: "Rövid válasz",
        multipleChoice: "Több válasz",
        essay: "Esszé"
      },
      validationMessages: {
        matchAllValidationMessage: "Sajnáljuk, nem minden mezőt töltöttek ki helyesen.",
        headerLength: "A fejléc maximum 100 karakter hosszú lehet.",
        headerRequired: "A fejléc kitöltése kötelező.",
        textLength: "A rövid szöveg maximum 255 karakter hosszú lehet.",
        textRequired: "A rövid szöveg kitöltése kötelező.",
        marksRequired: "A pontszám kitöltése kötelező.",
        marksMin: "A pontszám nem lehet kevesebb, mitn 0.",
        allQuestionRequired: "Minden kérdés szövege kötelező!"
      },
      toastMessages: {
        createUnexpectedError: "Váratlan hiba a feladat létrehozásakor!",
        changeOrderUnexpectedError: "Váratlan hiba az alfeladatok sorrendjének módosításakor!",
        deleteSuccess: "Sikeres törlés!",
        deleteUnexpectedError: "Váratlan hiba a törléskor!"
      },
      confirmDialogs: {
        deleteTask: "Biztos ki akarja törölni ezt az alfeladatot?",
        cancel: "Mégse",
        confirm: "Törlés"
      }
    },
    editTaskPage: {
      title: "Feladat szerkesztése",
      header: "Fejléc",
      text: "Rövid szöveg",
      subtaskQuestion: "Alfeladat szövege",
      subtaskType: "Alfeladat típusa",
      subtaskOptions: "Válaszlehetőségek",
      subtaskOptionsInstructions: "Kezdje el gépelni a válaszlehetőségeket! Az Enter billentyűvel tudja hozzáadni a válaszokat.",
      subtaskSolution: "Helyes válaszok",
      subtaskSolutionInstructions: "Kezdje el gépelni a helyes válaszokat! Az Enter billentyűvel tudja hozzáadni a helyes válaszokat.",
      subtaskMarks: "Alfeladat pontszáma",
      cancel: "Mégse",
      submit: "Mentés",
      typeOptions: {
        shortAnswer: "Rövid válasz",
        multipleChoice: "Több válasz",
        essay: "Esszé"
      },
      validationMessages: {
        matchAllValidationMessage: "Sajnáljuk, nem minden mezőt töltöttek ki helyesen.",
        headerLength: "A fejléc maximum 100 karakter hosszú lehet.",
        headerRequired: "A fejléc kitöltése kötelező.",
        textLength: "A rövid szöveg maximum 255 karakter hosszú lehet.",
        textRequired: "A rövid szöveg kitöltése kötelező.",
        marksRequired: "A pontszám kitöltése kötelező.",
        marksMin: "A pontszám nem lehet kevesebb, mitn 0.",
        allQuestionRequired: "Minden kérdés szövege kötelező!"
      },
      toastMessages: {
        updateUnexpectedError: "Váratlan hiba a feladat frissítésekor!",
        changeOrderUnexpectedError: "Váratlan hiba az alfeladatok sorrendjének módosításakor!",
        deleteSuccess: "Sikeres törlés!",
        deleteUnexpectedError: "Váratlan hiba a törléskor!"
      },
      confirmDialogs: {
        deleteTask: "Biztos ki akarja törölni ezt az alfeladatot?",
        cancel: "Mégse",
        confirm: "Törlés"
      }
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
      confirmDialogs: {
        message: "Biztos ki akarja törölni ezt a felhasználó(ka)t?",
        rejectLabel: "Mégsem",
        acceptLabel: "Törlés"
      }
    },
    profilePage: {
      title: 'Profil szerkesztése',
      nameLabel: "Név",
      emailLabel: "Email",
      currentPasswordLabel: "Jelenlegi jelszó",
      passwordLabel: "Új jelszó",
      confirmPasswordLabel: "Új jelszó megerősítése",
      submitButton: "Mentés",
      changePassword: "Jelszó szerkesztése",
      toastMessages: {
        success: "Profil sikeresen frissítve!",
        fail: "Profil frissítése sikertelen!"
      },
      validationMessages: {
        matchAllValidationMessage:
          "Sajnáljuk, nem minden mezőt töltöttek ki helyesen.",
        passwordRequired: "A jelszó kitöltése kötelező.",
        passwordLength:
          "Legalább 8, maximum 255 karakter hosszú lehet a jelszó.",
        confirmPasswordRequired: "A jelszó megerősítés kitöltése kötelező.",
        confirmPasswordLength:
          "Legalább 8, maximum 255 karakter hosszú lehet a jelszó megerősítés.",
        confirmPasswordConfirm: "A jelszavak nem egyeznek.",
      }
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
      confirmDialogs: {
        delete: "Biztos ki akarja törölni ezt a csoportot?",
        bulkDelete: "Biztos ki akarja törölni ezeket a csoportokat?",
      },
      toastMessages: {
        successfullyCreatedGroup: "Csoport hozzáadása sikeres volt!",
        failedToCreateGroup: "Csoport hozzáadása sikertelen volt!",
        successfullyDeletedGroup: "Csoport törlése sikeres volt!",
        failedToDeleteGroup: "Csoport törlése sikertelen volt!",
        successfullyDeletedMultipleGroups: "Csoportok törlése sikeres volt!",
        failedToDeleteMultipleGroups: "Csoportok törlése sikertelen volt!",
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
            "A kurzus nevének 5-nél több és 100-nál kevesebb karakterből kell állnia.",
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
        multiSelect: {
          title: "Csoportok hozzárendelése",
          searchPlaceholder: "Keresés...",
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
      confirmDialogs: {
        message: "Biztos ki akarja törölni ezt a kurzus(oka)t?",
        rejectLabel: "Mégsem",
        acceptLabel: "Törlés"
      }
    },
    assignmentPage: {
      dragFileText: "Húzza ide a fájlokat a feltöltéshez",
      returnButton: "Mégsem",
      comment: "Hozzászólás:",
      deadline: "Határidő:",
      courseName: "Kurzus:",
      task_name: "Feladat:",
      deadlineExpired: "Sajnálom kicsúsztál a határidőből!",
      teacherTask: "Feltöltött feladat:",
      downloadTeacherAssignment: "Feladat letöltése"
    },
    newAssignmentPage: {
      title: "Új feladat létrehozása",
      updateTitle: "Feladat módosítása",
      task_nameLabel: "Feladat címe",
      saveButton: "Mentés",
      cancelButton: "Mégsem",
      comment: "Hozzászólás",
      deadline: "Határidő",
      courseName: "Kurzus neve",
      topic_name: "Téma kiválasztása",
      teacher_task: "Feladat / jegyzet feltöltése diákok számára.",
      validationMessages: {
        task_nameRequired: "A feladat címének kitöltése kötelező.",
        task_nameLength:
          "A feladat címének kevesebbnek kell lennie, mint 255 karakter.",
        commentLength:
          "A hozzászólásnak kevesebbnek kell lennie, mint 255 karakter.",
        deadlineRequired: "Kötelező határidőt beállítani.",
        topicRequired: "Kötelező témát választani.",
      },
      toastMessages: {
        successfullyCreatedAssignment: "Feladat hozzáadása sikeres volt!",
        failedToCreateAssignment: "Feladat hozzáadása sikertelen volt!",
      },
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
      profile: 'Profilom',
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
