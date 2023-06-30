import { UserModel } from '@/models/interfaces/UserModel';
import { createStore } from 'vuex';

const userFromStorage = localStorage.getItem('user');
const userInitialState: UserModel | null = userFromStorage ? JSON.parse(userFromStorage) : null;

const state = {
  user: userInitialState,
  isLoggedIn: !!userInitialState,
}

export default createStore({
  state,

  getters: {
    user: (state) => state.user,
    isLoggedIn: (state) => state.isLoggedIn
  },

  mutations: {
    changeUser(state, user: UserModel): void {
      state.user = user;
      state.isLoggedIn = !!user;
      if (user) {
        localStorage.setItem('user', JSON.stringify(user));
      } else {
        localStorage.removeItem('user');
      }
    },
  },

  actions: {
    changeUser(context, user: UserModel): void {
      context.commit('changeUser', user);
    },
  },

  modules: {
  }
})
