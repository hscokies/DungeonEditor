import { httpClient } from '@/shared/api/http-client.ts';

export class SaveFileApi {
    public static upload(file: File): any {
        const formData = new FormData();
        formData.append('file', file);
        return httpClient.postForm('/api/savefiles', formData);
    }
}
