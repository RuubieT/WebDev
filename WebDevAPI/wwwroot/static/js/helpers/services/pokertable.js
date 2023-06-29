import {
  postData,
  getData,
  putAuthorizedData,
  postAuthorizedData,
  getAuthorizedData,
} from './apiCallTemplates.js';

export const createPokertable = async (pokertable, token) => {
  return await postAuthorizedData('api/Pokertable/Create', pokertable, token);
};

export const joinPokertable = async (data, token) => {
  return await putAuthorizedData('api/Pokertable/Join', data, token);
};

export const startPokertable = async (pokertableId, token) => {
  return await getAuthorizedData(
    `/api/Pokertable/Start/${pokertableId}`,
    token,
  );
};

export const findPokertable = async (pokertableId, token) => {
  return await getAuthorizedData(`/api/Pokertable/${pokertableId}`, token);
};

export const getPokertablePlayers = async (pokertableId, token) => {
  return await getAuthorizedData(
    `/api/Pokertable/Players/${pokertableId}`,
    token,
  );
};

export const getPlayerhand = async (username, token) => {
  return await getAuthorizedData(`/api/Pokertable/Hand/${username}`, token);
};
