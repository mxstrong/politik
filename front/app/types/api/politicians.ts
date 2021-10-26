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
  id: string | string[] | undefined;
  party: string;
  fullName: string;
  description: string;
}
