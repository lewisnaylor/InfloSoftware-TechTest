<script lang="js" setup >
    defineProps({
        userId: {
            required: true
        }
    })
</script>
<template>
    <template v-if="logList.length === 0">
        <br/><span>No Logs</span>
    </template>
    <table v-else class="log-table">
        <tr>
            <th>Log Date</th>
            <th>Log Description</th>
        </tr>
        <template v-for="log in logList">
            <tr>
                <td>{{log.createdDate}}</td>
                <td>{{log.logDescription}}</td>
            </tr>
        </template>
    </table>
</template>

<script lang="js">

    import $ from 'jquery'
    export default {
        name: 'logList',
        emits:['cancel', 'save'],
        data() {
            return {
                logList: []
            }
        },
        methods: {
            loadLogList() {
                let vm = this;
                let url = 'https://localhost:7037/log/Log/Index';
                let data = { UserId: this.userId };
                $.ajax({
                    url: url,
                    //type: 'GET',
                    //contentType: 'application/json',
                    data: data,
                    success: function (data) {
                        console.log(data.logs);
                        vm.logList = data.logs;
                        vm.logList.forEach(x => x.createdDate = new Date(x.createdDate).toISOString().split('T')[0]);
                    },
                    error: function (jqXHR) {
                        console.log(jqXHR);
                    }
                });
            }
        },
        mounted() {
            this.loadLogList();
        },
        watch: {
            userId() {
                this.loadLogList();
            }
        }
    }
    

</script>

<style scoped>
    .log-table {
        width: -webkit-fill-available;
    }

    .log-table td,
    .log-table th {
        text-align: left;
    }
    
</style>
