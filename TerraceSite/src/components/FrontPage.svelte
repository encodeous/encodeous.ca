<script lang="ts">
    import Victor from "victor";
    import {onMount} from "svelte";

    let time = 0;
    let prevTick;
    let canvas;
    onMount(() => {
        let ctx : CanvasRenderingContext2D = canvas.getContext("2d");
		let frame = requestAnimationFrame(loop);
        prevTick = Date.now();
        initialize();
        window.addEventListener('resize', initialize);
        window.addEventListener('mousemove', ev => {
            cursorPos = new Victor(ev.x - lPad, ev.y - tPad);
        })

        function loop(t) {

			frame = requestAnimationFrame(loop);
            render(ctx);
            time += Date.now() - prevTick;
            prevTick = Date.now();
		}

		return () => {
			cancelAnimationFrame(frame);
		};
	});

    let cellSize = 20;
    /**
     * @type Array<Array<int>>
     */
    let cellData : Array<Array<number>> = [];
    let gridData : Array<Array<number>> = [];
    let grid : Array<Array<boolean>> = [];
    let parent : Array<number>;
    let rows, cols;
    let tPad = 0, lPad = 0;
    let padding = 2;
    let cursorPos : Victor = new Victor(0, 0);

    function getPosHash(i : number, j : number) : number{
        return (i + 1) * Math.max(rows, cols) + j;
    }

    function getPosFromHash(x : number) : [number, number]{
        let j = x % Math.max(rows, cols);
        let i = (x - j) / Math.max(rows, cols) - 1;
        return [i, j];
    }

    function find(x : number) : number{
        if(parent[x] == x) return x;
        return parent[x] = find(parent[x]);
    }

    function initialize(){
        let ctx : CanvasRenderingContext2D = canvas.getContext("2d");
        let c = ctx.canvas;
        let w = window.innerWidth;
        let h = window.innerHeight;
        rows = Math.floor(h / cellSize);
        cols = Math.floor(w / cellSize);
        parent = [];
        if(rows % 2 == 0) rows--;
        if(cols % 2 == 0) cols--;
        for (let i = 0; i < cols; i++) {
            cellData[i] = [];
            for(let j = 0; j < rows; j++){
                cellData[i][j] = 0;
            }
        }
        for (let i = 0; i < cols; i++) {
            grid[i] = [];
            for(let j = 0; j < rows; j++){
                grid[i][j] = false;
            }
        }
        let edges : Array<[number, number, number]> = [];
        for (let i = 0; i < cols; i++) {
            gridData[i] = [];
            for(let j = 0; j < rows; j++){
                if(i % 2 == 0 && j % 2 == 0){
                    let hash = getPosHash(i, j);
                    gridData[i][j] = hash;
                    parent[hash] = hash;
                    if(j < rows - 1){
                        let cur = getPosHash(i, j);
                        edges.push([cur, getPosHash(i, j + 2), getPosHash(i, j + 1)]);
                    }
                    if(i < cols - 1){
                        let cur = getPosHash(i, j);
                        edges.push([cur, getPosHash(i + 2, j), getPosHash(i + 1, j)]);
                    }
                }
                else{
                    gridData[i][j] = 0;
                }
            }
        }
        shuffleArray(edges);
        let tTime = 0;
        let tInterval = 5000 * (1 / edges.length);
        for(let val of edges){
            let p1 = find(val[0]);
            let p2 = find(val[1]);
            if(p1 != p2){
                parent[p1] = p2;
                for(let hash of val){
                    let pos = getPosFromHash(hash);
                    if(!grid[pos[0]][pos[1]]){
                        grid[pos[0]][pos[1]] = true;
                        placeTileAfter(pos[0], pos[1], tTime, true);
                    }
                }
                tTime += tInterval;
            }
        }
        tPad = (h - rows * cellSize) / 2;
        lPad = (w - cols * cellSize) / 2;
        c.width = cellSize * cols + padding;
        c.height = cellSize * rows + padding;
    }

    function placeTileAfter(i : number, j : number, delay : number, isWall : boolean){
        if(isWall){
            cellData[i][j] = time + delay;
        }else{
            cellData[i][j] = -(time + delay);
        }
    }

    function shuffleArray(array) {
        for (let i = array.length - 1; i > 0; i--) {
            const j = Math.floor(Math.random() * (i + 1));
            [array[i], array[j]] = [array[j], array[i]];
        }
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
        let dist = Math.max(Math.abs(middle.x - cursorPos.x), Math.abs(middle.y - cursorPos.y));
        ctx.fillStyle = "#2d2d2d"
        if(cellData[i][j] != 0){
            if(cellData[i][j] > 0){
                // wall data
                let maxMs = 5000;
                let before = time - cellData[i][j];
                let value = 45 - linClamp(Math.min(maxMs, before), 0, maxMs) * (45 - 30);
                ctx.fillStyle = `rgb(${value}, ${value}, ${value})`;

            }else{
                // path data
                let realData = -cellData[i][j];
            }

            if(dist < cellSize / 2){
                ctx.fillStyle = "white"
            }
        }
        ctx.fillRect(x1, y1, cellSize - padding, cellSize - padding);
    }

</script>

<div class="c-bg">
    <canvas bind:this={canvas}/>
</div>

<style>
	canvas {
		background-color: #1e1e1e;
	}
    .c-bg{
        margin: 0;
        position: fixed;
        height: 100%;
        width: 100%;
        display: flex;
        align-items: center;
        justify-content: center;
        background-color: #2d2d2d;
    }
</style>