
import React, { useMemo } from 'react';
import { useParams } from 'react-router-dom';
import { useAppContext } from '../contexts/AppContext';
import UserTable from '../components/UserTable';
import Button from '../components/ui/Button';
import { useToast } from '../hooks/useToast';
import { Copy } from 'lucide-react';
import { User } from '../types';
import Card from '../components/ui/Card';

const VolunteerDashboard: React.FC = () => {
    const { volunteerId } = useParams<{ volunteerId: string }>();
    const { users } = useAppContext();
    const { addToast } = useToast();

    const assignedUsers = useMemo(() => {
        return users.filter(user => user.volunteerId === volunteerId);
    }, [users, volunteerId]);

    const handleCopyLink = (user: User) => {
        const url = new URL(window.location.href);
        url.hash = `#/f/${user.id}`;
        const link = url.toString();

        navigator.clipboard.writeText(link)
            .then(() => {
                addToast('Link copied to clipboard!', 'success');
            })
            .catch(err => {
                console.error('Failed to copy link: ', err);
                addToast('Failed to copy link.', 'error');
            });
    };

    const actionRenderer = (user: User) => (
        <Button onClick={() => handleCopyLink(user)} variant="secondary" leftIcon={<Copy className="w-4 h-4" />}>
            Copy Link
        </Button>
    );
    
    if (!volunteerId) {
        return <Card title="Error"><p>No volunteer specified.</p></Card>
    }

    return (
        <div className="space-y-6">
            <h2 className="text-3xl font-bold text-gray-900">Volunteer Dashboard: {volunteerId}</h2>
            <p className="text-gray-600">
                You are assigned {assignedUsers.length} user(s). Copy the unique link and share it with the user to allow them to update their details.
            </p>
            <UserTable 
                users={assignedUsers} 
                title={`Assigned Users for ${volunteerId}`} 
                actionRenderer={actionRenderer} 
            />
        </div>
    );
};

export default VolunteerDashboard;
