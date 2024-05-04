import React, { useContext, useState } from 'react'
import { SessionContext, generateGUID } from '../../../../../core/contexts/SessionContext';



interface ModalProps {
    isOpen: boolean; // Defina o tipo de isOpen como boolean
    onClose: () => void; // Função de fechar o modal
  }

const ProposalCreate : React.FC<ModalProps> = ({ isOpen, onClose }) => {
    const { privateKey, version, sessionId, setToken } = useContext(SessionContext);
    const [fullName, setFullName] = useState('');
    const [cpf, setCPF]  = useState('');
    const [cellphone, setCellphone] = useState('');
    const [produtoSelecionado, setProdutoSelecionado] = useState('');
    

    const handleChangeProduto = (event: any) => {
        
    };


    const handleClose = () => {
      //console.log("dentro do details, clicou para fechar");
      onClose();
    }

    const handleSubmit = (event: any) => {
        event.preventDefault();

        event.preventDefault();

        const data = JSON.stringify(
          { name : fullName, 
            cpf: cpf,
            cnpj: sessionStorage.getItem('store'),
            ddd: cellphone.substring(0, 2),
            cellphone: cellphone.substring(2)
          }
        );

        console.log(data);
        const headers = new Headers();
        headers.append('Content-Type', 'application/json'); 
        headers.append('sessionId', sessionId);
        headers.append('correlation-id', generateGUID());

        const requestOptions: RequestInit = {
          method: 'POST',
          headers: headers,
          body: data,
          redirect: 'follow'
        };
      
        fetch("https://localhost:7222/proposals", requestOptions)
          .then(response => response.json())
          .then(result => {
            console.log(result);
            onClose();
          })
          .catch(error => console.log('error', error)); 
    };

    return isOpen ? (
        <div className="fixed inset-0 z-50 flex items-center justify-center overflow-x-hidden overflow-y-auto outline-none focus:outline-none">
            <div className="relative  max-w-3xl mx-auto my-6">
              {/* Conteúdo do modal */}
              <div className="relative flex flex-col w-full h-full bg-slate-800 border-0 rounded-lg shadow-lg outline-none focus:outline-none">
                {/* Cabeçalho do modal */}
                <div className="flex items-start justify-between p-5 border-b border-solid border-gray-300 rounded-t">
                  <h3 className="text-3xl font-semibold text-white">
                    Create Proposal
                  </h3>
                  <button
                    className="p-1 ml-auto bg-transparent border-0 text-black float-right text-3xl leading-none font-semibold outline-none focus:outline-none"
                    onClick={handleClose}
                  >
                    <span className="bg-transparent text-black h-6 w-6 text-2xl block outline-none focus:outline-none">×</span>
                  </button>
                </div>
                {/* Corpo do modal */}
                <div className="relative p-6 flex-auto text-white flex gap-4">
                    <form onSubmit={(e) => handleSubmit(e)}>
                        <div className='text-left mb-4'>
                            <label htmlFor="name" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Full Name</label>
                            <input type="text" name="name" id="name" onChange={(e) => setFullName(e.target.value)} className="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder="Your full name" required/>
                        </div>

                        <div className='grid grid-cols-2 gap-6'>
                            <div className='text-left mb-4'>
                                <label htmlFor="cpf" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">CPF</label>
                                <input type="text" name="cpf" id="cpf" onChange={(e) => setCPF(e.target.value)} className="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder="Your cpf" required/>
                                </div>

                                <div className='text-left mb-4'>
                                <label htmlFor="cellphone" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Cellphone</label>
                                <input type="numb" name="cellphone" id="cellphone" onChange={(e) => setCellphone(e.target.value)} className="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder="Your cellphone" required/>
                                </div>
                        </div>

                        <div className='text-left mb-4'>
                            <label htmlFor="product" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Product</label>
                            <select
                                id="produtos"
                                name="produtos"
                                className="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500"
                                value={produtoSelecionado}
                                onChange={(e) => setProdutoSelecionado(e.target.value)}
                                required
                            >
                                <option value="" disabled selected>Select a product</option>
                                <option value="produto1">MasterCard</option>
                                <option value="produto2">Visa</option>
                                <option value="produto3">Private Label</option>
                            </select>

                        </div>
                        <div>
                            <button type='submit' className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded w-full">
                                Send
                            </button>
                        </div>

                    </form>
                    
                </div>
              </div>
            </div>
          </div>
  
      ) : null;
}

export default ProposalCreate