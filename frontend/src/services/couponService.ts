import axios from "axios";
import { CouponResponse } from "../types/CouponResponse";
import { CouponCreateUpdate } from "../types/CouponCreateUpdate";

const API_BASE = "https://localhost:7108/api/coupon"; 

export const getCouponByCode = async (couponCode: string): Promise<CouponResponse> => {
  const response = await axios.get(`${API_BASE}/${couponCode}`);
  return response.data;
};

export const createCoupon = async (coupon: CouponCreateUpdate): Promise<CouponResponse> => {
  const response = await axios.post(API_BASE, coupon);
  return response.data;
};

export const updateCoupon = async (couponId: number, coupon: CouponCreateUpdate): Promise<CouponResponse> => {
  const response = await axios.put(`${API_BASE}/${couponId}`, coupon);
  return response.data;
};

export const deleteCoupon = async (couponId: number): Promise<void> => {
  await axios.delete(`${API_BASE}/${couponId}`);
};

export const getCoupons = async (): Promise<CouponResponse[]> => {
  const response = axios.get(`${API_BASE}`);
  return (await response).data;
};
