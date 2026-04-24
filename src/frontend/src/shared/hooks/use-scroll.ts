import { onMounted, onUnmounted, ref, type Ref } from 'vue';

export function useScroll(elementRef: Ref<HTMLElement | null>) {
    const event: keyof HTMLElementEventMap = 'scroll';

    const x = ref(0);
    const y = ref(0);

    function onScroll(event: Event) {
        const element = event.target as HTMLElement;

        x.value = element.scrollLeft;
        y.value = element.scrollTop;
    }

    onMounted(() => {
        const element = elementRef.value;

        element?.addEventListener(event, onScroll);
    });

    onUnmounted(() => {
        elementRef.value?.removeEventListener(event, onScroll);
    });

    return {
        x,
        y,
    };
}
