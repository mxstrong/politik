export interface IPoliticiansListItem {
  id: string;
  party: string;
  fullName: string;
  description: string;
}

export interface IPoliticians {
  politicians: IPoliticiansListItem[];
}

export interface IPolitician {
  id: string;
  party: string;
  partyShort: string;
  fullName: string;
  description: string;
}
