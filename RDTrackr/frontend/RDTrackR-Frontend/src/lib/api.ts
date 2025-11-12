/**
 * API Wrapper for external inventory API
 * All requests are made server-side or with proper CORS handling
 */

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || 'https://api.example.com/v1';
const API_TOKEN = import.meta.env.VITE_API_TOKEN || '';

interface ApiFetchOptions {
  method?: 'GET' | 'POST' | 'PATCH' | 'DELETE';
  params?: Record<string, any>;
  body?: any;
  headers?: Record<string, string>;
}

export class ApiError extends Error {
  status: number;
  data: any;

  constructor(message: string, status: number, data?: any) {
    super(message);
    this.status = status;
    this.data = data;
    this.name = 'ApiError';
  }
}

export async function apiFetch<T>(path: string, options: ApiFetchOptions = {}): Promise<T> {
  const { method = 'GET', params, body, headers = {} } = options;

  // Build URL with query params
  const url = new URL(`${API_BASE_URL}${path}`);
  if (params) {
    Object.entries(params).forEach(([key, value]) => {
      if (value !== undefined && value !== null && value !== '') {
        url.searchParams.append(key, String(value));
      }
    });
  }

  // Build headers
  const requestHeaders: Record<string, string> = {
    'Content-Type': 'application/json',
    ...headers,
  };

  if (API_TOKEN) {
    requestHeaders['Authorization'] = `Bearer ${API_TOKEN}`;
  }

  try {
    const response = await fetch(url.toString(), {
      method,
      headers: requestHeaders,
      body: body ? JSON.stringify(body) : undefined,
    });

    if (!response.ok) {
      let errorData;
      try {
        errorData = await response.json();
      } catch {
        errorData = { message: response.statusText };
      }
      throw new ApiError(
        errorData.message || `API Error: ${response.status}`,
        response.status,
        errorData
      );
    }

    // Handle 204 No Content
    if (response.status === 204) {
      return {} as T;
    }

    return await response.json();
  } catch (error) {
    if (error instanceof ApiError) {
      throw error;
    }
    throw new ApiError(
      error instanceof Error ? error.message : 'Network error',
      0,
      error
    );
  }
}

// Type-safe API endpoint builders
export const api = {
  // Products
  products: {
    list: (params?: { search?: string; category?: string; page?: number; pageSize?: number; sort?: string; order?: 'asc' | 'desc' }) =>
      apiFetch<{ data: any[]; total: number; page: number; pageSize: number }>('/products', { params }),
    get: (id: string | number) => apiFetch<any>(`/products/${id}`),
    create: (data: any) => apiFetch<any>('/products', { method: 'POST', body: data }),
    update: (id: string | number, data: any) => apiFetch<any>(`/products/${id}`, { method: 'PATCH', body: data }),
    delete: (id: string | number) => apiFetch<void>(`/products/${id}`, { method: 'DELETE' }),
  },

  // Warehouses
  warehouses: {
    list: () => apiFetch<any[]>('/warehouses'),
    get: (id: string | number) => apiFetch<any>(`/warehouses/${id}`),
    create: (data: any) => apiFetch<any>('/warehouses', { method: 'POST', body: data }),
    update: (id: string | number, data: any) => apiFetch<any>(`/warehouses/${id}`, { method: 'PATCH', body: data }),
    delete: (id: string | number) => apiFetch<void>(`/warehouses/${id}`, { method: 'DELETE' }),
  },

  // Inventory
  inventory: {
    list: (params?: { productId?: string; warehouseId?: string; page?: number; pageSize?: number }) =>
      apiFetch<{ data: any[]; total: number }>('/inventory', { params }),
    adjust: (data: { productId: string; warehouseId: string; deltaQty: number; note?: string }) =>
      apiFetch<any>('/inventory/adjust', { method: 'POST', body: data }),
    transfer: (data: { productId: string; fromWarehouseId: string; toWarehouseId: string; quantity: number }) =>
      apiFetch<any>('/inventory/transfer', { method: 'POST', body: data }),
  },

  // Movements
  movements: {
    list: (params?: { type?: string; productId?: string; warehouseId?: string; from?: string; to?: string; ref?: string; page?: number; pageSize?: number }) =>
      apiFetch<{ data: any[]; total: number }>('/movements', { params }),
    create: (data: any) => apiFetch<any>('/movements', { method: 'POST', body: data }),
  },

  // Suppliers
  suppliers: {
    list: () => apiFetch<any[]>('/suppliers'),
    get: (id: string | number) => apiFetch<any>(`/suppliers/${id}`),
    create: (data: any) => apiFetch<any>('/suppliers', { method: 'POST', body: data }),
    update: (id: string | number, data: any) => apiFetch<any>(`/suppliers/${id}`, { method: 'PATCH', body: data }),
    delete: (id: string | number) => apiFetch<void>(`/suppliers/${id}`, { method: 'DELETE' }),
  },

  // Purchase Orders
  purchaseOrders: {
    list: (params?: { status?: string; supplierId?: string; page?: number; pageSize?: number }) =>
      apiFetch<{ data: any[]; total: number }>('/purchase-orders', { params }),
    get: (id: string | number) => apiFetch<any>(`/purchase-orders/${id}`),
    create: (data: any) => apiFetch<any>('/purchase-orders', { method: 'POST', body: data }),
    update: (id: string | number, data: any) => apiFetch<any>(`/purchase-orders/${id}`, { method: 'PATCH', body: data }),
    receive: (id: string | number) => apiFetch<any>(`/purchase-orders/${id}/receive`, { method: 'POST' }),
  },
};
