import axios, {
  AxiosRequestHeaders,
  AxiosResponse,
  AxiosResponseHeaders,
} from 'axios';
import { getLocalStorageItem } from './storage';

interface IParams extends AxiosRequestHeaders {
  [key: string]: any;
}

interface IAxiosResponse extends AxiosResponse {
  error: any;
  [key: string]: any;
}

export type IFetchReponse = IAxiosResponse | { [key: string]: any };

export const _fetch = async (
  params: IParams = { url: '' }
): Promise<IFetchReponse> => {
  return await axios({
    ...params,
    withCredentials: true,
    url: `${process.env.NEXT_PUBLIC_API_URL}/${params.url}`,
    headers: {
      Authorization: getLocalStorageItem('jwt')
        ? `Bearer ${getLocalStorageItem('jwt')}`
        : '',
    },
  }).catch((error) => {
    return { error };
  });
};

export const fetchAll = (requests: Promise<IFetchReponse>[]) => {
  return Promise.all(requests).then((response) => response);
};

export const getPaginationHeaderData = (headers: AxiosResponseHeaders) => {
  return JSON.parse(headers['x-pagination']);
};
