export interface AuthRequest {
    userName: string;
    password: string;
}

export type RegisterRequest = AuthRequest;
export type LoginRequest = AuthRequest;
