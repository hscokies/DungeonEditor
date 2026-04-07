import { type Placement, type PositioningStrategy } from '@popperjs/core';

export interface Props {
    reference: () => HTMLElement;
    placement?: Placement;
    strategy?: PositioningStrategy;
    offsetX?: number;
    offsetY?: number;
}

export interface Emits {
    (e: 'show'): void;
    (e: 'hide'): void;
}
