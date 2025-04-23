export interface ProductResponse {
  productId: number;
  name: string;
  price: number;
  description?: string;
  categoryId: number;
  categoryName: string;
  imageUrl?: string;
}

  