import './assets/scss/main.scss'

import { createApp } from 'vue'
import { createPinia } from 'pinia'

import App from './App.vue'
import router from './router'

import en from "./locales/en.json"
import {createI18n} from "vue-i18n";
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
import "@fontsource/source-sans-pro";
import "@fontsource/source-sans-pro/400.css";
import "@fontsource/source-sans-pro/400-italic.css";

const i18n = createI18n({
    locale: navigator.language,
    fallbackLocale: "en",
    messages: { en },
    legacy: false
})

const app = createApp(App)

app.use(createPinia())
app.use(router)
app.use(i18n)
app.component('font-awesome-icon', FontAwesomeIcon)

app.mount('#app')
