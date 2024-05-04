import React, { useContext, useState } from 'react'
import { Link, useNavigate } from 'react-router-dom';
import { SessionContext } from '../../../../core/contexts/SessionContext';

const SignUpPage = () => {
    const { privateKey, version, sessionId, setToken } = useContext(SessionContext);
    const [firstName, setFirstname] = useState("");
    const [lastName, setLasName]  = useState("");
    const [email, setEmail]  = useState("");
    const [password, setPassword]  = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");
    const [cnpj, setCNPJ]  = useState("");
    const navigate = useNavigate();

    const handleSubmit = (event: any) => {
        event.preventDefault();

        const data = JSON.stringify(
          { name : firstName + " " + lastName, 
            email: email,
            password: password,
            cnpj: cnpj
          }
        );

        console.log(data);
        const headers = new Headers();
        headers.append('Content-Type', 'application/json'); 
        headers.append('sessionId', sessionId);

        const requestOptions: RequestInit = {
          method: 'POST',
          headers: headers,
          body: data,
          redirect: 'follow'
        };
      
        fetch("https://localhost:7222/authentication/sign-up", requestOptions)
          .then(response => response.json())
          .then(result => {

            setToken(result.token);
            console.log(result);
            sessionStorage.setItem("name", result.name);
            sessionStorage.setItem("token", result.token);
            sessionStorage.setItem("role", result.role);
            sessionStorage.setItem("store", result.store);

            navigate("/dashboard");

          })
          .catch(error => console.log('error', error));
    
      }
    
    
      return (
        <section className="bg-gray-50 dark:bg-gray-900">
          <div>
          <div className="flex flex-col items-center justify-center px-6 py-8 mx-auto md:h-screen lg:py-0">
            <div className="w-full max-w-2xl bg-white rounded-lg shadow dark:border md:mt-0 sm:max-w-md xl:p-0 dark:bg-gray-800 dark:border-gray-700">
              <div className="p-2 space-y-4 md:space-y-6 sm:p-8">
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
                                <input type="cnpj" name="cnpj" id="cnpj" onChange={(e) => {setCNPJ(e.target.value)}} className="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder="name@company.com" required/>
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

                        <button type="submit" className="w-full text-white bg-blue-500 hover:bg-blue-700 focus:ring-4 focus:outline-none focus:ring-primary-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center dark:bg-primary-600 dark:hover:bg-primary-700 dark:focus:ring-primary-800 mb-4">Sign in</button>
                        <Link className='text-sm text-slate-200' to="/authentication/login"> ← Back to login</Link>
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