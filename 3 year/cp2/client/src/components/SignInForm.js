import { React } from "react";
import { Link } from "react-router-dom";

import { Form } from "react-bootstrap";
import { Card } from "react-bootstrap";
import { Alert } from "react-bootstrap";
import { Button } from "react-bootstrap";

function SignInForm(params) {
  return (
    <>
      <Card>
        <Card.Body>
          <h2 className="text-center mb-4">Sign In</h2>
          { params.error !== "" && <Alert variant="danger">{params.error}</Alert> }
          <Form onSubmit={params.submitForm}>
            <Form.Group id="email">
              <Form.Label>Email</Form.Label>
              <Form.Control type="email" ref={params.emailRef} required/>
            </Form.Group>
            <Form.Group id="password">
              <Form.Label>Password</Form.Label>
              <Form.Control type="password" ref={params.passwordRef} required/>
            </Form.Group>
            <Button disabled={params.loading} className="w-100 mt-3" type="submit">Sign In</Button>
          </Form>
        </Card.Body>
      </Card>
      <div className="w-100 text-center mt-2">
        Does not have an account? 
        <Link to="/signup">Sign Up</Link>
      </div>
    </>
  );
}

export default SignInForm;