<script lang="js" setup >
    defineProps({
        user: {
            required: true
        }
    })
</script>
<template>
    <form>
        <label for="forename">First name:</label>
        <input type="text" id="forename" name="forename" v-model="user.forename"><br>

        <label for="surname">Surname:</label>
        <input type="text" id="surname" name="surname" v-model="user.surname"><br>

        <label for="email">Email:</label>
        <input type="text" id="email" name="email" v-model="user.email"><br>

        <label for="dateofbirth">Date of Birth:</label>
        <input type="date" id="dateofbirth" name="dateofbirth" v-model="user.dateOfBirth"><br>

        <label> Is Active:</label>
        <div class="active-radio-btns">
            <template v-for="option in yesNoOptions" :key="option.value">
                <label>{{option.text}}</label>
                <input type="radio" :value="option.value" v-model="user.isActive">
            </template>
        </div>

    </form>
    <div class="action-btns">
        <button :disabled="isInvalid" @click="save">Save</button>
        <button @click="$emit('cancel')">Cancel</button>
    </div>
</template>

<script lang="js">

    import $ from 'jquery'
    export default {
        name: 'user',
        emits:['cancel', 'save'],
        data() {
            return {
                yesNoOptions: [
                    { text: 'Yes', value: true },
                    { text: 'No', value: false }
                ]
            }
        },
        methods: {
            save() {
                this.$emit('save', this.user);
            },
            isNullOrEmpty(val) {
                return val == null || val === '';
            }
        },
        computed: {
            isInvalid() {
                return this.isNullOrEmpty(this.user.forename)
                    || this.isNullOrEmpty(this.user.surname)
                    || this.isNullOrEmpty(this.user.email)
                    || this.isNullOrEmpty(this.user.dateOfBirth)
                    || this.isNullOrEmpty(this.user.isActive);
            }
        }
    }
    

</script>

<style scoped>
    form label{
        display: inline-block;
        width: 150px;
    }

    .active-radio-btns {
        display: inline-block;
    }

    .active-radio-btns label{
        width: auto;
    }

    .active-radio-btns input {
        width: 20px;
    }

    .action-btns {
        margin-top: 20px;
        justify-content: space-between;
        flex-direction: row-reverse;
    }
    
</style>
