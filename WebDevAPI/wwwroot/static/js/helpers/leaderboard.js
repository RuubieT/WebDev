function createLeaderboard(data){
    var leaderboard = document.getElementById("leaderboardDiv");
    if (!leaderboard && data) {    
        const leaderboardDiv = document.createElement('div');
        leaderboardDiv.id = 'leaderboardDiv';
        const leaderboardTable = document.createElement('table');
        leaderboardTable.innerHTML = `
            <table>
                <tr>
                    <th>Username</th>
                    <th>Chips</th>
                </tr>
            </table>
        `

        data.forEach(element => {
            var row = document.createElement('tr')

            var username = document.createElement('td');
            username.innerText = element.username;

            var chips = document.createElement('td');
            chips.innerText = element.chips;

            row.appendChild(username);
            row.appendChild(chips);
            leaderboardTable.appendChild(row);
            
        });
        leaderboardDiv.appendChild(leaderboardTable);
        document.body.appendChild(leaderboardDiv);
    }
}

function deleteLeaderboard(){
    var leaderboard = document.getElementById("leaderboardDiv");
    if (leaderboard) {
        document.body.removeChild(leaderboard);
    } 
}

export {
    createLeaderboard,
    deleteLeaderboard
}