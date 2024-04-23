import React from 'react';
import logo from './logo.svg';
import './App.css';
import LoginPage from './modules/authentication/pages/login/LoginPage';
import SignUpPage from './modules/authentication/pages/signup/SignUpPage';

function App() {
  return (
    <div className="App">
      <SignUpPage/>
    </div>
  );
}

export default App;
