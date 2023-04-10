import "./TodoApp.css";
import { useState } from 'react';
import { useEffect } from 'react';
import { BACKEND_URL } from "./Constants";

import TodoAuth from "./components/TodoAuth";
import TodoItem from "./components/TodoItem";
import TodoHeader from "./components/TodoHeader";

import io from 'socket.io-client';

const socket = io.connect(BACKEND_URL);

const responseMap = new Map();

function TodoApp() {
  const [items, setItems] = useState([]);
  const [inputText, setInputText] = useState("");

  const [authEmail, setAuthEmail] = useState("");
  const [authPassword, setAuthPassword] = useState("");

  responseMap.set('api.signin', TodoAuthResponse);
  responseMap.set('api.signup', TodoAuthResponse);
  responseMap.set('api.items', TodoItemsUpdateResponse);
  responseMap.set('api.items.add', TodoBlankResponse);
  responseMap.set('api.items.check', TodoBlankResponse);
  responseMap.set('api.items.delete', TodoBlankResponse);

  useEffect(() => {
    socket.on('response', (data) => {
      if (responseMap.has(data.route)) {
        responseMap.get(data.route)(data);
      } 
    });
  }, [socket]);

  function UpdateToken(token) {
    if (token) {
      localStorage.setItem("atoken", token);
    } 
  }

  function TodoAuthResponse(data) {
    if (data.status && data.status == 200) {
      UpdateToken(data.token);
      TodoItemsUpdate();
    }
    else {
      alert('Try again!');
    }
  }

  function TodoItemsUpdateResponse(data) {
    if (data.status == 200) {
      setItems(data.query);
    }
    if (data.status >= 400 && data.status < 500) {
      localStorage.setItem("atoken", '');
    }
  }

  function TodoBlankResponse(data) {
    if (data.status == 200) {
      TodoItemsUpdate();
    }
    if (data.status >= 400 && data.status < 500) {
      TodoRefresh();
    }
  }

  function TodoInputChanged(event) {
    setInputText(event.target.value);
  }

  function TodoAuthEmailChanged(event) {
    setAuthEmail(event.target.value);
  }

  function TodoAuthPasswordChanged(event) {
    setAuthPassword(event.target.value);
  }

  function TodoRefresh() {
    setAuthEmail("");
    setAuthPassword("");
    localStorage.setItem("atoken", "");
  }

  function TodoAuthSignIn() {
    socket.emit('api.signin', {
      query: {
        email: authEmail,
        psswd: authPassword
      }
    });
  }

  function TodoAuthSignUp() {
    socket.emit('api.signup', {
      query: {
        email: authEmail,
        psswd: authPassword
      }
    });
  }

  function TodoItemsUpdate() {
    socket.emit('api.items', {
      token: localStorage.getItem('atoken'),
    });
  }

  function TodoItemAdd(text) {
    if (inputText.trim() === "") {
      alert("Cannot add empty item!");
    }
    else {
      socket.emit('api.items.add', {
        token: localStorage.getItem('atoken'),
        query: {
          text: inputText
        }
      });
    }
  }

  function TodoItemCheck(id) {
    socket.emit('api.items.check', {
      query: {
        itemid: id
      },
      token: localStorage.getItem('atoken'),
    });
  }

  function TodoItemDelete(id) {
    socket.emit('api.items.delete', {
      query: {
        itemid: id
      },
      token: localStorage.getItem('atoken'),
    });
  }

  if (!localStorage.getItem("atoken")) {
    return (
      <div className="todo-app">
        <TodoAuth
          authSignIn={TodoAuthSignIn}
          authSignUp={TodoAuthSignUp}
          emailChanged={TodoAuthEmailChanged}
          passwordChanged={TodoAuthPasswordChanged}
        />
      </div>
    );
  }
  return (
    <div className="todo-app">
      <TodoHeader
        addItem={TodoItemAdd}
        inputChanged={TodoInputChanged}
      />
      {
        items.map(item => 
          <TodoItem
            key={item.id}
            id={item.id}
            text={item.text}
            isChecked={item.isChecked}
            checkItem={TodoItemCheck}
            deleteItem={TodoItemDelete}
          />
        )
      }
    </div>
  );
}

export default TodoApp;