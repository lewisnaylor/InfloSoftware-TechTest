## Setup

* This app uses Vue 3 for the front end. In order to get this run on your machine, you will have to install the latest version of NPM and Vue.
* To run the application, select the startup projects UserManagement.API and UserManagement.Vue.

## Standard

* Filter options now appear on the front end allowing the user to select active, inactive or all users.
* The User class now has a DOB property which is populated and retrieved from the DB.
* There is now an 'Add User' button which allows a user to insert a new user into the DB.
* Each entry on the user list has a View and Delete option. 
  * Basic validation has been done to make sure no empty values are passed into the backend.

## Advanced

* All actions against Users are logged in the database.
* The log page can be viewed on the user list page to view all actions against all users
* Clicking into a user and viewing the actions will show only the actions against that respective user.

## Expert
* I refactored most of the code base into a feature slice architecture.
* There is now a UserManagement API project which handles all the calls into the database, and uses MediatR to handle the requests. 
* Each action that is executed in the front end is seperated into it's own 'Slice' and it completes that action and returns the result.
* The front end has been changed into a Vue application. 
