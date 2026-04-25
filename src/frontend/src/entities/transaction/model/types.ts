export enum TransactionType {
    Inbound,
    Outbound,
}

export interface Transaction {
    id: string;
    type: TransactionType;
    createdAt: Date;
    amount: number;
    description: string;
}

export interface GetTransactionsResponse {
    transactions: Transaction[];
}
