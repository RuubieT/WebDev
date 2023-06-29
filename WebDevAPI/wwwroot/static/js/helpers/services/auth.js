import {
  deleteAuthorizedData,
  getData,
  postAuthorizedData,
  postData,
  putAuthorizedData,
  putData,
} from './apiCallTemplates.js';

export const Register = async (user) => {
  return await postData('api/Auth/Register', user);
};

export const Login = async (user) => {
  return await postData('/api/Auth/Login', user);
};

export const GetUser = async () => {
  return await getData('api/Auth/User');
};

export const Logout = async (token) => {
  return await postAuthorizedData('api/Auth/Logout', token);
};

export const ValidateCode = async (data) => {
  return await postData('api/Auth/GAuth', data);
};

export const UpdateUserRole = async (data, token) => {
  return await putAuthorizedData('api/User/UpdateRole', data, token);
};

export const ForgotPw = async (email) => {
  return await postData('api/User/ForgotPassword', email);
};

export const ForgotChangePw = async (data) => {
  return await putData('api/User/ForgotPassword', data);
};

export const DeleteUser = async (data, token) => {
  return await deleteAuthorizedData('api/Player/' + data, token);
};
