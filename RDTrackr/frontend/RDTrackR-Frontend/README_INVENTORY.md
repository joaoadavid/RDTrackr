# Módulo de Estoque - Admin Dashboard

Este módulo implementa um sistema completo de gestão de estoque consumindo uma API externa.

## Configuração da API

### 1. Variáveis de Ambiente

Crie um arquivo `.env.local` na raiz do projeto (copie de `.env.example`):

```bash
VITE_API_BASE_URL=https://api.example.com/v1
VITE_API_TOKEN=seu_token_aqui
```

### 2. Estrutura da API Esperada

O wrapper de API (`src/lib/api.ts`) espera os seguintes endpoints:

#### Produtos
- `GET /products` - Lista produtos (com filtros: search, category, page, pageSize, sort, order)
- `GET /products/:id` - Detalhes de um produto
- `POST /products` - Criar produto
- `PATCH /products/:id` - Atualizar produto
- `DELETE /products/:id` - Excluir produto

#### Depósitos
- `GET /warehouses` - Lista depósitos
- `POST /warehouses` - Criar depósito
- `PATCH /warehouses/:id` - Atualizar depósito
- `DELETE /warehouses/:id` - Excluir depósito

#### Inventário
- `GET /inventory` - Lista inventário (filtros: productId, warehouseId, page, pageSize)
- `POST /inventory/adjust` - Ajustar estoque
- `POST /inventory/transfer` - Transferir entre depósitos

#### Movimentações
- `GET /movements` - Lista movimentações (filtros: type, productId, warehouseId, from, to, ref, page, pageSize)
- `POST /movements` - Criar movimentação

#### Fornecedores
- `GET /suppliers` - Lista fornecedores
- `POST /suppliers` - Criar fornecedor
- `PATCH /suppliers/:id` - Atualizar fornecedor
- `DELETE /suppliers/:id` - Excluir fornecedor

#### Pedidos de Compra
- `GET /purchase-orders` - Lista pedidos (filtros: status, supplierId, page, pageSize)
- `POST /purchase-orders` - Criar pedido
- `PATCH /purchase-orders/:id` - Atualizar pedido
- `POST /purchase-orders/:id/receive` - Receber pedido

### 3. Mapeamento de Campos

Se sua API usar nomes de campos diferentes, edite `src/lib/api-mapping.ts` para mapear os campos.

Exemplo:
```typescript
export const fieldMapping = {
  products: {
    name: 'product_name',  // Se sua API usa 'product_name' em vez de 'name'
    sku: 'product_sku',    // etc.
  }
};
```

## Funcionalidades Implementadas

### ✅ Visão Geral do Estoque
- KPIs: produtos ativos, estoque total, itens abaixo do reorder, movimentações
- Gráfico de entradas vs saídas
- Lista de itens com estoque baixo
- CTA para criar pedido de compra

### ✅ Gestão de Itens
- Listagem com busca e filtros
- Visualização de estoque por depósito
- Ajuste rápido de estoque
- CRUD completo

### ✅ Depósitos
- Listagem de depósitos
- Visualização de utilização
- Transferência entre depósitos
- CRUD completo

### ✅ Movimentações
- Histórico completo (INBOUND/OUTBOUND/ADJUST)
- Filtros por tipo, produto, depósito, período
- Criação de novas movimentações

### ✅ Fornecedores
- Listagem com busca
- CRUD completo
- Link para pedidos relacionados

### ✅ Pedidos de Compra
- Listagem com filtros por status
- Criação e edição de pedidos
- Recebimento de pedidos (atualiza estoque)

## Hooks do TanStack Query

Todos os hooks estão em `src/hooks/use-inventory-api.ts`:

```typescript
// Produtos
useProducts(params)
useProduct(id)
useCreateProduct()
useUpdateProduct()
useDeleteProduct()

// Depósitos
useWarehouses()
useCreateWarehouse()

// Inventário
useInventory(params)
useAdjustInventory()
useTransferInventory()

// Movimentações
useMovements(params)
useCreateMovement()

// Fornecedores
useSuppliers()
useCreateSupplier()

// Pedidos de Compra
usePurchaseOrders(params)
useCreatePurchaseOrder()
useReceivePurchaseOrder()
```

## Dados Mock

Atualmente, as páginas usam dados mock para desenvolvimento. Para conectar à API real:

1. Configure as variáveis de ambiente
2. Descomente as chamadas aos hooks nas páginas
3. Remova ou comente os dados mock

Exemplo em `src/pages/inventory/Items.tsx`:
```typescript
// Descomente quando a API estiver pronta
const { data, isLoading, error } = useProducts({ search, category });

// Comente o mock
// const mockProducts = [...]
```

## Estrutura de Arquivos

```
src/
├── lib/
│   ├── api.ts              # Wrapper da API
│   └── api-mapping.ts      # Mapeamento de campos
├── hooks/
│   └── use-inventory-api.ts # Hooks do TanStack Query
├── pages/
│   └── inventory/
│       ├── Overview.tsx     # Visão geral
│       ├── Items.tsx        # Itens
│       ├── Warehouses.tsx   # Depósitos
│       ├── Movements.tsx    # Movimentações
│       ├── Suppliers.tsx    # Fornecedores
│       └── PurchaseOrders.tsx # Pedidos
```

## Tratamento de Erros

O wrapper de API (`apiFetch`) trata automaticamente:
- Erros HTTP (4xx, 5xx)
- Erros de rede
- Respostas vazias (204)

Os hooks do TanStack Query mostram toasts de sucesso/erro automaticamente.

## Próximos Passos

1. **Conectar à API Real**: Configure `.env.local` com URL e token reais
2. **Substituir Mock por API**: Descomentar hooks nas páginas
3. **Adicionar Modais/Sheets**: Para CRUD inline (criar/editar/ajustar)
4. **Implementar Paginação**: Server-side em todas as listagens
5. **Adicionar Validações**: Formulários com zod + react-hook-form
6. **Testes**: Adicionar testes com Vitest + React Testing Library

## Segurança

⚠️ **IMPORTANTE**: 
- Nunca exponha tokens de API no código
- Use variáveis de ambiente (`VITE_*` para frontend)
- Para tokens sensíveis, considere usar proxy server-side
- Implemente autenticação adequada em produção

## Suporte

Para dúvidas sobre o mapeamento de campos ou ajustes na API, consulte:
- `src/lib/api.ts` - Implementação do wrapper
- `src/lib/api-mapping.ts` - Configuração de mapeamento
- Este README
