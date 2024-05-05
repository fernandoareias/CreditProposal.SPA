import React from 'react'
import { Proposal } from '../models/Proposa';

interface ModalProps {
    proposal: Proposal;
    isOpen: boolean; // Defina o tipo de isOpen como boolean
    onClose: () => void; // Função de fechar o modal
  }

const ProposalDetail: React.FC<ModalProps> = ({ proposal, isOpen, onClose }) => {
    if (!isOpen) return null;

    const handleClose = () => {
      //console.log("dentro do details, clicou para fechar");
      onClose();
    }

    return (
      <div className="fixed inset-0 z-50 flex items-center justify-center overflow-x-hidden overflow-y-auto outline-none focus:outline-none">
          <div className="relative w-full  max-w-3xl mx-auto my-6">
            {/* Conteúdo do modal */}
            <div className="relative flex flex-col w-full h-full bg-slate-800 border-0 rounded-lg shadow-lg outline-none focus:outline-none">
              {/* Cabeçalho do modal */}
              <div className="flex items-start justify-between p-5 border-b border-solid border-gray-300 rounded-t">
                <h3 className="text-3xl font-semibold text-white">
                  {proposal.code}
                </h3>
                <button
                  className="p-1 ml-auto bg-transparent border-0 text-black float-right text-3xl leading-none font-semibold outline-none focus:outline-none"
                  onClick={handleClose}
                >
                  <span className="bg-transparent text-black h-6 w-6 text-2xl block outline-none focus:outline-none">×</span>
                </button>
              </div>
              {/* Corpo do modal */}
              <div className='relative pl-6 pt-2 flex gap-8'>
                <span>Create at: {proposal.created_at}</span>
                <span >Updated at: {proposal.updated_at}</span>
                <span>Analyst: {proposal.analyst}</span>
              </div>
              <div className="relative p-6 flex-auto text-white flex gap-4">

                <div className='' id='proponent'>
                  <h2 className='mb-6 text-xl'>Proponent</h2>
                  <div>
                    <div className='text-left mb-4'>
                      <label htmlFor="name" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Full Name</label>
                      <input type="text" name="name" id="name" onChange={(e) => {}} className="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder={proposal.fullname} disabled/>
                    </div>
                    <div className='grid grid-cols-2 gap-3'>              
                      <div className='text-left mb-4'>
                        <label htmlFor="cpf" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">CPF</label>
                        <input type="text" name="cpf" id="cpf" onChange={(e) => {}} className="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder={proposal.cpf} disabled/>
                      </div>

                      <div className='text-left mb-4'>
                        <label htmlFor="cellphone" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Cellphone</label>
                        <input type="text" name="cellphone" id="cellphone" onChange={(e) => {}} className="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder={proposal.cellphone} disabled/>
                      </div>
                  </div>

                  
                   
                  </div>
                </div>
                <div className='' id='proposal'>
                  <h2 className='mb-6 text-xl'>Proposal</h2>
                  <div className=''>
                     <div className='grid grid-cols-2 gap-4'>
                        <div className='text-left mb-4'>
                          <label htmlFor="status" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Status</label>
                          <input type="text" name="status" id="status" onChange={(e) => {}} className="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder={proposal.status} disabled/>
                        </div>

                        <div className='text-left mb-4'>
                        <label htmlFor="creditlimit" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Credit limit</label>
                        <input type="text" name="creditlimit" id="creditlimit" onChange={(e) => {}} className="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder={proposal.creadit_limit} disabled/>
                      </div>
                      
                     </div>

                     <div className='text-left mb-4'>
                        <label htmlFor="product" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Product</label>
                        <input type="text" name="product" id="product" onChange={(e) => {}} className="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder={proposal.product} disabled/>
                      </div> 
                      
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>

    );
  };

export default ProposalDetail