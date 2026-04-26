import { httpClient } from '@/shared/api/http-client.ts';
import type { GetSaveFilesResponse, ListSaveFilesResponse } from '@/entities/save-file/model/types.ts';

export class SaveFileApi {
    public static upload(file: File) {
        const formData = new FormData();
        formData.append('file', file);
        return httpClient.postForm('/api/savefiles', formData);
    }

    public static list(offset: number, limit: number, search: string | undefined) {
        return httpClient.get<ListSaveFilesResponse>('/api/savefiles', { offset, limit, ...(search && { search }) });
    }

    public static remove(id: string) {
        return httpClient.delete(`/api/savefiles/${id}`);
    }

    public static get(id: string) {
        return httpClient.get<GetSaveFilesResponse>(`/api/savefiles/${id}`);
    }

    public static compile(id: string) {
        return httpClient.patch(`/savefiles/${id}`);
    }

    public static async download(id: string) {
        window.location.href = `/api/savefiles/${id}/download`;
    }
}
