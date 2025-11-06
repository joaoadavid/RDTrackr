import { useState } from "react";
import {
  Card,
  CardContent,
  CardDescription,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table";
import { Badge } from "@/components/ui/badge";
import {
  Select,
  SelectTrigger,
  SelectContent,
  SelectItem,
  SelectValue,
} from "@/components/ui/select";
import { Search } from "lucide-react";
import { Input } from "@/components/ui/input";

const mockLogs = [
  {
    id: 1,
    user: "João Silva",
    action: "Criou pedido #ORD-2025-001",
    type: "CREATE",
    date: "2025-10-28 14:32",
  },
  {
    id: 2,
    user: "Maria Souza",
    action: "Atualizou estoque do produto #A123",
    type: "UPDATE",
    date: "2025-10-27 09:15",
  },
  {
    id: 3,
    user: "Admin",
    action: "Removeu cliente inativo",
    type: "DELETE",
    date: "2025-10-26 18:10",
  },
  {
    id: 4,
    user: "Carlos Pereira",
    action: "Login realizado",
    type: "LOGIN",
    date: "2025-10-25 07:45",
  },
];

const typeMap = {
  CREATE: { label: "Criação", variant: "default" as const },
  UPDATE: { label: "Atualização", variant: "secondary" as const },
  DELETE: { label: "Exclusão", variant: "destructive" as const },
  LOGIN: { label: "Login", variant: "outline" as const },
};

export default function AuditLog() {
  const [filter, setFilter] = useState<string>("all");
  const [search, setSearch] = useState("");

  const filteredLogs = mockLogs.filter(
    (log) =>
      (filter === "all" || log.type === filter) &&
      log.action.toLowerCase().includes(search.toLowerCase())
  );

  return (
    <div className="space-y-6">
      <div className="flex flex-col sm:flex-row sm:items-center sm:justify-between gap-4">
        <div>
          <h2 className="text-3xl font-bold tracking-tight">Auditoria</h2>
          <p className="text-muted-foreground">
            Registro de atividades do sistema
          </p>
        </div>
      </div>

      <Card>
        <CardHeader>
          <CardTitle>Log de Auditoria</CardTitle>
          <CardDescription>
            Histórico de ações de usuários e eventos do sistema
          </CardDescription>
        </CardHeader>

        <CardContent>
          <div className="flex flex-col sm:flex-row items-center justify-between gap-4 mb-4">
            <div className="flex items-center w-full sm:w-1/2 relative">
              <Search className="absolute left-2 h-4 w-4 text-muted-foreground" />
              <Input
                placeholder="Pesquisar ação..."
                value={search}
                onChange={(e) => setSearch(e.target.value)}
                className="pl-8"
              />
            </div>

            <Select value={filter} onValueChange={setFilter}>
              <SelectTrigger className="w-[180px]">
                <SelectValue placeholder="Tipo de ação" />
              </SelectTrigger>
              <SelectContent>
                <SelectItem value="all">Todos</SelectItem>
                <SelectItem value="CREATE">Criação</SelectItem>
                <SelectItem value="UPDATE">Atualização</SelectItem>
                <SelectItem value="DELETE">Exclusão</SelectItem>
                <SelectItem value="LOGIN">Login</SelectItem>
              </SelectContent>
            </Select>
          </div>

          <Table>
            <TableHeader>
              <TableRow>
                <TableHead>Usuário</TableHead>
                <TableHead>Ação</TableHead>
                <TableHead>Tipo</TableHead>
                <TableHead>Data</TableHead>
              </TableRow>
            </TableHeader>
            <TableBody>
              {filteredLogs.map((log) => (
                <TableRow key={log.id}>
                  <TableCell className="font-medium">{log.user}</TableCell>
                  <TableCell>{log.action}</TableCell>
                  <TableCell>
                    <Badge variant={typeMap[log.type].variant}>
                      {typeMap[log.type].label}
                    </Badge>
                  </TableCell>
                  <TableCell>{log.date}</TableCell>
                </TableRow>
              ))}
              {filteredLogs.length === 0 && (
                <TableRow>
                  <TableCell
                    colSpan={4}
                    className="text-center text-muted-foreground py-6"
                  >
                    Nenhum registro encontrado.
                  </TableCell>
                </TableRow>
              )}
            </TableBody>
          </Table>
        </CardContent>
      </Card>
    </div>
  );
}
