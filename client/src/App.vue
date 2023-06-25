<template>
  <div class="app-container">
    <MenuComponent
      @showLogInModal="isLogInModalVisiable = true">
    </MenuComponent>
    <div class="main-content-container">
      <BoardComponent
      :isLogInModalVisiable="isLogInModalVisiable"
      >
      </BoardComponent>
      <LogInComponent
        class="log-in-modal"
        v-show="isLogInModalVisiable"
        @closeLogInModal="isLogInModalVisiable = false">
      </LogInComponent>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import BoardComponent from "./components/board/BoardComponent.vue";
import MenuComponent from "./components/MenuComponent.vue";
import LogInComponent from "./components/LogInComponent.vue";

export default defineComponent({
  name: 'App',
  components: {
    MenuComponent,
    LogInComponent,
    BoardComponent
  },

  data() {
    return {
      isLogInModalVisiable: false,
    };
  },
})
</script>

<style>
  .main-content-container, .app-container {
    height: 100%;
  }
</style>
