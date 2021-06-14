import Vue from 'vue';
import App from './App.vue';
import router from './router'
import firebase from "firebase";

Vue.config.productionTip = true;

const firebaseConfig = {
    apiKey: "AIzaSyCUN0d9ZrwotrT2PEC2lKp8Ly-u4pGU60E",
    authDomain: "collaborative-authoring.firebaseapp.com",
    projectId: "collaborative-authoring",
    storageBucket: "collaborative-authoring.appspot.com",
    messagingSenderId: "619988519259",
    appId: "1:619988519259:web:767934029872ae9ab8b27e"
  };
firebase.initializeApp(firebaseConfig);
firebase.analytics();

new Vue({
    vuetify,
    router,
    render: h => h(App)
}).$mount('#app');
