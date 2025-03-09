<script lang="ts">
import NavBar from '@/components/layout/NavBar.vue'
import RecentPost from '@/components/RecentPost.vue'
export default {
  components: {
    NavBar,
    RecentPost,
  },
}
</script>
<template>
  <header>
    <NavBar />
  </header>
  <main class="container">
    <div class="w-25 my-4">
      <form class="d-flex" role="search">
        <input class="form-control me-2" type="search" placeholder="Search" aria-label="Search" />
        <button class="btn btn-outline-success" type="submit">Search</button>
      </form>
    </div>
    <div class="row">
      <div class="col-md-8 p-3">
        <router-view></router-view>
      </div>
      <div class="col-md-4 p-3">
        <RecentPost />
      </div>
    </div>
  </main>
</template>
