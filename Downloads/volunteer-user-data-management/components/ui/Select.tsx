
import React from 'react';

const Select = React.forwardRef<HTMLSelectElement, React.SelectHTMLAttributes<HTMLSelectElement>>(
    ({ className = '', children, ...props }, ref) => {
        return (
            <select
                className={`block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500 sm:text-sm ${className}`}
                ref={ref}
                {...props}
            >
                {children}
            </select>
        );
    }
);

Select.displayName = 'Select';
export default Select;
