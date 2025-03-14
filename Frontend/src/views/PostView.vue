<script lang="ts">
import { defineComponent } from 'vue'
import { useRoute } from 'vue-router'
import api from '@/services/api'
import type { Article } from '@/interfaces/'
// import { computed } from 'vue'
// import { Posts } from '../data/index'

export default defineComponent({
  setup() {
    const route = useRoute()
    return {
      route: route,
    }
  },
  data() {
    // const post = computed(() => Posts.find((x) => x.Id === Number(route.params.id)) || null)
    return {
      post: null as Article | null,
      loading: true as boolean,
      error: '' as string,
    }
  },
  created() {
    this.fetchArticle()
  },
  methods: {
    async fetchArticle() {
      try {
        const articleId = parseInt(this.route.params.id as string, 10) || 0
        const response = await api.getArticleById(articleId)

        if (response.data.success) {
          this.post = response.data.result
        } else {
          this.error = 'Product not found'
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
  <div v-if="post" class="card">
    <img v-if="post" :src="`/src/assets/img/${post.image}`" alt="img" class="card-img-top" />
    <div class="card-body">
      <h3 v-if="post" class="card-title">{{ post.title }}</h3>
      <!-- <h6 class="card-subtitle text-muted">{{ post.Author }}</h6>
      <p class="card-text"><strong>Tag:</strong> {{ post.Tag }}</p> -->
      <!-- <small class="text-muted">{{ new Date(post.CreatedAt).toLocaleDateString() }}</small> -->
    </div>
  </div>
</template>
<style scoped>
.height {
  height: 400px;
}
</style>
