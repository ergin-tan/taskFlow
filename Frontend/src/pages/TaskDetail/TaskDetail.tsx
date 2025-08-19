import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import './TaskDetail.css';

interface WorkTask {
  id: number;
  taskTitle: string;
  description: string;
  assignedByUserName: string;
  dueDate: string;
  priority: string;
  currentStatus: string;
}

const TaskDetail: React.FC = () => {
  const { taskId } = useParams<{ taskId: string }>();
  const [task, setTask] = useState<WorkTask | null>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const API_BASE_URL = import.meta.env.VITE_API_BASE_URL;

  useEffect(() => {
    const fetchTask = async () => {
      try {
        const response = await fetch(`${API_BASE_URL}/api/worktasks/${taskId}`);
        if (!response.ok) {
          throw new Error(`HTTP error! status: ${response.status}`);
        }
        const data: WorkTask = await response.json();
        setTask(data);
      } catch (err: any) {
        setError(err.message);
      } finally {
        setLoading(false);
      }
    };

    if (taskId) {
      fetchTask();
    }
  }, [taskId, API_BASE_URL]);

  if (loading) {
    return <div className="task-detail-container">Loading task details...</div>;
  }

  if (error) {
    return <div className="task-detail-container" style={{ color: 'red' }}>Error: {error}</div>;
  }

  if (!task) {
    return <div className="task-detail-container">Task not found.</div>;
  }

  return (
    <div className="task-detail-container">
      <h1>{task.taskTitle}</h1>
      <div className="task-detail-grid">
        <div className="task-detail-item">
          <strong>Status:</strong> {task.currentStatus}
        </div>
        <div className="task-detail-item">
          <strong>Priority:</strong> {task.priority}
        </div>
        <div className="task-detail-item">
          <strong>Assigned By:</strong> {task.assignedByUserName}
        </div>
        <div className="task-detail-item">
          <strong>Due Date:</strong> {new Date(task.dueDate).toLocaleDateString()}
        </div>
      </div>
      <div className="task-detail-description">
        <h2>Description</h2>
        <p>{task.description}</p>
      </div>
    </div>
  );
};

export default TaskDetail;
