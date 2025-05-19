import axios from "axios";
import { PaginatedProductResponse } from "../types/PaginatedProductResponse";
import { ProductCreateUpdate } from "../types/ProductCreateUpdate";

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

export const getProductById = async (id: number): Promise<ProductCreateUpdate> => {
  const response = await axios.get<ProductCreateUpdate>(`${BASE_URL}/${id}`);
  return response.data;
};

export const createProduct = async (product: FormData, token: string) => {
  const response = await axios.post(BASE_URL, product, {
    headers: {
      Authorization: `Bearer ${token}`
    }
    });
  return response.data;
};

export const updateProduct = async (id: number, productData: FormData, token: string): Promise<void> => {
  await axios.put(`${BASE_URL}/${id}`, productData, {
    headers: {
      "Content-Type": "multipart/form-data",
      Authorization: `Bearer ${token}`
    },
  }, );
};

export const deleteProduct = async (id: number, token: string): Promise<void> => {
  await axios.delete(`${BASE_URL}/${id}`, {
    headers: {
      Authorization: `Bearer ${token}`
    }
    });
};