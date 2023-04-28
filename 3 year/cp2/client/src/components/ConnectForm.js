import { Form } from "react-bootstrap";
import { Card } from "react-bootstrap";
import { Alert } from "react-bootstrap";
import { Badge } from "react-bootstrap";
import { Button } from "react-bootstrap";
import { Container } from "react-bootstrap";

import { useAuth } from "../context/AuthContext";

function HomeConnectForm(params) {
  const { user, signout } = useAuth();

  function onSignout(e) {
    signout();
  }

  return (
    <Container 
      className="d-flex justify-content-center align-items-center"
      style={{ minHeight: "100vh" }}
    >
      <div style={{ fontFamily: "Arial" }}>
        <h1>Hi, <Badge bg="secondary">{user.username}</Badge>: <Badge bg="secondary">{user.email}</Badge>!</h1>
        <h6 style={{ color: "gray" }}>Connect to any ssh server you want.. or <a style={{textDecoration: "underline"}} onClick={onSignout}>sign out</a></h6>
        <Card className="mt-3 p-5">
          { params.error !== "" && <Alert variant="danger">{params.error}</Alert> }
          <div className="w-100" style={{ maxWidth: "500px" }}>
            <Form onSubmit={params.submitForm}>
              <Form.Group className="mb-1" controlId="formBasicEmail">
                <Form.Label>Specify Host</Form.Label>
                <Form.Control ref={params.host} type="text" placeholder="Enter host" required/>
                <Form.Text className="text-muted">
                  format: user@host or user@ip.
                </Form.Text>
              </Form.Group>
              <Form.Group className="mb-3" controlId="formBasicPassword">
                <Form.Label>Specify Host's Password</Form.Label>
                <Form.Control ref={params.pass} type="password" placeholder="Enter password" required/>
              </Form.Group>
              <Button disabled={params.loading} className="w-100" variant="primary" type="submit">
                Connect
              </Button>
            </Form>
          </div>
        </Card>
      </div>
    </Container>
  );
}

export default HomeConnectForm;