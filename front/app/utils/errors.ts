import { toast } from 'react-toastify';
import { IFetchReponse } from './fetch';

const getResponseErrorMessages = (res: IFetchReponse) => {
  const errors = res.error?.response?.data?.errors;
  const errorMessages: string[] = [];

  if (errors) {
    Object.keys(errors).forEach((error) => {
      if (errors[error]) {
        errors[error].forEach((message: string) => {
          errorMessages.push(message);
        });
      }
    });
  }

  return errorMessages;
};

export const toastResponseErrorMessages = (res: IFetchReponse) => {
  const errorMessages = getResponseErrorMessages(res);

  if (errorMessages.length) {
    errorMessages.forEach((message) => {
      toast.error(message);
    });

    return;
  }

  toast.error('Įvyko klaida.');
};
