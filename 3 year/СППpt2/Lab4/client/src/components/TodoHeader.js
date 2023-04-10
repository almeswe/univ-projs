import TodoItemAddBtn from "./TodoItemAddBtn";
import TodoHeaderInput from "./TodoHeaderInput";

function TodoHeader(params) {
  return (
    <div className="todo-header">
      <TodoHeaderInput inputChanged={params.inputChanged}/>
      <div className="todo-buttons">
        <TodoItemAddBtn addItem={params.addItem}/>
      </div>
    </div>
  );
}

export default TodoHeader;