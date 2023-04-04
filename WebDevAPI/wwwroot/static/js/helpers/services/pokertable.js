import { postData, getData, putAuthorizedData } from './apiCallTemplates.js';

export const createPokertable = async (pokertable) => {
  return await postData('api/Pokertable/Create', pokertable);
};

export const joinPokertable = async (data, token) => {
  return await putAuthorizedData('api/Pokertable/Join', data, token);
};

export const startPokertable = async (pokertableId) => {
  return await getData(`/api/Pokertable/Start/${pokertableId}`);
};

export const findPokertable = async (pokertableId) => {
  return await getData(`/api/Pokertable/${pokertableId}`);
};

export const getPokertablePlayers = async (pokertableId) => {
  return await getData(`/api/Pokertable/Players/${pokertableId}`);
};

export const getPlayerhand = async (username) => {
  return await getData(`/api/Pokertable/Hand/${username}`);
};
