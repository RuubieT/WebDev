import { jwtToken } from '../index.js';
import {
  deleteAuthorizedData,
  deleteData,
  getAuthorizedData,
  getData,
} from './services/apiCallTemplates.js';
import { DeleteUser } from './services/auth.js';

async function createUserList() {
  var data = await getAuthorizedData('api/User', jwtToken.token);

  var userList = document.getElementById('userListDiv');
  console.log(userList);
  if (!userList && data) {
    const userListDiv = document.createElement('div');
    userListDiv.id = 'userListDiv';
    const userListTable = document.createElement('table');
    userListTable.innerHTML = `
              <table>
                  <tr>
                      <th>#</th>
                      <th>Username</th>
                      <th>Email</th>
                      <th>Action</th>
                  </tr>
              </table>
          `;

    var index = 0;
    data.forEach((element) => {
      var row = document.createElement('tr');

      var indexOfUser = document.createElement('td');
      indexOfUser.innerText = index;

      var username = document.createElement('td');
      username.innerText = element.userName;

      var email = document.createElement('td');
      email.innerText = element.email;

      var actiontd = document.createElement('td');
      actiontd.innerText = 'delete';
      actiontd.addEventListener('click', async () => {
        await DeleteUser(element.email);
        deleteUserList();
        createUserList();
      });

      row.appendChild(indexOfUser);
      row.appendChild(username);
      row.appendChild(email);
      row.appendChild(actiontd);
      userListTable.appendChild(row);
      index++;
    });
    userListDiv.appendChild(userListTable);
    document.body.appendChild(userListDiv);
  }
}

function deleteUserList() {
  var userList = document.getElementById('userList');
  if (userList) {
    document.body.removeChild(userList);
  }
}

async function createRoleList() {
  var data = await getAuthorizedData('api/User/Roles', jwtToken.token);
  console.log(data);
  var roleList = document.getElementById('roleListDiv');

  if (!roleList && data) {
    const roleListDiv = document.createElement('div');
    roleListDiv.id = 'roleListDiv';
    const roleListTable = document.createElement('table');
    roleListTable.innerHTML = `
              <table>
                  <tr>
                      <th>#</th>
                      <th>Username</th>
                      <th>Email</th>
                      <th>Role</th>
                      <th>Edit</th>
                  </tr>
              </table>
          `;

    var index = 0;
    data.forEach((element) => {
      var row = document.createElement('tr');

      var indexOfUser = document.createElement('td');
      indexOfUser.innerText = index;

      var username = document.createElement('td');
      username.innerText = element.username;

      var email = document.createElement('td');
      email.innerText = element.email;

      var role = document.createElement('td');
      role.innerText = element.role[0];

      var actiontd = document.createElement('td');
      actiontd.innerText = 'Edit';
      actiontd.addEventListener('click', async () => {
        console.log('Edit');
      });

      row.appendChild(indexOfUser);
      row.appendChild(username);
      row.appendChild(email);
      row.appendChild(role);
      row.appendChild(actiontd);

      roleListTable.appendChild(row);
      index++;
    });
    roleListDiv.appendChild(roleListTable);
    document.body.appendChild(roleListDiv);
  }
}

function deleteRoleList() {
  var roleList = document.getElementById('roleList');
  if (roleList) {
    document.body.removeChild(roleList);
  }
}

export { createUserList, deleteUserList, createRoleList, deleteRoleList };
