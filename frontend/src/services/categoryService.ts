import axios from "axios";
import { CategoryResponse } from "../types/CategoryResponse";
import { CategoryCreateUpdate} from "../types/CategoryCreateUpdate";

const BASE_URL = "https://localhost:7230/api/category";

export const getCategories = async (): Promise<CategoryResponse[]> => {
  const response = await axios.get(BASE_URL);
  return response.data;
};

export const getCategoryById = async (id: number): Promise<CategoryResponse> => {
  const response = await axios.get(`${BASE_URL}/${id}`);
  return response.data;
};

export const createCategory = async (category: CategoryCreateUpdate, token: string): Promise<void> => {
  await axios.post(`${BASE_URL}`, category, {
    headers: {
      Authorization: `Bearer ${token}`
    }
    });
};

export const updateCategory = async (
  id: number,
  data: CategoryCreateUpdate,
  token: string
): Promise<void> => {
  await axios.put(`${BASE_URL}/${id}`, data, {
    headers: {
      Authorization: `Bearer ${token}`
    }
    });
};

export const deleteCategory = async (id: number, token: string): Promise<void> => {
  await axios.delete(`${BASE_URL}/${id}`, {
    headers: {
      Authorization: `Bearer ${token}`
    }
    });
};