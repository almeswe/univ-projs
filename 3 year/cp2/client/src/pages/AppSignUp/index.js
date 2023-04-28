import { useAuth } from "../../context/AuthContext";
import { useRef, useState } from "react";

import SignUpForm from "../../components/SignUpForm";
import CenteredContainer from "../../components/CenteredContainer";

function AppSignUp() {
  const emailRef = useRef();
  const usernameRef = useRef();
  const passwordRef = useRef();
  const { signup } = useAuth();

  const [error, setError] = useState("");
  const [loading, setLoading] = useState(false);

  async function submitForm(e) {
    e.preventDefault();
    try {
      setError("");
      setLoading(true);
      await signup({
        email: emailRef.current.value,
        username: usernameRef.current.value, 
        password: passwordRef.current.value
      });
    }
    catch (e) {
      setError(`${e}`);
    }
    setLoading(false);
  }

  return (
    <CenteredContainer child={
      <SignUpForm
        error={error}
        loading={loading}
        submitForm={submitForm}
        emailRef={emailRef}
        usernameRef={usernameRef}
        passwordRef={passwordRef}
      ></SignUpForm>}
    ></CenteredContainer>
  );
}

export default AppSignUp;