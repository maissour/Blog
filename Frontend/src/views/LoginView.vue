<script lang="ts">
export default {}
</script>
<template>
  <div class="card w-50">
    <div class="card-body">
      <h1>Login to your account</h1>
      <hr />
      <form action="" method="post">
        <div class="mb-3">
          <label for="email" class="form-label">Email</label>
          <input type="email" class="form-control" id="email" placeholder="name@example.com" />
        </div>
        <div class="mb-3">
          <label for="password" class="form-label">Password</label>
          <input type="password" class="form-control" id="password" placeholder="password.." />
        </div>
        <div class="w-100 d-flex px-4 mb-3">
          <button type="submit" class="btn btn-primary flex-fill">Login</button>
        </div>
        <div class="form-check mb-3">
          <input class="form-check-input" type="checkbox" value="" id="rememberMe" />
          <label class="form-check-label" for="rememberMe">Remember me</label>
        </div>
      </form>
      <p>
        If you don't have account,
        <router-link :to="{ name: 'register' }">Register Here !!</router-link>
      </p>
    </div>
  </div>
</template>
