import { getData, getAuthorizedData } from './apiCallTemplates.js';

export const GetLeaderboard = async () => {
  return await getData('api/Player/Leaderboard');
};

export const FindUser = async (email, token) => {
  return await getAuthorizedData(`/api/Player/Find/${email}`, token);
};
