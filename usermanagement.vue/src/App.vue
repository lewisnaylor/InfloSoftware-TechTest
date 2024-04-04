<script lang="js" setup>
    import User from './components/User.vue'
    import LogList from './components/LogList.vue'
</script>

<template>
    <template v-if="isLoading">
        <span>Loading...</span>
    </template>
    <template v-else>
        <div>
            <div v-if="selectedUser == null" class="header-actions">
                <div class="active-filter">
                    <label>Active</label>
                    <input type="radio" :value="activeFilters.active" v-model="selectedActiveFilter">
                    <label>Inactive</label>
                    <input type="radio" :value="activeFilters.inactive" v-model="selectedActiveFilter">
                    <label>All</label>
                    <input type="radio" :value="activeFilters.all" v-model="selectedActiveFilter">
                </div>
                <button @click="addUser">Add User</button>
            </div>
            <table v-if="selectedUser === null" class="user-table">
                <tr>
                    <th>Forename</th>
                    <th>Surname</th>
                    <th>Email</th>
                    <th>Date of Birth</th>
                    <th>Is Active</th>
                </tr>
                <template v-for="(user, i) in userList" :key="i">
                    <tr>
                        <td>{{user.forename}}</td>
                        <td>{{user.surname}}</td>
                        <td>{{user.email}}</td>
                        <td>{{user.dateOfBirth}}</td>
                        <td>{{user.isActive ? 'Yes' : 'No'}}</td>
                        <td>
                            <select @change="changeOption($event, user)">
                                <option v-for="option in userOptions" :value="option.value">
                                    {{option.text}}
                                </option>
                            </select>
                        </td>
                    </tr>
                </template>

            </table>

            <User v-if="selectedUser != null"
                  :user="selectedUser"
                  @cancel="selectedUser = null; showLogs = false"
                  @save="save">
            </User>


            <br /><span><a @click="toggleViewLogs">{{showLogs ? 'Hide' : 'Show'}} Logs</a></span>
            <LogList :userId="selectedUser?.id"
                     v-if="showLogs">
            </LogList>
        </div>

</template>

</template>

<script lang="js">

    import $ from 'jquery'
    const activeFilters = Object.freeze({
        active: 0,
        inactive: 1,
        all: 2
    });

    export default {
        data() {
            return {
                userList: null,
                isLoading: false,
                showLogs: false,
                userOptions: [
                    {
                        text: "Options",
                        value: null
                    },
                    {
                        text: "View",
                        value: 1
                    },
                    {
                        text: "Delete",
                        value: 2
                    }
                ],
                selectedUser: null,
                activeFilters: activeFilters,
                selectedActiveFilter: activeFilters.all
            }
        },
        methods: {
            loadUsers() {
                this.isLoading = true;
                let vm = this;
                $.ajax({
                    url: 'https://localhost:7037/userList/UserList',
                    data: { ActiveFilter: this.selectedActiveFilter },
                    success: function (data) {
                        vm.userList = data.users;
                        vm.userList.forEach(x => x.dateOfBirth = new Date(x.dateOfBirth).toISOString().split('T')[0]);
                        vm.isLoading = false;
                    },
                    error: function (jqXHR) {
                        console.log(jqXHR);
                    }
                });
            },
            changeOption(event, user) {
                let test = event.target.value;
                switch (event.target.value) {
                    case "1":
                        this.selectedUser = user;
                        this.showLogs = false;
                        return;
                    case "2":
                        this.delete(user);
                        return;
                }
            },
            save(user) {
                this.isLoading = true;
                let vm = this;
                let action = user.id == 0 ? 'Create' : 'Edit';
                let url = 'https://localhost:7037/user/User/' + action;
                let data = { User: user };
                $.ajax({
                    url: url,
                    type: 'POST',
                    contentType: 'application/json',
                    data: JSON.stringify(data),
                    success: function (data) {
                        vm.loadUsers();
                        vm.selectedUser = null;
                        vm.isLoading = false;
                    },
                    error: function (jqXHR) {
                        console.log(jqXHR);
                    }
                });
            },
            delete(user) {
                this.isLoading = true;
                let vm = this;
                let data = { User: user };
                $.ajax({
                    url: 'https://localhost:7037/user/User/Delete',
                    type: 'DELETE',
                    contentType: 'application/json',
                    data: JSON.stringify(data),
                    success: function (data) {
                        vm.loadUsers();
                        vm.isLoading = false;
                    },
                    error: function (jqXHR) {
                        console.log(jqXHR);
                    }
                });
            },
            addUser() {
                this.selectedUser = {
                    id: 0,
                    forename: null,
                    surname: null,
                    email: null,
                    dateOfBirth: null,
                    isActive: true
                }
            },
            toggleViewLogs() {
                if (this.showLogs === true) {
                    this.showLogs = false;
                    return;
                }

                this.showLogs = true;
            }
        },
        mounted() {
            this.loadUsers();
        },
        watch: {
            selectedActiveFilter() {
                this.loadUsers();
            }
        }
    }

</script>

<style scoped>

    @media (min-width: 1024px) {
        header {
            display: flex;
            place-items: center;
            padding-right: calc(var(--section-gap) / 2);
        }

        .logo {
            margin: 0 2rem 0 0;
        }

        header .wrapper {
            display: flex;
            place-items: flex-start;
            flex-wrap: wrap;
        }
    }

    .header-actions {
        display: flex;
        justify-content: space-between;
    }
    .active-filter label{
        margin-left: 20px;
    }

    .active-filter input {
        margin-left: 5px;
    }

    .user-table {
        width: max-content;
    }

    .user-table td,
    .user-table th{
        text-align: left;
    }

    a{
        cursor: pointer;
    }
</style>
