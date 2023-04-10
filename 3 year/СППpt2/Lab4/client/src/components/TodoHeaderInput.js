function TodoHeaderInput(params) {
  return (
    <input 
      className="todo-input" 
      placeholder="Enter a name for new item"
      onChange={params.inputChanged}>
    </input>
  );
}

export default TodoHeaderInput;