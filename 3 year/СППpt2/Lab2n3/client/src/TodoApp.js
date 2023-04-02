import "./TodoApp.css";
import { useState } from 'react';
import TodoItem from "./components/TodoItem";
import TodoHeader from "./components/TodoHeader";
import { BACKEND_URL } from "./Constants";

import TodoAuth from "./components/TodoAuth";

function TodoApp() {
  const [items, setItems] = useState([]);
  const [inputText, setInputText] = useState("");

  const [authEmail, setAuthEmail] = useState("");
  const [authPassword, setAuthPassword] = useState("");

  function UpdateToken(token) {
    if (token) {
      localStorage.setItem("atoken", token);
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

  async function TodoAuthSignIn() {
    const res = await fetch(`${BACKEND_URL}/signin`, {
      method: 'POST',
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify({
        "email": authEmail,
        "psswd": authPassword
      })
    });
    const body = await res.json();
    if (res.status == 200) {
      UpdateToken(body.token);
      await TodoItemsUpdate();
    }
    else {
      alert(body);
    }
  }

  async function TodoAuthSignUp() {
    const res = await fetch(`${BACKEND_URL}/signup`, {
      method: 'POST',
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify({
        "email": authEmail,
        "psswd": authPassword
      })
    });
    const body = await res.json();
    alert(body);
  }

  async function TodoItemsUpdate() {
    const res = await fetch(`${BACKEND_URL}/api/items/`, {
      method: 'GET',
      headers: {
        "Authorization": `Bearer ${localStorage.getItem("atoken")}`
      }
    });
    const body = await res.json();
    if (res.status == 200) {
      setItems(body);
    }
    if (res.status >= 400 && res.status < 500) {
      localStorage.setItem("atoken", '');
    }
  }

  async function TodoItemAdd(text) {
    if (inputText.trim() == "") {
      alert("Cannot add empty item!");
    }
    else {
      const res = await fetch(`${BACKEND_URL}/api/items/add`, {
        method: 'POST',
        headers: {
          "Authorization": `Bearer ${localStorage.getItem("atoken")}`,
          "Content-Type": "application/json" 
        },
        body: JSON.stringify({
          "text": inputText
        })
      });
      if (res.status == 200) {
        await TodoItemsUpdate();
      }
      if (res.status >= 400 && res.status < 500) {
        TodoRefresh();
      }
    }
  }

  async function TodoItemCheck(id) {
    const res = await fetch(`${BACKEND_URL}/api/items/check/${id}`, {
      method: 'PUT',
      headers: {
        "Authorization": `Bearer ${localStorage.getItem("atoken")}`
      }
    });
    if (res.status == 200) {
      await TodoItemsUpdate();
    }
    if (res.status >= 400 && res.status < 500) {
      TodoRefresh();
    }
  }

  async function TodoItemDelete(id) {
    const res = await fetch(`${BACKEND_URL}/api/items/delete/${id}`, {
      method: 'DELETE',
      headers: {
        "Authorization": `Bearer ${localStorage.getItem("atoken")}`
      }
    });
    if (res.status == 200) {
      await TodoItemsUpdate();
    }
    if (res.status >= 400 && res.status < 500) {
      TodoRefresh();
    }
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