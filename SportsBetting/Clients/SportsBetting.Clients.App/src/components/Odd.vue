<template>
    <div>
        <div v-if="odd.Rank % 2 === 0">
            <span class="odd-name">{{odd.Name}}</span>
            <span class="odd-header" v-if="odd.Header > 0">{{odd.Symbol}}{{odd.Header}}</span> 
            <div class="odd-value"  
                v-bind:class="{ 'suspended-odd' : odd.IsSuspended }"
                @click="!odd.IsSuspended ? $emit('showBetslip', odd, marketName) : null">
                <span v-if="!odd.IsSuspended">{{odd.Value}}</span>
            </div>
        </div>
        <div v-else>
            <div class="odd-value" 
                v-bind:class="{ 'suspended-odd' : odd.IsSuspended }"
                @click="!odd.IsSuspended ? $emit('showBetslip', odd, marketName) : null">
                <span v-if="!odd.IsSuspended">{{odd.Value}}</span>
            </div>
            <span class="odd-header" v-if="odd.Header > 0">{{odd.Symbol}}{{odd.Header}}</span> 
            <span class="odd-name">{{odd.Name}}</span>
        </div>
    </div>
</template>

<script>
export default {
    props: {
        odd: Object,
        marketName: String
    }
}
</script>

<style>
    .odd {        
        margin-top: 20px;
        margin-bottom: 20px;
        width: 400px;
    }

    .odd-name {
        margin-right: 5px;
        margin-left: 5px;
    }

    .odd-value {
        display: inline-block;
        background: #e01e5a;
        font-weight: bold;
        margin-right: 10px;
        margin-left: 10px;
        padding: 5px;
        width: 50px;
        height: 35px;
        text-align: center;
        vertical-align: middle;
        cursor: pointer;
        border-radius: 10px;
    }

    .odd-value:hover {
        background-color: #5baf4e;
    }

    .odd:nth-child(even) {
        float: left;
        clear: both;
        margin-left: 150px;
        text-align: right;
    }

    .odd:nth-child(odd) {
        float: right;
        margin-right: 150px;
        text-align: left;
    }
    
    .suspended-odd {
        cursor: not-allowed;
        background-image: url('../assets/Suspended.png');
        background-size: 50px 35px;
        background-color: transparent ;
    }

    .suspended-odd:hover {
        background-color: transparent ;
    }
</style>