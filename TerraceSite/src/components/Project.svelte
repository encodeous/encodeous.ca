<script>
  // https://api.encodeous.ca/loadpath/projects/projects.json
  export let projectData;
</script>

<div
  class="bg-white hover:bg-slate-50 rounded-sm text-black p-6 max-w-sm m-5 h-min shadow-slate-400
   shadow-md hover:shadow-slate-400 hover:shadow-sm transition bg-cover bg-center relative"
  style={projectData.imageUrl !== ""
    ? `background-image: url(${projectData.imageUrl})`
    : ""}
>
  <div class="z-20 relative">
    <div
      class="text-4xl font-bold text-white bg-black w-min hover:text-slate-200 transition"
    >
      <div class="ml-1 mr-1">
        {projectData.projectName}
      </div>
    </div>
    {#if projectData.tags !== []}
      <div class="flex">
        {#each projectData.tags as tag}
          <p
            class="text-white bg-slate-700 pl-2 pr-2 mr-1 rounded-lg hover:bg-slate-900 transition"
          >
            {tag}
          </p>
        {/each}
      </div>
    {/if}
    <div>
      {projectData.description}
    </div>
    <div class="mt-3 float-right">
      {#if projectData.repoUrl !== ""}
        <a
          class="bg-blue-300 rounded-sm p-2 hover:bg-blue-500 text-black hover:text-slate-100 transition"
          href={projectData.repoUrl}
          target="_blank"
        >
          View On GitHub
        </a>
      {/if}
    </div>
  </div>
  <div
    class="backdrop-blur-xl absolute w-full h-full top-0 right-0 z-0 brightness-150 backdrop-contrast-50 hover:opacity-95 transition"
  />
</div>

<style>
</style>
