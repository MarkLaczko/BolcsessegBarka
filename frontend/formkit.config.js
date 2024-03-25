import {defaultConfig} from '@formkit/vue'
import {generateClasses} from "@formkit/themes";


export default defaultConfig({
    config: {
        classes: generateClasses({
            global: {
                input: 'form-control'

            },
            select: {
                input: 'form-select'
            },
            submit: {
                input:
                    'btn btn-info mt-3'

            }
        })
    }
})
