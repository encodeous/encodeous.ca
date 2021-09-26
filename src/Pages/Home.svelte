<svelte:head>
    <title>
        About Me
    </title>
</svelte:head>

<script>
    function getGreeting(){
        const t = new Date().getHours();
        if(t < 12){
            return "Good Morning,"
        }
        if(t < 18){
            return "Good Afternoon,"
        }
        return "Good Evening,"
    }
</script>

<div class="site-container">
    <strong class="large">
        {getGreeting()}
    </strong>
</div>