import { defaultConfig } from "@formkit/vue";
import { generateClasses } from "@formkit/themes";

export default defaultConfig({
  config: {
    classes: generateClasses({
      global: {
        messages: "list-style-none",
        message: "text-danger p-0",
        checkbox: "form-check",
      },
      select: {
        input: "form-select",
      },
      submit: {
        input: "btn",
      },
    }),
  },
});
