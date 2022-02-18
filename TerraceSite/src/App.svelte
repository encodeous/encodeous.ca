<script>
	import { Router, Link, Route } from "svelte-routing";
	import Home from "./routes/Home.svelte";
	import { getVersion } from "./js/api";

	export let url = "";
</script>

<div class="page-container">
	<div />
	<Router {url}>
		<nav class="nav">
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
					<a
						class="nav-link"
						target="_blank"
						href="https://github.com/encodeous">GitHub</a
					>
				</div>
			</div>
		</nav>
		<div>
			<Route path="/">
				<Home />
			</Route>
		</div>
	</Router>
</div>

<style lang="scss">
	@import "styles/global";
	.page-container {
		display: grid;
		grid-template-rows: 55px auto;
		height: 100%;
	}
	.nav {
		display: grid;
		grid-template-columns: auto 1fr auto;
		align-items: center;
		position: absolute;
		width: 100%;
	}
	.nav-section {
		display: flex;
		@extend .fira;
	}
	.nav-item {
		display: flex;
		height: 100%;
		margin-left: 5px;
		margin-right: 5px;
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
		backdrop-filter: hue-rotate(-100deg);
	}
	a {
		text-decoration: none;
	}
</style>
