<template>
    <div>
        <b-jumbotron>
            <b-table striped hover :items="games"></b-table>
        </b-jumbotron>
    </div>
</template>

<script>
import axios from 'axios'

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
    }
}

</script>