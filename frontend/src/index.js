import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import {UserProvider} from "./stores/userStore";
import {RoomProvider} from "./stores/roomStore";


const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
      <UserProvider>
          <RoomProvider>
              <App/>
          </RoomProvider>
      </UserProvider>
  </React.StrictMode>
);
