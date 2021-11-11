import axios, {
  AxiosRequestHeaders,
  AxiosResponse,
  AxiosResponseHeaders,
} from 'axios';

interface IParams extends AxiosRequestHeaders {
  url: string;
  [key: string]: any;
}

interface IAxiosResponse extends AxiosResponse {
  error: any;
}

export const _fetch = async (
  params: IParams = { url: '' }
): Promise<IAxiosResponse | { [key: string]: any }> => {
  return await axios({
    ...params,
    url: `${process.env.NEXT_PUBLIC_API_URL}/${params.url}`,
  }).catch((error) => {
    return { error };
  });
};

export const fetchAll = (
  requests: Promise<IAxiosResponse | { [key: string]: any }>[]
) => {
  return Promise.all(requests).then((response) => response);
};

export const getPaginationHeaderData = (headers: AxiosResponseHeaders) => {
  return JSON.parse(headers['x-pagination']);
};
