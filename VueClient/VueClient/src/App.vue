<template>
  <section>
   <the-nav></the-nav>
   <router-view></router-view>
  </section>
</template>

<script>
 import TheNav from './components/ui/TheNav.vue';

 export default ({
  components: {
    TheNav
  }
 })
</script>

<style>
@import url("https://fonts.googleapis.com/css2?family=Roboto:wght@400;700&display=swap");

* {
  box-sizing: border-box;
}

html {
  font-family: "Roboto", sans-serif;
}

body {
  margin: 0;
}
</style>
