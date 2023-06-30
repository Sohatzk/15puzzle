<template>
  <div class="log-in-wrapper">
    <div class="form-container">
      <div class="close-btn-container">
        <button @click="close" class="close-btn">&#10006;</button>
      </div>
      <div class="header">
        <h2>Log In</h2>
      </div>
      <div class="main-container">
        <div class="username-container">
          <label for="username">Username</label>
          <input
            type="text"
            v-model="username"
            id="username"
            placeholder="Enter username"
          />
          <span v-if="!username" class="error">Required</span>
        </div>
        <div class="password-container">
          <label for="password">Password</label>
          <input
            type="password"
            v-model="password"
            id="password"
            placeholder="Enter password"
          />
          <span v-if="!password" class="error">Required</span>
          <span v-if="passwordInvalid" class="error">Invalid password</span>
        </div>
        <div class="log-in-btn-container">
          <button
            class="log-in-btn puzzle-btn"
            @click="logIn"
            :disabled="!username || !password"
            type="submit"
          >
            Log In
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import { UserModel } from "@/models/interfaces/UserModel";
import LogInModel from "@/models/LogInModel";
import authService from "@/services/authService";
import { defineComponent } from "vue";
import { mapActions } from "vuex";
import { useToast } from "vue-toastification";

const toast = useToast();

export default defineComponent({
  name: "LogInComponent",
  data() {
    return {
      username: "",
      password: "",
      passwordInvalid: false,
    };
  },

  props: {
    showResultsModalAfter: {
      default: false,
      type: Boolean,
    },
    saveResultAfter: {
      default: false,
      type: Boolean,
    },
  },

  methods: {
    ...mapActions(["changeUser"]),

    close(): void {
      this.$emit("closeLogInModal");
    },

    async logIn(): Promise<void> {
      const logInModel = new LogInModel(this.username, this.password);
      try {
        const user = (await authService.logIn(logInModel)) as UserModel;
        this.changeUser(user);
      } catch (e: any) {
        if (e.response && e.response.status === 401) {
          this.passwordInvalid = true;
        } else {
          toast.error(e, {
            timeout: 3000
          })
        }
        return;
      }
      if (this.showResultsModalAfter) {
        this.$emit("showResultsModal");
      } else if (this.saveResultAfter) {
        this.$emit("saveResult");
      }
      this.close();
    },
  },
});
</script>

<style scoped>
.log-in-wrapper {
  position: absolute;
  top: 40%;
  left: 50%;
  transform: translate(-50%, -50%);
  z-index: 1000;
}
.form-container {
  position: relative;
  display: flex;
  flex-direction: column;
  align-items: center;
  width: 600px;
  height: 400px;
  border-radius: 10px;
  border: 1px solid rgb(113, 127, 191);
  align-items: stretch;
  color: rgb(113, 127, 191);
  background-color: azure;
}

.header > h2 {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 80px;
  width: 100%;
  background-color: transparent;
  border-radius: 10px 10px 0 0;
  font-weight: 500;
  color: rgb(28, 59, 194);
}

.username-container,
.password-container {
  display: flex;
  flex-direction: column;
}

.main-container > div {
  margin: 5px 0 5px 0;
}

#password,
#username {
  width: 400px;
  height: 30px;
  border-width: 1px;
  border-radius: 7px;
  padding: 10px;
  font-size: 15px;
  margin: 5px 0 5px 0;
  border: 1px solid rgb(113, 127, 191);
  outline: none !important;
  color: rgb(28, 59, 194);
}

#password::placeholder,
#username::placeholder {
  color: rgb(113, 127, 191);
}

.main-container {
  flex: 1;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  width: 100%;
}

.log-in-btn-container {
  margin-top: 15px !important;
}

::v-deep #password:focus,
::v-deep #username:focus,
::v-deep #log-in-btn:hover {
  border-color: rgb(36, 75, 229) !important;
  box-shadow: rgba(136, 234, 225, 0.966) 0 0 1px 1px !important;
}
</style>
