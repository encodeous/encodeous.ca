<script>
import RenderMD from "../components/RenderMD.svelte";

let navStyle = `
<style>
.nav-link:hover {
  background-color: rgba(180,182,181,0.58) !important;
}
.nav-bg{
  background-color: rgba(98,98,98,0.58) !important;
}
</style>`
</script>

{@html navStyle}

<style lang="scss">
  @import "../styles/global";
.page-bg{
  background-color: black;
  color: white;
}

</style>

<div class="page page-bg fira">
    <div>
        <RenderMD path="projects" name="description"/>
    </div>
</div>