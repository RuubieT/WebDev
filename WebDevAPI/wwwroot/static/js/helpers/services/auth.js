import { postData } from './apiCallTemplates.js';

export const Login = async (data) => {
  return await postData('/api/Auth/Login', data);
};

export const Register = async (data) => {
  return await postData('api/Auth/Register', data);
};
