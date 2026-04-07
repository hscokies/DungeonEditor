import { ref, type TemplateRef } from 'vue';
import { UiPopup } from '@/shared/ui';

export function useKeyboardNavigation(
    popup: TemplateRef<InstanceType<typeof UiPopup>>,
    findNearestOption: (idx: number, forward: boolean) => number,
    moveFocus: (idx: number) => void,
    selectByIndex: (idx: number) => void,
    closeOnTab: boolean
) {
    const focusedIndex = ref(-1);

    function onKeyDown(e: KeyboardEvent) {
        if (e.code === 'Enter') {
            OpenPopupOrSelect();
        } else if (e.code === 'Space') {
            // TODO: multiselect shouldn't close popup
            OpenPopupOrSelect();
        } else if (e.code.startsWith('Arrow')) {
            onArrowKeyDown(e);
        } else if (e.code === 'Home') {
            onHomeKeyDown();
        } else if (e.code === 'End') {
            onEndKeyDown();
        } else if (e.code === 'Tab') {
            onTabKeyDown();
            return;
        }
    }

    function OpenPopupOrSelect() {
        const popupInstance = popup.value!;

        if (popupInstance.visible) {
            selectByIndex(focusedIndex.value);
        } else {
            popupInstance.show();
        }
    }

    function onArrowKeyDown(e: KeyboardEvent) {
        const popupInstance = popup.value!;
        if (e.key === 'ArrowDown') {
            if (!popupInstance.visible) {
                return popupInstance.show();
            }

            const index = findNearestOption(focusedIndex.value + 1, true);
            moveFocus(index);
        } else if (e.key === 'ArrowUp') {
            if (!popupInstance.visible) {
                return popupInstance.show();
            }

            const index = findNearestOption(focusedIndex.value - 1, false);
            moveFocus(index);
        } else if (e.key === 'ArrowLeft') {
            if (popupInstance.visible) {
                return popupInstance.hide();
            }
        }
    }

    function onHomeKeyDown() {
        const index = 0;
        moveFocus(index);
    }

    function onEndKeyDown() {
        const index = findNearestOption(-1, false);
        moveFocus(index);
    }

    function onTabKeyDown() {
        if (closeOnTab) {
            popup.value!.hide();
        }
    }

    return { focusedIndex, onKeyDown };
}
