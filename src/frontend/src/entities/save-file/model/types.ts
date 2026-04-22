export enum SaveFileState {
    Uploaded = 0,
    Processing = 1,
    Processed = 2,
    Failed = 3,
    Error = 4,
}

export interface SaveFile {
    id: string;
    fileName: string;
    uploadedAt: Date;
    state: SaveFileState;
}

export interface ListSaveFilesResponse {
    saveFiles: SaveFile[];
}
