import { httpClient } from '@/shared/api/http-client.ts';
import type { Dungeon, GetMapsResponse } from '@/entities/dungeon/model/types.ts';

export class DungeonApi {
    public static async get(id: string) {
        return await httpClient.get<Dungeon>(`/api/dungeons/${id}`);
    }

    public static async getMaps(offset: number, limit: number, search?: string) {
        return await httpClient.get<GetMapsResponse>(`/api/dungeons/maps`, {
            offset,
            limit,
            ...(search && { search }),
        });
    }

    public static async edit(id: string, dungeon: Dungeon) {
        return await httpClient.put(`/api/dungeons/${id}`, dungeon);
    }
}
