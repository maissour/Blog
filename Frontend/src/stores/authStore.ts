import { defineStore } from 'pinia';
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import api from '@/services/api';
import type { LoginDto } from '@/interfaces';

export const useAuthStore = defineStore('auth', () => {
    const isAuthenticated = ref(localStorage.getItem('isAuthenticated') === 'true');
    const userName = ref(localStorage.getItem('userName') || '');
    const router = useRouter();

    const login = async (loginData: LoginDto) => {
        const response = await api.login(loginData)
        if (response.data.success) {
            isAuthenticated.value = true;
            userName.value = response.data.userName;
            localStorage.setItem('isAuthenticated', 'true');
            localStorage.setItem('userName', response.data.userName);
            router.push('/');
        } else {
            isAuthenticated.value = false;
            userName.value = '';
            localStorage.removeItem('isAuthenticated');
            localStorage.removeItem('userName');
        }
    };

    const logout = async () => {
        try {
            const response = await api.logout()
            if (response.data.success) {
                isAuthenticated.value = false;
                userName.value = '';
                localStorage.removeItem('isAuthenticated');
                localStorage.removeItem('userName');
                router.push("/");
            }
        } catch (error) {
            console.error('Logout failed:', error);
        }
    };

    return { isAuthenticated, login, logout, userName };
});