<template>
  <div class="general-wrapper">
    <div class="main">
      <div class="close-btn-container">
        <button @click.prevent="$emit('close')" class="close-btn">&#10006;</button>
      </div>
      <div class="content">
        <slot></slot>
        <div class="buttons">
          <button
            type="submit"
            @click.prevent="$emit('noClick')"
            class="puzzle-btn no-btn"
          >
            No
          </button>
          <button
            type="submit"
            @click.prevent="$emit('yesClick');"
            class="puzzle-btn yes-btn"
          >
            Yes
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";

export default defineComponent({
  name: "ConfirmationComponent",
});
</script>

<style scoped>
.content {
  display: flex;
  flex-direction: column;
  align-items: center;
}

.buttons {
  margin: 30px;
  display: flex;
  width: 100%;
  justify-content: space-evenly;
}

.no-btn {
  background-image: linear-gradient(rgb(207, 63, 53), rgb(204, 52, 41));
  border-color: rgb(225, 59, 47);
  box-shadow: rgb(235, 57, 44) 0 0 1px 1px;
  color: rgb(232, 226, 226);
}

.no-btn:hover {
  background-image: linear-gradient(rgb(219, 76, 66), rgb(234, 56, 43));
  border-color: rgb(234, 75, 64) rgb(213, 74, 64) rgb(212, 49, 38);
  box-shadow: rgb(235, 57, 44) 0 0 1px 1px;
}

.yes-btn {
  border-color: rgb(34, 131, 34) rgb(38, 144, 38) rgb(10, 124, 10);
  border-color: rgb(23, 192, 23);
  box-shadow: rgb(24, 160, 24) 0 0 1px 1px;
  color: rgb(232, 226, 226);
}

.yes-btn:hover {
  background-image: linear-gradient(rgb(14, 150, 14), rgb(11, 145, 11));
  box-shadow: rgb(24, 160, 24) 0 0 1px 1px;
  outline: 0;
}

.yes-btn {
  background-image: none;
  background-color: green !important;
}
.general-wrapper,
.main,
.content {
  height: 160px;
  width: 350px;
}

.general-wrapper {
  border: 1px solid rgb(113, 127, 191);
  border-radius: 5px;
  background-color: azure;
  position: absolute;
  top: 40%;
  left: 50%;
  transform: translate(-50%, -50%);
  z-index: 1000;
}
</style>
