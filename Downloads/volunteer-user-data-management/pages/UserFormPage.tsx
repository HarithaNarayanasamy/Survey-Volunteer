import React, { useState, useEffect, useMemo } from 'react';
import { useParams } from 'react-router-dom';
import { useAppContext } from '../contexts/AppContext';
import Card from '../components/ui/Card';
import Input from '../components/ui/Input';
import Select from '../components/ui/Select';
import Button from '../components/ui/Button';
import { useToast } from '../hooks/useToast';
import { Save } from 'lucide-react';
import { User } from '../types';

const UserFormPage: React.FC = () => {
    const { userId } = useParams<{ userId: string }>();
    const { users, lovData, updateSingleUser } = useAppContext();
    const { addToast } = useToast();
    
    // Find the user from the centrally managed state
    const user = useMemo(() => users.find(u => u.id === userId), [users, userId]);
    
    const [formData, setFormData] = useState<User | null>(null);
    const [isSaving, setIsSaving] = useState(false);

    useEffect(() => {
        if (user) {
            setFormData(user);
        }
    }, [user]);

    const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
        const { name, value } = e.target;
        setFormData((prev) => prev ? { ...prev, [name]: value } : null);
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        if (!formData) return;
        
        setIsSaving(true);
        try {
            const updatedUserData = { ...formData, status: 'Updated' as const };
            await updateSingleUser(updatedUserData);
            addToast('Your information has been saved successfully!', 'success');
        } catch (error) {
            console.error(error);
            addToast('Could not save your information. Please try again.', 'error');
        } finally {
            setIsSaving(false);
        }
    };

    // If data has loaded but this specific user wasn't found
    if (users.length > 0 && !user) {
        return (
            <Card title="Error">
                <p className="text-red-500">User not found. The link may be invalid or expired.</p>
            </Card>
        );
    }

    // Still waiting for initial data load or this user's data
    if (!formData) {
        return (
            <div className="flex items-center justify-center min-h-[calc(100vh-128px)]">
                <div className="animate-spin rounded-full h-16 w-16 border-t-2 border-b-2 border-blue-500"></div>
                <p className="ml-4 text-gray-600">Loading user data...</p>
            </div>
        );
    }
    
    const formFields = Object.keys(formData).filter(key => key !== 'id' && key !== 'volunteerId' && key !== 'status');

    return (
        <div className="max-w-2xl mx-auto">
            <Card title="Update Your Details">
                <p className="mb-6 text-sm text-gray-600">Please review and update your information below. Your changes will be saved to our central database.</p>
                <form onSubmit={handleSubmit} className="space-y-4">
                    {formFields.map(field => {
                        const isDropdown = lovData && field in lovData;
                        return (
                            <div key={field}>
                                <label htmlFor={field} className="block text-sm font-medium text-gray-700 capitalize">
                                    {field.replace(/_/g, ' ')}
                                </label>
                                {isDropdown ? (
                                    <Select 
                                        id={field} 
                                        name={field} 
                                        value={formData[field] || ''} 
                                        onChange={handleChange}
                                        className="mt-1"
                                    >
                                        <option value="" disabled>Select an option</option>
                                        {lovData[field].map(option => (
                                            <option key={option} value={option}>{option}</option>
                                        ))}
                                    </Select>
                                ) : (
                                    <Input 
                                        id={field}
                                        name={field}
                                        type="text" 
                                        value={formData[field] || ''} 
                                        onChange={handleChange}
                                        className="mt-1"
                                    />
                                )}
                            </div>
                        );
                    })}
                    <div className="pt-4">
                        <Button type="submit" disabled={isSaving} className="w-full" leftIcon={<Save className="w-4 h-4"/>}>
                            {isSaving ? 'Saving...' : 'Save Changes'}
                        </Button>
                    </div>
                </form>
            </Card>
        </div>
    );
};

export default UserFormPage;
