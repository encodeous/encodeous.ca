<script>
    import {API_URL} from "../js/api";
    import TypeOMatic from "../components/TypeOMatic.svelte";

    const fetchImage = (async () => {
        const response = await fetch(API_URL + "/WeatherForecast")
        return await response.json()
    })()
    const greetingName = "Hey There!\nI'm Adam.\nWelcome to my site!";
</script>

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



<style lang="scss">
  @import "../styles/global";
  :global(html, body){
    @extend .terri-blue;
  }
  .main-grid{
    //@extend .tserra;
    @extend .fira;
    display: grid;
    grid-template-columns: repeat(7, 1fr);
    grid-template-rows: repeat(4, 1fr);
    grid-column-gap: 0;
    grid-row-gap: 0;
    height: 100%;
  }
  .greeting {
    grid-area: 2 / 2 / 4 / 4;
    display: inline-block;
  }
  .extra-info{
    grid-area: 4 / 2 / 8 / 4;
  }
</style>