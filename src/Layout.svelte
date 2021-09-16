<script>
    import { Router, Link, Route } from "svelte-routing";
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
        SideNavDivider,
        SkipToContent,
        Content,
        Grid,
        Row,
        Column,
    } from "carbon-components-svelte";
    let isSideNavOpen = false;
</script>

<Header company="" platformName="Adam Chen" class="azonix" bind:isSideNavOpen expandedByDefault={false}>
    <div slot="skip-to-content">
        <SkipToContent />
    </div>

    <HeaderNav>
        <HeaderNavItem href="/" text="Link 1" />
        <HeaderNavItem href="/" text="Link 2" />
        <HeaderNavItem href="/" text="Link 3" />
        <HeaderNavMenu text="Menu">
            <HeaderNavItem href="/" text="Link 1" />
            <HeaderNavItem href="/" text="Link 2" />
            <HeaderNavItem href="/" text="Link 3" />
        </HeaderNavMenu>
    </HeaderNav>
</Header>

<SideNav bind:isOpen={isSideNavOpen}>
    <SideNavItems>
        <SideNavLink text="Link 1" />
        <SideNavLink text="Link 2" />
        <SideNavLink text="Link 3" />
        <SideNavMenu text="Menu">
            <SideNavMenuItem href="/" text="Link 1" />
            <SideNavMenuItem href="/" text="Link 2" />
            <SideNavMenuItem href="/" text="Link 3" />
        </SideNavMenu>
        <SideNavDivider />
        <SideNavLink text="Link 4" />
    </SideNavItems>
</SideNav>

<Content>
    <Grid>
        <Row>
            <Column>
                <h1>Hello, World!</h1>
            </Column>
        </Row>
    </Grid>
</Content>
