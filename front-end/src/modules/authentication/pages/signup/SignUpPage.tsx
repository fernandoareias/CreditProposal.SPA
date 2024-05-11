import React, { useContext, useState } from 'react'
import { Link, useNavigate } from 'react-router-dom';
import { SessionContext } from '../../../../core/contexts/SessionContext';
import Alert from '../../../../core/components/Alert';
import Button from '../../../../core/components/Button';
import { cnpjMask, removeCNPJMask } from '../../../../core/masks/cnpjMask';
import { isCNPJValid } from '../../../../core/validators/isCNPJValid';

const SignUpPage = () => {
    const { privateKey, version, sessionId, setToken } = useContext(SessionContext);
    const [firstName, setFirstname] = useState("");
    const [lastName, setLasName]  = useState("");
    const [email, setEmail]  = useState("");
    const [password, setPassword]  = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");
    const [cnpj, setCNPJ]  = useState("");
    const [submited, setSubmited] = useState(false);
    const [error, setError] = useState<string | null>(null);
    const navigate = useNavigate();

    const handleSubmit = (event: any) => {
        event.preventDefault();
        setSubmited(true);

        if(password !== confirmPassword){
          setError("Passwords do not match"); 
          setSubmited(false);
          return null;
        }

        if(!isCNPJValid(cnpj))
        {
          setError("CNPJ invalid");
          setSubmited(false);
          return null;
        }

        const data = JSON.stringify(
          { name : firstName + " " + lastName, 
            email: email,
            password: password,
            cnpj: removeCNPJMask(cnpj)
          }
        );

        const headers = new Headers();
        headers.append('Content-Type', 'application/json'); 
        headers.append('sessionId', sessionId);

        const requestOptions: RequestInit = {
          method: 'POST',
          headers: headers,
          body: data,
          redirect: 'follow'
        };
      
        fetch(`${process.env.REACT_APP_BFF_API}authentication/sign-up`, requestOptions)
        .then(response => {
          if (!response.ok) {
            if (response.status === 401) {
              throw new Error("Unauthorized: You are not authorized to access this resource.");
            } else if (response.status === 400) {
              return response.text().then(errorMessage => {
                throw new Error(errorMessage);
              });
            } else {
              throw new Error("Something went wrong. Please try again later.");
            }
          }
      
          return response.json();
        })
        .then(result => {
          setToken(result.token);
          sessionStorage.setItem("name", result.name);
          sessionStorage.setItem("token", result.token);
          sessionStorage.setItem("role", result.role);
          sessionStorage.setItem("store", result.store);
      
          navigate("/dashboard/proposals");
        })
        .catch(error => {
          setError(error.message); 
          console.error('Fetch error:', error);
        })
        .finally(() => {
          setSubmited(false);
        });
    
      }
    
      return (
        <section className="bg-gray-50 dark:bg-gray-900">
          <div>
          <div className="flex flex-col items-center justify-center px-6 py-8 mx-auto md:h-screen lg:py-0">
          {error && (
            <div className='w-full rounded-lg shadow  md:mt-0 sm:max-w-md xl:p-0'>
              <Alert message={error} setError={setError} type="red" />
            </div>
          )}
            <div className="w-full max-w-2xl bg-white rounded-lg shadow dark:border md:mt-0 sm:max-w-md xl:p-0 dark:bg-gray-800 dark:border-gray-700">
              <div className="p-2 space-y-4 md:space-y-6 sm:p-8">
              <Link className='text-sm text-slate-200' to="/authentication/login"> ← Back to login</Link>
                      <h1 className="text-left text-xl font-bold leading-tight tracking-tight text-gray-900 md:text-2xl dark:text-white">
                          Sign Up
                      </h1>
                      <form className='' onSubmit={(e) => handleSubmit(e)}>
                        
                        <div className='grid grid-cols-2 gap-4 mb-3'>
                            <div className='text-left flex-12'>
                              <label htmlFor="name" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">First name</label>
                              <input type="text" name="name" id="first-name" onChange={(e) => {setFirstname(e.target.value)}} className="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder="Your first name" required/>
                            </div>

                            <div className='text-left flex-12'>
                              <label htmlFor="name" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Last name</label>
                              <input type="text" name="name" id="last-name" onChange={(e) => {setLasName(e.target.value)}} className="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder="Your first name" required/>
                            </div>
                        </div>

                        <div className='mb-4'>
                            <div className='text-left'>
                                <label htmlFor="email" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Email</label>
                                <input type="email" name="email" id="email" onChange={(e) => {setEmail(e.target.value)}} className="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder="name@company.com" required/>
                            </div>
                        </div>
                        <div className='mb-4'>
                            <div className='text-left'>
                                <label htmlFor="cnpj" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">CNPJ</label>
                                <input type="text" name="cnpj" id="cnpj" onChange={(e) => setCNPJ(cnpjMask(e.target.value))} value={cnpj} className="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder="12.345.678/0001-90" required/>
                            </div>
                        </div>
                        <div className=''>
                            <div className='text-left mb-4'>
                                <label htmlFor="password" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Password</label>
                                <input type="password" name="password" id="password" onChange={(e) => {setPassword(e.target.value)}} placeholder="••••••••" className="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" required/>
                            </div>

                            <div className='text-left mb-16'>
                                <label htmlFor="confirm-password" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Confirm password</label>
                                <input type="password" name="confirm-password" id="confirm-password" onChange={(e) => {setConfirmPassword(e.target.value)}} placeholder="••••••••" className="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" required/>
                            </div>
                        </div>

                        <Button loading={submited} disabled={submited}>
                          Sign in
                        </Button>
                      </form>
                       
                  </div>
              </div>
            
            <div className='flex flex-col items-center justify-center px-1 py-1 mx-auto pt-3'> 
              <span className='text-sm text-slate-500'>v{version}</span>
            </div>
          </div>
    
          </div>
        </section>
      );
}

export default SignUpPage