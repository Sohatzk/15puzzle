<template>
  <div
    id="tile"
    :class="{ 'contentTile': tileNumber }"
    :draggable="canBeDragged"
    @dragstart="dragStart"
    @dragover="dragPrevent"
    @dragenter="dragPrevent"
    @dragleave="dragPrevent"
    @drop="dragDrop"
    @dragend="dragEnd">
      <span class="tileNumber">{{ tileNumber }}</span>
  </div>
</template>

<script lang="ts">
import { defineComponent, PropType } from 'vue';
import DragDropModel from '../../models/DragDropModel'

export default defineComponent({
  name: 'TileComponent',

  props: {
    tileNumber: {
      type: [Number, null] as PropType<number|null>,
      required: true
    },
    index: {
      type: Number,
      required: true,
    },
    col: {
      type: Number,
      required: true,
    },
    row: {
      type: Number,
      required: true,
    },
    gameOn: {
      type: Boolean,
      required: true,
    },
    puzzleSolved: {
      type: Boolean,
      required: true,
    },
    gameStopped: {
      type: Boolean,
      required: true,
    },
  },

  computed: {
    canBeDragged(): boolean {
      return this.gameOn && !this.puzzleSolved && !this.gameStopped;
    },
  },

  methods: {
    dragStart(): void {
      if (!this.canBeDragged && this.tileNumber != null) return;
      this.$emit('dragStartEvent', new DragDropModel(this.index, this.row, this.col));
    },

    dragPrevent(e: DragEvent): void {
      e.preventDefault();
    },

    dragDrop(): void {
      if (!this.canBeDragged) return;
      const model = new DragDropModel(this.index, this.row, this.col);
      this.$emit('dragDropEvent', model);
    },

    dragEnd(): void {
      if (!this.canBeDragged) return;
      this.$emit('dragEndEvent');
    },
  }
})
</script>

<style scoped>
  #tile {
    height: 122.6px;
    width: 122.6px;
    border: 1px solid rgb(54, 88, 225);
    display: flex;
    justify-content: center;
    align-items: center;
    font-size: 20px;
    cursor: grab;
  }

  .contentTile {
    background-color: aquamarine;
  }
</style>
