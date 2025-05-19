export interface CartHeader {
    cartHeaderId?: number;
    userId: string;
    couponCode?: string;
    orderTotal: number;
    discountTotal: number;
    firstName: string;
    lastName: string;
    pickupDateTime: string; 
    phone: string;
    email: string;
    cardNumber: string;
    cvv: string;
    expiryMonthYear: string;
  }
  