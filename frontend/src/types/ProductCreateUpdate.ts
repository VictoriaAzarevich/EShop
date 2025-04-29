export interface ProductCreateUpdate {
    name: string;
    price: number;
    description?: string;
    categoryId: number;
    categoryName: string;
    imageUrl?: string;
    image?: File
  }
  