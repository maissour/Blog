<script lang="ts">
import { defineComponent } from 'vue'
import api from '@/services/api'
import type { Article } from '@/interfaces'
// import { Posts } from '../data/index'
export default defineComponent({
  data() {
    return {
      articles: [] as Article[],
      loading: true as boolean,
      error: '' as string,
    }
  },
  created() {
    this.fetchArticles()
  },
  methods: {
    async fetchArticles() {
      try {
        const response = await api.getRecentArticles()
        if (response.data.success) {
          this.articles = response.data.result
        } else {
          this.error = response.data.message
        }
      } finally {
        this.loading = false
      }
    },
  },
})
</script>
<template>
  <h2>Recent</h2>
  <p v-if="loading">Loading ...</p>
  <p v-else-if="error">{{ error }}</p>
  <router-link v-else v-for="post in articles" :key="post.id" :to="`/post/${post.id}`">
    <div class="card mb-3">
      <img :src="`src/assets/img/${post.image}`" alt="img" class="card-img-top" />
      <div class="card-body">
        <h5 class="card-title">{{ post.title }}</h5>
        <!-- <h6 class="card-subtitle text-muted">{{ post.Author }}</h6>
        <p class="card-text"><strong>Tag:</strong> {{ post.Tag }}</p>
        <small class="text-muted">{{ new Date(post.CreatedAt).toLocaleDateString() }}</small> -->
      </div>
    </div>
  </router-link>
</template>
