
import React from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../contexts/AppContext';
import Button from './ui/Button';
import { User as UserIcon, LogOut } from 'lucide-react';

const Header: React.FC = () => {
  const { user, logout } = useAuth();
  const navigate = useNavigate();

  const handleLogout = () => {
    logout();
    navigate('/login');
  };

  return (
    <header className="bg-white shadow-sm">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="flex justify-between items-center h-16">
          <div className="flex-shrink-0">
            <h1 className="text-xl font-bold text-blue-600">Data Management Portal</h1>
          </div>
          {user && (
            <div className="flex items-center space-x-4">
              <div className="flex items-center">
                <UserIcon className="h-5 w-5 text-gray-500" />
                <span className="ml-2 text-sm font-medium text-gray-700">
                  {user.role === 'admin' ? `Admin: ${user.username}` : `Volunteer: ${user.volunteerId || user.username}`}
                </span>
              </div>
              <Button onClick={handleLogout} variant="secondary" leftIcon={<LogOut className="w-4 h-4"/>}>
                Logout
              </Button>
            </div>
          )}
        </div>
      </div>
    </header>
  );
};

export default Header;
