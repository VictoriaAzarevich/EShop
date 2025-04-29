import { CartHeader } from "./CartHeader";
import { CartDetails } from "./CartDetails";

export interface Cart {
  cartHeader: CartHeader;
  cartDetails: CartDetails[];
}
