export interface AuthRequest {
    userName: string;
    password: string;
}

export type RegisterRequest = AuthRequest;
export type LoginRequest = AuthRequest;

export interface User {
    id: string;
    username: string;
    balance: number;
    admin?: boolean;
}

export interface ListUsersResponse {
    users: User[];
}
