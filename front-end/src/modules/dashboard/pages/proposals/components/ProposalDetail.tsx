import React from 'react'

interface ModalProps {
    isOpen: boolean; // Defina o tipo de isOpen como boolean
    onClose: () => void; // Função de fechar o modal
  }

const ProposalDetail: React.FC<ModalProps> = ({ isOpen, onClose }) => {
    if (!isOpen) return null;
  
    return (
      <div className="fixed inset-0 z-50 flex items-center justify-center overflow-x-hidden overflow-y-auto outline-none focus:outline-none">
        <div className="relative w-auto max-w-3xl mx-auto my-6">
          {/* Conteúdo do modal */}
          <div className="relative flex flex-col w-full bg-white border-0 rounded-lg shadow-lg outline-none focus:outline-none">
            {/* Cabeçalho do modal */}
            <div className="flex items-start justify-between p-5 border-b border-solid border-gray-300 rounded-t">
              <h3 className="text-3xl font-semibold">
                Modal Title
              </h3>
              <button
                className="p-1 ml-auto bg-transparent border-0 text-black float-right text-3xl leading-none font-semibold outline-none focus:outline-none"
                onClick={onClose}
              >
                <span className="bg-transparent text-black h-6 w-6 text-2xl block outline-none focus:outline-none">×</span>
              </button>
            </div>
            {/* Corpo do modal */}
            <div className="relative p-6 flex-auto">
              <p>Modal content goes here...</p>
            </div>
            
          </div>
        </div>
        <div className="fixed inset-0 z-40 bg-black opacity-25"></div>
      </div>
    );
  };

export default ProposalDetail