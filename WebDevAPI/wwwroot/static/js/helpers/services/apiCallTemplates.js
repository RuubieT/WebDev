async function postData(url = '', data) {
  let response = await fetch(url, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(data),
  })
    .then(function (res) {
      return handleResponse(res);
    })
    .catch((e) => console.log(e));
  return returnResponse(response);
}

async function postAuthorizedData(url = '', data, token) {
  let response = await fetch(url, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
      Authorization: 'bearer ' + token,
    },
    body: JSON.stringify(data),
  })
    .then(function (res) {
      return handleResponse(res);
    })
    .catch((e) => console.log(e));
  return returnResponse(response);
}

async function getData(url = '') {
  let response = await fetch(url, {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json',
    },
  })
    .then(function (res) {
      return handleResponse(res);
    })
    .catch((e) => console.log(e));
  return returnResponse(response);
}

async function getAuthorizedData(url = '', token) {
  let response = await fetch(url, {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json',
      Authorization: 'bearer ' + token,
    },
  })
    .then(function (res) {
      return handleResponse(res);
    })
    .catch((e) => console.log(e));
  return returnResponse(response);
}

async function putData(url = '', data) {
  let response = await fetch(url, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(data),
  })
    .then(function (res) {
      return handleResponse(res);
    })
    .catch((e) => console.log(e));
  return returnResponse(response);
}

async function putAuthorizedData(url = '', data, token) {
  let response = await fetch(url, {
    method: 'PUT',
    headers: {
      'Content-Type': 'application/json',
      Authorization: 'bearer ' + token,
    },
    body: JSON.stringify(data),
  })
    .then(function (res) {
      return handleResponse(res);
    })
    .catch((e) => console.log(e));
  return returnResponse(response);
}

function handleResponse(response) {
  if (!response.ok) {
    return response.text().then((text) => {
      throw new Error(text);
    });
  }
  return response;
}

function returnResponse(response) {
  if (response) return response.json();
  return response;
}

export {
  postData,
  postAuthorizedData,
  getData,
  getAuthorizedData,
  putData,
  putAuthorizedData,
};
