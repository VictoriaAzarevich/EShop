import axios from "axios";
import { CategoryResponse } from "../types/CategoryResponse";
import { CategoryRequest} from "../types/CategoryRequest";

const BASE_URL = "https://localhost:7230/api/category";

export const getCategories = async (): Promise<CategoryResponse[]> => {
  const response = await axios.get(BASE_URL);
  return response.data;
};

export const getCategoryById = async (id: number): Promise<CategoryResponse> => {
  const response = await axios.get(`${BASE_URL}/${id}`);
  return response.data;
};

export const createCategory = async (category: CategoryRequest): Promise<void> => {
  await axios.post(`${BASE_URL}`, category);
};

export const updateCategory = async (
  id: number,
  data: CategoryRequest
): Promise<void> => {
  await axios.put(`${BASE_URL}/${id}`, data);
};

export const deleteCategory = async (id: number): Promise<void> => {
  await axios.delete(`${BASE_URL}/${id}`);
};