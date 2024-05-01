import React, { createContext, useContext, useEffect, useState } from "react";

export const generateGUID = () => {
  const s4 = () =>
    Math.floor((1 + Math.random()) * 0x10000)
      .toString(16)
      .substring(1);
  return `${s4()}${s4()}-${s4()}-${s4()}-${s4()}-${s4()}${s4()}${s4()}`;
};

interface SessionContextType {
  privateKey: string | null;
  version: string | null;
  sessionId: string | null;
  token: string | null;
  setToken: (token: string) => void;
}

export const SessionContext = createContext<SessionContextType>({
  privateKey: null,
  version: null,
  sessionId: null,
  token: null,
  setToken: () => {},
});
