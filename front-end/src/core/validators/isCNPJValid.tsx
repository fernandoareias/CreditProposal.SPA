export const isCNPJValid = (cnpj: string): boolean => {
    cnpj = cnpj.replace(/\D/g, '');

    if (cnpj.length !== 14) return false;

    if (/^(\d)\1{13}$/.test(cnpj)) return false;

    let sum = 0;
    let weight = 5;
    for (let i = 0; i < 12; i++) {
        sum += parseInt(cnpj.charAt(i)) * weight;
        weight = weight === 2 ? 9 : weight - 1;
    }
    let mod = sum % 11;
    let digit = mod < 2 ? 0 : 11 - mod;

    if (parseInt(cnpj.charAt(12)) !== digit) return false;

    sum = 0;
    weight = 6;
    for (let i = 0; i < 13; i++) {
        sum += parseInt(cnpj.charAt(i)) * weight;
        weight = weight === 2 ? 9 : weight - 1;
    }
    mod = sum % 11;
    digit = mod < 2 ? 0 : 11 - mod;

    if (parseInt(cnpj.charAt(13)) !== digit) return false;

    return true;
};
