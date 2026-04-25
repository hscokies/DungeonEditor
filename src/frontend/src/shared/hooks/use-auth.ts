import { httpClient } from '@/shared/api/http-client.ts';
import type { User } from '@/entities/account/model/types.ts';
import { ref } from 'vue';

const user = ref<User>();
const authorized = ref(false);

export function useAuth() {
    async function fetch() {
        user.value = await httpClient.get<User>('/api/users/me');
        authorized.value = true;
    }

    return {
        user,
        fetch,
        authorized,
    };
}
