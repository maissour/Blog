<template>
  <div class="card w-50">
    <div class="card-body">
      <h1>Create new account</h1>
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
        <div class="mb-3">
          <label for="confirmPassword" class="form-label">Confirm password</label>
          <input
            type="password"
            class="form-control"
            id="confirmPassword"
            placeholder="confirm password.."
          />
        </div>
        <div class="w-100 d-flex px-4 mb-3">
          <button type="submit" class="btn btn-primary flex-fill">Register</button>
        </div>
        <div class="form-check mb-3">
          <input class="form-check-input" type="checkbox" value="" id="rememberMe" />
          <label class="form-check-label" for="rememberMe">Remember me</label>
        </div>
      </form>
      <p>
        If you already have account,
        <router-link :to="{ name: 'login' }">Login Here !!</router-link>
      </p>
    </div>
  </div>
</template>
