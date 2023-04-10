import TodoApp from './TodoApp';

import React from 'react';
import ReactDOM from 'react-dom/client';

import { ApolloClient, InMemoryCache } from '@apollo/client';
import { ApolloProvider } from '@apollo/client';
import { BACKEND_URL } from './Constants';

const root = ReactDOM.createRoot(document.getElementById('root'));

const client = new ApolloClient({
  uri: `${BACKEND_URL}/graphql`,
  cache: new InMemoryCache()
});

root.render(
  <ApolloProvider client={client}>
    <TodoApp/>
  </ApolloProvider>
);
