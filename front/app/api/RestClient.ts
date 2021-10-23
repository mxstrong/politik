import axios, { AxiosRequestConfig, AxiosResponse } from 'axios';

axios.defaults.headers.common.Accept = 'application/json';
axios.defaults.timeout = 12000;

const getHttpHeaders = (isAuthenticated = false): AxiosRequestConfig => {
  // @TODO: add along with auth
  //   if (isAuthenticated) {
  //     return {
  //       headers: {
  //         Authorization: 'Bearer YOUR_TOKEN',
  //       },
  //     };
  //   }

  return {};
};

const getURL = (path: string) => {
  return `${process.env.NEXT_PUBLIC_API_URL}/${path}`;
};

const get = (path: string): Promise<AxiosResponse> =>
  axios.get(getURL(path), getHttpHeaders());

const del = (path: string): Promise<AxiosResponse> =>
  axios.delete(getURL(path), getHttpHeaders());

const post = (path: string, data: any): Promise<AxiosResponse> =>
  axios.post(getURL(path), data, getHttpHeaders());

const put = (path: string, data: any): Promise<AxiosResponse> =>
  axios.post(getURL(path), data, getHttpHeaders());

const patch = (path: string, data: any): Promise<AxiosResponse> =>
  axios.post(getURL(path), data, getHttpHeaders());

export default {
  get,
  del,
  post,
  put,
  patch,
};
