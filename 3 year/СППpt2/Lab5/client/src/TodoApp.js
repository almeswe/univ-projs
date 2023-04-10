import './TodoApp.css';
import { useState } from 'react';
import { useEffect } from 'react';
import { BACKEND_URL } from './Constants';

import { useQuery } from '@apollo/client';
import { useMutation } from '@apollo/client';

import { ADD_ITEM } from './mutation/item';
import { CHECK_ITEM } from './mutation/item';
import { DELETE_ITEM } from './mutation/item';
import { GET_ALL_ITEMS } from './query/item';

import TodoItem from "./components/TodoItem";
import TodoHeader from "./components/TodoHeader";

function TodoApp() {
  const [items, setItems] = useState([]);
  const [inputText, setInputText] = useState("");

  const [addItem] = useMutation(ADD_ITEM);
  const [checkItem] = useMutation(CHECK_ITEM);
  const [deleteItem] = useMutation(DELETE_ITEM);

  const { data, loading, refetch } = useQuery(GET_ALL_ITEMS);

  useEffect(() => {
    if (!loading) {
      setItems(data.get_items);
    }
  }, [data])

  function TodoInputChanged(event) {
    setInputText(event.target.value);
  }

  async function TodoItemsUpdate() {
    const res = await fetch(`${BACKEND_URL}/api/items/`, {
      method: 'GET',
    });
    const body = await res.json();
    if (res.status == 200) {
      setItems(body);
    }
  }

  function TodoItemAdd() {
    if (inputText.trim() === "") {
      alert('Item cannot be empty!');
    }
    else {
      addItem({
        variables: {
          text: inputText
        }
      });
      refetch();
    }
  }

  function TodoItemCheck(id) {
    checkItem({
      variables: {
        id: id
      }
    });
    refetch();
  }

  function TodoItemDelete(id) {
    deleteItem({
      variables: {
        id: id
      }
    });
    refetch();
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