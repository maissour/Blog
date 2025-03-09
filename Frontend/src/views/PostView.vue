<script lang="ts">
import { useRoute } from 'vue-router'
import { computed } from 'vue'
import { Posts } from '../data/index'

export default {
  data() {
    const route = useRoute()
    const post = computed(() => Posts.find((x) => x.Id === Number(route.params.id)) || null)
    return {
      post: post,
    }
  },
}
</script>
<template>
  <div v-if="post">
    <div class="card">
      <img :src="`/src/assets/img/${post.Img}`" alt="Post Image" class="card-img-top" />
      <div class="card-body">
        <h3 class="card-title">{{ post.Title }}</h3>
        <h6 class="card-subtitle text-muted">{{ post.Author }}</h6>
        <p class="card-text"><strong>Tag:</strong> {{ post.Tag }}</p>
        <small class="text-muted">{{ new Date(post.CreatedAt).toLocaleDateString() }}</small>
      </div>
    </div>
  </div>

  <div v-else>
    <p>Post not found.</p>
  </div>
</template>
<style scoped>
.height {
  height: 400px;
}
</style>
