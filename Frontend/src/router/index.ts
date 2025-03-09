import { createRouter, createWebHistory } from 'vue-router'
import DefaultLayout from '@/components/layout/DefaultLayout.vue'
import AuthLayout from '@/components/layout/AuthLayout.vue'
import HomeView from '@/views/HomeView.vue'
import AboutView from '@/views/AboutView.vue'
import PostView from '@/views/PostView.vue'
import LoginView from '@/views/LoginView.vue'
import RegisterView from '@/views/RegisterView.vue'
const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'public',
      component: DefaultLayout,
      children: [
        {
          path: '/',
          name: 'home',
          component: HomeView
        },
        {
          path: '/about',
          name: 'about',
          component: AboutView,
        },
        {
          path: '/post/:id',
          name: 'posts',
          component: PostView,
        }
      ]
    },
    {
      path: '/auth/',
      name: 'auth',
      component: AuthLayout,
      children: [
        {
          path: '/auth/login',
          name: 'login',
          component: LoginView
        },
        {
          path: '/auth/register',
          name: 'register',
          component: RegisterView
        }
      ]
    }
  ],
})

export default router
