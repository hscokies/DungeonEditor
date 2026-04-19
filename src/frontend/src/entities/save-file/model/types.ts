import type { SaveFileState } from '@/features/uploads/model/types.ts';

export interface ListSaveFilesResponse {
    saveFiles: {
        id: string;
        fileName: string;
        uploadedAt: Date;
        state: SaveFileState;
    }[];
}
