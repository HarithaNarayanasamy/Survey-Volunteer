import React, { createContext, useState, useContext, useMemo, useEffect } from 'react';
import { User, LovData, AppContextType, AuthenticatedUser } from '../types';
import { 
    isFirebaseInitialized, 
    fetchUsers, 
    fetchLovData, 
    uploadUsers, 
    uploadLovData, 
    updateUser 
} from '../services/firebaseService';
import { useToast } from '../hooks/useToast';

const AppContext = createContext<AppContextType | undefined>(undefined);

export const AppProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
    const [users, setUsers] = useState<User[]>([]);
    const [lovData, setLovData] = useState<LovData>({});
    const [isLoading, setIsLoading] = useState(true);
    const [isFirebaseConfigured] = useState(isFirebaseInitialized);
    const { addToast } = useToast();

    const [user, setUser] = useState<AuthenticatedUser | null>(() => {
        const storedUser = sessionStorage.getItem('authUser');
        return storedUser ? JSON.parse(storedUser) : null;
    });

    useEffect(() => {
        const loadData = async () => {
            try {
                setIsLoading(true);
                const [fetchedUsers, fetchedLovData] = await Promise.all([
                    fetchUsers(),
                    fetchLovData()
                ]);
                setUsers(fetchedUsers);
                setLovData(fetchedLovData || {});
            } catch (error) {
                console.error("Failed to load data from Firebase:", error);
                addToast("Could not connect to the database. Please check configuration.", "error");
            } finally {
                setIsLoading(false);
            }
        };

        if (isFirebaseConfigured) {
            loadData();
        } else {
            setIsLoading(false); // No config, so not loading.
        }
    }, [isFirebaseConfigured, addToast]);
    

    const volunteers = useMemo(() => {
        const volunteerSet = new Set<string>();
        users.forEach(u => {
            if (u.volunteerId) {
                volunteerSet.add(u.volunteerId);
            }
        });
        return Array.from(volunteerSet).sort();
    }, [users]);
    
    const login = (username: string, volunteerId?: string) => {
        let authUser: AuthenticatedUser | null = null;
        if (username.toLowerCase() === 'superadmin') {
            authUser = { username: 'SuperAdmin', role: 'admin' };
        } else if (username.toLowerCase() === 'volunteer') {
            authUser = { username: 'Volunteer', role: 'volunteer', volunteerId };
        }
        setUser(authUser);
        if(authUser) sessionStorage.setItem('authUser', JSON.stringify(authUser));
    };

    const logout = () => {
        setUser(null);
        sessionStorage.removeItem('authUser');
    };
    
    const handleUpdateUsers = async (newUsers: User[]) => {
        setIsLoading(true);
        try {
            await uploadUsers(newUsers);
            setUsers(newUsers);
        } finally {
            setIsLoading(false);
        }
    };
    
    const handleUpdateSingleUser = async (updatedUser: User) => {
        const oldUsers = users;
        const userIndex = users.findIndex(u => u.id === updatedUser.id);
        if (userIndex !== -1) {
            const newUsers = [...users];
            newUsers[userIndex] = updatedUser;
            setUsers(newUsers);
        }

        try {
            await updateUser(updatedUser);
        } catch (error) {
            console.error("Failed to update user:", error);
            setUsers(oldUsers);
            addToast("Failed to save user data.", "error");
        }
    };
    
    const handleUpdateLovData = async (newLovData: LovData) => {
        setIsLoading(true);
        try {
            await uploadLovData(newLovData);
            setLovData(newLovData);
        } finally {
            setIsLoading(false);
        }
    };

    const auth = { user, login, logout };

    return (
        <AppContext.Provider value={{ 
            isFirebaseConfigured,
            isLoading, 
            users, 
            lovData, 
            updateUsers: handleUpdateUsers,
            updateSingleUser: handleUpdateSingleUser,
            updateLovData: handleUpdateLovData,
            auth, 
            volunteers 
        }}>
            {children}
        </AppContext.Provider>
    );
};

export const useAppContext = () => {
    const context = useContext(AppContext);
    if (context === undefined) {
        throw new Error('useAppContext must be used within an AppProvider');
    }
    return context;
};

export const useAuth = () => {
    const { auth } = useAppContext();
    return auth;
}