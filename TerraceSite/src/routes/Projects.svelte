<script>
  import RenderMD from "../components/RenderMD.svelte";
  import { rcObject } from "../js/api";
  import Project from "../components/Project.svelte";

  let navStyle = `
<style>
.nav-link:hover {
  background-color: rgba(180,182,181,0.58) !important;
}
.nav-bg{
  background-color: rgba(98,98,98,0.58) !important;
}
</style>`;

  let projects = rcObject("projects/projects.json");
</script>

{@html navStyle}

<div class="page fira bg-slate-700 p-3">
  <div>
    <RenderMD path="projects" name="description" />
  </div>
  <div>
    {#await projects}
      Loading Projects...
    {:then loadedProjects}
      <div class="flex flex-wrap">
        {#each loadedProjects as project}
          <Project projectData={project} />
        {/each}
      </div>
    {/await}
  </div>
</div>

<style lang="scss">
  @import "../styles/global";
</style>
