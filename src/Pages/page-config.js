export let pages = [
    ["blog/*", import("./Home.svelte")],
    ["/", import("./Home.svelte")]
]

export let navMenu = [
    {
        "url": "/",
        "name": "Home"
    },
    {
        "name": "Projects",
        "items": [
            {
                "url": "/",
                "name": "Home"
            },
            {
                "url": "/",
                "name": "Home"
            },
            {
                "url": "/",
                "name": "Home"
            },
        ]
    }
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