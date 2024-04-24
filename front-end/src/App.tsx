import React from 'react';
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

function App() {
  return (
    <>
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
    </>
  );
}

export default App;
