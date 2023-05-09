import "bootstrap/dist/css/bootstrap.css"
import { createApp } from 'vue'
import App from './App.vue'
import router from "./pages/employees/Router"
import BaseHeader from './components/BaseHeader.vue'
import BaseCard from './components/BaseCard.vue'
import BaseTable from './components/BaseTable.vue'
import BaseDiv from './components/BaseDiv.vue'
import BaseSpinner from './components/BaseSpiner.vue'
import BaseDialog from './components/BaseDialog.vue'
import MyStore from './store/index'


const app = createApp(App)

app.use(router);
app.use(MyStore);

app.component('base-header', BaseHeader);
app.component('base-card', BaseCard);;
app.component('base-table', BaseTable);
app.component('base-div', BaseDiv)
.component('base-spinner', BaseSpinner)
.component('base-dialog', BaseDialog);

app.mount('#app')

import "bootstrap/dist/js/bootstrap.js";
