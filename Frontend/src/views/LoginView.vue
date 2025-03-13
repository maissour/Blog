<script lang="ts">
import { defineComponent } from 'vue'
import type { LoginDto } from '@/interfaces/index'
import { useAuthStore } from '@/stores/authStore'
export default defineComponent({
  setup() {
    const authStore = useAuthStore()
    return {
      authStore,
    }
  },
  data() {
    return {
      email: '',
      password: '',
      rememberMe: false,
    }
  },
  methods: {
    async login() {
      try {
        const loginData: LoginDto = {
          email: this.email,
          password: this.password,
          rememberMe: this.rememberMe,
        }
        await this.authStore.login(loginData)
      } catch (err) {
        console.log('ERROR WHILE LOGIN :', err)
      }
    },
  },
})
</script>
<template>
  <div class="card w-50">
    <div class="card-body">
      <h1>Login to your account</h1>
      <hr />
      <!-- <form action="" method="post">
        
      </form> -->
      <div class="mb-3">
        <label for="email" class="form-label">Email</label>
        <input
          v-model="email"
          type="email"
          class="form-control"
          id="email"
          placeholder="name@example.com"
        />
      </div>
      <div class="mb-3">
        <label for="password" class="form-label">Password</label>
        <input
          v-model="password"
          type="password"
          class="form-control"
          id="password"
          placeholder="password.."
        />
      </div>
      <div class="w-100 d-flex px-4 mb-3">
        <button @click="login" class="btn btn-primary flex-fill">Login</button>
      </div>
      <div class="form-check mb-3">
        <input
          v-model="rememberMe"
          class="form-check-input"
          type="checkbox"
          value=""
          id="rememberMe"
        />
        <label class="form-check-label" for="rememberMe">Remember me</label>
      </div>
      <p>
        If you don't have account,
        <router-link :to="{ name: 'register' }">Register Here !!</router-link>
      </p>
    </div>
  </div>
</template>
