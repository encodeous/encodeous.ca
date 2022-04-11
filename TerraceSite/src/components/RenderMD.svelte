<script>
    import {rcIndex, renderMdPath} from "../js/api";

    export let path = "";
    export let name = "";
</script>
{#await rcIndex(path, name) then path}
    {#await renderMdPath(path) then value}
        {@html value}
    {/await}
{/await}
