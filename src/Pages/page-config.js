export let pages = [
    ["/anotherpage", import("./AnotherPage.svelte")],
    ["/", import("./Home.svelte")]
]

export let navMenu = [
    {
        "url": "/",
        "name": "Home"
    },
    {
        "url": "/anotherpage",
        "name": "Another Page"
    },
    {
        "name": "Projects",
        "url": "/projects",
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