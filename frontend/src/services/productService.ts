import axios from "axios";
import { PaginatedProductResponse } from "../types/PaginatedProductResponse";

const BASE_URL = "https://localhost:7230/api/product";

export const getAllProducts = async (
  page: number = 1,
  pageSize: number = 10,
  categoryId?: number
): Promise<PaginatedProductResponse> => {
  const url = new URL(BASE_URL);
  url.searchParams.append("page", page.toString());
  url.searchParams.append("pageSize", pageSize.toString());
  if (categoryId) url.searchParams.append("categoryId", categoryId.toString());

  const response = await axios.get(url.toString());
  return response.data;
};
