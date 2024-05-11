import React, { useEffect, useState } from 'react';
import { Navigate, Route, Routes } from 'react-router-dom';
import Loading from '../components/Loading';


const PrivateRoute: React.FC<{ children: any }> = ({ children }) => {
  const [authenticated, setAuthenticated] = useState<boolean>(false);
  const [loading, setLoading] = useState<boolean>(true);

  useEffect(() => {
    const token = sessionStorage.getItem('token');
    const isAuthenticated = !!token;
    setAuthenticated(isAuthenticated);
    setLoading(false); 
  }, []);

  if (loading) return <Loading/>;

  if (!authenticated) {
    return <Navigate to="/authentication/login" replace={true} />;
  }

  return <Routes>{children}</Routes>;
};



export default PrivateRoute;


