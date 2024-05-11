import React, { useState, useEffect } from 'react';

interface SearchProps {
  placeholder: string;
  search: (query: string) => void;
  clear: () => void;
  searchSubimit: (query: any) => void;
}

const Search: React.FC<SearchProps> = ({ placeholder, search, clear, searchSubimit }) => {
  const [query, setQuery] = useState('');

  const handleClear = () => {
    setQuery('');
    clear();
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    searchSubimit(query);
  }

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setQuery(e.target.value);
  };

  useEffect(() => {
    if (query === '') {
      clear();
    }
  }, [query]);

  useEffect(() => {
    if (query.length >= 11) {
      search(query);
    }
  }, [query]);


  return (
    <form onSubmit={handleSubmit}>
      <div className='text-left flex mb-12 relative'>
        <button type="submit" className='btn p-3 text-white'>
          <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" strokeWidth={1.5} stroke="currentColor" className="w-6 h-6">
            <path strokeLinecap="round" strokeLinejoin="round" d="m21 21-5.197-5.197m0 0A7.5 7.5 0 1 0 5.196 5.196a7.5 7.5 0 0 0 10.607 10.607Z" />
          </svg>
        </button>
        <input 
          type="text" 
          name="query" 
          id="query" 
          value={query} 
          onChange={handleInputChange} 
          className="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" 
          placeholder={placeholder} 
          required
        />
        {query && (
          <button type="button" onClick={handleClear} className='btn p-3 text-white absolute right-0 top-0'>
            <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" strokeWidth={1.5} stroke="currentColor" className="w-6 h-6">
              <path strokeLinecap="round" strokeLinejoin="round" d="M6 18L18 6M6 6l12 12" />
            </svg>
          </button>
        )}
      </div>
    </form>
  );
};

export default Search;
