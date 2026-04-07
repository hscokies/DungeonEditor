enum CloseEvent {
    Mousedown = 'mousedown',
    Keydown = 'keydown',
}

type EventTypes = KeyboardEvent | MouseEvent;

type EventHandler = (event: EventTypes) => void;

export class PopupService {
    private readonly activePopups = new Map<HTMLElement, EventHandler>();

    constructor() {
        document.body.addEventListener(CloseEvent.Keydown, this.onKeydown.bind(this));
        document.body.addEventListener(CloseEvent.Mousedown, this.onMousedown.bind(this));
    }

    public register(element: HTMLElement, handler: EventHandler) {
        this.activePopups.set(element, handler);
    }

    public remove(element: HTMLElement) {
        this.activePopups.delete(element);
    }

    private onKeydown(event: KeyboardEvent) {
        if (event instanceof KeyboardEvent && event.key === 'Escape') {
            const handler = [...this.activePopups.values()].pop();
            if (handler) {
                handler(event);
            }
        }
    }

    private onMousedown(event: MouseEvent) {
        for (const [element, handler] of this.activePopups) {
            if (!(element === event.target || element.contains(event.target as Node))) {
                handler(event);
            }
        }
    }
}
