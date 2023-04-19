async function postData(url = '', data) {
  let response = await fetch(url, {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(data),
  })
    .then(function (res) {
      // first then()
      if (res.ok) {
        return res;
      }

      throw new Error('Something went wrong.');
    })
    .catch((e) => console.log(e));
  return response.json();
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
      // first then()
      if (res.ok) {
        return res;
      }

      throw new Error('Something went wrong.');
    })
    .catch((e) => console.log(e));
  return response.json();
}

async function getData(url = '') {
  let response = await fetch(url, {
    method: 'GET',
    headers: {
      'Content-Type': 'application/json',
    },
  })
    .then(function (res) {
      // first then()
      if (res.ok) {
        return res;
      }

      throw new Error('Something went wrong.');
    })
    .catch((e) => console.log(e));
  return response.json();
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
      // first then()
      if (res.ok) {
        return res;
      }

      throw new Error('Something went wrong.');
    })
    .catch((e) => console.log(e));
  return response.json();
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
      // first then()
      if (res.ok) {
        return res;
      }

      throw new Error('Something went wrong.');
    })
    .catch((e) => console.log(e));
  return response.json();
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
      // first then()
      if (res.ok) {
        return res;
      }

      throw new Error('Something went wrong.');
    })
    .catch((e) => console.log(e));
  return response.json();
}

export {
  postData,
  postAuthorizedData,
  getData,
  getAuthorizedData,
  putData,
  putAuthorizedData,
};
