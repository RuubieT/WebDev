import { postData, getData } from './apiCallTemplates.js';

export const createPokerTable = async (data) => {
  return await postData('api/Pokertable/Create', data);
};

export const startPokertable = async (data) => {
  return await getData(`/api/Pokertable/Start/${data}`);
};

export const getPokertablePlayers = async (data) => {
  return await getData(`/api/Pokertable/Players/${data}`);
};

export const getPlayerhand = async (data) => {
  return await getData(`/api/Pokertable/Hand/${data}`);
};
