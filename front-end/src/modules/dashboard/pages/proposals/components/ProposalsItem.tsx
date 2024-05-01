import React, { useState } from 'react'
import ProposalDetail from './ProposalDetail';

const ProposalsItem = () => {

    const [isModalOpen, setIsModalOpen] = useState(false); 

    const handleOpenModal = () => {
      setIsModalOpen(true);
    };

    const handleCloseModal = () => {
        setIsModalOpen(false);
      };

  return (
    <div className='pt-5 pb-[.15rem] flex'>
        <div className='flex flex-none bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-4 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500'>
            <div className='text-white '>
                <p className='text-xs mb-2'>Código: <span>MSS8J2F4G9H1K5</span></p>
                <span className='flex text-sm mb-2'>142.700.667-92 - Fernando Calheiros Areias</span>
                <p className='text-sm'>Product:<span>Mastercard</span></p>
            </div>

            <div className="flex-grow flex items-center justify-center"> 
                <div className="flex flex-col items-center"> 
                <span className="mb-1">Analista: Helene Tavares Vieira</span>  
                <span className="mb-2">Approved</span>
                <span className="mb-2">23/04/2024 20:56:00</span>
                </div>
            </div>

            <div className="flex flex-none justify-end items-center"> 
                <button className='btn p-3 text-white' onClick={handleOpenModal}>
                <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" strokeWidth={1.5} stroke="currentColor" className="w-6 h-6">
                    <path strokeLinecap="round" strokeLinejoin="round" d="m21 21-5.197-5.197m0 0A7.5 7.5 0 1 0 5.196 5.196a7.5 7.5 0 0 0 10.607 10.607Z" />
                </svg>
                </button>
            </div>
            <ProposalDetail isOpen={isModalOpen} onClose={() => handleCloseModal()} />
        </div>
    </div>
  )
}

export default ProposalsItem