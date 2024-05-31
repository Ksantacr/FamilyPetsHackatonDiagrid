import React, { useState, useEffect } from 'react';
import './toggle.css'; // Importamos los estilos CSS
import { BiAdjust, BiMoon, BiSun  } from "react-icons/bi";

const ThemeToggle = () => {
  const [theme, setTheme] = useState('light');

  useEffect(() => {
    const savedTheme = localStorage.getItem('theme') || 'light';
    setTheme(savedTheme);
    document.body.setAttribute('data-theme', savedTheme);
  }, []);

  const toggleTheme = (newTheme) => {
    setTheme(newTheme);
    document.body.setAttribute('data-theme', newTheme);
    localStorage.setItem('theme', newTheme);
  };

  return (
    <div className="theme-toggle-container">
      <button onClick={() => toggleTheme('light')} className={`theme-toggle-btn ${theme === 'light' ? 'active' : ''}`}>
        {theme === 'light' ? <BiSun /> : <BiSun />}
      </button>
      <button onClick={() => toggleTheme('dark')} className={`theme-toggle-btn ${theme === 'dark' ? 'active' : ''}`}>
        {theme === 'dark' ? <BiMoon /> : <BiMoon />}
      </button>
      <button onClick={() => toggleTheme('system')} className={`theme-toggle-btn ${theme === 'system' ? 'active' : ''}`}>
        <span><BiAdjust /></span>
      </button>
    </div>
  );
};

export default ThemeToggle;
