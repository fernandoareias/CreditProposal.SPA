import React, { createContext, useEffect, useState } from "react";
import * as signalR from "@microsoft/signalr";
import { Proposal } from "../pages/proposals/models/Proposa";

let proposals: Proposal[] = [];

export const ProposalsContext = createContext<{
  proposals: Proposal[];
  setProposals: React.Dispatch<React.SetStateAction<Proposal[]>>;
}>({
  proposals: proposals,
  setProposals: () => {},
});
