import { postData, getData } from './apiCallTemplates.js';

export const createContactform = async (data) => {
  return await postData('api/Contactform', data);
};
