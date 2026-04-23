import type { LoginRequest, RegisterRequest, User } from '@/entities/account/model/types.ts';
import { httpClient } from '@/shared/api/http-client.ts';

export class AccountApi {
    public static register(request: RegisterRequest) {
        return httpClient.postJson('/api/users', request);
    }

    public static login(request: LoginRequest) {
        return httpClient.postJson('/api/session', request);
    }

    public static logout() {
        return httpClient.delete('/api/session');
    }

    public static get() {
        return httpClient.get<User>('/api/users/me');
    }
}
