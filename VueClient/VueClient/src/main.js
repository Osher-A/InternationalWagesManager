import "bootstrap/dist/css/bootstrap.css"
import { createApp } from 'vue'
import App from './App.vue'
import router from "./pages/employees/Router"
import BaseHeader from './components/ui/BaseHeader.vue'
import BaseCard from './components/ui/BaseCard.vue'
import BaseTable from './components/ui/BaseTable.vue'
import BaseDiv from './components/ui/BaseDiv.vue'
import BaseSpinner from './components/ui/BaseSpiner.vue'
import BaseDialog from './components/ui/BaseDialog.vue'
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
