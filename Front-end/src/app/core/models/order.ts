export interface CreateOrderItem {
  productId: string
  quantity: number
}

export interface CreateOrderRequest {
  userId: string
  items: CreateOrderItem[]
}

export interface OrderItemResponse {
  productId: string
  productName: string
  quantity: number
  price: number
  total: number
}

export interface OrderResponse {
  orderId: string
  totalAmount: number
  discount: number
  items: OrderItemResponse[]
}