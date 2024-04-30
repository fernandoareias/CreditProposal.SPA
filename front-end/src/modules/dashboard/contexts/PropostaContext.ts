import React, { createContext, useEffect, useState } from "react";
import * as signalR from "@microsoft/signalr";

export class ProposalContextType {
  id: string = null!;
  code: string = null!;
}
// Builds the SignalR connection, mapping it to /proposals
// const hubConnection = new signalR.HubConnectionBuilder()
//   .withUrl("https://localhost:7222/proposals", {
//     withCredentials: true, // Enviar credenciais
//   })
//   .configureLogging(signalR.LogLevel.Debug)
//   .build();

// hubConnection.on("ReceberPropostas", (x) => {
//   console.log("Recebdno proposta");
//   console.log(x);
// });

// // Inicia a conexão
// hubConnection
//   .start()
//   .then(() => {
//     console.log("SignalR connection established.");

//     // if (hubConnection.connectionId) {
//     //   hubConnection.invoke("sendConnectionId", hubConnection.connectionId);
//     // }
//     //hubConnection.invoke("Streaming", "teste");
//     hubConnection.send("Streaming", "teste").then((x) => {
//       console.log("Invocou streaming");
//       console.log(x);
//     });
//   })
//   .catch((error) => {
//     console.error("SignalR connection failed: ", error);
//   });

let proposals = [];
// hubConnection.on("", (m: ProposalContextType[]) => {
//   proposals = [...proposals, m];
// });

// const SignalRTime: React.FC = () => {
//   // Sets the time from the server
//   const [time, setTime] = useState<string | null>(null);

//   useEffect(() => {
//     hubConnection.on("setTime", (message) => {
//       setTime(message);
//     });
//   });

//   return <p>The time is {time}</p>;
// };

export const ProposalsContext = createContext<ProposalContextType[]>(proposals);
