<script lang="ts">
    import Victor from "victor";
    import {onMount} from "svelte";

    let time = 0;
    let prevTick;
    let lastAgentTick = 0;
    let canvas;
    let shouldMoveAgent = false;
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
            if(time - lastAgentTick > 100){
                lastAgentTick = time;
                shouldMoveAgent = true;
            }else{
                shouldMoveAgent = false;
            }
            render(ctx);
            time += Date.now() - prevTick;
            prevTick = Date.now();
		}

		return () => {
			cancelAnimationFrame(frame);
		};
	});

    let cellSize;
    let cellData : Array<Array<number>> = [];
    let gridData : Array<Array<number>> = [];
    let grid : Array<Array<boolean>> = [];
    let traceGrid : Array<Array<boolean>> = [];
    let parent : Array<number>;
    let agents : Array<Victor>;
    let target : Victor = new Victor(0, 0);
    let rows, cols;
    let tPad = 0, lPad = 0;
    let padding = 2;
    let cursorPos : Victor = new Victor(0, 0);
    let totalAgents = 4;

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
        cellSize = Math.max(20, Math.floor(window.innerWidth / 60));
        let ctx : CanvasRenderingContext2D = canvas.getContext("2d");
        let c = ctx.canvas;
        let w = window.innerWidth;
        let h = window.innerHeight;
        rows = Math.floor(h / cellSize);
        cols = Math.floor(w / cellSize);
        agents = [];
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
        for(let i = 0; i < totalAgents; i++){
            addAgent();
        }
        tPad = (h - rows * cellSize) / 2;
        lPad = (w - cols * cellSize) / 2;
        c.width = cellSize * cols + padding;
        c.height = cellSize * rows + padding;
    }

    function addAgent() {
        let pi = null;
        while (pi == null || !grid[pi.x][pi.y])
            pi = new Victor(Math.floor(Math.random() * cols), Math.floor(Math.random() * rows));
        agents.push(pi);
    }

    function placeTileAfter(i : number, j : number, delay : number, isWall : boolean){
        if(isWall){
            cellData[i][j] = time + delay;
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
            traceGrid[i] = [];
            for(let j = 0; j < rows; j++){
                traceGrid[i][j] = false;
            }
        }
        for(let i = 0; i < agents.length; i++){
            let result = moveAgent(agents[i]);
            if(result != null && shouldMoveAgent){
                agents[i] = result;
            }
        }
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
        ctx.fillStyle = "#282828"
        if(cellData[i][j] != 0){
            if(cellData[i][j] > 0){
                // wall data
                let maxMs = 5000;
                let before = time - cellData[i][j];
                let value = 40 - linClamp(Math.min(maxMs, before), 0, maxMs) * (40 - 30);
                ctx.fillStyle = `rgb(${value}, ${value}, ${value})`;

            }
            if(traceGrid[i][j]){
                ctx.fillStyle = "#2f3a2f";
            }

            if(dist < cellSize / 2){
                target = new Victor(i, j);
            }
            if(i == target.x && j == target.y){
                ctx.fillStyle = "white";
                agents = agents.filter(x => {
                    return !(x.x == i && x.y == j);
                });
                let toAdd = totalAgents - agents.length;
                for(let i = 0; i < toAdd; i++){
                    addAgent();
                }
            }
        }
        for(let agent of agents){
            if(agent.x == i && agent.y == j){
                ctx.fillStyle = "#174754";
                break;
            }
        }
        ctx.fillRect(x1, y1, cellSize - padding, cellSize - padding);
    }

    function hashV(vector : Victor) : number{
        return getPosHash(vector.x, vector.y);
    }

    let mvarr = [0, 0, 1, -1];
    let mvarrc = [1, -1, 0, 0];

    function moveAgent(agent : Victor) : Victor{
        if(agent == null) return null;
        let q : Array<Victor> = [];
        let prev = new Map<number, number>();
        let visited = new Set<number>();
        q.push(agent);
        visited.add(hashV(agent));
        while(q.length != 0){
            let cur = q.shift();
            for(let i = 0; i < 4; i++){
                let nc = cur.clone().add(new Victor(mvarr[i], mvarrc[i]));
                let nch = hashV(nc);
                if(nc.x >= 0 && nc.y >= 0 && nc.x < cols && nc.y < rows
                    && (grid[nc.x][nc.y]) && !visited.has(nch)){
                    visited.add(nch);
                    q.push(nc);
                    prev.set(nch, hashV(cur));
                }
            }
        }

        if(prev.has(hashV(target))){
            let cur = hashV(target);
            let pv = null;
            while(cur !== null && prev.has(cur) && prev.get(cur) != cur){
                let pos = getPosFromHash(cur);
                traceGrid[pos[0]][pos[1]] = true;
                pv = cur;
                cur = prev.get(cur);
            }
            let rPos = getPosFromHash(pv);
            return new Victor(rPos[0], rPos[1]);
        }
        return null;
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