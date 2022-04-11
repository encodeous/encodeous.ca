// noinspection JSUnresolvedVariable

export let API_URL = isProduction ? "https://api.encodeous.ca" : "https://localhost:7283";

export async function getVersion() {
    return (await fetch(API_URL + "/Version")).text();
}

/**
 * Gets an object from RCFS
 * @param {string} path
 * @returns {Promise<any>}
 */
export async function rcObject(path){
    return (await fetch(API_URL + "/loadpath/" + path)).json();
}

/**
 * Gets text from RCFS
 * @param {string} path
 * @returns {Promise<string>}
 */
export async function rcText(path){
    return (await fetch(API_URL + "/loadpath/" + path)).text();
}

/**
 * Gets redirected object from an index
 * @param {string} indexedPath
 * @param {string} objectName
 * @returns {Promise<string>}
 */
export async function rcIndexedText(indexedPath, objectName){
    const index = await rcObject(indexedPath + "/index.json");
    return await rcText(indexedPath + "/" + index[objectName]);
}

/**
 * Gets path from an index
 * @param {string} indexedPath
 * @param {string} objectName
 * @returns {Promise<string>}
 */
export async function rcIndex(indexedPath, objectName){
    const index = await rcObject(indexedPath + "/index.json");
    return indexedPath + "/" + index[objectName];
}

/**
 * Renders markdown to HTML
 * @param {string} md
 * @returns {Promise<string>}
 */
export async function renderMd(md){
    return (await fetch(
        API_URL + "/render",
        {
            method: 'POST',
            mode: 'cors',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(md),
        }
    )).text();
}

/**
 * Renders markdown to HTML
 * @param {Promise<string>} md
 * @returns {Promise<string>}
 */
export async function renderMdPath(path) {
    return (await fetch(API_URL + "/renderpath/" + path)).text();
}