/**
 * API Field Mapping Configuration
 * Centralize field name conversions between API and frontend
 */

export const fieldMapping = {
  products: {
    // API field -> Frontend field
    id: 'id',
    name: 'name',
    sku: 'sku',
    price: 'price',
    uom: 'uom', // unit of measure
    category: 'category',
    imageUrl: 'imageUrl',
    reorderPoint: 'reorderPoint',
    stock: 'stock',
    updatedAt: 'updatedAt',
  },
  warehouses: {
    id: 'id',
    name: 'name',
    location: 'location',
    capacity: 'capacity',
    createdAt: 'createdAt',
  },
  inventory: {
    id: 'id',
    productId: 'productId',
    warehouseId: 'warehouseId',
    quantity: 'quantity',
    updatedAt: 'updatedAt',
  },
  movements: {
    id: 'id',
    type: 'type', // INBOUND | OUTBOUND | ADJUST
    productId: 'productId',
    warehouseId: 'warehouseId',
    quantity: 'quantity',
    reference: 'reference',
    note: 'note',
    createdAt: 'createdAt',
  },
  suppliers: {
    id: 'id',
    name: 'name',
    contact: 'contact',
    email: 'email',
    phone: 'phone',
    address: 'address',
  },
  purchaseOrders: {
    id: 'id',
    number: 'number',
    supplierId: 'supplierId',
    status: 'status', // DRAFT | PENDING | APPROVED | RECEIVED | CANCELLED
    items: 'items',
    total: 'total',
    createdAt: 'createdAt',
  },
};

export const queryParamMapping = {
  search: 'search',
  category: 'category',
  page: 'page',
  pageSize: 'pageSize',
  sort: 'sort',
  order: 'order',
};

/**
 * If your API uses different field names, update the mappings above.
 * Example: if API returns 'product_name' instead of 'name', update:
 * products: { name: 'product_name', ... }
 */
