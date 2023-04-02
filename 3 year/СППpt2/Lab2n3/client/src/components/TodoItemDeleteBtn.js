import { MdDelete } from "react-icons/md";

function TodoItemDeleteBtn(params) {
  return (
    <button 
    className="todo-delete-btn todo-btn"
    onClick={() => params.deleteItem(params.id)}> 
      <MdDelete size={20}/>
    </button>
  );
} 

export default TodoItemDeleteBtn;