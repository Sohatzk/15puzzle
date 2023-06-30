<template>
  <div class="general-container">
    <div class="board-container">
      <span v-if="puzzleSolved" class="finished"> Puzzle solved! </span>
      <div class="buttons">
        <button @click="start" class="puzzle-btn">
          {{ gameStopped ? "Continue" : "Play" }}
        </button>
        <button class="puzzle-btn" @click="stop">Stop</button>
        <button class="puzzle-btn" @click="reset">Reset</button>
        <button
          class="puzzle-btn"
          @click="showConfirmation = true"
          :disabled="!puzzleSolved"
        >
          Save
        </button>
      </div>
      <ConfirmationComponent
        v-if="showConfirmation"
        @yesClick="saveButtonConfirmed"
        @noClick="showConfirmation = false"
        @close="showConfirmation = false"
      >
        <h5>Save result?</h5>
      </ConfirmationComponent>
      <div id="board">
        <div v-for="rowIndex in rows" :key="`col-${rowIndex}`">
          <TileComponent
            v-for="colIndex in cols"
            :key="`tile-${colIndex}-${rowIndex}`"
            :tileNumber="tileOrder[(colIndex - 1) * cols + rowIndex - 1]"
            :index="(colIndex - 1) * cols + rowIndex - 1"
            :puzzleSolved="puzzleSolved"
            :gameOn="gameOn"
            :gameStopped="gameStopped"
            :col="colIndex - 1"
            :row="rowIndex - 1"
            @dragStartEvent="dragStart"
            @dragDropEvent="dragDrop"
            @dragEndEvent="dragEnd"
          />
        </div>
      </div>
      <span class="turns">Turns: {{ turns }}</span>
      <span class="time">Time: {{ timerString }}</span>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import TileComponent from "./TileComponent.vue";
import ConfirmationComponent from "../ConfirmationComponent.vue";
import TimerModel from "../../models/TimerModel";
import DragDropModel from "@/models/DragDropModel";
import { mapGetters } from "vuex";
import ResultModel from "@/models/ResultModel";
import resultsService from "@/services/resultsService";
import { AfterLogInEvent } from "@/enums/AfterLogInEvent";
import { useToast } from "vue-toastification";

const toast = useToast();

