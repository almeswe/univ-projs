import TodoApp from './TodoApp';

import React from 'react';
import ReactDOM from 'react-dom/client';

localStorage.setItem("atoken", "");
const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <TodoApp/>
  </React.StrictMode>
);
