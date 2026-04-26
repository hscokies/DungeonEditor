export function getEnumKeys<T extends Record<string, string | number>>(enumeration: T): (keyof T)[] {
    return Object.keys(enumeration).filter(key => Number.isNaN(Number(key))) as (keyof T)[];
}
