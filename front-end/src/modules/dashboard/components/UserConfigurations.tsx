import React, { useEffect, useState } from 'react' 
import { Proposal } from '../pages/proposals/models/Proposa';
import { phoneMask } from '../../../core/masks/phoneMasks';
import { cpfMask } from '../../../core/masks/cpfMasks'; 
import { useNavigate } from 'react-router-dom';
import Alert from '../../../core/components/Alert';


interface ModalProps {
    isOpen: boolean; 
    onClose: () => void; 
    name: string;
    setName: React.Dispatch<React.SetStateAction<string>>
  }

const UserConfigurations: React.FC<ModalProps> = ({ isOpen, onClose, name, setName }) => {

    
    const handleClose = () => {
      onClose();
    }
    const [tempName, setTempName] = useState(name);
    const [sessionId, setSessionId] = useState("");
    const [role, setRole] = useState("");
    const [store, setStore] = useState("");
    const [submited, setSubmited] = useState(false);
    const [error, setError] = useState<string | null>(null);
    const navigate = useNavigate();

    useEffect(() => {
        let timer: NodeJS.Timeout;
        if (error) {
            timer = setTimeout(() => {
                setError(null);
            }, 5000);
        }
        return () => clearTimeout(timer);
    }, [error]);

    useEffect(() => {
        setSessionId(sessionStorage.getItem("sessionId"));
        setRole(sessionStorage.getItem("role"));
        setStore(sessionStorage.getItem("store"));
    }, []);

    const handleUpdate = () => {
        setSubmited(true);

        const headers = new Headers();
        headers.append('Content-Type', 'application/json'); 
        headers.append('Authorization', sessionStorage.getItem("token"));

        const data = JSON.stringify({ Name: name });

        const requestOptions: RequestInit = {
            method: 'PATCH',
            headers: headers,
            body: data,
            redirect: 'follow'
        };

        fetch(`${process.env.REACT_APP_BFF_API}retailers`, requestOptions)
        .then(response => {
            if (response.status === 200) {
                sessionStorage.setItem("name", tempName);
                setName(tempName);
            } else {
                setTempName(name);
                setError("Unauthorized access.");
            }
            return response.json();
        })
        .catch(error => console.log('error', error))
        .finally(() => {
            setSubmited(false);
            onClose();
        });
    };

    const handleDelete = () => {
        setSubmited(true);

        const headers = new Headers();
        headers.append('Content-Type', 'application/json'); 
        headers.append('Authorization', sessionStorage.getItem("token"));

        const requestOptions: RequestInit = {
            method: 'DELETE',
            headers: headers,
            redirect: 'follow'
        };
                
        fetch(`${process.env.REACT_APP_BFF_API}retailers`, requestOptions)
        .then(response => {
            if (response.status === 200) {
                sessionStorage.clear();
                navigate("/authentication/login");
            } else if (response.status === 401) {
                setError("Unauthorized access.");
            }
            return response.json();
        })
        .catch(error => console.log('error', error))
        .finally(() => {
            setSubmited(false);
            onClose();
        });
    };

    if (!isOpen) return null;

    return (
      <div className="fixed inset-0 z-50 flex items-center justify-center overflow-x-hidden overflow-y-auto outline-none focus:outline-none">
          <div className="relative  max-w-3xl mx-auto my-6">
          {error && (
            <div className='w-full  md:mt-0 sm:max-w-md xl:p-0'>
              <Alert message={error} setError={setError} type="red" />
            </div>
          )}
            <div className="relative flex flex-col w-full h-full bg-slate-800 border-0 rounded-lg shadow-lg outline-none focus:outline-none">
              <div className="flex items-start justify-between p-5 border-b border-solid border-gray-300 rounded-t">
                <h3 className="text-3xl font-semibold text-white">
                  Profile
                </h3>
                <button
                  className="p-1 ml-auto bg-transparent border-0 text-black float-right text-3xl leading-none font-semibold outline-none focus:outline-none"
                  onClick={handleClose}
                >
                  <span className="bg-transparent text-black h-6 w-6 text-2xl block outline-none focus:outline-none">×</span>
                </button>
              </div>
              <div className="relative p-6 flex-auto text-white flex gap-4">

                <div id='profile'>
                  <div className=''>
                    <div className='text-left mb-4'>
                      <label htmlFor="name" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Full Name</label>
                      <input type="text" name="name" id="name" onChange={(e) => {setTempName(e.target.value)}} className="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" value={tempName}/>
                    </div>
                    
                </div>
                <div className='flex gap-4'>
                    <div className='text-left mb-4'>
                        <label htmlFor="role" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Role</label>
                        <input type="text" name="role" id="role" onChange={(e) => {setRole(e.target.value)}}  className="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" value={role} disabled/>
                    </div>

                    <div className='text-left'>
                        <label htmlFor="store" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Store</label>
                        <input type="text" name="store" id="store" onChange={(e) => {setStore(e.target.value)}} className="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" value={store} disabled/>
                    </div>
                </div>
                </div>
               
              </div>

              <div className="flex shrink-0 px-8 items-center justify-between h-[96px]">
            
                <div className='flex gap-4 '>
                    <button onClick={handleUpdate} className="bg-blue-500 hover:bg-blue-700 text-white font-bold py-2 px-4 rounded flex" >
                        Update profile
                    </button>

                    <button onClick={handleDelete} className="bg-red-500 hover:bg-red-700 text-white font-bold py-2 px-4 rounded flex" >
                        Delete profile
                    </button>
                </div>
          </div>
            </div>
          </div>
        </div>

    );
  };

export default UserConfigurations