function TodoAuth(params) {
  return (
    <div className="todo-auth-div">
      <input 
        type="text"
        placeholder="Enter your email" 
        className="todo-auth-input"
        onChange={params.emailChanged}>
      </input>
      <input 
        type="password" 
        placeholder="Enter your password"
        className="todo-auth-input"
        onChange={params.passwordChanged}>
      </input>
      <button 
        type="submit" 
        className="todo-btn todo-auth-btn"
        onClick={() => params.authSignIn()}>
        Sign In
      </button>
      <button 
        type="submit" 
        className="todo-btn todo-auth-btn"
        onClick={() => params.authSignUp()}>
        Sign Up
      </button>
    </div>
  );
}

export default TodoAuth;