export interface ProblemDetails {
    traceId: string;
    type: URL;
    title: string;
    status: number;
    detail: string;
    errors: Record<string, Error[]>;
}

interface Error {
    code: string;
    description: string;
}
