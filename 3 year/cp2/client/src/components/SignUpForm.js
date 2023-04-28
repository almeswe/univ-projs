import { React } from "react";
import { Link } from "react-router-dom";

import { Form } from "react-bootstrap";
import { Card } from "react-bootstrap";
import { Alert } from "react-bootstrap";
import { Button } from "react-bootstrap";

function SignUpForm(params) {
  return (
    <>
      <Card>
        <Card.Body>
          <h2 className="text-center mb-4">Sign Up</h2>
          { params.error !== "" && <Alert variant="danger">{params.error}</Alert> }
          <Form onSubmit={params.submitForm}>
            <Form.Group id="email">
              <Form.Label>Email</Form.Label>
              <Form.Control type="email" ref={params.emailRef} required/>
            </Form.Group>
            <Form.Group id="username">
              <Form.Label>Username</Form.Label>
              <Form.Control type="text" ref={params.usernameRef} required/>
            </Form.Group>
            <Form.Group id="password">
              <Form.Label>Password</Form.Label>
              <Form.Control type="password" ref={params.passwordRef} required/>
            </Form.Group>
            <Button disabled={params.loading} className="w-100 mt-3" type="submit">Sign Up</Button>
          </Form>
        </Card.Body>
      </Card>
      <div className="w-100 text-center mt-2">
        Already have an account? 
        <Link to="/signin">Sign In</Link>
      </div>
    </>
  );
}

export default SignUpForm;