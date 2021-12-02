import { IParty } from './parties';

export interface IPoliticiansListItem {
  id: string;
  party: IParty;
  fullName: string;
  description: string;
}

export interface IPoliticians {
  politicians: IPoliticiansListItem[];
}

export interface IPolitician {
  id: string;
  party: IParty;
  fullName: string;
  description: string;
}
