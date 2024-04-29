export default {
  pages: {
    registerPage: {
      title: "Registration",
      submitButton: "Register",
      accountText: "Don't have an account yet?",
      namePlaceholder: "Name",
      emailPlaceholder: "E-mail",
      passwordPlaceholder: "Password",
      passwordConfirmPlaceholder: "Confirm Password",
      validationMessages: {
        matchAllValidationMessage:
          "Sorry, not all fields have been filled out correctly.",
        nameRequired: "Username is required.",
        nameLength: "Username must be less than 255 characters.",
        emailRequired: "Email is required.",
        validEmail: "Please provide a valid email address.",
        passwordRequired: "Password is required.",
        passwordLength:
          "Password must be at least 8 and maximum 255 characters long.",
        confirmPasswordRequired: "Confirm password is required.",
        confirmPasswordLength:
          "Confirm password must be at least 8 and maximum 255 characters long.",
        confirmPasswordConfirm: "Passwords do not match.",
      },
      registerFailed: "Registration failed!",
      registerSucceeded: "Registration successful!",
    },
    loginPage: {
      title: "Login",
      emailPlaceholder: "Email",
      passwordPlaceholder: "Password",
      loginButtonText: "Login",
      accountText: "Don't have an account yet?",
      loginFailed: "Login failed!",
    },
    homePage: {
      title: "Welcome",
      taskTitle: "Tasks to be completed",
      materialsTitle: "Latest Learning Materials",
    },
    coursesPage: {
      title: "Courses",
      youDontHaveCoursesText: "You don't have any courses!",
    },
    coursePage: {
      newTopicButton: "New Topic",
      deleteButton: "Delete",
      editButton: "Edit",
      viewButton: "View",
      cancelButton: "Cancel",
      groupTreatmentButton: "Manage Groups",
      confirmDialogs: {
        messageTopic: "Are you sure you want to delete this topic?",
        messageAssignmnet: "Are you sure you want to delete this assignment?",
        messageNote: "Are you sure you want to delete this note?",
        rejectLabel: "Cancel",
        acceptLabel: "Delete"
      },
      newTopicDialog: {
        title: "Add New Topic",
        nameLabel: "Name",
        orderLabel: "Order",
        saveButton: "Save",
        cancelButton: "Cancel",
        validationMessages: {
          nameRequired: "Selecting a topic name is required.",
          nameLength: "The topic name must be less than 60 characters.",
          orderNumber: "Order must be a number.",
          matchAllValidationMessage:
            "Sorry, not all fields have been filled out correctly.",
        },
      },
      editTopicDialog: {
        title: "Edit Topic",
        nameLabel: "Name",
        orderLabel: "Order",
        saveButton: "Save",
        cancelButton: "Cancel",
        validationMessages: {
          nameLength: "The topic name must be less than 60 characters.",
          orderNumber: "The order must be a number.",
          matchAllValidationMessage: "Sorry, not all fields were filled out correctly.",
        },
      },
      deleteTopic: {
        toastMessages: {
          successfullyDeletedTopic: "Topic deletion was successful!",
          failedToDeleteTopic: "Failed to delete topic!"
        }
      },
      addTopic: {
        toastMessages: {
          successfulyCreatedTopic: "Topic creation was successful!",
          failedToCreateTopic: "Failed to create topic!",
        }
      },
      editTopic: {
        toastMessages: {
          successfullyUpdatedTopic: "Topic update was successful!",
          failedToUpdateTopic: "Failed to update topic!",
        }
      },
      groupTreatmentDialog: {
        title: "Group Treatment",
        multiSelectPlaceholder: "Select Groups",
        cancelButton: "Cancel",
        saveButton: "Save"
      },
      saveGroups: {
        toastMessages: {
          successfullyUpdatedGroups: "Groups update was successful!",
          failedToUpdateGroups: "Failed to update groups!",
        }
      },
      newNoteDialog: {
        title: "Create Note",
        notesNameText: "Note name:",
        cancelButton: "Cancel",
        saveButton: "Save",
      },
      currentNoteDialog: {
        title: "Modify Note",
        notesNameText: "Note name:",
        cancelButton: "Cancel",
        saveButton: "Save",
        editButton: "Edit",
        deleteButton: "Delete"
    },
      accordionText: {
        actions: "Actions",
        newAssignment: "New Assignment",
        newNote: "New Note",
        newQuiz: "New Quiz",
        edit: "Edit",
        delete: "Delete",
        createNoteButton: "Create note"
      },
      note: {
        name: "Note",
        text: "Note Title:",
        viewButton: "View",
        toastMessages: {
          successfullyCreatedNote: "Note successfully saved!",
          failedToCreateNote: "Failed to save note!",
          successfullyUpdatedNote: "Note update was successful!",
          failedToUpdateNote: "Failed to update note!",
          successfullyDeletedNote: "Note deletion was successful!",
          failedToDeleteNote: "Failed to delete note!"
        }
      },
      quiz: {
        name: "Quiz",
        viewButton: "View",
        opens: "Opens",
        closes: "Closes",
        time: "Time limit",
        attempts: "Maximum attempts",
        minutes: "minutes"
      },
      deleteAssignment: {
        toastMessages: {
          successfullyDeletedAssignment: "Assignment deletion was successful!",
          failedToDeleteAssignment: "Failed to delete assignment!",
        }
      }
    },
    quizPage: {
      opens: "Opens",
      closes: "Closes",
      time: "Time limit",
      attempts: "Maximum attempts",
      minutes: "minutes",
      userAttempts: "Attempts",
      user: "User",
      begin: "Started",
      finished: "Finished",
      marks: "Marks",
      grade: "Grade",
      evaluate: "Evaluate",
      startAttempt: "Start attempt",
      ownAttempts: "Own attempts",
      beginAttemptDialog: {
        begin: "Start quiz",
        time: "Time limit for attempt",
        minutes: "minutes",
        confirm: "Are you sure you want to start this quiz?",
        attemptOnce: "This quiz can only be attempted once.",
        attemptMultipleStart: "This quiz can be attempted",
        attemptMultipleMiddle: "howevery many",
        attemptMultipleEnd: "times",
        cancel: "Cancel",
        start: "Start",
      }
    },
    createQuizPage: {
      title: "Create quiz",
      topic: "Topic",
      name: "Name",
      attempts: "Maximum attempts",
      opens: "Quiz opens",
      closes: "Quiz closes",
      time: "Time limit (minutes)",
      submit: "Save",
      validationMessages: {
        matchAllValidationMessage: "Sorry, not all fields are filled out correctly.",
        nameLength: "The name field must be less than 100 characters long.",
        nameRequired: "The name field is required.",
        attemptsMin: "The maximum attempts can not less than 0.",
        timeMin: "The time limit can not less than 0.",
      },
      toastMessages: {
        unexpectedError: "Unexpected error when creating the quiz.",
      }
    },
    editQuizPage: {
      title: "Edit",
      name: "Name",
      attempts: "Maximum attempts",
      opens: "Quiz opens",
      closes: "Quiz closes",
      time: "Time limit (minutes)",
      submit: "Save",
      tasks: "Tasks",
      validationMessages: {
        matchAllValidationMessage: "Sorry, not all fields are filled out correctly.",
        nameLength: "The name field must be less than 100 characters long.",
        nameRequired: "The name field is required.",
        attemptsMin: "The maximum attempts can not less than 0.",
        timeMin: "The time limit can not less than 0."
      },
      toastMessages: {
        updateSuccess: "Quiz updated!",
        updateUnexpectedError: "Unexpected error when updating the quiz!",
        changeOrderUnexpectedError: "Unexpected error when changing the order of tasks!",
        deleteSuccess: "Quiz deleted!",
        deleteUnexpectedError: "Unexpected error when deleting qui!"
      },
      confirmDialogs: {
        deleteTask: "Are you sure you want to delete this tasks and all of it's subtasks?",
        cancel: "Cancel",
        confirm: "Delete"
      }
    },
    createTaskPage: {
      title: "Create task",
      header: "Header",
      text: "Short text",
      subtaskQuestion: "Subtask title",
      subtaskType: "Subtask type",
      subtaskOptions: "Answer options",
      subtaskOptionsInstructions: "Start typing the answer options! Hit Enter to add a new option.",
      subtaskSolution: "Correct answers",
      subtaskSolutionInstructions: "Start typing the correct answers! Hit Enter to add a new answer.",
      subtaskMarks: "Subtask marks",
      cancel: "Cancel",
      submit: "Save",
      typeOptions: {
        shortAnswer: "Short answer",
        multipleChoice: "Multiple choice",
        essay: "Essay"
      },
      validationMessages: {
        matchAllValidationMessage: "Sorry, not all fields are filled out correctly.",
        headerLength: "The header field must be less than 100 characters long.",
        headerRequired: "The header field is required.",
        textLength: "The short text field must be less than 255 characters long.",
        textRequired: "The short text field is required.",
        marksRequired: "The marks field is required.",
        marksMin: "The marks field can not be less than 0.",
        allQuestionRequired: "All questions are required!"
      },
      toastMessages: {
        createUnexpectedError: "Unexpected error when creating the task!",
        changeOrderUnexpectedError: "Unexpected error when changing the order of subtasks!",
        deleteSuccess: "Subtask deleted!",
        deleteUnexpectedError: "Unexcpected error when deleting subtask!"
      },
      confirmDialogs: {
        deleteTask: "Are you sure you want to delte this subtask?",
        cancel: "cancel",
        confirm: "Delete"
      }
    },
    editTaskPage: {
      title: "Create task",
      header: "Header",
      text: "Short text",
      subtaskQuestion: "Subtask title",
      subtaskType: "Subtask type",
      subtaskOptions: "Answer options",
      subtaskOptionsInstructions: "Start typing the answer options! Hit Enter to add a new option.",
      subtaskSolution: "Correct answers",
      subtaskSolutionInstructions: "Start typing the correct answers! Hit Enter to add a new answer.",
      subtaskMarks: "Subtask marks",
      cancel: "Cancel",
      submit: "Save",
      typeOptions: {
        shortAnswer: "Short answer",
        multipleChoice: "Multiple choice",
        essay: "Essay"
      },
      validationMessages: {
        matchAllValidationMessage: "Sorry, not all fields are filled out correctly.",
        headerLength: "The header field must be less than 100 characters long.",
        headerRequired: "The header field is required.",
        textLength: "The short text field must be less than 255 characters long.",
        textRequired: "The short text field is required.",
        marksRequired: "The marks field is required.",
        marksMin: "The marks field can not be less than 0.",
        allQuestionRequired: "All questions are required!"
      },
      toastMessages: {
        createUnexpectedError: "Unexpected error when creating the task!",
        changeOrderUnexpectedError: "Unexpected error when changing the order of subtasks!",
        deleteSuccess: "Subtask deleted!",
        deleteUnexpectedError: "Unexcpected error when deleting subtask!"
      },
      confirmDialogs: {
        deleteTask: "Are you sure you want to delte this subtask?",
        cancel: "cancel",
        confirm: "Delete"
      }
    },
    userManagementPage: {
      title: "User Management",
      newUser: " New User",
      deleteUser: " Delete",
      exportButton: " Export",
      namePlaceholder: "Name...",
      idText: "ID",
      nameText: "Name",
      adminText: "Administrator",
      modifyText: "Modify",
      deleteText: "Delete",
      newUserDialog: {
        title: "Add User",
        nameLabel: "Name",
        emailLabel: "Email",
        passwordLabel: "Password",
        confirmPasswordLabel: "Confirm Password",
        saveButton: "Save",
        cancelButton: "Cancel",
        validationMessages: {
          nameRequired: "Username is required.",
          emailRequired: "Email is required.",
          passwordRequired: "Password is required.",
          confirmPasswordRequired: "Confirm password is required.",
          matchAllValidationMessage:
            "Sorry, not all fields have been filled out correctly.",
          nameLength: "The username must be less than 255 characters.",
          validEmail: "Please enter a valid email address.",
          passwordLength:
            "The password must be at least 8 characters and no more than 255 characters long.",
          confirmPasswordLength:
            "The password confirmation must be at least 8 characters and no more than 255 characters long.",
          confirmPasswordConfirm: "The passwords do not match.",
        },
      },
      editUserDialog: {
        title: "Edit:",
        nameLabel: "Name",
        emailLabel: "Email",
        passwordLabel: "Password",
        confirmPasswordLabel: "Confirm Password",
        isAdmin: "Admin",
        saveButton: "Save",
        cancelButton: "Cancel",
        validationMessages: {
          nameRequired: "Username is required.",
          emailRequired: "Email is required.",
          matchAllValidationMessage:
            "Sorry, not all fields have been filled out correctly.",
          nameLength: "The username must be less than 255 characters.",
          validEmail: "Please enter a valid email address.",
          passwordLength:
            "The password must be at least 8 characters and no more than 255 characters long.",
          confirmPasswordLength:
            "The password confirmation must be at least 8 characters and no more than 255 characters long.",
          confirmPasswordConfirm: "The passwords do not match.",
        },
      },
      toastMessages: {
        successfullyCreatedUser: "User addition was successful!",
        failedToCreateUser: "User addition failed!",
        successfullyDeletedUser: "User deletion was successful!",
        failedToDeleteUser: "User deletion failed!",
        successfullyDeletedMultipleUsers:
          "Deletion of multiple users was successful!",
        failedToDeleteMultipleUsers: "Deletion of multiple users failed!",
        successfullyUpdatedUser: "User modification was successful!",
        failedToUpdateUser: "User modification failed!",
      },
      confirmDialogs: {
        message: "Are you sure you want to delete this user(s)?",
        rejectLabel: "Cancel",
        acceptLabel: "Delete"
      }
    },
    profilePage: {
      title: 'Edit profile',
      nameLabel: "Name",
      emailLabel: "Email",
      currentPasswordLabel: "Current password",
      passwordLabel: "New password",
      confirmPasswordLabel: "New password confirmation",
      submitButton: "Save",
      changePassword: "Change password",
      toastMessages: {
        success: "Profile updated successfully!",
        fail: "Failed to update profile!"
      },
      validationMessages: {
        matchAllValidationMessage:
          "Sorry, not all fields have been filled out correctly.",
        passwordRequired: "Password is required.",
        passwordLength:
          "Password must be at least 8 and maximum 255 characters long.",
        confirmPasswordRequired: "Confirm password is required.",
        confirmPasswordLength:
          "Confirm password must be at least 8 and maximum 255 characters long.",
        confirmPasswordConfirm: "Passwords do not match.",
      }
    },
    groupManagementPage: {
      title: "Group Management",
      newGroup: " New Group",
      deleteGroup: " Delete",
      exportButton: " Export",
      namePlaceholder: "Name...",
      idText: "ID",
      nameText: "Name",
      member: "member",
      course: "course",
      memberText: "Number of Members",
      courseText: "Number of Courses",
      modifyText: "Modify",
      deleteText: "Delete",
      newGroupDialog: {
        title: "Add Group",
        nameLabel: "Name",
        saveButton: "Save",
        cancelButton: "Cancel",
        validationMessages: {
          nameRequired: "Group name is required.",
          nameLength: "The group name must be less than 100 characters.",
          matchAllValidationMessage:
            "Sorry, not all fields have been filled out correctly.",
        },
      },
      editGroupDialog: {
        title: "Edit:",
        idLabel: "ID",
        nameLabel: "Name",
        emailLabel: "Email",
        permissionLabel: "Permission",
        namePlaceholder: "Name...",
        emailPlaceholder: "Email...",
        saveButton: "Save",
        cancelButton: "Cancel",
        validationMessages: {
          matchAllValidationMessage:
            "Sorry, not all fields have been filled out correctly.",
          nameRequired: "Group name is required.",
          nameLength: "The group name must be less than 100 characters.",
        },
      },
      confirmDialogs: {
        delete: "Are you sure you want to delete this group?",
        bulkDelete: "Are you sure you want to delete these groups?",
      },
      toastMessages: {
        successfullyCreatedGroup: "Successfully added group!",
        failedToCreateGroup: "Failed to add group!",
        successfullyDeletedGroup: "Successfully deleted group!",
        failedToDeleteGroup: "Failed to delete group!",
        successfullyDeletedMultipleGroups: "Successfully deleted groups!",
        failedToDeleteMultipleGroups: "Failed to delete groups!",
        successfullyUpdatedGroup: "Successfully updated group!",
        failedToUpdateGroup: "Failed to update group!",
      },
    },
    courseManagementPage: {
      title: "Course Management",
      newUser: " New Course",
      deleteUser: " Delete",
      exportButton: " Export",
      namePlaceholder: "Name...",
      idText: "ID",
      nameText: "Name",
      imageText: "Image",
      modifyText: "Modify",
      deleteText: "Delelte",
      newCourseDialog: {
        title: "Add Course",
        nameLabel: "Course Name",
        saveButton: "Save",
        cancelButton: "Cancel",
        validationMessages: {
          nameRequired: "Filling out the course name is mandatory.",
          nameLength:
            "The Course Name Must Be Between 5 and 100 Characters Long.",
          imageRequired: "Filling out the image is mandatory.",
          fileUpload: "Upload Image",
          matchAllValidationMessage:
            "Sorry, not all fields were filled out correctly.",
        },
      },
      editCourseDialog: {
        title: "Edit:",
        idLabel: "ID",
        nameLabel: "Name",
        emailLabel: "Email",
        permissionLabel: "Permission",
        namePlaceholder: "Name...",
        emailPlaceholder: "Email...",
        saveButton: "Save",
        cancelButton: "Cancel",
        validationMessages: {
          matchAllValidationMessage:
            "Sorry, not all fields were filled out correctly.",
          fileUpload: "Add Image",
          nameLength: "The course name must be less than 255 characters.",
        },
        multiSelect: {
          title: "Assigning Groups",
          searchPlaceholder: "Search...",
        },
      },
      toastMessages: {
        successfullyCreatedCourse: "Successfully added course!",
        failedToCreateCourse: "Failed to add course!",
        successfullyDeletedCourse: "Successfully deleted course!",
        failedToDeleteCourse: "Failed to delete course!",
        successfullyDeletedMultipleCourses: "Successfully deleted courses!",
        failedToDeleteMultipleCourses: "Failed to delete courses!",
        successfullyUpdatedCourse: "Successfully updated course!",
        failedToUpdateCourse: "Failed to update course!",
      },
      confirmDialogs: {
        message: "Are you sure you want to delete this course(s)?",
        rejectLabel: "Cancel",
        acceptLabel: "Delete"
      }
    },
    assignmentPage: {
      dragFileText: "Drag files here to upload",
      returnButton: "Return",
      comment: "Comment",
      deadline: "Deadline:",
      courseName: "Course:",
      task_name: "Assignment:",
      deadlineExpired: "Sorry you missed the deadline!"
    },
    newAssignmentPage: {
      title: "Create a new task",
      updateTitle: "Modify task",
      task_nameLabel: "Task title",
      saveButton: "Save",
      cancelButton: "Cancel",
      comment: "Comment",
      deadline: "Deadline",
      courseName: "Course name",
      topic_name: "Select a topic",
      teacher_task: "Upload assignment / note for students.",
      validationMessages: {
        task_nameRequired: "Filling in the title of the task is required.",
        task_nameLength:
          "The title of the task must be less than 255 characters.",
        commentLength: "Comments must be less than 255 characters.",
        deadlineRequired: "You must set a deadline.",
        topicRequired: "It is mandatory to choose a topic",
      },
      toastMessages: {
        successfullyCreatedAssignment: "Successfully added assignment!",
        failedToCreateAssignment: "Failed to add assignment!",
      },
    },
  },
  layout: {
    header: {
      home: "Home",
      courses: "My Courses",
      admin: "Administration",
      userAdministration: "User Management",
      groupAdministration: "Group Management",
      courseAdministration: "Course Management",
      profile: 'Profile',
      logout: "Logout",
    },
    footer: {},
  },
  components: {
    BaseCourseCard: {
      viewButton: "View Course",
    },
    BaseAssignmentCard: {
      buttonTitle: "Assignment Submission",
      deadline: "Submission Deadline",
    },
    BaseLearningMaterialCard: {
      courseTitle: "Course",
      viewButton: "View Material",
    },
  },
};
