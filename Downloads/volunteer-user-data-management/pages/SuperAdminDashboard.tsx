import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAppContext } from '../contexts/AppContext';
import { parseUsers, parseLov, exportUsers } from '../services/excelService';
import FileUploader from '../components/FileUploader';
import UserTable from '../components/UserTable';
import Card from '../components/ui/Card';
import Button from '../components/ui/Button';
import Select from '../components/ui/Select';
import { useToast } from '../hooks/useToast';
import { Download, LogIn } from 'lucide-react';

const SuperAdminDashboard: React.FC = () => {
    const { users, volunteers, auth, updateUsers, updateLovData, isLoading } = useAppContext();
    const { addToast } = useToast();
    const navigate = useNavigate();
    const [selectedVolunteer, setSelectedVolunteer] = useState<string>('');
    
    const handleUserDataUpload = async (file: File) => {
        try {
            const fileData = await file.arrayBuffer();
            const parsedUsers = await parseUsers(fileData);
            if (parsedUsers.length > 0) {
                await updateUsers(parsedUsers);
                addToast('User data uploaded to the cloud successfully!', 'success');
            } else {
                addToast('User data file has no data rows.', 'info');
            }
        } catch (error) {
            console.error(error);
            addToast((error as Error).message || 'Failed to process user data file.', 'error');
        }
    };

    const handleLovDataUpload = async (file: File) => {
        try {
            const fileData = await file.arrayBuffer();
            const parsedLov = await parseLov(fileData);
            if (Object.keys(parsedLov).length > 0) {
                await updateLovData(parsedLov);
                addToast('LOV Master data uploaded to the cloud successfully!', 'success');
            } else {
                addToast('LOV Master file has no data.', 'info');
            }
        } catch (error) {
            console.error(error);
            addToast((error as Error).message || 'Failed to process LOV Master file.', 'error');
        }
    };

    const handleExport = () => {
        if(users.length > 0) {
            exportUsers(users);
            addToast('User data exported.', 'success');
        } else {
            addToast('No user data to export.', 'info');
        }
    };
    
    const handleProceedToVolunteerLogin = () => {
        if (selectedVolunteer) {
            auth.logout();
            navigate('/login', { state: { prefilledVolunteer: selectedVolunteer } });
        } else {
            addToast('Please select a volunteer to continue.', 'info');
        }
    };

    React.useEffect(() => {
        if (volunteers.length > 0 && !selectedVolunteer) {
            setSelectedVolunteer(volunteers[0]);
        }
    }, [volunteers, selectedVolunteer]);

    return (
        <div className="space-y-8">
            <h2 className="text-3xl font-bold text-gray-900">SuperAdmin Dashboard</h2>
            
            {isLoading && (
                 <div className="fixed inset-0 bg-gray-500 bg-opacity-75 flex items-center justify-center z-50">
                    <div className="animate-spin rounded-full h-32 w-32 border-t-2 border-b-2 border-white"></div>
                </div>
            )}

            <div className="grid grid-cols-1 lg:grid-cols-2 gap-8">
                <FileUploader title="1. Upload User Data" onFileUpload={handleUserDataUpload} />
                <FileUploader title="2. Upload LOV Master" onFileUpload={handleLovDataUpload} />
            </div>

            <Card title="Volunteer Portal Access" actions={
                <Button onClick={handleExport} leftIcon={<Download className="w-4 h-4"/>}>Export User Data</Button>
            }>
                <p className="mb-4 text-sm text-gray-600">Select a volunteer and proceed to the login page to access their dashboard.</p>
                <div className="flex items-center space-x-4">
                    <Select 
                        value={selectedVolunteer} 
                        onChange={e => setSelectedVolunteer(e.target.value)}
                        className="flex-grow"
                        disabled={volunteers.length === 0}
                        aria-label="Select a Volunteer"
                    >
                         <option value="" disabled>Select a Volunteer</option>
                         {volunteers.map(v => <option key={v} value={v}>{v}</option>)}
                    </Select>
                    <Button onClick={handleProceedToVolunteerLogin} disabled={!selectedVolunteer} leftIcon={<LogIn className="w-4 h-4"/>}>
                        Proceed to Volunteer Login
                    </Button>
                </div>
            </Card>

            <UserTable users={users} title="All Users" />
        </div>
    );
};

export default SuperAdminDashboard;
