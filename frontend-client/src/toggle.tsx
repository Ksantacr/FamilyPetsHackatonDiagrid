import React, { useState, useEffect } from 'react';
import './toggle.css'; // Importamos los estilos CSS
import { BiAdjust, BiMoon, BiSolidMoon, BiSun, BiSolidSun, BiSolidAdjust } from "react-icons/bi";

const ThemeToggle = () => {
  const [theme, setTheme] = useState('light');

  useEffect(() => {
    const savedTheme = localStorage.getItem('theme') || 'light';
    setTheme(savedTheme);
    updateTheme(savedTheme); // Actualizamos el tema al montar el componente
  }, []);

  const toggleTheme = (newTheme) => {
    setTheme(newTheme);
    updateTheme(newTheme); // Actualizamos el tema al cambiar el toggle
    localStorage.setItem('theme', newTheme);
  };

  const updateTheme = (selectedTheme) => {
    const root = document.documentElement;
    if (selectedTheme === 'light') {
      root.style.setProperty('--background-color', '#f0f4f8'); // Color de fondo claro
      root.style.setProperty('--card-background-color', 'white'); // Color de fondo del card claro
      root.style.setProperty('--card-shadow', '0px 4px 6px rgba(0, 0, 0, 0.1)'); // Sombra del card claro
      root.style.setProperty('--text-color', 'black'); // Color del texto claro
      root.style.setProperty('--heading-color', 'black'); // Color del encabezado claro
      root.style.setProperty('--label-color', 'gray'); // Color de las etiquetas claro
    } else {
      root.style.setProperty('--background-color', '#121212'); // Color de fondo oscuro
      root.style.setProperty('--card-background-color', '#1e1e1e'); // Color de fondo del card oscuro
      root.style.setProperty('--card-shadow', '0px 4px 6px rgba(0, 0, 0, 0.6)'); // Sombra del card oscuro
      root.style.setProperty('--text-color', 'white'); // Color del texto oscuro
      root.style.setProperty('--heading-color', 'white'); // Color del encabezado oscuro
      root.style.setProperty('--label-color', 'lightgray'); // Color de las etiquetas oscuro
    }
  };

  return (
    <div className="card theme-toggle-card">
      <div className="theme-toggle-container">
        <button onClick={() => toggleTheme('light')} className={`theme-toggle-btn ${theme === 'light' ? 'active' : ''}`}>
          {theme === 'light' ? <BiSolidSun color="#ffd700" size={20} /> : <BiSun color="#ffd700" size={20}/>}
        </button>
        <button onClick={() => toggleTheme('dark')} className={`theme-toggle-btn ${theme === 'dark' ? 'active' : ''}`}>
          {theme === 'dark' ? <BiSolidMoon color="#483d8b" size={20} /> : <BiMoon color="#483d8b" size={20}/>}
        </button>
        <button onClick={() => toggleTheme('system')} className={`theme-toggle-btn ${theme === 'system' ? 'active' : ''}`}>
          {theme === 'light' ? <BiSolidAdjust color="#808080" size={20} /> : <BiAdjust color="#808080" size={20}/>}
        </button>
      </div>
    </div>
  );
};

export default ThemeToggle;
