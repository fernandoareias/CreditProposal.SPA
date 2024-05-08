import React, { useState } from 'react'
import { Proposal } from '../models/Proposa';
import ProposalDetail from './ProposalDetail';
import { cpfMask } from '../../../../../core/masks/cpfMasks';



interface ProposalsItemProps {
    proposal: Proposal
  }

const ProposalsItem: React.FC<ProposalsItemProps> = ({ proposal }) => {

    const [isModalOpen, setIsModalOpen] = useState(false); 

    const handleOpenModal = () => {
      setIsModalOpen(true);
    };

    const handleCloseModal = () => {
        setIsModalOpen(false);
      };

      return (
        <div className='pt-5 pb-[.15rem] flex'>
            <div className='flex bg-gray-50 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-4 dark:bg-gray-700 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500'>
                <div className='w-2/3 pr-4'>
                    <div className='text-white '>
                        <p className='text-xs mb-1'>Code: <span>{proposal.code}</span></p>
                        <span className='flex text-sm mb-1'>{cpfMask(proposal.cpf)} - {proposal.fullname}</span>
                        <p className='text-sm mb-1'>Product:<span>{proposal.product}</span></p>
                    </div>
                </div>
    
                <div className="w-3/4 pr-4">
                    <div className="flex flex-col items-center justify-center">
                        <div className="mb-1 text-center"> 
                            <span>Analyst: {proposal.analyst}</span>  
                        </div>
                        <div className="mb-1 text-center"> 
                            <span>Status: {proposal.status}</span>
                        </div>
                        <div className="mb-1 text-center"> 
                            <span>Created at: {proposal.created_at}</span>
                        </div>
                    </div>
                </div>
    
                <div className="w-1/4">
                    <div className="flex justify-end items-center"> 
                        <button className='btn p-3 text-white' onClick={handleOpenModal}>
                            <svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" strokeWidth={1.5} stroke="currentColor" className="w-6 h-6">
                                <path strokeLinecap="round" strokeLinejoin="round" d="m21 21-5.197-5.197m0 0A7.5 7.5 0 1 0 5.196 5.196a7.5 7.5 0 0 0 10.607 10.607Z" />
                            </svg>
                        </button>
                    </div>
                </div>
            </div>
            
            <ProposalDetail proposal={proposal} isOpen={isModalOpen} onClose={() => handleCloseModal()} />
        </div>
    );
    
    
    
}

export default ProposalsItem