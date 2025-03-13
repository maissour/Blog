import instance from "@/services/index";

instance.interceptors.response.use(
    response => response,
    error => {
        if (error.response && error.response.status === 401) {
            alert("Session expired. Please log in again.");
            window.location.href = "/auth/login"; // Redirect to login page
        }
        return Promise.reject(error);
    }
);

export default instance;
