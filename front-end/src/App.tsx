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

function App() {
    const [privateKey, setPrivateKey] = useState<string | null>("");
    const [token, setToken] = useState<string | null>("");

    const id = generateGUID();
    console.log(id);

    useEffect(() => {

      fetch(
        "https://localhost:7222/authentication/session",
        {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
            sessionId: id,
          },
        }
      ).catch((e) => console.log(e)).then((x) => {
        console.log(x);
      })
        //   async function createSession() {
        //     try {
        //       const response = await fetch(
        //         "https://localhost:7222/authentication/session",
        //         {
        //           method: "POST",
        //           headers: {
        //             "Content-Type": "application/json",
        //             sessionId: generateGUID(),
        //           },
        //         }
        //       );
      
        //       if (!response.ok) {
        //         throw new Error("Erro ao carregar os usuários");
        //       }
        //       const r =  await response;
        //       console.log(r);
        //       const data = r.json();
        //       console.log(data);

        //     } catch (error) {
        //       console.error(error);
        //     }
        //   }
        //   createSession();
        }, []);


  return (
    <>
      <SessionContext.Provider value={{ privateKey, token }}>
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
  );
}

export default App;
