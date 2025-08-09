import React, { useEffect, useState } from 'react';
import './MyWorks.css'; // Assuming you'll create this for specific styles

interface WorkTask {
  id: number;
  taskTitle: string;
  description: string;
  assignedByUserName: string;
  dueDate: string;
  priority: string;
  currentStatus: string;
}

interface UserProfile {
  id: number;
}

const MyWorks: React.FC = () => {
  const [myWorkTasks, setMyWorkTasks] = useState<WorkTask[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const API_BASE_URL = import.meta.env.VITE_API_BASE_URL;

  useEffect(() => {
    const fetchMyWorkTasks = async () => {
      setLoading(true);
      setError(null);
      try {
        const storedUser = localStorage.getItem('user');
        if (!storedUser) {
          throw new Error('User not logged in.');
        }
        const user: UserProfile = JSON.parse(storedUser);
        const userId = user.id;

        const response = await fetch(`${API_BASE_URL}/api/worktasks?userId=${userId}`);
        if (!response.ok) {
          throw new Error(`HTTP error! status: ${response.status}`);
        }
        const data: WorkTask[] = await response.json();
        setMyWorkTasks(data);
      } catch (err: any) {
        setError(err.message);
      } finally {
        setLoading(false);
      }
    };

    fetchMyWorkTasks();
  }, [API_BASE_URL]);

  if (loading) {
    return <div className="my-works-container">Loading your work tasks...</div>;
  }

  if (error) {
    return <div className="my-works-container" style={{ color: 'red' }}>Error: {error}</div>;
  }

  return (
    <div className="my-works-container">
      <h1>My Works</h1>
      {myWorkTasks.length === 0 ? (
        <p>No work tasks assigned by you.</p>
      ) : (
        <table style={{ width: '100%' }}>
          <thead>
            <tr>
              <th>ID</th>
              <th>Title</th>
              <th>Description</th>
              <th>Assigned By</th>
              <th>Due Date</th>
              <th>Priority</th>
              <th>Status</th>
            </tr>
          </thead>
          <tbody>
            {myWorkTasks.map((task) => (
              <tr key={task.id}>
                <td>{task.id}</td>
                <td>{task.taskTitle}</td>
                <td>{task.description}</td>
                <td>{task.assignedByUserName}</td>
                <td>{new Date(task.dueDate).toLocaleDateString()}</td>
                <td>{task.priority}</td>
                <td>{task.currentStatus}</td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
};

export default MyWorks;
