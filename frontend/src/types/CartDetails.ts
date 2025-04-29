import { ProductResponse } from "./ProductResponse";

export interface CartDetails {
  cartDetailsId: number;
  cartHeaderId: number;
  productId: number;
  count: number;
  product: ProductResponse;
}
