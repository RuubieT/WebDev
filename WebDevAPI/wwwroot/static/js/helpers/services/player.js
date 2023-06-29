import { getData, getAuthorizedData } from './apiCallTemplates.js';

export const GetAllPlayers = async (token) => {
  return await getAuthorizedData(`/api/Player`, token);
};

export const GetLeaderboard = async () => {
  return await getData('api/Player/Leaderboard');
};

export const FindUser = async (email, token) => {
  return await getAuthorizedData(`/api/Player/Find/${email}`, token);
};

export const test = async (token) => {
  return await getAuthorizedData('api/test/cards', token);
};

export const test2 = async (token) => {
  return await getAuthorizedData('api/test/tablecards', token);
};

export const GetUsers = async (token) => {
  return await getAuthorizedData(`/api/User`, token);
};

export const GetUserRoles = async (token) => {
  return await getAuthorizedData(`/api/User/UserRoles`, token);
};

export const GetExistingRoles = async (token) => {
  return await getAuthorizedData(`/api/User/Roles`, token);
};
