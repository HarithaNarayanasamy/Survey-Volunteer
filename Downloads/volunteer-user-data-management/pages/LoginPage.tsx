import React, { useState, useEffect } from 'react';
import { useNavigate, useLocation } from 'react-router-dom';
import { useAuth, useAppContext } from '../contexts/AppContext';
import Card from '../components/ui/Card';
import Input from '../components/ui/Input';
import Button from '../components/ui/Button';
import { LogIn } from 'lucide-react';
import Select from '../components/ui/Select';

const LoginPage: React.FC = () => {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [selectedVolunteer, setSelectedVolunteer] = useState('');
  const [error, setError] = useState('');
  const navigate = useNavigate();
  const location = useLocation();
  const { login } = useAuth();
  const { volunteers } = useAppContext();

  useEffect(() => {
    const prefilled = location.state?.prefilledVolunteer;
    if (prefilled && volunteers.includes(prefilled)) {
      setUsername('Volunteer'); // Set username to show the dropdown
      setSelectedVolunteer(prefilled);
    }
  }, [location.state, volunteers]);


  const handleLogin = (e: React.FormEvent) => {
    e.preventDefault();
    setError('');

    if (username.toLowerCase() === 'superadmin' && password === 'SuperAdmin') {
        login(username);
        navigate('/admin');
    } else if (username.toLowerCase() === 'volunteer' && password === 'Volunteer') {
        if (!selectedVolunteer) {
            setError('Please select your Volunteer ID.');
            return;
        }
        login(username, selectedVolunteer);
        navigate(`/volunteer/${selectedVolunteer}`);
    } else {
      setError('Invalid username or password.');
    }
  };

  return (
    <div className="flex items-center justify-center min-h-[calc(100vh-128px)]">
      <Card title="Login" className="w-full max-w-md">
        <form onSubmit={handleLogin} className="space-y-6">
          <div>
            <label htmlFor="username" className="block text-sm font-medium text-gray-700">Username</label>
            <Input
              id="username"
              type="text"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
              required
              className="mt-1"
              placeholder="SuperAdmin or Volunteer"
              autoComplete="username"
            />
          </div>
          <div>
            <label htmlFor="password" className="block text-sm font-medium text-gray-700">Password</label>
            <Input
              id="password"
              type="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              required
              className="mt-1"
              placeholder="Password"
              autoComplete="current-password"
            />
          </div>
          
          {username.toLowerCase() === 'volunteer' && (
            <div>
              <label htmlFor="volunteer-id" className="block text-sm font-medium text-gray-700">Volunteer ID</label>
              <Select
                  id="volunteer-id"
                  value={selectedVolunteer}
                  onChange={(e) => setSelectedVolunteer(e.target.value)}
                  required
                  className="mt-1"
                  disabled={volunteers.length === 0}
              >
                  <option value="" disabled>
                      {volunteers.length > 0 ? "Select your ID" : "No volunteers assigned"}
                  </option>
                  {volunteers.map(v => <option key={v} value={v}>{v}</option>)}
              </Select>
            </div>
          )}

          {error && <p className="text-sm text-red-600">{error}</p>}
          <div>
            <Button type="submit" className="w-full" leftIcon={<LogIn className="w-4 h-4"/>}>
              Sign In
            </Button>
          </div>
        </form>
      </Card>
    </div>
  );
};

export default LoginPage;