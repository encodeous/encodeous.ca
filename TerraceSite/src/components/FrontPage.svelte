<script lang="ts">
    import Victor from "victor";
    import {onMount} from "svelte";

    let canvas;
    onMount(() => {
        let ctx : CanvasRenderingContext2D = canvas.getContext("2d");
		let frame = requestAnimationFrame(loop);

        initialize();
        window.addEventListener('resize', initialize);
        window.addEventListener('mousemove', ev => {
            cursorPos = new Victor(ev.x - lPad, ev.y - tPad);
        })

        function loop(t) {

			frame = requestAnimationFrame(loop);
            render(ctx);
		}

		return () => {
			cancelAnimationFrame(frame);
		};
	});

    let cellSize = 40;
    /**
     * @type Array<Array<int>>
     */
    let cellData = [];
    let rows, cols;
    let tPad = 0, lPad = 0;
    let padding = 2;
    let cursorPos : Victor = new Victor(0, 0);

    function initialize(){
        let ctx : CanvasRenderingContext2D = canvas.getContext("2d");
        let c = ctx.canvas;
        let w = window.innerWidth;
        let h = window.innerHeight;
        rows = Math.floor(h / cellSize);
        cols = Math.floor(w / cellSize);
        for (let i = 0; i < cols; i++) {
            cellData[i] = [];
            for(let j = 0; j < rows; j++){
                cellData[i][j] = 0;
            }
        }
        tPad = (h - rows * cellSize) / 2;
        lPad = (w - cols * cellSize) / 2;
        c.width = cellSize * cols + padding;
        c.height = cellSize * rows + padding;
    }

    function render(ctx : CanvasRenderingContext2D){
        for (let i = 0; i < cols; i++) {
            for(let j = 0; j < rows; j++){
                renderCell(i, j, ctx);
            }
        }
    }

    function linClamp(x: number, min: number, max: number){
        return (x - min) / (max - min);
    }

    function renderCell(i, j, ctx : CanvasRenderingContext2D) {
        let x1 = i * cellSize + padding;
        let y1 = j * cellSize + padding;
        let offset = cellSize / 2;
        let middle = new Victor(i * cellSize + offset, j * cellSize + offset);
        let dist = middle.distance(cursorPos);
        if(dist < cellSize / 2){
            ctx.fillStyle = "rgb(255, 255, 255)";
        }
        else if(dist < 2 * cellSize){
            let value = 100 + -linClamp(dist, cellSize / 2, 2 * cellSize) * (100 - 28);
            ctx.fillStyle = `rgb(${value}, ${value}, ${value})`;
        }
        else{
            ctx.fillStyle = "#1c1c1c"
        }
        ctx.fillRect(x1, y1, cellSize - padding, cellSize - padding);
    }

</script>

<div class="c-bg">
    <canvas bind:this={canvas}/>
</div>

<style>
	canvas {
		background-color: #262626;
	}
    .c-bg{
        margin: 0;
        position: fixed;
        height: 100%;
        width: 100%;
        display: flex;
        align-items: center;
        justify-content: center;
        background-color: #1e1e1e;
    }
</style>