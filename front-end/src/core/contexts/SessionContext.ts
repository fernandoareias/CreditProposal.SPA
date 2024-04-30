import React, { createContext, useContext } from "react";

interface SessionContextType {
  login: (aggregateId: string) => void;
}

const SessionContext = createContext<SessionContextType | undefined>(undefined);
