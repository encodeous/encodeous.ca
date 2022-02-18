<script>
    import { onMount } from 'svelte';
    export let text = "";
    let curText = "";
    let len = 0;
    onMount(async () => {
        for(const c of text){
            await new Promise(r => setTimeout(r, Math.random() * 50 + 60));
            curText += c;
        }
    });
</script>

<pre class="text">{curText}<span class="cursor-blink text">_</span></pre>


<style lang="scss">
  @import "../styles/global";
  .text {
    @extend .fira;
    font-size: 40pt;
    font-weight: 800;
  }
  pre {
    white-space: pre-wrap;
    padding: 0;
    margin: 0;
  }
  .cursor-blink{
    animation: blink 0.8s infinite;
    -webkit-animation: blink 0.8s infinite;
    animation: blink 0.8s infinite;
  }
  @keyframes blink{
    50% { opacity: 0.0; }
  }
  @-webkit-keyframes blink{
    0% { opacity: 1; }
    50% { opacity: 0.0; }
    100% { opacity: 1; }
  }

</style>