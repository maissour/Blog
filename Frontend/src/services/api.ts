import type { LoginDto } from "@/interfaces";
import instance from ".";

export default {
    // Article
    getArticles() {
        return instance.get("/Home/AllArticle")
    },
    getArticleById(id: number) {
        return instance.get(`/Home/ArticleById/${id}`)
    },
    //Login
    async login(loginData: LoginDto) {
        return await instance.post("/Auth/login", loginData)
    },
    //Logout
    async logout() {
        return await instance.get("/Auth/logout")
    }
}