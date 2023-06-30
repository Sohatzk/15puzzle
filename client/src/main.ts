import { createApp } from 'vue';
import App from './App.vue';
import store from './store';
import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap-vue-3/dist/bootstrap-vue-3.css';
import './main.css';
import { BootstrapVue3 } from 'bootstrap-vue-3';
import axios from './axiosConfig';
import Toast from "vue-toastification";
import "vue-toastification/dist/index.css";

axios.defaults.baseURL = 'https://localhost:7239/';

createApp(App)
  .use(Toast)
  .use(BootstrapVue3)
  .use(store)
  .mount('#app');
