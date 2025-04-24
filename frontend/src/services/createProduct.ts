import axios from "axios";
const BASE_URL = "https://localhost:7230/api/product";

export const createProduct = async (product: FormData) => {
  const response = await axios.post(BASE_URL, product);
  return response.data;
};
