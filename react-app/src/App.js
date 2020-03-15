import React from 'react';
import './App.css';
import { store } from './actions/store';
import { Provider } from 'react-redux';
import Vehicles from './components/vehicles';
import { ToastProvider } from "react-toast-notifications";

function App() {
  return (
    <Provider store={store}>
      <ToastProvider autoDismiss={true}>
        <Vehicles />
      </ToastProvider>
    </Provider>
  );
}

export default App;
