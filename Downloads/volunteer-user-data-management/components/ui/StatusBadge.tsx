import React from 'react';

interface StatusBadgeProps {
  status?: 'Pending' | 'Updated' | string;
}

const StatusBadge: React.FC<StatusBadgeProps> = ({ status }) => {
  if (!status) return null;

  const baseClasses = 'inline-flex items-center px-2.5 py-0.5 rounded-full text-xs font-medium capitalize';
  
  const statusStyles: { [key: string]: string } = {
    Pending: 'bg-yellow-100 text-yellow-800',
    Updated: 'bg-green-100 text-green-800',
  };

  const style = statusStyles[status] || 'bg-gray-100 text-gray-800';

  return (
    <span className={`${baseClasses} ${style}`}>
      {status}
    </span>
  );
};

export default StatusBadge;
