import React from 'react';
import Card from '../components/ui/Card';
import { Flame, Link } from 'lucide-react';

const SetupPage: React.FC = () => {

    const configTemplate = `
const firebaseConfig = {
  apiKey: "AIzaSy...",
  authDomain: "your-project-id.firebaseapp.com",
  projectId: "your-project-id",
  storageBucket: "your-project-id.appspot.com",
  messagingSenderId: "1234567890",
  appId: "1:1234567890:web:abcdef123456"
};
`.trim();

    return (
        <div className="flex items-center justify-center min-h-screen bg-gray-100">
            <div className="max-w-4xl mx-auto p-4">
                <Card title="Application Setup Required">
                    <div className="space-y-6 text-gray-700">
                        <div className="flex items-center space-x-3">
                            <Flame className="h-8 w-8 text-yellow-500" />
                            <h2 className="text-xl font-semibold">Connect to Firebase</h2>
                        </div>
                        <p>
                            This application requires a Firebase backend to store and manage data. Please follow these steps to get started. It's a one-time setup.
                        </p>

                        <div className="space-y-4">
                            <h3 className="font-semibold text-lg">Step 1: Create a Firebase Project</h3>
                            <p>
                                If you don't have one already, go to the{' '}
                                <a href="https://console.firebase.google.com/" target="_blank" rel="noopener noreferrer" className="text-blue-600 hover:underline">
                                    Firebase Console
                                </a>
                                {' '}and create a new project. It's free!
                            </p>

                            <h3 className="font-semibold text-lg">Step 2: Create a Web App</h3>
                            <p>
                                In your Firebase project dashboard, click the Web icon <strong>(&lt;/&gt;)</strong> to add a web application. Give it a name and register the app.
                            </p>
                            
                            <h3 className="font-semibold text-lg">Step 3: Enable Firestore Database</h3>
                            <p>
                                From the main menu, go to <strong>Build &gt; Firestore Database</strong>. Click "Create database", choose <strong>Start in test mode</strong> (important!), select a location, and enable it.
                            </p>

                            <h3 className="font-semibold text-lg">Step 4: Create `firebaseConfig.js`</h3>
                            <p>
                                After creating the web app, Firebase will give you a `firebaseConfig` object. Create a file named <strong>`firebaseConfig.js`</strong> in the same root folder as your `index.html` file. Copy and paste the code from Firebase into it. It should look like this:
                            </p>
                            <pre className="bg-gray-100 p-4 rounded-md text-sm overflow-x-auto">
                                <code>
                                    {configTemplate}
                                </code>
                            </pre>
                            
                        </div>

                        <div className="bg-blue-50 border-l-4 border-blue-400 p-4">
                            <div className="flex">
                                <div className="flex-shrink-0">
                                    <Link className="h-5 w-5 text-blue-400" aria-hidden="true" />
                                </div>
                                <div className="ml-3">
                                    <p className="text-sm text-blue-700">
                                        Once you have created the `firebaseConfig.js` file, please <strong>refresh this page</strong> to start the application.
                                    </p>
                                </div>
                            </div>
                        </div>
                    </div>
                </Card>
            </div>
        </div>
    );
};

export default SetupPage;