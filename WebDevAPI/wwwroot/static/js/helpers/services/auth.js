import { postData } from './apiCallTemplates.js';

export const Login = async (user) => {
  return await postData('/api/Auth/Login', user);
};

export const Register = async (user) => {
  return await postData('api/Auth/Register', user);
};
