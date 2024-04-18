<template>
  <BaseLayout>
    <div class="d-flex justify-content-center" v-if="loading">
      <img
        src="../assets/images/logo.png"
        alt="logo"
        width="300px"
        class="rotating-pulsating"
      />
    </div>

    <BaseDialog
      v-if="topicDialogVisible"
      :visible="topicDialogVisible"
      header="Új téma felvétele"
      :width="'25rem'"
    >
      <FormKit
        type="form"
        :actions="false"
        @submit="addTopic"
        incomplete-message="
          
        "
      >
        <FormKit
          type="text"
          name="name"
          label="name"
          validation="required|length:0,60"
          :validation-messages="{
            required: 'asd',
            length: 'asd',
          }"
          :classes="{
            input: {
              'mb-1': true,
              'form-control': true,
            },
          }"
        />

        <FormKit
          type="text"
          name="order"
          label="order"
          validation="required|length:0,50"
          :validation-messages="{
            required: 'asd',
            length: 'asd',
          }"
          :classes="{
            input: {
              'mb-1': true,
              'form-control': true,
            },
          }"
        />

        <div class="d-flex justify-content-end mt-2 mb-3">
          <Button
            type="button"
            class="btn btn-outline-danger mx-1"
            @click="topicDialogVisible = false"
            >Mégse</Button
          >
          <FormKit
            type="submit"
            label="
              Mentés
            "
            :classes="{
              input: {
                btn: true,
                'btn-success': true,
                'w-auto': true,
              },
            }"
          />
        </div>
      </FormKit>
    </BaseDialog>

    <div v-if="!loading">
      <h1 class="text-center my-3">
        {{ course.name }}
        <button
          class="btn btn-info"
          v-if="currentUserData.is_admin"
          @click="topicDialogVisible = true"
        >
          Új Téma
        </button>
      </h1>

      <div
        class="accordion mb-3"
        id="accordionExample"
        v-for="topic in topics"
        :key="topic.id"
      >
        <div class="accordion-item">
          <h2 class="accordion-header" :id="'heading' + topic.id">
            <button
              class="accordion-button"
              type="button"
              :class="{ collapsed: activeTopicId !== topic.id }"
              :data-bs-toggle="activeTopicId !== topic.id ? 'collapse' : ''"
              :data-bs-target="'#collapse' + topic.id"
              :aria-expanded="activeTopicId === topic.id ? 'true' : 'false'"
              :aria-controls="'collapse' + topic.id"
            >
              <h2>{{ topic.name }}</h2>
              <button
                v-if="currentUserData.is_admin"
                class="ms-2 btn btn-success text-light"
              >
                Szerkesztés
              </button>
              <button
                v-if="currentUserData.is_admin"
                class="ms-2 btn btn-danger text-light"
                @click="deleteTopic(topic.id)"
              >
                Törlés
              </button>
            </button>
          </h2>
          <div
            :id="'collapse' + topic.id"
            class="accordion-collapse collapse"
            :class="{ show: activeTopicId === topic.id }"
          >
            <div class="accordion-body">
              <strong>This is the accordion body for {{ topic.name }}.</strong>
              It is shown by default, until the collapse plugin adds the
              appropriate classes that we use to style each element. These
              classes control the overall appearance, as well as the showing and
              hiding via CSS transitions. You can modify any of this with custom
              CSS or overriding our default variables. It's also worth noting
              that just about any HTML can go within the
              <code>.accordion-body</code>, though the transition does limit
              overflow.
            </div>
          </div>
        </div>
      </div>
    </div>
  </BaseLayout>
</template>

<script>
import BaseLayout from "@layouts/BaseLayout.vue";
import { mapState, mapActions } from "pinia";
import { courseStore } from "@stores/CourseStore.mjs";
import { userStore } from "@stores/UserStore.mjs";
import { topicStore } from "@stores/TopicStore.mjs";
import BaseDialog from "@components/BaseDialog.vue";
import { http } from "@utils/http.mjs";
import Button from "primevue/button";

export default {
  data() {
    return {
      course: {},
      topics: [],
      loading: true,
      topicDialogVisible: false,
      activeTopicId: null,
    };
  },
  components: {
    BaseLayout,
    BaseDialog,
    Button,
  },
  methods: {
    ...mapActions(courseStore, ["getCourse"]),
    ...mapActions(topicStore, ["postTopic", "putTopic", "destroyTopic"]),

    async deleteTopic(id) {
      await this.destroyTopic(id);
      this.course = await this.getCourse(this.$route.params.id);
      this.topics = this.course.topics;
    },

    async addTopic(data) {
      const response = await this.postTopic(data);

      await http.post(
        `/courses/${this.$route.params.id}/topics`,
        {
          topic_ids: [response.data.data.id],
        },
        {
          headers: {
            Authorization: `Bearer ${this.token}`,
          },
        }
      );

      this.course = await this.getCourse(this.$route.params.id);
      this.topics = this.course.topics;
      this.topicDialogVisible = false;
    },
  },
  computed: {
    ...mapState(userStore, ["currentUserData", "token"]),
  },
  async mounted() {
    this.course = await this.getCourse(this.$route.params.id);
    this.topics = this.course.topics;
    this.loading = false;
  },
};
</script>
