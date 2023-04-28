import { Button, Form } from "react-bootstrap";
import { Card } from "react-bootstrap";
import { Container } from "react-bootstrap";
import { InputGroup } from "react-bootstrap";

function HomeTerminalForm(params) {
  return (
    <Container 
      className="d-flex justify-content-center"
      style={{
         minHeight: "100vh",
         fontFamily: "Arial"
      }}
    >
      <div className="w-100" style={{ maxWidth: "1000px" }}>
        <InputGroup className="mt-3 mb-3">
          <Form.Control 
            ref={params.command} 
            style={{ fontFamily: "Consolas" }}
            onKeyPress={params.submitExecute}
            required
          />
          <Button onClick={params.submitDisconnect} variant="outline-secondary" id="button-addon1">
            DISCONNECT
          </Button>
        </InputGroup>
        <Card className="h-100"
          style={{
             fontFamily: "Consolas",
             fontWeight: "10px"
          }}>
          <p>{params.data}</p>
        </Card>
      </div>
    </Container>
  );
}

export default HomeTerminalForm;