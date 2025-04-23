import axios from "axios";
import { PaginatedProductResponse } from "../types/PaginatedProductResponse";

const BASE_URL = "https://localhost:7230/api/product";

export const getAllProducts = async (
  page: number = 1,
  pageSize: number = 10
): Promise<PaginatedProductResponse> => {
  const response = await axios.get(`${BASE_URL}?page=${page}&pageSize=${pageSize}`);
  return response.data;
};
