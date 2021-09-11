<svelte:head>
	<title>
		Theres nothing here, yet...
	</title>
</svelte:head>

<script>
	export let name;
	let count = 0;

	setInterval(()=>{
		count++;
	}, 1000);
</script>

<main>
	<h1>Hello, {name}!</h1>
	<p>This is just a filler page.</p>
	<i>
		Watch as this counter counts up: {count}
	</i>
</main>

<style>
	main {
		text-align: center;
		padding: 1em;
		max-width: 240px;
		margin: 0 auto;
	}

	h1 {
		color: #ff3e00;
		text-transform: uppercase;
		font-size: 4em;
		font-weight: 100;
	}

	@media (min-width: 640px) {
		main {
			max-width: none;
		}
	}
</style>