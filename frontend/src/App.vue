<template>
   <div id="app">
      <router-view/>
   </div>
</template>


<script>
import AOS from "aos";
import 'animate.css';
export default {
   mounted(){
      AOS.init();
   }
  }
</script>
<style >
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}


</style>
