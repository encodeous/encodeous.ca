<script lang="ts">
  import { onMount } from "svelte";
  import { quartIn } from "svelte/easing";

  export let char;
  export let visible = false;
  let tDuration = rescale(Math.random(), 2000, 3500);

  function rescale(val, min, max) {
    return (max - min) * val + min;
  }
  function fceffects(node, {}) {
    var start = rescale(Math.random(), 0, 40);
    return {
      duration: tDuration,
      css: (t) => {
        const eased = quartIn(t);
        return `
        color: transparent;
        opacity: ${rescale(eased, start, 100)}%;
        text-shadow: 0 0 ${10 - rescale(eased, 5, 10)}px rgba(0,0,0,0.9);
        `;
      },
    };
  }

  onMount(() => {
    visible = true;
  });
</script>

{#if visible}
  <div in:fceffects class="large fcstyle raleway">
    {char}
  </div>
{/if}

<style lang="scss">
  //mixin to provide just a basic outline for the text
  @mixin outline($color: #fff) {
    text-shadow: 1.5px 1.5px 0 $color, -1.5px -1.5px 0 $color,
      -1.5px -1.5px 0 $color, -1.5px 1.5px 0 $color, 1.5px -1.5px 0 $color;
  }
  .fcstyle {
    display: inline-block;
  }
</style>
