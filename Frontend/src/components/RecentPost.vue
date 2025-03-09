<script lang="ts">
import { Posts } from '../data/index'
export default {
  data() {
    return {
      Posts: Posts,
    }
  },
}
</script>
<template>
  <h2>Recent</h2>
  <router-link v-for="post in Posts" :key="post.Id" :to="`/post/${post.Id}`">
    <div class="card mb-3">
      <img :src="`src/assets/img/${post.Img}`" alt="img" class="card-img-top" />
      <div class="card-body">
        <h5 class="card-title">{{ post.Title }}</h5>
        <h6 class="card-subtitle text-muted">{{ post.Author }}</h6>
        <p class="card-text"><strong>Tag:</strong> {{ post.Tag }}</p>
        <small class="text-muted">{{ new Date(post.CreatedAt).toLocaleDateString() }}</small>
      </div>
    </div>
  </router-link>
</template>
