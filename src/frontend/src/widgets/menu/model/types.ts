import { Routes } from '@/app/router';
import type { FunctionalComponent } from 'vue';

interface NavigationLink {
    route: {
        name: Routes;
    };
    icon: FunctionalComponent;
    label: string;
    admin: boolean;
}

export interface PropTypes {
    links: NavigationLink[];
    balance?: number;
}

export type Emits = (e: 'logout') => void;
