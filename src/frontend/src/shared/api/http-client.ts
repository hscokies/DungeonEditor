import { router, Routes } from '@/app/router';

const ISO8601 = /^\d{4}-\d{2}-\d{2}(?:[T\s]\d{2}:\d{2}:\d{2}(?:\.\d+)?(?:Z|[+\-]\d{2}:\d{2})?)?$/;

enum HTTP_METHOD {
    GET = 'GET',
    POST = 'POST',
    PUT = 'PUT',
    PATCH = 'PATCH',
    DELETE = 'DELETE',
}

enum CONTENT_TYPE {
    JSON = 'application/json',
}

interface HttpGetRequest {
    path: string;
    options: RequestInit;
}

type HttpRequest = HttpGetRequest;

class HttpClient {
    private relativeUrl(path: string, query?: Record<string, unknown>) {
        if (!query) {
            return path;
        }

        // @ts-ignore
        const searchParams = new URLSearchParams(query);
        return `${path}?${searchParams}`;
    }

    private async parseJson(response: Response) {
        const content = await response.text();
        return JSON.parse(content, (key, value) => {
            if (typeof value === 'string' && ISO8601.test(value)) {
                return new Date(value);
            }

            return value;
        });
    }

    public async sendRequest(request: HttpRequest) {
        const response = await fetch(request.path, request.options);
        if (response.status === 401) {
            await router.replace({ name: Routes.Login });
            throw new Error('Redirecting...');
        }

        return response;
    }

    public async get<TResult>(path: string, query?: Record<string, unknown>): Promise<TResult> {
        const response = await this.sendRequest({
            path: this.relativeUrl(path, query),
            options: {
                method: HTTP_METHOD.GET,
            },
        });

        const data = await this.parseJson(response);
        if (!response.ok) {
            throw data;
        }

        return data as TResult;
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
            throw await this.parseJson(response);
        }
    }

    public async postForm(path: string, data: FormData): Promise<void> {
        const response = await this.sendRequest({
            path,
            options: {
                method: HTTP_METHOD.POST,
                body: data,
            },
        });

        if (!response.ok) {
            throw await this.parseJson(response);
        }
    }

    public async patch<TRequest = Record<string, unknown>>(path: string, body: TRequest) {
        const response = await this.sendRequest({
            path,
            options: {
                method: HTTP_METHOD.PATCH,
                headers: {
                    'Content-Type': CONTENT_TYPE.JSON,
                },
                body: JSON.stringify(body),
            },
        });

        if (!response.ok) {
            throw await this.parseJson(response);
        }
    }

    public async delete(path: string) {
        return await this.sendRequest({
            path,
            options: {
                method: HTTP_METHOD.DELETE,
            },
        });
    }
}

export const httpClient = new HttpClient();
