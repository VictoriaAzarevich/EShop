import { ProductResponse } from "./ProductResponse";

export interface PaginatedProductResponse {
  products: ProductResponse[];
  totalPages: number;
}
