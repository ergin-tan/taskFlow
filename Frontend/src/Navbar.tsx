import React from 'react';
import { Link } from 'react-router-dom';
import './Navbar.css';

interface NavbarProps {
  onLogout: () => void;
}

const Navbar: React.FC<NavbarProps> = ({ onLogout }) => {
  return (
    <header className="navbar">
      <div className="navbar-content">
        <div className="navbar-brand">
          <Link to="/dashboard" className="gradient-text">TaskFlow</Link>
        </div>
        <nav className="navbar-menu">
          <Link to="/profile" className="nav-link">
            <span className="link-icon">👤</span>
            <span className="link-text">Profile</span>
          </Link>
          <Link to="/my-works" className="nav-link">
            <span className="link-icon">📋</span>
            <span className="link-text">My Works</span>
          </Link>
          <button onClick={onLogout} className="nav-link logout-button">
            <span className="link-icon">🚪</span>
            <span className="link-text">Logout</span>
          </button>
        </nav>
      </div>
    </header>
  );
};

export default Navbar;