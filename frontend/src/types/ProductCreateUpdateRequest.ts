export interface ProductCreateUpdateRequest {
    name: string;
    price: number;
    description?: string;
    categoryId: number;
    imageUrl?: string;
    image?: File;
  }
  