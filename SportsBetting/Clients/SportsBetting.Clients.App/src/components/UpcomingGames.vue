<template>
    <div>
        <b-container class="bv-example-row">
            <b-row id=GamesHeader>
                <b-col cols="1">Date</b-col>
                <b-col cols="4">Game</b-col>
                <b-col cols="3">1</b-col>
                <b-col cols="1">x</b-col>
                <b-col cols="3">2</b-col>
            </b-row>
        </b-container>
        <b-jumbotron >
            <b-container v-for="game in games">
               <router-link :to="{ path: '/matches/'+ game.Id}">
                    <b-row class="game-row">
                        <b-col cols="1">{{formatDate(game.StartTime)}}</b-col>
                        <b-col cols="4">{{game.Category}}: {{game.Tournament}}</b-col>
                        <b-col cols="3">{{game.HomeTeam}}</b-col>
                        <b-col cols="1">{{game.Score}}</b-col>
                        <b-col cols="3">{{game.AwayTeam}}</b-col>
                    </b-row>
                </router-link>
            </b-container>
        </b-jumbotron>
    </div>
</template>

<script>
import axios from 'axios'
import moment from 'moment'

export default {
    name: 'UpcomingGames',
    data() {
        return {
            games: []
        }
    },
    created() {
        axios.get('http://localhost:64399/api/UpcomingGames')
            .then(res => this.games = res.data)
            .catch(err => console.log(err));
    },
    methods: {
        formatDate(date) {
            return moment(date).format('DD MMM hh:mm')
        }
    }
}
</script>

<style scoped>
    #GamesHeader {
        font-weight: bold;
        font-size: 16px;
    }
    .jumbotron {
        background: #e01e5a;
        color: #fff;
        font-weight: bold;
    }
    .jumbotron a {
        text-decoration: none;
        color: #fff;
    }
    .row {
        margin-bottom: 20px;
        text-align: center;
    }
    .game-row:hover {
        background-color: #ab0b00;
        border-radius: 10px;
    }
</style>
