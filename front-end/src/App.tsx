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
import Loading from './core/components/Loading';
import PrivateRoute from './core/guards/PrivateRoute';

function App() {

  // Session
  const [privateKey, setPrivateKey] = useState<string | null>(null);
  const [sessionId, setSessionId] = useState<string | null>(null);
  const [version, setVersion] = useState<string | null>(null);
  const [token, setToken] = useState<string | null>(null);
 

  useEffect(() => {
    const storedSessionId = sessionStorage.getItem('sessionId');
    if (storedSessionId) {
      setSessionId(storedSessionId);
    } else {
      const newSessionId = generateGUID();
      sessionStorage.setItem('sessionId', newSessionId);
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

  const dashboardRoutes = [
    <Route key="proposals" path="/dashboard/proposals" element={<ProposalsPage />} />,
    <Route key="credit-cards" path="/dashboard/credit-cards" element={<CreditCardPage />} />
    // Outras rotas da dashboard aqui
  ];

  return (
    sessionId && version?
      <>
        <SessionContext.Provider value={{ privateKey, version, sessionId, token, setToken: updateToken }}>
          <BrowserRouter>
                <Routes>
                  <Route path='*' Component={NotExistsPage}/>
                  
                  <Route path="/authentication/login" Component={LoginPage} />
                  <Route path="/authentication/signup" Component={SignUpPage} />
                  <Route path='/authentication/recovery' Component={RecoveryPassword}/>
                  
                  <Route path="/dashboard/*" element={
                    <PrivateRoute>
                      <Route index element={<Dashboard />} />
                      {/* Renderize as rotas da dashboard dentro do outlet do componente Dashboard */}
                      <Route element={<Dashboard />}>
                        <Route path='proposals' element={<ProposalsPage />} />
                        <Route path='credit-cards' element={<CreditCardPage />} />
                      </Route>
                    </PrivateRoute>
                  } />


  
                </Routes>
            </BrowserRouter>
          </SessionContext.Provider>
        </>
        :
        <>
          <Loading/>
        </>
     

  );
}

export default App;
