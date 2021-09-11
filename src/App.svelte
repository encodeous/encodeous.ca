<svelte:head>
	<title>
		Theres nothing here, yet...
	</title>
</svelte:head>

<script>
	import { spring } from 'svelte/motion';
	export let name;
	let count = 0;

	setInterval(()=>{
		count++;
	}, 1000);

	function handleMouseMovements(node){
		let coords = spring({x: 0, y: 0}, {
			damping: 0.4,
			stiffness: 0.2
		});
		let rect = node.getBoundingClientRect();
		coords.subscribe(cur =>{
			node.style.transform = `translate3d(${cur.x}px, ${cur.y}px, 0)`
		})
		window.addEventListener('mousemove', mouseMove);
		let prevX = (rect.left + rect.right) / 2, prevY = (rect.top + rect.bottom) / 2;
		function mouseMove(event){
			coords.update(()=>{
				return {x: event.clientX - prevX, y: event.clientY - prevY};
			})
		}
	}
</script>

<main>
	<h1>Hello, {name}!</h1>
	<p>This is just a filler page.</p>
	<i use:handleMouseMovements class="float">
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

	.float{
		position: absolute;
	}
</style>