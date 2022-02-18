<script>
    import {API_URL} from "../js/api";
    import TypeOMatic from "../components/TypeOMatic.svelte";
    import { onMount } from 'svelte';

    const greetingName = "Hey There!\nI'm Adam.\nWelcome to my site!";

    let degree = -20;
    let newDegree = -20;
    let velocity = 0;
    let acceleration = 0;

    $: blue_style = `
        <style>
        html, body{
            background: linear-gradient(${degree + 90}deg, rgb(67,55,147) 12%, rgb(102,133,190) 45%, rgb(65, 83, 162) 100%);
        }
        </style>
        `;

    function handleMousemove(event) {
        let x = window.innerWidth / 2;
        let y = window.innerHeight / 2;
        let nx = event.clientX;
        let ny = event.clientY;
        newDegree = Math.atan2(y - ny, x - nx)  * 180 / Math.PI;
        newDegree %= 360;
    }

    onMount(async () => {
        let rid = requestAnimationFrame(function update() {
            degree = velocity + degree;
            degree %= 360;
            var delta = newDegree - degree;
            if(Math.abs(delta) > 180){
                if(delta < 0){
                    delta = 360 + delta;
                }else{
                    delta = 360 - delta;
                }
            }
            acceleration = delta / 10000;
            velocity /= 1.14;
            velocity += acceleration;
            // console.log(acceleration + " " + velocity + " " + newDegree + " " + degree);
            newDegree %= 360;
            rid = requestAnimationFrame(update);
        });
        return () => cancelAnimationFrame(rid);
    });

</script>

<svelte:window on:mousemove={handleMousemove}/>

{@html blue_style}

<div class="main-page">
    <div class="main-grid">
        <div class="greeting">
            <TypeOMatic text={greetingName}/>
        </div>
        <div class="extra-info">
            <p>
                This site is still under construction :)
            </p>
        </div>
    </div>
</div>

<style lang="scss">
  @import "../styles/global";
  .main-page{
    display: flex;
    align-items: center;
    height: 100%;
  }
  .main-grid{
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
  .extra-info{
  }
</style>