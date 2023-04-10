import { MdCancel, MdCheck } from "react-icons/md";

function TodoItemCheckBtn(params) {
  let icon = <MdCheck size={20}/>;
  if (params.isChecked) {
    icon = <MdCancel size={20}/>;
  }
  return (
    <button 
    className="todo-check-btn todo-btn"
    onClick={() => params.checkItem(params.id)}>
      {icon}
    </button>
  );
}

export default TodoItemCheckBtn;