import React, { useState, useEffect } from 'react';

interface AlertProps {
  message: string;
  type: string;
  setError: React.Dispatch<React.SetStateAction<string | null>>;
}

const Alert: React.FC<AlertProps> = ({ message, type, setError }) => {
  const [show, setShow] = useState<boolean>(true);

  useEffect(() => {
    const timer = setTimeout(() => {
      setShow(false);
      setError(null);
    }, 5000);

    return () => clearTimeout(timer);
  }, [setError]);

  if (!show) return null;

  return (
    <div className={`bg-${type}-100 border border-${type}-400 text-${type}-700 px-4 py-3 rounded relative flex justify-between items-center mb-2 max-w-full bg-red-500 text-white`} role="alert">
        <span className="block sm:inline text-center">{message}</span>
        <span className="cursor-pointer" onClick={() => setShow(false)}>
            <svg className="fill-current h-6 w-6 text-red-500" role="button" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 20 20"><title>Close</title><path d="M14.348 14.849a1.2 1.2 0 0 1-1.697 0L10 11.819l-2.651 3.029a1.2 1.2 0 1 1-1.697-1.697l2.758-3.15-2.759-3.152a1.2 1.2 0 1 1 1.697-1.697L10 8.183l2.651-3.031a1.2 1.2 0 1 1 1.697 1.697l-2.758 3.152 2.758 3.15a1.2 1.2 0 0 1 0 1.698z"/></svg>
        </span>
    </div>
    
  );
};

export default Alert;
