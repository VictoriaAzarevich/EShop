import axios from "axios";
import { Cart } from "../types/Cart";
import { CartHeader } from "../types/CartHeader";

const API_URL = "https://localhost:7153/api/cart";

export const getCartByUserId = async (userId: string, token: string): Promise<Cart> => {
  const response = await axios.get(`${API_URL}/${userId}`, {
    headers: {
      Authorization: `Bearer ${token}`
    }
    });
  return response.data;
};

export const createOrUpdateCart = async (cart: Cart, token: string): Promise<Cart> => {
  const response = await axios.post(API_URL, cart, {
    headers: {
      Authorization: `Bearer ${token}`
    }
    });
  return response.data;
};

export const removeCartItem = async (cartDetailsId: number, token: string): Promise<void> => {
  await axios.delete(`${API_URL}/remove-item/${cartDetailsId}`, {
    headers: {
      Authorization: `Bearer ${token}`
    }
    });
};

export const applyCoupon = async (cart: Cart, token: string): Promise<void> => {
  await axios.post(`${API_URL}/apply-coupon`, cart, {
    headers: {
      Authorization: `Bearer ${token}`
    }
    });
};

export const removeCoupon = async (userId: string, token: string): Promise<void> => {
  await axios.post(`${API_URL}/remove-coupon/${userId}`, {}, {
    headers: {
      Authorization: `Bearer ${token}`
    }
    });
};

export const checkout = async (cartHeader: CartHeader, token: string): Promise<void> => {
  await axios.post(`${API_URL}/checkout`, cartHeader, {
    headers: {
      Authorization: `Bearer ${token}`
    }
    });
};
