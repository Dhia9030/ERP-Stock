import {jwtDecode} from "jwt-decode"

const TOKEN_KEY = 'token';

export const setToken = (token, rememberMe) => {
  if (rememberMe) {
    localStorage.setItem(TOKEN_KEY, token);
  } else {
    sessionStorage.setItem(TOKEN_KEY, token);
  }
};

export const getToken = () => {
  return localStorage.getItem(TOKEN_KEY) || sessionStorage.getItem(TOKEN_KEY);
};

export const removeToken = () => {
  localStorage.removeItem(TOKEN_KEY);
  sessionStorage.removeItem(TOKEN_KEY);
};

export const getUserRole = () => {
  const token = getToken();
  if (!token) return null;

  try {
    const decodedToken = jwtDecode(token);
    return decodedToken.role;
  } catch (error) {
    console.error('Failed to decode token:', error);
    return null;
  }
};