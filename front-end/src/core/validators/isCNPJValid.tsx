const isCNPJValid = (cnpj: string): boolean => {
    // Remover caracteres não numéricos
    cnpj = cnpj.replace(/\D/g, '');

    // Verificar se o CNPJ tem 14 dígitos
    if (cnpj.length !== 14) return false;

    // Verificar se todos os dígitos são iguais (ex: 00.000.000/0000-00)
    if (/^(\d)\1{13}$/.test(cnpj)) return false;

    // Calcular o primeiro dígito verificador
    let sum = 0;
    let weight = 5;
    for (let i = 0; i < 12; i++) {
        sum += parseInt(cnpj.charAt(i)) * weight;
        weight = weight === 2 ? 9 : weight - 1;
    }
    let mod = sum % 11;
    let digit = mod < 2 ? 0 : 11 - mod;

    if (parseInt(cnpj.charAt(12)) !== digit) return false;

    // Calcular o segundo dígito verificador
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
