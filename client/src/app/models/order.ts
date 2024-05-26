export interface ShippingEmailAddress {
  fullName: string;
  emailAddress: string;
}

export interface OrderItem {
  productId: number;
  name: string;
  pictureUrl: string;
  price: number;
  quantity: number;
}

export interface Order {
  id: number;
  buyerId: string;
  orderDate: string;
  shippingEmailAddress: ShippingEmailAddress;
  // serviceFee: number;
  orderItems: OrderItem[];
  subtotal: number;
  orderStatus: string;
  total: number;
}
