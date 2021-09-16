<script>
    import { Sun24, AsleepFilled24 } from "carbon-icons-svelte";
    import {
        TooltipIcon
    } from "carbon-components-svelte";
    import {toggleDarkTheme, isDark} from "./theme";
</script>
<TooltipIcon
        style="cursor: pointer"
        on:click={toggleDarkTheme}
        tooltipText={`Enable ${isDark() ? "light" : "dark"} mode`}
        icon={isDark() ? Sun24 : AsleepFilled24}
        direction="left"
/>