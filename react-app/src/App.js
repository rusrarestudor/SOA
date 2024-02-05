import React, { useState,useEffect } from 'react';
import logo from './logo.svg';
import './App.css';
import { store } from "./actions/store";
import { Provider } from "react-redux";
import DCandidates from './components/DCandidates';
import { Container } from "@mui/material";

// Import toast and the CSS for toast
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

const WS_URL = 'ws://localhost:8080'
const WS_URL2 = 'ws://localhost:26353'

function App() {
  const [webSocket, setWebSokcet] = useState(new WebSocket(WS_URL));
  const [webSocket2, setWebSocket2] = useState(new WebSocket(WS_URL2));


  useEffect(() => {

    webSocket.onmessage = (msg) => {
      console.log('MSG', msg);
      
      // Parse the incoming message data
      const data = JSON.parse(msg.data);
      
      // Check the message type or structure and display accordingly
      if (data.bloodType && data.quantity) {
        // This message is assumed to be from Kafka for blood donation
        toast(`Blood Needed: ${data.bloodType} for ${data.quantity} l`, {position: "top-left"});
      }
    };

    webSocket2.onmessage = (msg2) => {
      console.log('MSG', msg2);

      // Parse the incoming message data
      const data2 = JSON.parse(msg2.data);

      if (data2.name && data2.appointment_time) {
        // This message is assumed to be from RabbitMQ for an appointment
        toast(`${data2.name} has an appointment at ${data2.appointment_time}`, {position: "top-right"});
      }
    }

  }, []);

  return (
    <Provider store={store}>
        <Container maxWidth="lg">
          <DCandidates />
          <ToastContainer position="top-left" />
        </Container>
    </Provider>
  );
}

export default App;
