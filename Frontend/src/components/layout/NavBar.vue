<script lang="ts">
export default {}
</script>
<template>
  <nav class="navbar navbar-expand-lg bg-body-tertiary shadow shadow-sm fixed-top">
    <div class="container-fluid">
      <a class="navbar-brand" href="#">
        <img src="@/assets/img/logo.png" width="40" height="40" alt="logo" />
      </a>
      <button
        class="navbar-toggler"
        type="button"
        data-bs-toggle="collapse"
        data-bs-target="#navbarSupportedContent"
        aria-controls="navbarSupportedContent"
        aria-expanded="false"
        aria-label="Toggle navigation"
      >
        <span class="navbar-toggler-icon"></span>
      </button>
      <div class="collapse navbar-collapse" id="navbarSupportedContent">
        <ul class="navbar-nav me-auto mb-2 mb-lg-0">
          <li class="nav-item">
            <router-link
              class="nav-link"
              :class="$route.name == 'home' ? 'active' : ''"
              :to="{ name: 'home' }"
            >
              Home
            </router-link>
          </li>
          <li class="nav-item">
            <router-link
              class="nav-link"
              :class="$route.name == 'about' ? 'active' : ''"
              :to="{ name: 'about' }"
            >
              About
            </router-link>
          </li>
          <li class="nav-item">
            <a class="nav-link" href="#">Contact</a>
          </li>
          <li class="nav-item">
            <a class="nav-link" href="#">Our Bloggers</a>
          </li>
        </ul>
        <div class="d-flex gap-3">
          <router-link class="btn btn-success" :to="{ name: 'login' }">Login</router-link>
          <!-- <router-link class="btn btn-primary" :to="{ name: 'register' }">Register</router-link> -->
        </div>
      </div>
    </div>
  </nav>
</template>
