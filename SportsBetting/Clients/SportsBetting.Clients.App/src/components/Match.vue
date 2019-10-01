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
import $ from 'jquery';

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
    .market-name {
        font-weight: bold;
        font-size: 18px;
    }
    .odd-value {
        background: #e01e5a;
        font-weight: bold;
    }
</style>
