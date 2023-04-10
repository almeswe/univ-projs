import TodoItemCheckBtn from "./TodoItemCheckBtn";
import TodoItemDeleteBtn from "./TodoItemDeleteBtn";

function TodoItem(params) {
  const itemClass = "todo-item" + 
    (params.isChecked ?
      " todo-completed" : ""); 
  return (
    <div className={itemClass}>
      <h3>{params.text}</h3>
      <div className="todo-buttons">
        <TodoItemCheckBtn 
          id={params.id}
          isChecked={params.isChecked}
          checkItem={params.checkItem}  
        />
        <TodoItemDeleteBtn 
          id={params.id}
          deleteItem={params.deleteItem}
        />
      </div>
    </div>
  )
}

export default TodoItem;