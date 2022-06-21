<script>
  import TypeOMatic from "../components/TypeOMatic.svelte";
  import { onMount } from "svelte";
  import { rcIndexedText } from "../js/api";
  import RenderMD from "../components/RenderMD.svelte";
  import FrontPage from "../components/FrontPage.svelte";

  let degree = -20;
  let newDegree = -20;
  let velocity = 0;
  let acceleration = 0;

  let pct = 45;
  let pctV = 0;

  let bgStyle;

  $: bgStyle = `background: linear-gradient(${
    degree + 90
  }deg, rgb(67,55,147) 5%, rgb(102,133,190) ${pct}%, rgb(65, 83, 162) 100%)`;

  function handleMousemove(event) {
    let x = window.innerWidth / 2;
    let y = window.innerHeight / 2;
    let nx = event.clientX;
    let ny = event.clientY;
    let dx = x - nx;
    let dy = y - ny;
    newDegree = (Math.atan2(y - ny, x - nx) * 180) / Math.PI;
    newDegree %= 360;
  }

  onMount(async () => {
    let rid = requestAnimationFrame(function update() {
      degree = velocity + degree;
      degree %= 360;
      var delta = newDegree - degree;
      if (Math.abs(delta) > 180) {
        if (delta < 0) {
          delta = 360 + delta;
        } else {
          delta = delta - 360;
        }
      }
      acceleration = delta / 5000;
      velocity /= 1.07;
      let tPct = Math.max(velocity * velocity, 40);
      let deltaPct = Math.cbrt(tPct - pct) * 2;
      pct += deltaPct;
      velocity += acceleration;
      let sign = 1;
      if (velocity < 0) sign = -1;
      velocity = sign * Math.min(Math.abs(velocity), 0.005);
      // console.log(acceleration + " " + velocity + " " + newDegree + " " + degree);
      newDegree %= 360;
      rid = requestAnimationFrame(update);
    });
    return () => cancelAnimationFrame(rid);
  });
</script>

<svelte:window on:mousemove={handleMousemove} />

<div class="page-center">
  <div class="main-grid">
    <div class="greeting">
      {#await rcIndexedText("home", "typed") then value}
        <TypeOMatic text={value} />
      {/await}
    </div>
    <div class="extra-info">
      <p>
        <RenderMD path="home" name="subtitle" />
      </p>
    </div>
  </div>
</div>

<div class="fp-back">
  <FrontPage />
</div>

<style lang="scss">
  @import "../styles/global";
  .main-grid {
    //@extend .tserra;
    @extend .fira;
    display: grid;
    grid-template-rows: 1fr auto;
    grid-column-gap: 0;
    grid-row-gap: 0;
    padding: 80px;
  }
  .greeting {
    display: inline-block;
  }
  .extra-info {
  }
  .fp-back {
    position: absolute;
    pointer-events: none;
    z-index: -1;
  }
  //.nav-bg {
  //  @extend .acrylic;
  //}
</style>
