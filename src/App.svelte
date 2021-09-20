<script>
    import {Theme} from "carbon-components-svelte";
    import "carbon-components-svelte/css/all.css";
    import {Router, Link, Route} from "svelte-routing";
    import {getRoutesAsync} from "./Pages/page-config.js"
    import Layout from "./Layout.svelte";
    import {theme} from "./Shared/store";
    import {onMount} from "svelte";
    const dark = "g90", light = "g10";

    $: document.documentElement.setAttribute("theme", $theme);

    onMount(() => {
        theme.set(localStorage.getItem("site-theme") ?? light)
    });
</script>

<Theme bind:theme={$theme} />

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