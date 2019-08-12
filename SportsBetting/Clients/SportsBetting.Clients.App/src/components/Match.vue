<template>
    <div>
        <b-container class="bv-example-row">
            <b-row>
                <b-col cols="4">{{match.HomeTeam}}</b-col>
                <b-col cols="3">{{match.Category}}: {{match.Tournament}}</b-col>
                <b-col cols="1">{{match.Score}}</b-col>
                <b-col cols="4">{{match.AwayTeam}}</b-col>
            </b-row>
        </b-container>
        <b-jumbotron >
            <b-container v-for="market in match.Markets">
                <b-row class="market">
                    <b-col class="market-name">{{market.Name}}</b-col>
                    <b-container v-for="odd in market.Odds">
                        <b-row class="odd">
                            <b-col>{{odd.Name}} <b-button size="sm" class="odd-value">{{odd.Value}}</b-button></b-col>
                        </b-row>
                    </b-container>
                </b-row>
            </b-container>
        </b-jumbotron>
    </div>
</template>

<script>
import axios from 'axios'

export default {
    data() {
        return {
            match: {}
        }
    },
    created() {
        axios.get(`http://localhost:64399/api/matches/${this.$route.params.id}`)
            .then(res => this.match = res.data)
            .catch(err => console.log(err));
    }
}
</script>

<style scoped>
    .market {
        background-color: #ff365f;
        border-radius: 10px;
        margin-bottom: 10px;
        color: #fff;
    }
    .market-name {
        font-weight: bold;
        font-size: 18px;
    }
    .odd-value {
        background: #e01e5a;
        font-weight: bold;
    }
</style>
