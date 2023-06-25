<template>
  <header>
    <div class="logo-container">
      <img class="logo" alt="15_puzzle_logo" src="../assets/puzzle-piece-solid.svg"/>
      <span class="title">15 Puzzle Game</span>
    </div>
    <nav class="nav-links">
      <ul>
        <li><a href="#">Show history</a></li>
        <li>
          <a 
            @click="logIn"
            href="#">
              Log In
          </a>
        </li>
      </ul>
    </nav>
  </header>
</template>

<script lang="ts">
import { defineComponent } from 'vue';

export default defineComponent({
  name: 'MenuComponent',

  methods: {
    logIn(): void {
      this.$emit('showLogInModal');
    }
  }
})
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
</style>
