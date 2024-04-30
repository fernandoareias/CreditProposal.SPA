import React, { createContext, useContext, useEffect, useState } from "react";

interface SessionContextType {
  privateKey: string | null;
  token: string | null;
}

export const SessionContext = createContext<SessionContextType | undefined>(
  undefined
);

export const generateGUID = () => {
  // Função para gerar um GUID aleatório
  const s4 = () =>
    Math.floor((1 + Math.random()) * 0x10000)
      .toString(16)
      .substring(1);
  return `${s4()}${s4()}-${s4()}-${s4()}-${s4()}-${s4()}${s4()}${s4()}`;
};

// interface Props {
//   children: React.ReactNode;
// }

// const SessionContextProvider: React.FC<Props> = ({
//   children,
// }): React.ReactElement => {
//   const [privateKey, setPrivateKey] = useState<string | null>("");
//   const [token, setToken] = useState<string | null>("");

//   useEffect(() => {
//     async function createSession() {
//       try {
//         const response = await fetch(
//           "https://localhost:7222/authentication/session",
//           {
//             method: "POST",
//             headers: {
//               "Content-Type": "application/json",
//               aggregate_id: generateGUID(),
//             },
//           }
//         );

//         if (!response.ok) {
//           throw new Error("Erro ao carregar os usuários");
//         }
//         const data = await response.json();
//         console.log(data);
//       } catch (error) {
//         console.error(error);
//       }
//     }
//     createSession();
//   }, []);

//   return (
//     <SessionContext
//   );
// };
