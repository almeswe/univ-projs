import { useAuth } from "../../context/AuthContext";
import { useRef, useState } from "react";

import SignInForm from "../../components/SignInForm";
import CenteredContainer from "../../components/CenteredContainer";

function AppSignIn() {
  const emailRef = useRef();
  const passwordRef = useRef();
  const { signin } = useAuth();

  const [error, setError] = useState("");
  const [loading, setLoading] = useState(false);

  async function submitForm(e) {
    e.preventDefault();
    try {
      setError("");
      setLoading(true);
      await signin({
        email: emailRef.current.value,
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
      <SignInForm
        error={error}
        loading={loading}
        submitForm={submitForm}
        emailRef={emailRef}
        passwordRef={passwordRef}
      ></SignInForm>}
    ></CenteredContainer>
  );
}

export default AppSignIn;