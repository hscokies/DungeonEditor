import type { GetTransactionsResponse } from '@/entities/transactions/model/types.ts';
import { httpClient } from '@/shared/api/http-client.ts';

export class TransactionsApi {
    public static async list(offset: number, limit: number) {
        return httpClient.get<GetTransactionsResponse>('/api/users/transactions', { offset, limit });
    }
}
