// noinspection JSUnresolvedVariable
export let API_URL = isProduction ? "https://api.encodeous.ca" : "https://localhost:7283";

export async function getVersion() {
    return (await fetch(API_URL + "/Version")).text();
}