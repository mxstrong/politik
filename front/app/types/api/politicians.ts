export interface IPoliticiansListItem {
  id: number;
  party: string;
  fullName: string;
  description: string;
}

export interface IPoliticians {
  politicians: IPoliticiansListItem[];
}
