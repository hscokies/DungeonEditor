import { httpClient } from '@/shared/api/http-client.ts';
import type { ListSaveFilesResponse } from '@/entities/save-file/model/types.ts';

export class SaveFileApi {
    public static upload(file: File) {
        const formData = new FormData();
        formData.append('file', file);
        return httpClient.postForm('/api/savefiles', formData);
    }

    public static list(offset: number, limit: number) {
        return httpClient.get<ListSaveFilesResponse>('/api/savefiles', { offset, limit });
    }

    public static remove(id: string) {
        return httpClient.delete(`/api/savefiles/${id}`);
    }
}
