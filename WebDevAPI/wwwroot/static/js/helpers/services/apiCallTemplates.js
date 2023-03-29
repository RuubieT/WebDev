async function postData(url = "", data) {
    const response = await fetch(url, {
        method: "POST", 
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(data), 
    }).catch(e => console.log(e));;
    return response.json(); 
}

async function postAuthorizedData(url = "", data, token) {
    const response = await fetch(url, {
        method: "POST",
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'bearer ' + token
        },
        body: JSON.stringify(data),
    }).catch(e => console.log(e));;
    return response.json();
}

async function getData(url = "") {
    const response = await fetch(url, {
        method: "GET",
        headers: {
            'Content-Type': 'application/json',
        },
    }).catch(e => console.log(e));;
    return response.json();
}

async function getAuthorizedData(url = "", token) {
    const response = await fetch(url, {
        method: "GET",
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'bearer ' + token
        },
    }).catch(e => console.log(e));;
    return response.json();
}

async function putData(url = "", data) {
    const response = await fetch(url, {
        method: "PUT",
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(data),
    }).catch(e => console.log(e));;
    return response.json();
}

async function putAuthorizedData(url = "", data, token) {
    const response = await fetch(url, {
        method: "PUT",
        headers: {
            'Content-Type': 'application/json',
            'Authorization': 'bearer ' + token
        },
        body: JSON.stringify(data),
    }).catch(e => console.log(e));;
    return response.json();
}


export {
    postData,
    postAuthorizedData,
    getData,
    getAuthorizedData,
    putData,
    putAuthorizedData
}

