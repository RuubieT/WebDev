import { getData, getAuthorizedData } from './apiCallTemplates.js';

export const GetLeaderboard = async () => {
  return await getData('api/Player/Leaderboard');
};

export const FindUser = async (data, token) => {
  return await getAuthorizedData(`/api/Player/Find/${data}`, token);
};
