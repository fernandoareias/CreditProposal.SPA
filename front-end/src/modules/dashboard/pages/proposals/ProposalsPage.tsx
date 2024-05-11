import React, { useContext, useEffect, useState } from 'react'
import { Outlet } from 'react-router-dom'
import Search from '../../../../core/components/Search'
import ProposalsItem from './components/ProposalsItem'
import ProposalCreate from './components/ProposalCreate'
import { ProposalsContext } from '../../contexts/PropostaContext'

const ProposalsPage = () => {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const { proposals, setProposals } = useContext(ProposalsContext);
  const [displayProposals, setDisplayProposals] = useState([]);

  useEffect(() => {
    setDisplayProposals(proposals);
  }, [proposals]);
  
  const handleOpenModal = () => {
    setIsModalOpen(true);
  };

  const handleCloseModal = () => {
      setIsModalOpen(false);
    };
  
    const submitSearchProposal = (query: string) => {
      const filteredProposals = displayProposals.filter(proposal => proposal.cpf.includes(query));

      if(filteredProposals.length > 0)
      {
        setDisplayProposals(filteredProposals);
        return;
      }
      

    }


    const searchProposal = (query: string) => { 
      const filteredProposals = displayProposals.filter(proposal => proposal.cpf.includes(query));
      setDisplayProposals(filteredProposals);
    };
  
    const clearSearch = () => {
      setDisplayProposals(proposals);
    };

  return (
    <>
      <section className=''>
          <div className="flex shrink-0 px-8 items-center justify-between h-[96px]">
            <div>

            </div>
            <div>
              
              <button className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded flex" onClick={handleOpenModal}>
                Create a new proposal
              </button>
            </div>
          </div>
        <Search placeholder='CPF' search={searchProposal} clear={clearSearch} searchSubimit={submitSearchProposal}/>
        <div className='relative pl-3 my-5' style={{ maxHeight: 'calc(100vh - 15rem)', overflowY: 'auto' }}>
          <div className='flex flex-col w-full font-medium'>
          {displayProposals.length > 0 ? (
            displayProposals.map((displayProposals, index) => (
              <ProposalsItem key={index} proposal={displayProposals} />
            ))
          ) : (
            <p className="text-white text-center">Nenhuma proposta encontrada.</p>
          )}
          </div>
        </div>
        <ProposalCreate isOpen={isModalOpen} onClose={() => handleCloseModal()} />
      </section>
    </>
  )
}

export default ProposalsPage