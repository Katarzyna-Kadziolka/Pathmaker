import {createRouter, createWebHistory} from 'vue-router'
import ExploreView from "@/views/ExploreView.vue";

const router = createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes: [
        {
            path: '/',
            redirect: {name: 'explore'}
        },
        {
            path: '/',
            name: 'explore',
            component: ExploreView
        },
        {
            path: '/create',
            name: 'create',
            component: () => import('../views/CreateView.vue')
        },
        {
            path: '/play',
            name: 'play',
            component: () => import('../views/PlayView.vue')
        },
        {
            path: '/log-in',
            name: 'logIn',
            component: () => import('../views/LogInView.vue')
        },
    ]
})

export default router
