<script lang="ts">
import { defineComponent } from 'vue'
import api from '@/services/api'
import type { Article } from '@/interfaces'
export default defineComponent({
  data() {
    return {
      articles: [] as Article[],
      loading: true as boolean,
      error: '' as string,
    }
  },
  computed: {
    groupedPosts(): Article[][] {
      const result: Article[][] = []
      for (let i = 0; i < this.articles.length; i++) {
        result.push(this.articles.slice(i, i + 2))
      }
      return result
    },
  },
  created() {
    this.fetchArticles()
  },
  methods: {
    async fetchArticles() {
      try {
        const response = await api.getArticles()
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
  <p v-if="loading">Loading ...</p>
  <p v-else-if="error">{{ error }}</p>
  <div class="row" v-else v-for="(pair, index) in groupedPosts" :key="index">
    <div v-for="post in pair" :key="post.id" class="col-md-6 mb-3">
      <router-link :to="`/post/${post.id}`">
        <div class="card">
          <img :src="`src/assets/img/${post.image}`" class="card-img-top" alt="img" />
          <div class="card-body">
            <h5 class="card-title">{{ post.title }}</h5>
            <!-- <h6 class="card-subtitle text-muted">{{ post.Author }}</h6> -->
            <!-- <p class="card-text"><strong>Tag:</strong> {{ post.Tag }}</p> -->
            <!-- <small class="text-muted">{{ new Date(post.CreatedAt).toLocaleDateString() }}</small> -->
          </div>
        </div>
      </router-link>
    </div>
  </div>
</template>
