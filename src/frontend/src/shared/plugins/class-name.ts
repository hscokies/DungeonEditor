import { type App, getCurrentInstance } from 'vue';
import { toKebabCase } from '@/shared/utils/string.ts';
import { getFileNameWithoutExtension } from '@/shared/utils/file.ts';

export type ClassNameModifiers = Record<string, string | number | boolean>;

function className(blockName: string, modifiers: ClassNameModifiers): string[];
function className(blockName: string, elementName?: string, modifiers?: ClassNameModifiers): string[];
function className(blockName: string, ...args: unknown[]): string[];

function className(blockName: string, ...args: unknown[]): string[] {
    blockName = toKebabCase(blockName);

    const [arg1, arg2] = args;
    if (!arg1) return [blockName];

    let elementName: string | undefined;
    let modifiers: ClassNameModifiers | undefined;

    if (typeof arg1 === 'string') {
        // className(blockName: string, elementName: string | undefined, modifiers: ClassNameModifiers)
        elementName = toKebabCase(arg1);
        modifiers = arg2 as ClassNameModifiers;
    } else {
        // className(blockName: string, modifiers: ClassNameModifiers)
        modifiers = arg1 as ClassNameModifiers;
    }

    const classNames: string[] = [];

    const baseName = elementName ? `${blockName}__${elementName}` : blockName;
    classNames.push(baseName);

    if (modifiers) {
        for (const [key, value] of Object.entries(modifiers)) {
            let className = baseName;
            const modifierName = toKebabCase(key);

            if (typeof value === 'boolean') {
                if (!value) continue;
                className += `--${modifierName}`;
            } else {
                className += `--${modifierName}-${value}`;
            }

            classNames.push(className);
        }
    }

    return classNames;
}

function getComponentName() {
    const fallbackComponentName = 'default-component';

    const instance = getCurrentInstance();
    if (!instance) {
        return fallbackComponentName;
    }

    let name = instance.type.name ?? getFileNameWithoutExtension(instance.type.__file);

    return name ? toKebabCase(name) : fallbackComponentName;
}

export const classNamePlugin = {
    install(app: App) {
        app.config.globalProperties.$cn = (...args: unknown[]) => {
            return className(getComponentName(), ...args);
        };
    },
};
