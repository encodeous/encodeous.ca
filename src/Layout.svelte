<script>
    import { Router, Link, Route, links } from "svelte-routing";
    import {navMenu} from "./Pages/page-config";
    import {toggleDarkTheme, dark} from "./Shared/theme";
    import {theme} from "./Shared/store";
    import { Sun24, AsleepFilled24, LogoGithub24 } from "carbon-icons-svelte";
    import Tooltip from "./Shared/Tooltip.svelte"
    import {
        Header,
        HeaderNav,
        HeaderNavItem,
        HeaderNavMenu,
        SideNav,
        SideNavItems,
        SideNavMenu,
        SideNavMenuItem,
        SideNavLink,
        HeaderUtilities,
        HeaderGlobalAction,
        SkipToContent,
        Content,
        Grid,
        Row,
        Column,
    } from "carbon-components-svelte";
    let isSideNavOpen = false;
</script>
<div use:links>
    <Header company="Adam's" platformName="Site" href="/" class="azonix" bind:isSideNavOpen expandedByDefault={false}>
        <div slot="skip-to-content">
            <SkipToContent />
        </div>
        <HeaderNav>
            {#each navMenu as i}
                {#if i.hasOwnProperty("items")}
                    <HeaderNavMenu href={i["url"]} text={i["name"]}>
                        {#each i["items"] as j}
                            <HeaderNavItem href={j["url"]} text={j["name"]} />
                        {/each}
                    </HeaderNavMenu>
                {:else}
                    <HeaderNavItem href={i["url"]} text={i["name"]} />
                {/if}
            {/each}
        </HeaderNav>
        <HeaderUtilities>
            <HeaderGlobalAction
                    on:click={toggleDarkTheme}
                    icon={$theme === dark ? Sun24 : AsleepFilled24}
            />
            <HeaderGlobalAction
                    on:click={()=>{
                            open("https://github.com/encodeous/encodeous.ca");
                        }}
                    icon={LogoGithub24}
            />
        </HeaderUtilities>

    </Header>

    <SideNav bind:isOpen={isSideNavOpen}>
        <SideNavItems>
            {#each navMenu as i}
                {#if i.hasOwnProperty("items")}
                    <SideNavMenu href={i["url"]} text={i["name"]}>
                        {#each i["items"] as j}
                            <SideNavMenuItem href={j["url"]} text={j["name"]} />
                        {/each}
                    </SideNavMenu>
                {:else}
                    <SideNavLink href={i["url"]} text={i["name"]} />
                {/if}
            {/each}
        </SideNavItems>
    </SideNav>

    <Content>
        <slot/>
    </Content>
</div>