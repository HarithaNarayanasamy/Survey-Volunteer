import React from 'react';
import { HashRouter, Routes, Route, Navigate } from 'react-router-dom';
import { AppProvider, useAuth, useAppContext } from './contexts/AppContext';
import LoginPage from './pages/LoginPage';
import SuperAdminDashboard from './pages/SuperAdminDashboard';
import VolunteerDashboard from './pages/VolunteerDashboard';
import UserFormPage from './pages/UserFormPage';
import { ToastProvider } from './hooks/useToast';
import Header from './components/Header';
import SetupPage from './pages/SetupPage';

const AppRoutes: React.FC = () => {
    const { user } = useAuth();
    const { isLoading, isFirebaseConfigured } = useAppContext();

    if (!isFirebaseConfigured) {
        return <SetupPage />;
    }

    if (isLoading) {
        return (
            <div className="flex items-center justify-center h-screen">
                <div className="animate-spin rounded-full h-16 w-16 border-t-2 border-b-2 border-blue-500"></div>
                <p className="ml-4 text-gray-600">Connecting to database...</p>
            </div>
        );
    }

    return (
        <div className="min-h-screen bg-gray-50 text-gray-800">
            <Header />
            <main className="p-4 sm:p-6 lg:p-8">
                <Routes>
                    <Route path="/login" element={!user ? <LoginPage /> : <Navigate to={user.role === 'admin' ? '/admin' : `/volunteer/${user.volunteerId || 'V1'}`} />} />
                    
                    <Route path="/admin" element={user?.role === 'admin' ? <SuperAdminDashboard /> : <Navigate to="/login" />} />
                    
                    <Route path="/volunteer/:volunteerId" element={user ? <VolunteerDashboard /> : <Navigate to="/login" />} />
                    
                    <Route path="/f/:userId" element={<UserFormPage />} />
                    
                    <Route path="*" element={<Navigate to={user ? (user.role === 'admin' ? '/admin' : `/volunteer/${user.volunteerId || 'V1'}`) : '/login'} />} />
                </Routes>
            </main>
        </div>
    );
};

const App: React.FC = () => {
    return (
        <ToastProvider>
            <AppProvider>
                <HashRouter>
                    <AppRoutes />
                </HashRouter>
            </AppProvider>
        </ToastProvider>
    );
};

export default App;