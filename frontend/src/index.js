import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import {UserProvider} from "./stores/userStore";
import {GameProvider} from "./stores/gameStore";


const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
      <UserProvider>
          <GameProvider>
              <App/>
          </GameProvider>
      </UserProvider>
  </React.StrictMode>
);
