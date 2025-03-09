<script lang="ts">
import { computed } from 'vue'
import { Posts } from '../data/index'
export default {
  data() {
    const groupedPosts = computed(() => {
      const result = []
      for (let i = 0; i < Posts.length; i += 2) {
        result.push(Posts.slice(i, i + 2))
      }
      return result
    })
    return {
      groupedPosts: groupedPosts,
    }
  },
}
</script>

<template>
  <div class="row" v-for="(pair, index) in groupedPosts" :key="index">
    <div v-for="post in pair" :key="post.Id" class="col-md-6 mb-3">
      <router-link :to="`/post/${post.Id}`">
        <div class="card">
          <img :src="`src/assets/img/${post.Img}`" class="card-img-top" alt="img" />
          <div class="card-body">
            <h5 class="card-title">{{ post.Title }}</h5>
            <h6 class="card-subtitle text-muted">{{ post.Author }}</h6>
            <p class="card-text"><strong>Tag:</strong> {{ post.Tag }}</p>
            <small class="text-muted">{{ new Date(post.CreatedAt).toLocaleDateString() }}</small>
          </div>
        </div>
      </router-link>
      <!-- <RouterLink :to="`/post/${post.Id}`">
        <div class="card">
          <img :src="`src/assets/img/${post.Img}`" class="card-img-top" alt="img" />
          <div class="card-body">
            <h5 class="card-title">{{ post.Title }}</h5>
            <h6 class="card-subtitle text-muted">{{ post.Author }}</h6>
            <p class="card-text"><strong>Tag:</strong> {{ post.Tag }}</p>
            <small class="text-muted">{{ new Date(post.CreatedAt).toLocaleDateString() }}</small>
          </div>
        </div>
      </RouterLink> -->
    </div>
  </div>
</template>
