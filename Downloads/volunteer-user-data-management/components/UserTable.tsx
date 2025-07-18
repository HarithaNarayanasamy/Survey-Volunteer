
import React from 'react';
import { User } from '../types';
import Card from './ui/Card';
import StatusBadge from './ui/StatusBadge';

interface UserTableProps {
  users: User[];
  title: string;
  actionRenderer?: (user: User) => React.ReactNode;
}

const UserTable: React.FC<UserTableProps> = ({ users, title, actionRenderer }) => {
  if (users.length === 0) {
    return (
      <Card title={title}>
        <p className="text-gray-500">No user data available. Please upload a user data file.</p>
      </Card>
    );
  }

  const headers = Object.keys(users[0]).filter(key => key !== 'id');
  
  return (
    <Card title={title} className="overflow-x-auto">
      <div className="w-full">
        <table className="min-w-full divide-y divide-gray-200">
          <thead className="bg-gray-50">
            <tr>
              {headers.map(header => (
                <th key={header} scope="col" className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  {header}
                </th>
              ))}
              {actionRenderer && (
                <th scope="col" className="px-6 py-3 text-left text-xs font-medium text-gray-500 uppercase tracking-wider">
                  Action
                </th>
              )}
            </tr>
          </thead>
          <tbody className="bg-white divide-y divide-gray-200">
            {users.map(user => (
              <tr key={user.id} className="hover:bg-gray-50">
                {headers.map(header => (
                  <td key={`${user.id}-${header}`} className="px-6 py-4 whitespace-nowrap text-sm text-gray-700">
                    {header === 'status' ? <StatusBadge status={user[header]} /> : user[header]}
                  </td>
                ))}
                {actionRenderer && (
                  <td className="px-6 py-4 whitespace-nowrap text-sm font-medium">
                    {actionRenderer(user)}
                  </td>
                )}
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </Card>
  );
};

export default UserTable;
