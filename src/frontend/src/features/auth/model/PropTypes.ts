import type { AuthRequest } from '@/entities/account/model/types.ts';

export interface PropTypes {
    title: string;
    handleAuth: (request: AuthRequest) => Promise<void>;
}
