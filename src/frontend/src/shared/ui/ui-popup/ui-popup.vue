<script setup lang="ts">
import type { Emits, Props } from './ui-popup.types.ts';
import { createPopper, type Instance as PopperInstance } from '@popperjs/core';
import { nextTick, onBeforeUnmount, ref, useTemplateRef } from 'vue';
import { popupService } from '@/shared/servies';

const { placement = 'bottom', strategy = 'absolute', offsetX, offsetY, reference } = defineProps<Props>();
const emit = defineEmits<Emits>();

const triggerClassName = 'ui-popup__trigger';
let popper: PopperInstance | null = null;
const visible = ref(false);

const $el = useTemplateRef('element');
const popup = useTemplateRef('popup');

function toggle() {
    if (visible.value) {
        hide();
    } else {
        show();
    }
}

function show() {
    if (visible.value) {
        return;
    }

    visible.value = true;
    popupService.register($el.value!, handlePopupClose);
    emit('show');

    nextTick(() => {
        const popupReference = reference();
        if (!popupReference) {
            throw new Error('unable to create popup: Reference is undefined');
        }

        const content = popup.value;
        if (!content) {
            throw new Error('unable to create popup: Content is undefined');
        }

        popper = createPopper(popupReference, content, {
            placement,
            strategy,
            modifiers: [
                {
                    name: 'offset',
                    options: { offset: [offsetX, offsetY] },
                },
            ],
        });
    });
}

function hide() {
    if (!visible.value) {
        return;
    }

    popper?.destroy();
    visible.value = false;
}

function update() {
    popper?.update();
}

function handlePopupClose(ev: KeyboardEvent | MouseEvent) {
    hide();

    $el.value?.querySelector<HTMLElement>(`.${triggerClassName}`)?.focus();
    popupService.remove($el.value!);
}

onBeforeUnmount(() => {
    popupService.remove($el.value!);
});

defineExpose({
    visible,
    show,
    hide,
    toggle,
});
</script>

<template>
    <div ref="element" :class="$cn()">
        <slot v-bind="{ triggerClassName, active: visible }" />
        <div v-show="visible" ref="popup" :class="$cn('popup', { visible })">
            <slot name="popup" />
        </div>
    </div>
</template>

<style scoped lang="scss">
.ui-popup {
    position: relative;
    display: flex;

    &__popup {
        width: 100%;

        &--active {
            z-index: 1;
        }
    }
}
</style>
