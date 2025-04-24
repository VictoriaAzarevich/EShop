import axios from "axios";
import { CategoryResponse } from "../types/CategoryResponse";

const CATEGORY_URL = "https://localhost:7230/api/category";

export const getCategories = async (): Promise<CategoryResponse[]> => {
  const response = await axios.get(CATEGORY_URL);
  return response.data;
};
