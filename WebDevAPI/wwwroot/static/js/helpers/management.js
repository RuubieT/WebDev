import { jwtToken } from '../index.js';
import {
  deleteAuthorizedData,
  deleteData,
  getAuthorizedData,
  getData,
} from './services/apiCallTemplates.js';
import { DeleteUser, UpdateUserRole } from './services/auth.js';
import { GetExistingRoles, GetUserRoles, GetUsers } from './services/player.js';

async function createUserList() {
  var data = await GetUsers(jwtToken.token);

  var userList = document.getElementById('userListDiv');
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
        await DeleteUser(element.email, jwtToken.token);
        alert('Deleted: ' + element.email);
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
  var userList = document.getElementById('userListDiv');

  if (userList) {
    document.body.removeChild(userList);
  }
}

async function createRoleList() {
  var data = await GetUserRoles(jwtToken.token);
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
        var modal = document.getElementById('myModal');
        var span = document.getElementsByClassName('close')[0];
        modal.style.display = 'block';

        span.onclick = function () {
          modal.style.display = 'none';
        };

        var modalcontent = document.getElementById('modalcontent');
        if (modalcontent.hasChildNodes) {
          modalcontent.innerHTML = '';
        }
        var dropdowndiv = document.createElement('div');
        dropdowndiv.classList.add('dropdown');

        var selectbutton = document.createElement('button');
        selectbutton.classList.add('dropbtn');
        selectbutton.innerText = element.role[0];

        var roles = await GetExistingRoles(jwtToken.token);

        selectbutton.addEventListener('click', async (e) => {
          document.getElementById('roleDropdown').classList.toggle('show');
          window.onclick = function (event) {
            if (event.target == modal) {
              modal.style.display = 'none';
            }
            var validValue = false;
            roles.forEach((role) => {
              if (role.name == event.target.innerText) {
                validValue = true;
              }
            });
            if (validValue) {
              selectbutton.innerText = event.target.innerText;
            }
            if (!event.target.matches('.dropbtn')) {
              var dropdowns =
                document.getElementsByClassName('dropdown-content');
              var i;
              for (i = 0; i < dropdowns.length; i++) {
                var openDropdown = dropdowns[i];
                if (openDropdown.classList.contains('show')) {
                  openDropdown.classList.remove('show');
                }
              }
            }
          };
        });

        var dropdownOptions = document.createElement('div');
        dropdownOptions.id = 'roleDropdown';
        dropdownOptions.classList.add('dropdown-content');

        roles.forEach((role) => {
          var option = document.createElement('a');
          option.innerText = role.name;
          dropdownOptions.appendChild(option);
        });

        var button = document.createElement('button');
        button.id = 'EditRole';
        button.innerText = 'Confirm edit role';
        button.addEventListener('click', async (e) => {
          e.preventDefault();

          await UpdateUserRole(
            {
              email: element.email,
              RoleName: selectbutton.innerText,
            },
            jwtToken.token,
          );

          modal.style.display = 'none';
          deleteRoleList();
          createRoleList();
        });

        dropdowndiv.appendChild(selectbutton);
        dropdowndiv.appendChild(dropdownOptions);

        modalcontent.appendChild(button);
        modalcontent.appendChild(dropdowndiv);
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
  var roleList = document.getElementById('roleListDiv');
  if (roleList) {
    document.body.removeChild(roleList);
  }
}

export { createUserList, deleteUserList, createRoleList, deleteRoleList };
