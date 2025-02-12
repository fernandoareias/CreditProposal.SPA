import React, { useContext, useEffect, useState } from 'react'
import { Link, useNavigate } from 'react-router-dom';
import { SessionContext } from '../../../../core/contexts/SessionContext';
import JSEncrypt from 'jsencrypt';

import CryptoJS from 'crypto-js';
import Alert from '../../../../core/components/Alert';
import Button from '../../../../core/components/Button';


const LoginPage = () => {
  const { privateKey, version, sessionId, setToken } = useContext(SessionContext);
  const [email, setEmail] = useState(""); 
  const [password, setPassword] = useState("");
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

  const handleSubmit = (event: any) => {
    event.preventDefault();
    setSubmited(true);
    
    const encrypt = new JSEncrypt(); 
    encrypt.setPrivateKey(privateKey);

    const valorAssinado = sessionId + email + password;
    const data = JSON.stringify({ email : email, password: password});
    const signature = encrypt.sign(valorAssinado, CryptoJS.SHA256, "sha256"); 
    

    const headers = new Headers();
    headers.append('Content-Type', 'application/json');
     headers.append('Authorization', `Bearer ${signature}`);
    headers.append('sessionId', sessionId);

    const requestOptions: RequestInit = {
      method: 'POST',
      headers: headers,
      body: data,
      redirect: 'follow'
  };
  
  fetch(`${process.env.REACT_APP_BFF_API}authentication/sign-in`, requestOptions)
  .then(response => {

    if (!response.ok) {
      if (response.status === 401) {
        throw new Error("Unauthorized");
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
  .catch(error =>{
    setError(error.message); 
    console.error('Fetch error:', error);
  }).finally(() => {
    setSubmited(false);
  });
  }


  return (
    <section className="bg-gray-50 dark:bg-gray-900">
      <div>
      <div className="flex flex-col items-center justify-center px-6 py-8 mx-auto md:h-screen lg:py-0">
          {error && (
            <div className='w-full  md:mt-0 sm:max-w-md xl:p-0'>
              <Alert message={error} setError={setError} type="red" />
            </div>
          )}
          <div className="w-full bg-white rounded-lg shadow dark:border md:mt-0 sm:max-w-md xl:p-0 dark:bg-gray-800 dark:border-gray-700">
              <div className="p-6 space-y-4 md:space-y-6 sm:p-8">
                  <h1 className="text-xl font-bold leading-tight tracking-tight text-gray-900 md:text-2xl dark:text-white">
                      Sign in to your account
                  </h1>
                  <form className="space-y-4 md:space-y-6" onSubmit={(e) => handleSubmit(e)}>
                      <div className='text-left'>
                          <label htmlFor="email" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Email</label>
                          <input type="email" name="email" id="email" onChange={(e) => setEmail(e.target.value)} className="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" placeholder="name@company.com" required/>
                      </div>
                      <div className='text-left'>
                          <label htmlFor="password" className="block mb-2 text-sm font-medium text-gray-900 dark:text-white">Password</label>
                          <input type="password" name="password" id="password" onChange={(e) => {setPassword(e.target.value)}} placeholder="••••••••" className="bg-gray-50 border border-gray-300 text-gray-900 sm:text-sm rounded-lg focus:ring-primary-600 focus:border-primary-600 block w-full p-2.5 dark:bg-gray-700 dark:border-gray-600 dark:placeholder-gray-400 dark:text-white dark:focus:ring-blue-500 dark:focus:border-blue-500" required/>
                      </div>
                      <div className="flex items-center justify-between">
                          <div className="flex items-start">
                              <div className="flex items-center h-5">
                                <input id="remember" aria-describedby="remember" type="checkbox" className="w-4 h-4 border border-gray-300 rounded bg-gray-50 focus:ring-3 focus:ring-primary-300 dark:bg-gray-700 dark:border-gray-600 dark:focus:ring-primary-600 dark:ring-offset-gray-800"/>
                              </div>
                              <div className="ml-3 text-sm">
                                <label htmlFor="remember" className="text-gray-500 dark:text-gray-300">Remember me</label>
                              </div>
                          </div>
                          <Link className="text-sm font-medium text-primary-600  text-white" to="/authentication/recovery">Forgot password?</Link>
                      </div>
                      <Button loading={submited} disabled={submited}>
                        Sign in
                      </Button>
                      <p className="text-sm font-light text-gray-500 dark:text-gray-400">
                          Don’t have an account yet? <Link className="font-medium text-primary-600 text-white" to="/authentication/signup ">Sign up</Link>
                      </p>
                  </form>
              </div>
          </div>
        
        <div className='flex flex-col items-center justify-center px-1 py-1 mx-auto pt-3'> 
          <span className='text-sm text-slate-500'>v{version}</span>
        </div>
      </div>

      </div>
    </section>
  )
}

export default LoginPage