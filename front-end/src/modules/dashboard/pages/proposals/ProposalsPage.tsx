import React, { useState } from 'react'
import { Outlet } from 'react-router-dom'
import Search from '../../../../core/components/Search'
import ProposalsItem from './components/ProposalsItem'
import ProposalCreate from './components/ProposalCreate'

const ProposalsPage = () => {
  const [isModalOpen, setIsModalOpen] = useState(false); // Estado para controlar a visibilidade do modal

    // Função para abrir o modal
    const handleOpenModal = () => {
        console.log("modal aberto");
      setIsModalOpen(true);
    };

    const handleCloseModal = () => {
        console.log("modal fechado");
        setIsModalOpen(false);
      };
  
  return (
    <>
      <section className=''>
       

          <div className="flex shrink-0 px-8 items-center justify-between h-[96px]">
            <div>

            </div>
            <div>
              
              <button className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded flex" onClick={handleOpenModal}>
                {/* <i><svg xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24" strokeWidth={1.5} stroke="currentColor" className="w-6 h-6 ">
                  <path strokeLinecap="round" strokeLinejoin="round" d="M12 9v6m3-3H9m12 0a9 9 0 1 1-18 0 9 9 0 0 1 18 0Z" />
                  </svg>
                </i>  */}
                Create a new proposal
              </button>
            </div>
          </div>

        <Search placeholder='CPF'/>
        

        <div className='relative pl-3 my-5' style={{ maxHeight: 'calc(100vh - 15rem)', overflowY: 'auto' }}>
          <div className='flex flex-col w-full font-medium'>
            <ProposalsItem/>
            <ProposalsItem/>
            <ProposalsItem/>
            <ProposalsItem/>
            <ProposalsItem/>
            <ProposalsItem/>
          </div>
        </div>

        <ProposalCreate isOpen={isModalOpen} onClose={() => handleCloseModal()} />
      </section>
    </>
  )
}

export default ProposalsPage