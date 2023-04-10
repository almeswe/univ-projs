import { MdAdd } from "react-icons/md";

function TodoItemAddBtn(params) {
  return (
    <button 
    className="todo-add-btn todo-btn" 
    onClick={() => params.addItem()}> 
      <MdAdd size={20}/>
    </button>
  );
}

export default TodoItemAddBtn;