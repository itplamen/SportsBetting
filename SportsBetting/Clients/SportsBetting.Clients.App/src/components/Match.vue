<template>
    <div>
        <b-container class="container-row">
            <b-row>
                <b-col cols="5" class="team-name">{{match.HomeTeam}}</b-col>
                <b-col cols="2" class="score">{{match.Score}}</b-col>
                <b-col cols="5" class="team-name">{{match.AwayTeam}}</b-col>
            </b-row>
            <b-row>
                <b-col cols="12" class="category-name">{{match.Category}}</br>{{match.Tournament}}</b-col>
            </b-row>
        </b-container>
        <b-jumbotron >
            <b-container v-for="market in match.Markets">
                <div class="market">
                    <div class="market-name">{{market.Name}}</div>
                    <div v-bind:key="odd.Id" v-for="odd in market.Odds" class="odd">
                        <div v-if="odd.Rank % 2 === 0">
                            <span class="odd-name">{{odd.Name}}</span>
                            <span class="odd-header" v-if="odd.Header > 0">{{odd.Symbol}}{{odd.Header}}</span> 
                            <b-button size="sm" class="odd-value">{{odd.Value}}</b-button>
                        </div>
                        <div v-else>
                            <b-button size="sm" class="odd-value">{{odd.Value}}</b-button> 
                            <span class="odd-header" v-if="odd.Header > 0">{{odd.Symbol}}{{odd.Header}}</span>
                            <span class="odd-name">{{odd.Name}}</span>
                        </div>
                    </div>
                </div>
            </b-container>
        </b-jumbotron>
    </div>
</template>

<script>
import axios from 'axios'
import $ from 'jquery'
import enums from '../common/constants/enums'

export default {
    data() {
        return {
            match: {}
        }
    },
    created() {
        $(window).off('scroll');

        axios.get(`http://localhost:64399/api/Matches/${this.$route.params.id}`)
            .then(res => this.match = res.data)
            .catch(err => console.log(err));
    }
}
</script>

<style scoped>
    .score{
        font-weight: bold;
        font-size: 24px;
        color: #ff365f;
    }

    .team-name{
        font-weight: bold;
        font-size: 18px;
    }

    .category-name {
        margin-top: 20px;
        font-weight: bold;
        font-size: 14px;
    }

    .market {
        background-color: #ff365f;
        border-radius: 10px;
        margin-bottom: 10px;
        color: #fff;
     }

    .market:after { 
        content: "";
        display: table;
        clear: both;
    }

    .market-name {
        font-weight: bold;
        font-size: 18px;
    }

    .odd-name {
        margin-right: 5px;
        margin-left: 5px;
    }

    .odd-value {
        background: #e01e5a;
        font-weight: bold;
        margin-right: 10px;
        margin-left: 10px;
    }

    .odd {        
        margin-top: 20px;
        margin-bottom: 20px;
        width: 400px;
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
    
</style>