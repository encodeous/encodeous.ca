<script>
    import {Router, Link, Route} from "svelte-routing";
    import {getRoutesAsync} from "./Pages/page-index.js"
    import Layout from "./Layout.svelte";
</script>
<Layout>
    {#await getRoutesAsync()}
        Dynamically loading routes... This message should never show in normal circumstances.
    {:then value}
        <Router>
            {#each value as q}
                <Route path={q[0]} component="{q[1]}" />
            {/each}
        </Router>
    {/await}
</Layout>