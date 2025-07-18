import { initializeApp, type FirebaseApp } from 'firebase/app';
import {
  getFirestore,
  collection,
  getDocs,
  doc,
  setDoc,
  writeBatch,
  getDoc,
  Firestore
} from 'firebase/firestore';
import { firebaseConfig } from '../firebaseConfig'; // ✅ Use local module import
import { User, LovData } from '../types';

let db: Firestore | undefined;
export let isFirebaseInitialized = false;

try {
  if (firebaseConfig?.apiKey) {
    const app: FirebaseApp = initializeApp(firebaseConfig);
    db = getFirestore(app);
    isFirebaseInitialized = true;
  } else {
    console.warn("Firebase config is missing or incomplete.");
  }
} catch (e) {
  console.error("Firebase initialization failed:", e);
  isFirebaseInitialized = false;
}

const USERS_COLLECTION = 'users';
const CONFIG_COLLECTION = 'config';
const LOV_DOC = 'lovMaster';

const ensureDb = () => {
  if (!db) throw new Error("Database not initialized. Please check your Firebase configuration.");
  return db;
};

// Fetch all users from Firestore
export const fetchUsers = async (): Promise<User[]> => {
  const db = ensureDb();
  const usersCollection = collection(db, USERS_COLLECTION);
  const userSnapshot = await getDocs(usersCollection);
  return userSnapshot.docs.map(doc => doc.data() as User);
};

// Fetch the LOV data object from Firestore
export const fetchLovData = async (): Promise<LovData | null> => {
  const db = ensureDb();
  const docRef = doc(db, CONFIG_COLLECTION, LOV_DOC);
  const docSnap = await getDoc(docRef);
  return docSnap.exists() ? (docSnap.data() as LovData) : null;
};

// Batch upload/overwrite all users
export const uploadUsers = async (users: User[]): Promise<void> => {
  const db = ensureDb();
  const batch = writeBatch(db);
  users.forEach(user => {
    const userRef = doc(db, USERS_COLLECTION, user.id);
    batch.set(userRef, user);
  });
  await batch.commit();
};

// Upload/overwrite the LOV data
export const uploadLovData = async (lovData: LovData): Promise<void> => {
  const db = ensureDb();
  const lovRef = doc(db, CONFIG_COLLECTION, LOV_DOC);
  await setDoc(lovRef, lovData);
};

// Update a single user document
export const updateUser = async (user: User): Promise<void> => {
  const db = ensureDb();
  const userRef = doc(db, USERS_COLLECTION, user.id);
  await setDoc(userRef, user, { merge: true }); // Use merge to avoid overwriting fields
};
