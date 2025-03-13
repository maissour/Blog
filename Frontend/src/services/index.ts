import axios from "axios";

const instance = axios.create({
    baseURL: "https://localhost:44327/api",
    withCredentials: true,
    headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json'
    }
})

instance.interceptors.response.use(
    response => response,
    error => {
        if (error.response && error.response.status === 401) {
            alert("Session expired. Please log in again.");
            window.location.href = "/auth/login";
        }
        return Promise.reject(error);
    }
);

export default instance