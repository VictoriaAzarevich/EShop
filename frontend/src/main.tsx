import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.tsx'
import { authConfig } from './authConfig.ts'
import { Auth0Provider } from "@auth0/auth0-react";

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <Auth0Provider {...authConfig}>
      <App />
    </Auth0Provider>
  </StrictMode>,
)
