
import { User, LovData } from '../types';

declare var XLSX: any; // Using XLSX from CDN

const VOLUNTEER_CHUNK_SIZE = 40;

export const parseUsers = async (data: ArrayBuffer): Promise<User[]> => {
    const workbook = XLSX.read(data, { type: 'array' });
    const sheetName = workbook.SheetNames[0];
    if (!sheetName) {
        throw new Error("Excel file contains no sheets.");
    }
    const worksheet = workbook.Sheets[sheetName];
    if (!worksheet) {
        throw new Error(`Sheet "${sheetName}" not found or is empty.`);
    }
    
    // Use `raw: false` to get formatted strings (e.g. for dates)
    // Use `defval: ""` to prevent undefined/null for empty cells
    const json: Omit<User, 'id' | 'volunteerId' | 'status'>[] = XLSX.utils.sheet_to_json(worksheet, { raw: false, defval: "" });
    
    if (json.length === 0) {
        return [];
    }
    
    const timestamp = Date.now().toString(36);
    const usersWithIds: User[] = json.map((user, index) => ({
        ...user,
        id: `${timestamp}-${index.toString(36)}`, // e.g., 'kzik1a-0'
        volunteerId: `V${Math.floor(index / VOLUNTEER_CHUNK_SIZE) + 1}`,
        status: 'Pending',
    }));
    return usersWithIds;
};

export const parseLov = async (data: ArrayBuffer): Promise<LovData> => {
    const workbook = XLSX.read(data, { type: 'array' });
    const sheetName = workbook.SheetNames[0];
    if (!sheetName) {
        throw new Error("Excel file contains no sheets.");
    }
    const worksheet = workbook.Sheets[sheetName];
    if (!worksheet) {
        throw new Error(`Sheet "${sheetName}" not found or is empty.`);
    }
    const json: any[][] = XLSX.utils.sheet_to_json(worksheet, { header: 1 });

    if (json.length === 0 || json[0].length === 0) {
        return {};
    }
    
    const headers = json[0];
    const lov: LovData = {};

    headers.forEach((header, colIndex) => {
        if(header) {
            lov[header] = json
                .slice(1)
                .map(row => row[colIndex])
                .filter(cell => cell !== null && cell !== undefined && cell !== '');
        }
    });

    return lov;
};

export const exportUsers = (users: User[], filename: string = 'updated_user_data.xlsx') => {
    // Create a new array of users, removing the internal 'id' property for export
    const usersForExport = users.map(({ id, ...rest }) => rest);

    const worksheet = XLSX.utils.json_to_sheet(usersForExport);
    const workbook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(workbook, worksheet, 'Users');
    XLSX.writeFile(workbook, filename);
};