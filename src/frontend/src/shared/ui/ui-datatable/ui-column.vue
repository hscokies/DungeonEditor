<script setup lang="ts">
import { type DatatableColumn, Injections, type RegisterColumn } from '@/shared/ui/ui-datatable/ui-datatable.types.ts';
import { inject, onMounted } from 'vue';

const { field, header } = defineProps<DatatableColumn>();
const register = inject<RegisterColumn>(Injections.RegisterColumn);

const slots = defineSlots<{
    default(props: { rowKey: string }): any;
}>();

onMounted(() => {
    if (register) {
        register({ field, header, slots });
    }
});
</script>

<template />
