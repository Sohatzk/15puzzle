<template>
  <div class="results-wrapper">
    <div class="results-container">
      <div class="close-btn-container">
        <button @click="close" class="close-btn">&#10006;</button>
      </div>
      <b-table class="results-table text-center" :items="results" show-empty>
        <template v-slot:table-busy>
          <div class="text-center text-info">
            <b-spinner class="align-middle"></b-spinner>
          </div>
        </template>
        <template v-slot:empty>
          <tr class="empty-results">
            <td colspan="100%">User has no results saved</td>
          </tr>
        </template>
      </b-table>
      <div class="d-flex justify-content-center">
        <b-pagination
          class="custom-pagination"
          v-model="getResultsModel.pageNumber"
          :total-rows="totalRows"
          :per-page="getResultsModel?.itemsPerPage"
          aria-controls="my-table"
        ></b-pagination>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import GetResultsModel from "@/models/GetResultsModel";
import ResultModel from "@/models/ResultModel";
import { defineComponent } from "vue";
import { useToast } from "vue-toastification";
import { mapGetters } from "vuex";
import ResultsService from "../services/resultsService";

const toast = useToast();

export default defineComponent({
  name: "ResultsComponent",

  data() {
    return {
      fields: [
        { header: "Turns", name: "turns" },
        { header: "Time", name: "time" },
        { header: "Date", name: "date" },
      ],

      results: [] as ResultModel[],

      getResultsModel: null as GetResultsModel | null,

      totalRows: 0,
    };
  },

  props: {
    refreshResults: {
      default: false,
      type: Boolean,
    },
  },

  computed: {
    ...mapGetters(["user"]),
  },

  async created() {
    this.getResultsModel = new GetResultsModel(this.user.id, 1, 5);
  },

  watch: {
    getResultsModel: {
      async handler() {
        if (this.getResultsModel) {
          try {
            const result = await ResultsService.getResults(this.getResultsModel);
            this.totalRows = result.totalCount;
            this.results = result.results;
          }
          catch(e: any) {
            toast.error(e, {
              timeout: 3000
            })
          }
        }
      },
      deep: true,
    },

    refreshResults: {
      async handler() {
        if (this.refreshResults && this.getResultsModel) {
          try {
            const result = await ResultsService.getResults(this.getResultsModel);
            this.totalRows = result.totalCount;
            this.results = result.results;
          }
          catch(e: any) {
            toast.error(e, {
              timeout: 3000
            })
          }
        }
      }
    }
  },

  methods: {
    close(): void {
      this.$emit("closeResultsModal");
    },
  },
});
</script>

<style scoped>
.results-container {
  width: 700px;
  max-height: 800px;
  border: 1px solid rgb(113, 127, 191);
  background-color: azure;
  margin-top: 71px;
}

::v-deep .results-container {
  border-radius: 7px;
  overflow: hidden;
}
::v-deep .custom-pagination .page-link {
  color: rgb(35, 51, 119);
}

::v-deep .custom-pagination .page-item.active .page-link {
  background-color: rgba(136, 234, 225, 0.966);
  border-color: rgb(35, 51, 119);
}

.empty-results {
  display: flex;
  justify-content: center;
}
</style>
