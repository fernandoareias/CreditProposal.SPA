import React, { useEffect, useState } from 'react';
import logo from './logo.svg';
import './App.css';
import LoginPage from './modules/authentication/pages/login/LoginPage';
import SignUpPage from './modules/authentication/pages/signup/SignUpPage';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import RecoveryPassword from './modules/authentication/pages/recovery-password/RecoveryPassword';
import Dashboard from './modules/dashboard/pages/DashboardPage';
import ProposalsPage from './modules/dashboard/pages/proposals/ProposalsPage';
import NotExistsPage from './core/pages/NotExistsPage';
import CreditCardPage from './modules/dashboard/pages/credit-card/CreditCardPage';
import { SessionContext, generateGUID } from './core/contexts/SessionContext';
import { JSEncrypt } from "jsencrypt";

function App() {

  const [privateKey, setPrivateKey] = useState<string | null>(null);
  const [token, setToken] = useState<string | null>(null);
  const [sessionId, setSessionId] = useState<string | null>(null);
  const [version, setVersion] = useState<string | null>(null);
 
  useEffect(() => {
    const storedSessionId = localStorage.getItem('sessionId');
    if (storedSessionId) {
      setSessionId(storedSessionId);
    } else {
      const newSessionId = generateGUID();
      localStorage.setItem('sessionId', newSessionId);
      setSessionId(newSessionId);
    }
  }, []);

  
  useEffect(() => {
    if (sessionId && privateKey === null) {

      const keySize: string = "2048";

      const crypt = new JSEncrypt({ default_key_size: keySize });

      var PublicPrivateKey = {
        PublicKey: crypt.getPublicKey(),
        PrivateKey:crypt.getPrivateKey()
    };

      var frontpublicKey = PublicPrivateKey.PublicKey;

      var frontprivateKey = PublicPrivateKey.PrivateKey;

      fetch(
        "https://localhost:7222/authentication/session",
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
            sessionId: sessionId,
          },
          body: JSON.stringify({
            "key": frontpublicKey
          }),
        }
      ).then(response => response.json())
      .then(data => {
        const crypt = new JSEncrypt();
        crypt.setPrivateKey(frontprivateKey);
        let chavePrivadaOriginal = '';
        data.privateKey.forEach(blocoCriptografado => {
          const blocoDescriptografado = crypt.decrypt(blocoCriptografado);
          chavePrivadaOriginal += blocoDescriptografado;
      });
       

        setPrivateKey(chavePrivadaOriginal);
        setVersion(data.version);
      })
      .catch(error => console.error('Erro:', error));

    }
  }, [sessionId]);

  const updateToken = (newToken: string) => {
    setToken(newToken);
  };

  return (
    version ?
      <>
      <SessionContext.Provider value={{ privateKey, version, sessionId, token, setToken: updateToken }}>
      <BrowserRouter>
            <Routes>
              <Route path='*' Component={NotExistsPage}/>
              
              <Route path="/authentication/login" Component={LoginPage} />
              <Route path="/authentication/signup" Component={SignUpPage} />
              <Route path='/authentication/recovery' Component={RecoveryPassword}/>

              <Route path='/dashboard' Component={Dashboard}>
                <Route path='proposals' Component={ProposalsPage}/>
                <Route path='credit-cards' Component={CreditCardPage}/>
              </Route>
            </Routes>
        </BrowserRouter>
        </SessionContext.Provider>
      </>
      :
      <>
      <div className="flex items-center justify-center h-screen">
        <div role="status">
          <svg
            aria-hidden="true"
            className="inline w-8 h-8 text-gray-200 animate-spin dark:text-gray-600 fill-blue-600"
            viewBox="0 0 100 101"
            fill="none"
            xmlns="http://www.w3.org/2000/svg"
          >
            <path
              d="M100 50.5908C100 78.2051 77.6142 100.591 50 100.591C22.3858 100.591 0 78.2051 0 50.5908C0 22.9766 22.3858 0.59082 50 0.59082C77.6142 0.59082 100 22.9766 100 50.5908ZM9.08144 50.5908C9.08144 73.1895 27.4013 91.5094 50 91.5094C72.5987 91.5094 90.9186 73.1895 90.9186 50.5908C90.9186 27.9921 72.5987 9.67226 50 9.67226C27.4013 9.67226 9.08144 27.9921 9.08144 50.5908Z"
              fill="currentColor"
            />
            <path
              d="M93.9676 39.0409C96.393 38.4038 97.8624 35.9116 97.0079 33.5539C95.2932 28.8227 92.871 24.3692 89.8167 20.348C85.8452 15.1192 80.8826 10.7238 75.2124 7.41289C69.5422 4.10194 63.2754 1.94025 56.7698 1.05124C51.7666 0.367541 46.6976 0.446843 41.7345 1.27873C39.2613 1.69328 37.813 4.19778 38.4501 6.62326C39.0873 9.04874 41.5694 10.4717 44.0505 10.1071C47.8511 9.54855 51.7191 9.52689 55.5402 10.0491C60.8642 10.7766 65.9928 12.5457 70.6331 15.2552C75.2735 17.9648 79.3347 21.5619 82.5849 25.841C84.9175 28.9121 86.7997 32.2913 88.1811 35.8758C89.083 38.2158 91.5421 39.6781 93.9676 39.0409Z"
              fill="currentFill"
            />
          </svg>
          <span className="sr-only">Loading...</span>
        </div>
      </div>
    
      </>
     

  );
}

export default App;
