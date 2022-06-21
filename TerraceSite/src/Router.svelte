<script>
  import { Router, Link, Route } from "svelte-routing";
  import Home from "./routes/Home.svelte";
  import { getVersion } from "./js/api";
  import Projects from "./routes/Projects.svelte";

  export let url = "";
</script>

<div class="page-container">
  <div />
  <Router {url}>
    <nav class="nav nav-bg">
      <a
        style="margin-left: 20px;"
        href="https://github.com/encodeous/Terrace"
        class="nav-section"
      >
        encodeous/Terrace
        {#await getVersion()}
          #...
        {:then value}
          #{value}
        {/await}
      </a>
      <div class="nav-section" />
      <div class="nav-section">
        <div class="nav-item">
          <Link class="nav-link" to="/">Home</Link>
        </div>
        <div class="nav-item">
          <Link class="nav-link" to="/projects">Projects</Link>
        </div>
        <div class="nav-item">
          <a
            class="nav-link"
            target="_blank"
            href="https://github.com/encodeous">GitHub</a
          >
        </div>
      </div>
    </nav>
    <Route path="/projects" component={Projects} />
    <Route path="/" component={Home} />
  </Router>
</div>

<style lang="scss">
  @import "styles/global";
  .page-container {
    display: grid;
    grid-template-rows: auto 1fr;
    height: 100%;
  }
  .nav {
    display: grid;
    grid-template-columns: auto 1fr auto;
    align-items: center;
    position: fixed;
    width: 100%;
    z-index: 100;
  }
  :global(.nav-bg) {
    @extend .acrylic;
  }
  .nav-section {
    display: flex;
    @extend .fira;
    padding-left: 10px;
    padding-right: 10px;
  }
  .nav-item {
    display: flex;
    height: 100%;
    align-items: center;
  }
  :global(.nav-link) {
    text-decoration: none;
    color: white;
    padding: 10px;
    border-radius: 2px;
    transition: 1.5s ease-out;
    margin: 0;
  }
  :global(.nav-link:hover) {
    backdrop-filter: brightness(0.1);
    background-color: rgba(0, 0, 0, 0.58);
  }
  a {
    text-decoration: none;
  }
  .acrylic {
    backdrop-filter: blur(100px) brightness(0.8);
  }
</style>
