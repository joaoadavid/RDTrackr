import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { api } from '@/lib/api';
import { useToast } from '@/hooks/use-toast';

// Products
export function useProducts(params?: any) {
  return useQuery({
    queryKey: ['products', params],
    queryFn: () => api.products.list(params),
  });
}

export function useProduct(id: string | number) {
  return useQuery({
    queryKey: ['products', id],
    queryFn: () => api.products.get(id),
    enabled: !!id,
  });
}

export function useCreateProduct() {
  const queryClient = useQueryClient();
  const { toast } = useToast();

  return useMutation({
    mutationFn: (data: any) => api.products.create(data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['products'] });
      toast({
        title: 'Produto criado',
        description: 'O produto foi criado com sucesso.',
      });
    },
    onError: (error: any) => {
      toast({
        title: 'Erro ao criar produto',
        description: error.message,
        variant: 'destructive',
      });
    },
  });
}

export function useUpdateProduct() {
  const queryClient = useQueryClient();
  const { toast } = useToast();

  return useMutation({
    mutationFn: ({ id, data }: { id: string | number; data: any }) =>
      api.products.update(id, data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['products'] });
      toast({
        title: 'Produto atualizado',
        description: 'O produto foi atualizado com sucesso.',
      });
    },
    onError: (error: any) => {
      toast({
        title: 'Erro ao atualizar produto',
        description: error.message,
        variant: 'destructive',
      });
    },
  });
}

export function useDeleteProduct() {
  const queryClient = useQueryClient();
  const { toast } = useToast();

  return useMutation({
    mutationFn: (id: string | number) => api.products.delete(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['products'] });
      toast({
        title: 'Produto excluído',
        description: 'O produto foi excluído com sucesso.',
      });
    },
    onError: (error: any) => {
      toast({
        title: 'Erro ao excluir produto',
        description: error.message,
        variant: 'destructive',
      });
    },
  });
}

// Warehouses
export function useWarehouses() {
  return useQuery({
    queryKey: ['warehouses'],
    queryFn: () => api.warehouses.list(),
  });
}

export function useCreateWarehouse() {
  const queryClient = useQueryClient();
  const { toast } = useToast();

  return useMutation({
    mutationFn: (data: any) => api.warehouses.create(data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['warehouses'] });
      toast({
        title: 'Depósito criado',
        description: 'O depósito foi criado com sucesso.',
      });
    },
    onError: (error: any) => {
      toast({
        title: 'Erro ao criar depósito',
        description: error.message,
        variant: 'destructive',
      });
    },
  });
}

// Inventory
export function useInventory(params?: any) {
  return useQuery({
    queryKey: ['inventory', params],
    queryFn: () => api.inventory.list(params),
  });
}

export function useAdjustInventory() {
  const queryClient = useQueryClient();
  const { toast } = useToast();

  return useMutation({
    mutationFn: (data: { productId: string; warehouseId: string; deltaQty: number; note?: string }) =>
      api.inventory.adjust(data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['inventory'] });
      queryClient.invalidateQueries({ queryKey: ['movements'] });
      toast({
        title: 'Estoque ajustado',
        description: 'O estoque foi ajustado com sucesso.',
      });
    },
    onError: (error: any) => {
      toast({
        title: 'Erro ao ajustar estoque',
        description: error.message,
        variant: 'destructive',
      });
    },
  });
}

export function useTransferInventory() {
  const queryClient = useQueryClient();
  const { toast } = useToast();

  return useMutation({
    mutationFn: (data: { productId: string; fromWarehouseId: string; toWarehouseId: string; quantity: number }) =>
      api.inventory.transfer(data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['inventory'] });
      queryClient.invalidateQueries({ queryKey: ['movements'] });
      toast({
        title: 'Transferência realizada',
        description: 'A transferência foi realizada com sucesso.',
      });
    },
    onError: (error: any) => {
      toast({
        title: 'Erro ao transferir',
        description: error.message,
        variant: 'destructive',
      });
    },
  });
}

// Movements
export function useMovements(params?: any) {
  return useQuery({
    queryKey: ['movements', params],
    queryFn: () => api.movements.list(params),
  });
}

export function useCreateMovement() {
  const queryClient = useQueryClient();
  const { toast } = useToast();

  return useMutation({
    mutationFn: (data: any) => api.movements.create(data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['movements'] });
      queryClient.invalidateQueries({ queryKey: ['inventory'] });
      toast({
        title: 'Movimentação criada',
        description: 'A movimentação foi registrada com sucesso.',
      });
    },
    onError: (error: any) => {
      toast({
        title: 'Erro ao criar movimentação',
        description: error.message,
        variant: 'destructive',
      });
    },
  });
}

// Suppliers
export function useSuppliers() {
  return useQuery({
    queryKey: ['suppliers'],
    queryFn: () => api.suppliers.list(),
  });
}

export function useCreateSupplier() {
  const queryClient = useQueryClient();
  const { toast } = useToast();

  return useMutation({
    mutationFn: (data: any) => api.suppliers.create(data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['suppliers'] });
      toast({
        title: 'Fornecedor criado',
        description: 'O fornecedor foi criado com sucesso.',
      });
    },
    onError: (error: any) => {
      toast({
        title: 'Erro ao criar fornecedor',
        description: error.message,
        variant: 'destructive',
      });
    },
  });
}

// Purchase Orders
export function usePurchaseOrders(params?: any) {
  return useQuery({
    queryKey: ['purchaseOrders', params],
    queryFn: () => api.purchaseOrders.list(params),
  });
}

export function useCreatePurchaseOrder() {
  const queryClient = useQueryClient();
  const { toast } = useToast();

  return useMutation({
    mutationFn: (data: any) => api.purchaseOrders.create(data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['purchaseOrders'] });
      toast({
        title: 'Pedido de compra criado',
        description: 'O pedido de compra foi criado com sucesso.',
      });
    },
    onError: (error: any) => {
      toast({
        title: 'Erro ao criar pedido',
        description: error.message,
        variant: 'destructive',
      });
    },
  });
}

export function useReceivePurchaseOrder() {
  const queryClient = useQueryClient();
  const { toast } = useToast();

  return useMutation({
    mutationFn: (id: string | number) => api.purchaseOrders.receive(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['purchaseOrders'] });
      queryClient.invalidateQueries({ queryKey: ['inventory'] });
      queryClient.invalidateQueries({ queryKey: ['movements'] });
      toast({
        title: 'Pedido recebido',
        description: 'O pedido foi recebido e o estoque atualizado.',
      });
    },
    onError: (error: any) => {
      toast({
        title: 'Erro ao receber pedido',
        description: error.message,
        variant: 'destructive',
      });
    },
  });
}
