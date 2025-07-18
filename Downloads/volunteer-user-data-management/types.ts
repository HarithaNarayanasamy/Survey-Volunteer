export interface User {
  id: string;
  volunteerId?: string;
  status?: 'Pending' | 'Updated';
  [key: string]: any;
}

export interface LovData {
  [header: string]: string[];
}

export interface AuthenticatedUser {
    username: string;
    role: 'admin' | 'volunteer';
    volunteerId?: string;
}

export interface AppContextType {
  isFirebaseConfigured: boolean;
  isLoading: boolean;
  users: User[];
  lovData: LovData;
  updateUsers: (users: User[]) => Promise<void>;
  updateSingleUser: (user: User) => Promise<void>;
  updateLovData: (lovData: LovData) => Promise<void>;
  auth: {
    user: AuthenticatedUser | null;
    login: (username: string, volunteerId?: string) => void;
    logout: () => void;
  };
  volunteers: string[];
}