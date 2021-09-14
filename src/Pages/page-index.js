export let pages = [
    ["blog/*", import("./Home.svelte")],
    ["/", import("./Home.svelte")]
]

export async function getRoutesAsync(){
    let routes = [];
    for (let x in pages) {
        let [i, j] = pages[x];
        let v = (await j).default;
        routes.push([i, v])
    }
    return routes;
}