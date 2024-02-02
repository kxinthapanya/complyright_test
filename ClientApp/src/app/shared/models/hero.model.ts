export interface Hero {
  name: string | null;
  alias: string | null;
  brandId: number | null;
  brandName: string | null;
  createdOn: Date | null;
  id: number | null;
  isActive: boolean | null;
  updatedOn: Date | null;
}

export interface AddHero {
  name: string | null;
  alias: string | null;
  brandId: number | undefined;
  brandName: string | undefined;
}
