import { getData, postData, putData } from './apiCallTemplates.js';

export const Login = async (user) => {
  return await postData('/api/Auth/Login', user);
};

export const Register = async (user) => {
  return await postData('api/Auth/Register', user);
};

export const GetUser = async () => {
  return await getData('api/Auth/User');
};

export const Logout = async () => {
  return await postData('api/Auth/Logout');
};

export const ForgotPw = async (email) => {
  return await postData('api/User/ForgotPassword', email);
};

export const ChangePw = async (data) => {
  return await putData('api/User/ChangePassword', data);
};
