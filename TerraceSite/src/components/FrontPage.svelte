<script>
    import {onMount} from "svelte";

    const app = new PIXI.Application({
        autoResize: true,
        resolution: devicePixelRatio
    });
    // Create the sprite and add it to the stage
    let obj = new PIXI.Graphics();
    obj.beginFill(0xff0000);
    obj.drawRect(0, 0, 200, 100);
    app.stage.addChild(obj);

    // Add a ticker callback to move the sprite back and forth
    let elapsed = 0.0;
    app.ticker.add((delta) => {
        elapsed += delta;
        obj.x = 100.0 + Math.cos(elapsed/50.0) * 100.0;
    });
    let container;
    onMount(() => {
        container.appendChild(app.view);
        return () => {
            // free memory
        };
    });

    window.addEventListener('resize', resize);

    // Resize function window
    function resize() {
        // Resize the renderer
        app.renderer.resize(window.innerWidth, window.innerHeight);
    }

    resize();

</script>

<div bind:this={container}/>