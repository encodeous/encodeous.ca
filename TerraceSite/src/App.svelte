<script>
  import Router from "./Router.svelte";

  export let url = "";
</script>

<Router {url} />

<style global lang="postcss">
  @tailwind base;
  @layer base {
    h1,
    h2,
    h3,
    h4,
    h5,
    h6 {
      font-size: revert;
      font-weight: revert;
    }
    ol,
    ul,
    ol {
      list-style: revert;
      margin: revert;
      padding: revert;
    }
    blockquote,
    dl,
    dd,
    h1,
    h2,
    h3,
    h4,
    h5,
    h6,
    hr,
    figure,
    p,
    pre {
      margin: revert;
    }
  }
  @tailwind components;
  @tailwind utilities;
</style>
