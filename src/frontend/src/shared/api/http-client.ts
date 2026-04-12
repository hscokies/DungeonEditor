import { router, Routes } from '@/app/router';

enum HTTP_METHOD {
    GET = 'GET',
    POST = 'POST',
    PUT = 'PUT',
    PATCH = 'PATCH',
    DELETE = 'DELETE',
}

enum CONTENT_TYPE {
    JSON = 'application/json',
    FORM = 'application/x-www-form-urlencoded',
}

interface HttpGetRequest {
    path: string;
    options: RequestInit;
}

type HttpRequest = HttpGetRequest;

class HttpClient {
    public async sendRequest(request: HttpRequest) {
        const response = await fetch(request.path, request.options);
        if (response.status === 401) {
            await router.replace(Routes.Login);
        }

        return response;
    }

    public async postJson<TRequest = Record<string, unknown>>(path: string, body: TRequest): Promise<void> {
        const response = await this.sendRequest({
            path,
            options: {
                method: HTTP_METHOD.POST,
                headers: {
                    'Content-Type': CONTENT_TYPE.JSON,
                },
                body: JSON.stringify(body),
            },
        });

        if (!response.ok) {
            throw await response.json();
        }

        return undefined;
    }
}

export const httpClient = new HttpClient();
