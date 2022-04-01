import React from "react";

export interface UserContextState {
    userId: string | null,
    setUser(userId: string): void
}

export const UserContext = React.createContext<UserContextState>({
    userId: null,
    setUser(userId: string) {
    }
})