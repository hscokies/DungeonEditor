import { router } from '@/app/router';

export enum HTTP_METHOD {
    GET = 'GET',
    POST = 'POST',
    PUT = 'PUT',
    PATCH = 'PATCH',
    DELETE = 'DELETE',
}

export enum CONTENT_TYPE {
    JSON = 'application/json',
    FORM = 'application/x-www-form-urlencoded',
}

interface HttpGetRequest {
    path: string;
    options: RequestInit;
}

type HttpRequest = HttpGetRequest;

export class HttpClient {
    constructor(private readonly baseUrl: string) {}

    private getFullUrl(path: string) {
        return new URL(path, this.baseUrl);
    }

    public async sendRequest(request: HttpRequest) {
        const response = await fetch(this.getFullUrl(request.path), request.options);
        if (response.status === 401) {
            await router.push('/login');
            return;
        }

        return response;
    }
}
