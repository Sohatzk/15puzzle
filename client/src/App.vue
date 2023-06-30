<template>
  <div class="app-container">
    <MenuComponent
      @showLogInModal="showLogInModal"
      @showResultsModal="isResultsModalVisiable = true"
      @closeResultsModal="isResultsModalVisiable = false"
    >
    </MenuComponent>
    <div class="main-wrapper">
      <div class="main-content-container">
        <BoardComponent
          @showLogInModal="showLogInModal"
          @addResult="saveResultAfter = true"
          :canSaveResult="canSaveResult"
          @refreshResults="refreshResults = true"
        >
        </BoardComponent>
        <LogInComponent
          v-show="isLogInModalVisiable"
          @closeLogInModal="isLogInModalVisiable = false"
          :showResultsModalAfter="showResultsModalAfter"
          @showResultsModal="isResultsModalVisiable = true"
          :saveResultAfter="saveResultAfter"
          @saveResult="canSaveResult = true"
        >
        </LogInComponent>
      </div>
      <ResultsComponent
        v-if="isResultsModalVisiable"
        @closeResultsModal="isResultsModalVisiable = false"
        @showResults="showResultsModalAfter = true"
        :refreshResults="refreshResults"
      >
      </ResultsComponent>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import BoardComponent from "./components/board/BoardComponent.vue";
import MenuComponent from "./components/MenuComponent.vue";
import LogInComponent from "./components/LogInComponent.vue";
import ResultsComponent from "./components/ResultsComponent.vue";
import { AfterLogInEvent } from "./enums/AfterLogInEvent";

export default defineComponent({
  name: "App",
  components: {
    MenuComponent,
    LogInComponent,
    BoardComponent,
    ResultsComponent,
  },

  data() {
    return {
      isLogInModalVisiable: false,
      isResultsModalVisiable: false,
      showResultsModalAfter: false,
      saveResultAfter: false,
      canSaveResult: false,
      refreshResults: false,
    };
  },

  methods: {
    showLogInModal(eventToEmitAfter: AfterLogInEvent | null): void {
      this.resetModals();
      this.isLogInModalVisiable = true;
      if (eventToEmitAfter === AfterLogInEvent.ShowResults) {
        this.showResultsModalAfter = true;
      } else if (eventToEmitAfter === AfterLogInEvent.AddResult) {
        this.saveResultAfter = true;
      }
    },

    resetModals(): void {
      this.showResultsModalAfter = false;
      this.saveResultAfter = false;
    },
  },
});
</script>

<style>
.main-content-container,
.app-container {
  height: 100%;
}

.main-wrapper {
  display: flex;
  justify-content: space-evenly;
  align-items: top;
}
</style>
