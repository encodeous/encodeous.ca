import {theme} from "./store";
import {get} from "svelte/store";
const dark = "g90", light = "g10";
export function isDark(){
    return get(theme) === dark;
}
export function toggleDarkTheme(){
    if(isDark()){
        theme.set(light);
    }else{
        theme.set(dark);
    }
    localStorage.setItem("site-theme", get(theme));
    console.log(get(theme))
}