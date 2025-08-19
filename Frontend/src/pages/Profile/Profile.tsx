import React, { useEffect, useState } from 'react';
import './Profile.css';

interface UserProfile {
  id: number;
  firstName: string;
  lastName: string;
  employeeID: string;
  title: string;
  officeName: string;
}

const Profile: React.FC = () => {
  const [user, setUser] = useState<UserProfile | null>(null);

  useEffect(() => {
    const storedUser = localStorage.getItem('user');
    if (storedUser) {
      setUser(JSON.parse(storedUser));
    }
  }, []);

  if (!user) {
    return <div className="profile-container">Loading profile...</div>;
  }

  return (
    <div className="profile-container">
      <h1>User Profile</h1>
      <div className="profile-details">
        <p><strong>Name:</strong> {user.firstName} {user.lastName}</p>
        <p><strong>Employee ID:</strong> {user.employeeID}</p>
        <p><strong>Title:</strong> {user.title}</p>
        <p><strong>Office:</strong> {user.officeName}</p>
      </div>
    </div>
  );
};

export default Profile;
