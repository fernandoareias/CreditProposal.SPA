const isCPFValid = (cpf: string): boolean => {
    // Remover caracteres não numéricos
    cpf = cpf.replace(/\D/g, '');

    // Verificar se o CPF tem 11 dígitos
    if (cpf.length !== 11) return false;

    // Verificar se todos os dígitos são iguais (ex: 000.000.000-00)
    if (/^(\d)\1{10}$/.test(cpf)) return false;

    // Calcular o primeiro dígito verificador
    let sum = 0;
    for (let i = 0; i < 9; i++) {
        sum += parseInt(cpf.charAt(i)) * (10 - i);
    }
    let mod = sum % 11;
    let digit = mod < 2 ? 0 : 11 - mod;

    if (parseInt(cpf.charAt(9)) !== digit) return false;

    // Calcular o segundo dígito verificador
    sum = 0;
    for (let i = 0; i < 10; i++) {
        sum += parseInt(cpf.charAt(i)) * (11 - i);
    }
    mod = sum % 11;
    digit = mod < 2 ? 0 : 11 - mod;

    if (parseInt(cpf.charAt(10)) !== digit) return false;

    return true;
};
