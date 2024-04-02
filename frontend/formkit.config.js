import {defaultConfig} from '@formkit/vue'
import {generateClasses} from "@formkit/themes";


export default defaultConfig({
    config: {
        classes: generateClasses({
            global: {
                input: 'form-control',
                messages: 'list-style-none',
                message: 'text-danger p-0'
            },
            select: {
                input: 'form-select'
            },
            submit: {
                input: 'btn'
            },
        })
    }
})
