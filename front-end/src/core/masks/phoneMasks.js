export const phoneMask = (value) => {
    return value
      .replace(/\D/g, '') // Remove any non-numeric characters
      .replace(/(\d{2})(\d)/, '($1) $2') // Captures the first two digits and adds parentheses
      .replace(/(\d{5})(\d)/, '$1-$2') // Captures the next five digits and adds a hyphen
      .replace(/(-\d{4})\d+?$/, '$1'); // Limits the total number of characters and adds a hyphen
  };
  
  export const removePhoneMask = (value) => {
    return value.replace(/\D/g, ''); // Removes all non-numeric characters
  };