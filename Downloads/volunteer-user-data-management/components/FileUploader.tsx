import React, { useState, useCallback } from 'react';
import { UploadCloud, File, X } from 'lucide-react';
import Button from './ui/Button';
import Card from './ui/Card';

interface FileUploaderProps {
  onFileUpload: (file: File) => void;
  title: string;
  acceptedFileTypes?: string;
}

const FileUploader: React.FC<FileUploaderProps> = ({ onFileUpload, title, acceptedFileTypes = ".xlsx, .xls" }) => {
  const [file, setFile] = useState<File | null>(null);
  const [isDragging, setIsDragging] = useState(false);
  
  // Generate a unique ID from the title to avoid collisions
  const fileInputId = `file-upload-${title.toLowerCase().replace(/[^a-z0-9]/g, '-')}`;

  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    if (e.target.files && e.target.files[0]) {
      setFile(e.target.files[0]);
    }
  };
  
  const handleDragEnter = (e: React.DragEvent<HTMLDivElement>) => {
    e.preventDefault();
    e.stopPropagation();
    setIsDragging(true);
  };
  
  const handleDragLeave = (e: React.DragEvent<HTMLDivElement>) => {
    e.preventDefault();
    e.stopPropagation();
    setIsDragging(false);
  };
  
  const handleDragOver = (e: React.DragEvent<HTMLDivElement>) => {
    e.preventDefault();
    e.stopPropagation();
  };
  
  const handleDrop = (e: React.DragEvent<HTMLDivElement>) => {
    e.preventDefault();
    e.stopPropagation();
    setIsDragging(false);
    if (e.dataTransfer.files && e.dataTransfer.files[0]) {
      setFile(e.dataTransfer.files[0]);
    }
  };

  const handleUploadClick = () => {
    if (file) {
      onFileUpload(file);
      setFile(null); // Clear after upload
    }
  };

  return (
    <Card title={title}>
      <div 
        className={`flex flex-col items-center justify-center p-6 border-2 border-dashed rounded-lg transition-colors ${isDragging ? 'border-blue-500 bg-blue-50' : 'border-gray-300'}`}
        onDragEnter={handleDragEnter}
        onDragLeave={handleDragLeave}
        onDragOver={handleDragOver}
        onDrop={handleDrop}
      >
        {!file ? (
          <>
            <UploadCloud className="w-12 h-12 text-gray-400" />
            <p className="mt-2 text-sm text-gray-600">
              <label htmlFor={fileInputId} className="font-medium text-blue-600 hover:text-blue-500 cursor-pointer">
                Choose a file
                <input id={fileInputId} name={fileInputId} type="file" className="sr-only" onChange={handleFileChange} accept={acceptedFileTypes} />
              </label>
              {' '}or drag and drop
            </p>
            <p className="text-xs text-gray-500">XLSX or XLS files</p>
          </>
        ) : (
          <div className="text-center">
            <File className="w-12 h-12 text-gray-500 mx-auto" />
            <p className="mt-2 text-sm font-medium text-gray-800">{file.name}</p>
            <p className="text-xs text-gray-500">{(file.size / 1024).toFixed(2)} KB</p>
            <button onClick={() => setFile(null)} className="mt-2 text-red-500 hover:text-red-700">
                <X className="w-5 h-5 mx-auto" />
            </button>
          </div>
        )}
      </div>
      {file && (
        <Button onClick={handleUploadClick} className="w-full mt-4">
          Upload and Process
        </Button>
      )}
    </Card>
  );
};

export default FileUploader;