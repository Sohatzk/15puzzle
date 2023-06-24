<template>
  <div class="app-container">
    <Menu></Menu>
    <div class="main-content-container">
      <Board></Board>
    </div>
  </div>
</template>

<script lang="ts">
import { Options, Vue } from 'vue-class-component';
import Board from './components/board/BoardComponent.vue';
import Menu from './components/MenuComponent.vue';

@Options({
  components: {
    Menu,
    Board
  },
})
export default class App extends Vue {}
</script>

<style>
  .main-content-container, .app-container {
    height: 100%;
  }
</style>
