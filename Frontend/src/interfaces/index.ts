export interface Article {
    id: number,
    title: string,
    sulg: string,
    description: string,
    text: string,
    image?: string,
    videoUrl?: string,
    categories?: Category[]
}

export interface Category {
    id: number,
    name: string,
    slug: string
}

export interface LoginDto {
    email: string,
    password: string,
    rememberMe: boolean
}