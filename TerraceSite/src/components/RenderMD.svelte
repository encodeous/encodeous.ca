<script>
  import { rcIndex, renderMdPath } from "../js/api";

  export let path = "";
  export let name = "";
</script>

{#await rcIndex(path, name) then path}
  {#await renderMdPath(path) then value}
    {@html value}
  {/await}
{/await}

<style lang="scss">
  :global(a) {
    @apply text-blue-400 hover:text-blue-300;
  }
  :global(table) {
    @apply table-auto text-sm text-left text-gray-500 dark:text-gray-400 overflow-x-auto;
  }
  :global(thead) {
    @apply bg-white border-b;
  }
  :global(tr) {
    @apply bg-white border-b transition duration-300 ease-in-out hover:bg-gray-100;
  }
  :global(th) {
    @apply text-sm font-medium text-gray-900 px-2 py-2 text-left;
  }
  :global(td) {
    @apply text-sm text-gray-900 font-light px-2 py-2 whitespace-nowrap;
  }
</style>
