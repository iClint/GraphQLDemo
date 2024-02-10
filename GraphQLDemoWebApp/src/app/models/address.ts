import { State } from '../enums/state';

export interface Address {
  line1: string;
  line2: string;
  city: string;
  state: State;
  postcode: string;
}
