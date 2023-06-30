<template>
  <header>
    <div class="logo-container">
      <img
        class="logo"
        alt="15_puzzle_logo"
        src="../assets/puzzle-piece-solid.svg"
      />
      <span class="title">15 Puzzle Game</span>
    </div>
    <div v-if="isLoggedIn" class="greeting">Welcome, {{ user.username }}!</div>
    <nav class="nav-links">
      <ul>
        <li>
          <a @click.prevent="showResultsModal" href="#"> Results history </a>
        </li>
        <li>
          <a @click.prevent="handleAuthAction" href="#">
            {{ isLoggedIn ? "Log out" : "Log In" }}
          </a>
        </li>
      </ul>
    </nav>
  </header>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { mapActions, mapGetters } from "vuex";
import { AfterLogInEvent } from '../enums/AfterLogInEvent'

export default defineComponent({
  name: "MenuComponent",

  computed: {
    ...mapGetters(["user", "isLoggedIn"]),
  },

  methods: {
    ...mapActions(["changeUser"]),

    handleAuthAction(): void {
      if (this.isLoggedIn) {
        this.changeUser(null);
        this.$emit('closeResultsModal')
        return;
      }
      this.$emit("showLogInModal", null);
    },

    showResultsModal(): void {
      if (this.isLoggedIn) {
        this.$emit("showResultsModal");
        return;
      }
      this.$emit("showLogInModal", AfterLogInEvent.ShowResults);
    },
  },
});
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.logo {
  height: 50px;
  width: 50px;
  margin-top: -5px;
  margin-right: 8px;
}

.logo-container {
  display: flex;
  align-items: center;
}

.title {
  font-weight: 500;
  font-size: 25px;
  cursor: default;
}

header {
  background-color: rgb(192, 205, 216);
  padding: 15px 5%;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.nav-links {
  list-style: none;
}

ul {
  padding: 0;
  margin: 0;
  display: flex;
  justify-content: center;
  align-items: center;
}

.nav-links li {
  display: inline-block;
  padding: 0 20px;
}

.nav-links li a {
  transition: all 0.3s ease 0s;
  text-decoration: none;
  color: black;
}

.nav-links li a:hover {
  color: rgb(54, 88, 225);
  text-decoration: underline;
}

.greeting {
  font-size: 20px;
  font-weight: 500;
}
</style>
