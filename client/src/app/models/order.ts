export interface ShippingEmailAddress {
  fullName: string;
  address1: string;
  address2: string;
  city: string;
  state: string;
  zip: string;
  country: string;
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
