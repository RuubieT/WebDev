import { jwtToken } from "../index.js";
import { deleteAuthorizedData, deleteData, getAuthorizedData, getData } from "./services/apiCallTemplates.js"
import { DeleteUser } from "./services/auth.js";

async function createUserList(){
    var data = await getAuthorizedData('api/User', jwtToken.token);
    console.log(data);
    var userList = document.getElementById('userListDiv');
    console.log(userList)
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
        actiontd.addEventListener("click", async () => {
            console.log(element.id);
            await DeleteUser(element.email);
            
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

export {
    createUserList,
    deleteUserList,
}