export default defineComponent({
  name: "BoardComponent",

  components: {
    TileComponent,
    ConfirmationComponent,
  },

  props: {
    rows: {
      default: 4,
      type: Number,
    },
    cols: {
      default: 4,
      type: Number,
    },
    maxTileNumber: {
      default: 15,
      type: Number,
    },
    isLogInModalVisiable: {
      default: false,
      type: Boolean,
    },
    isResultsModalVisiable: {
      default: false,
      type: Boolean,
    },
    canSaveResult: {
      default: false,
      type: Boolean,
    },
  },

  data() {
    return {
      currentDragDropModel: new DragDropModel(null, null, null),
      tileOrder: [] as (number | null)[],
      turns: 0,
      puzzleSolved: false,
      gameOn: false,
      gameStopped: false,
      totalSeconds: 0,
      timerObj: new TimerModel(0, 0, 0),
      timer: 0,
      timerString: "00:00:00",
      showConfirmation: false,
    };
  },

  computed: {
    ...mapGetters(["user", "isLoggedIn"]),
  },

  mounted() {
    this.generateRandomArray(this.$props.maxTileNumber);
  },

  watch: {
    puzzleSolved: {
      handler() {
        if (this.puzzleSolved) {
          clearInterval(this.timer);
        }
      },
    },

    canSaveResult: {
      handler() {
        if (this.$props.canSaveResult) {
          this.saveResult();
        }
      },
    },
  },

  methods: {
    start(): void {
      if (!this.gameOn) {
        this.reset();
        this.gameOn = true;
        this.gameStopped = false;
        this.timer = setInterval(this.upTimer, 1000);
      }
    },

    stop(): void {
      if (this.gameOn) {
        this.gameOn = false;
        this.gameStopped = true;
        clearInterval(this.timer);
      }
    },

    reset(): void {
      this.gameOn = false;
      this.gameStopped = false;
      this.puzzleSolved = false;
      this.turns = 0;
      this.timerString = "00:00:00";
      clearInterval(this.timer);
      this.totalSeconds = 0;
      this.timerObj = new TimerModel(0, 0, 0);
      this.tileOrder = [];
      this.generateRandomArray(this.$props.maxTileNumber);
    },

    generateRandomArray(size: number): void {
      this.tileOrder = [
        1,
        2,
        3,
        4,
        5,
        6,
        7,
        8,
        9,
        10,
        15,
        11,
        13,
        14,
        12,
        null,
      ];
      // while (this.tileOrder.length < size) {
      //   var r = Math.floor(Math.random() * size) + 1;
      //   if (this.tileOrder.indexOf(r) === -1) this.tileOrder.push(r);
      // }
      // this.tileOrder.push(null);
    },

    dragStart(dragDropModel: DragDropModel): void {
      this.currentDragDropModel.updateProperties(dragDropModel);
    },

    dragDrop(targetModel: DragDropModel): void {
      if (
        this.currentDragDropModel.index === targetModel.index ||
        !this.isNeighborToCurrent(targetModel)
      )
        return;

      const currIndex = this.currentDragDropModel.index;
      const targetIndex = targetModel.index;

      if (
        currIndex !== null &&
        targetIndex !== null &&
        this.tileOrder[targetIndex] === null
      ) {
        const temp = this.tileOrder[currIndex];
        this.tileOrder[currIndex] = this.tileOrder[targetIndex];
        this.tileOrder[targetIndex] = temp;
        this.turns++;
      }
    },

    dragEnd(): void {
      this.currentDragDropModel.resetProperties();
      this.checkOrder();
    },

    getIndexFromRowCol(row: number, col: number): number {
      return row * this.cols + col;
    },

    checkOrder(): void {
      for (let i = 0; i < this.tileOrder.length - 1; i++) {
        const current = this.tileOrder[i];
        const next = this.tileOrder[i + 1];
        if (!current || (next && current > next)) {
          return;
        }
      }
      this.gameOn = false;
      this.puzzleSolved = true;
    },

    upTimer(): void {
      this.totalSeconds++;
      this.timerObj.hours = Math.floor(this.totalSeconds / 3600);
      this.timerObj.minutes = Math.floor(
        (this.totalSeconds - this.timerObj.hours * 3600) / 60
      );
      this.timerObj.seconds =
        this.totalSeconds -
        (this.timerObj.hours * 3600 + this.timerObj.minutes * 60);
      this.timerString = `${this.formatTime(
        this.timerObj.hours
      )}:${this.formatTime(this.timerObj.minutes)}:${this.formatTime(
        this.timerObj.seconds
      )}`;
    },

    formatTime(timeValue: number): string {
      return timeValue.toString().length === 1
        ? `0${timeValue}`
        : timeValue.toString();
    },

    isNeighborToCurrent(dragDropModel: DragDropModel): boolean {
      const currentRow = this.currentDragDropModel.row;
      const currentCol = this.currentDragDropModel.col;
      const targetRow = dragDropModel.row;
      const targetCol = dragDropModel.col;

      if (
        currentRow !== null &&
        currentCol !== null &&
        targetRow !== null &&
        targetCol !== null
      ) {
        const currentIndex = this.getIndexFromRowCol(currentRow, currentCol);
        const targetIndex = this.getIndexFromRowCol(targetRow, targetCol);

        return (
          Math.abs(currentIndex - targetIndex) === 1 ||
          Math.abs(currentIndex - targetIndex) === this.cols
        );
      }
      return false;
    },

    async saveButtonConfirmed() {
      if (!this.isLoggedIn) {
        this.$emit("showLogInModal", AfterLogInEvent.AddResult);
      } else {
        await this.saveResult();
      }
      this.showConfirmation = false;
    },

    async saveResult() {
      const resultModel = new ResultModel(
        this.user.id,
        this.turns,
        this.timerString
      );
      try {
        await resultsService.addResult(resultModel);
      } catch (e: any) {
        toast.error(e, {
          timeout: 3000,
        });
      }

      toast.success("Result was successfully saved", {
        timeout: 3000,
      });
      this.$emit('refreshResults');
    },
  },
});
</script>

<style scoped>
.general-container {
  display: flex;
  justify-content: space-evenly;
}
.board-container {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  height: 100%;
}

#board {
  width: 500px;
  height: 500px;
  background-color: lightblue;
  display: flex;
  flex-wrap: wrap;
  border: 5px solid rgb(54, 88, 225);
  border-radius: 10px;
}

.finished {
  margin-bottom: 30px;
  font-size: 25px;
  font-weight: 550;
  color: rgb(54, 88, 225);
}

.turns,
.time {
  margin-top: 8px;
  font-size: 20px;
  font-weight: 500;
}
.replay-img {
  height: 15px;
  width: 15px;
}

.buttons {
  display: flex;
  margin: 10px;
}

.buttons > button {
  margin: 5px 15px 5px 15px;
}
</style>
