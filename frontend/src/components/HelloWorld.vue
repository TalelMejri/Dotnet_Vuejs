<template>
  <div>
      {{ test }}
  </div>
</template>

<script lang="ts">
import { Options, Vue,} from 'vue-class-component';
@Options({
  components:{
    //lahne componenet
  },
  mounted(){
    console.log(this.test);
    this.getTest();
  },
  methods:{
    getTest():void{
      alert(this.tab[0].name);
    }
  }
})

export default class HelloWorld extends Vue {
  test: string="test test";
  tab:any=[
    {
      name:"qd",desc:"dqsd"
    },
    {
      name:"qd",desc:"dqsd"
    }
  ];
  created(): void {
      console.log(this.tab);
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
