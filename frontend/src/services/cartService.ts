import axios from "axios";
import { Cart } from "../types/Cart";

const API_URL = "https://localhost:7153/api/cart";

export const getCartByUserId = async (userId: string): Promise<Cart> => {
  const response = await axios.get(`${API_URL}/${userId}`);
  return response.data;
};

export const createOrUpdateCart = async (cart: Cart): Promise<Cart> => {
  const response = await axios.post(API_URL, cart);
  return response.data;
};

export const removeCartItem = async (cartDetailsId: number): Promise<void> => {
  await axios.delete(`${API_URL}/remove-item/${cartDetailsId}`);
};

export const applyCoupon = async (cart: Cart): Promise<void> => {
  await axios.post(`${API_URL}/apply-coupon`, cart);
};

export const removeCoupon = async (userId: string): Promise<void> => {
  await axios.post(`${API_URL}/remove-coupon/${userId}`);
};
