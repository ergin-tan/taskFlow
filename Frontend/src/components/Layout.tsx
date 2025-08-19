import React, { useState, useEffect } from 'react';
import { Routes, Route, Navigate, useLocation, useNavigate } from 'react-router-dom';
import Dashboard from '../pages/Dashboard/Dashboard';
import Login from '../pages/Login/Login';
import Navbar from './Navbar/Navbar';
import Profile from '../pages/Profile/Profile';
import MyWorks from '../pages/MyWorks/MyWorks';
import TaskDetail from '../pages/TaskDetail/TaskDetail';

const Layout: React.FC = () => {
  const location = useLocation();
  const navigate = useNavigate();
  const [isLoggedIn, setIsLoggedIn] = useState(false);

  useEffect(() => {
    const token = localStorage.getItem('token');
    setIsLoggedIn(token !== null);
  }, []);

  const handleLoginSuccess = () => {
    setIsLoggedIn(true);
  };

  const handleLogout = () => {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    setIsLoggedIn(false);
    navigate('/login');
  };

  const isAuthenticated = () => {
    const token = localStorage.getItem('token');
    return token !== null;
  };

  const showNavbar = isLoggedIn && location.pathname !== '/login';

  return (
    <>
      {showNavbar && <Navbar onLogout={handleLogout} />} 
      <Routes>
        <Route path="/login" element={isAuthenticated() ? <Navigate to="/dashboard" replace /> : <Login onLoginSuccess={handleLoginSuccess} />} />
        <Route
          path="/dashboard"
          element={isAuthenticated() ? <Dashboard /> : <Navigate to="/login" replace />}
        />
        <Route
          path="/profile"
          element={isAuthenticated() ? <Profile /> : <Navigate to="/login" replace />}
        />
        <Route
          path="/my-works"
          element={isAuthenticated() ? <MyWorks /> : <Navigate to="/login" replace />}
        />
        <Route
          path="/task/:taskId"
          element={isAuthenticated() ? <TaskDetail /> : <Navigate to="/login" replace />}
        />
        <Route path="*" element={<Navigate to="/login" replace />} /> 
      </Routes>
    </>
  );
};

export default Layout;
