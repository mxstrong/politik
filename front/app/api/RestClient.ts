import axios, { AxiosRequestHeaders, AxiosResponse } from 'axios';

interface IParams extends AxiosRequestHeaders {
  url: string;
  [key: string]: any;
}

interface IAxiosResponse extends AxiosResponse {
  error: any;
}

export const fetch = async (
  params: IParams = { url: '' }
): Promise<IAxiosResponse | { [key: string]: any }> => {
  return await axios({
    ...params,
    url: `${process.env.NEXT_PUBLIC_API_URL}/${params.url}`,
  }).catch((error) => {
    return { error };
  });
};
