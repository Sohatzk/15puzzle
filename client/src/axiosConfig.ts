import axios from 'axios';
import { UserModel } from './models/interfaces/UserModel';

axios.interceptors.request.use(
  (config) => {
    const userString = localStorage.getItem('user');
    if (userString) {
        const user = JSON.parse(userString) as UserModel;
        if (user.token) {
          config.headers.Authorization = 'Bearer ' + user.token;
        }
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

export default axios;