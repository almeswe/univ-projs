import React from "react";
import { Container } from "react-bootstrap";

function CenteredContainer(params) {
  return (
    <Container 
      className="d-flex align-items-center justify-content-center"
      style={{
         minHeight: "100vh", 
         fontFamily: "Arial"
      }}
    >
      <div className="w-100" style={{ maxWidth: "400px" }}>
        {params.child}
      </div>
    </Container>
  );
}

export default CenteredContainer;