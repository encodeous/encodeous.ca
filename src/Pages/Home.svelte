<script lang="ts">
  import FlowChar from "../Components/FlowChar.svelte";

  function getGreeting() {
    const t = new Date().getHours();
    if (t < 12) {
      return "Good Morning,";
    }
    if (t < 18) {
      return "Good Afternoon,";
    }
    return "Good Evening,";
  }
</script>

<div class="site-container">
  <FlowChar char="A" />
  <FlowChar char="d" />
  <FlowChar char="a" />
  <FlowChar char="m" />
</div>
