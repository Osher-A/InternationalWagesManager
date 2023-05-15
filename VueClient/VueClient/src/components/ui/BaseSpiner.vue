<template>
<div class="container">
  <div class="spinner spinner-border"  role="status"  >
  </div>
  <div class="item">Loading...</div>
</div>
</template>

<style scoped>
.spinner {
  width: 4rem;
  height: 4rem;
}
.container{
  margin-top: 10%;
  display: grid;
  justify-content: center;
}
.item{
  justify-self: center;
  margin-top: 5px;
  font-size:medium;
  font-weight: bold;
}
</style>