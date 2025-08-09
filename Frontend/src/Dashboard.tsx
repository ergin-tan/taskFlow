import React, { useEffect, useState } from 'react';
import './Dashboard.css'; // Import the CSS file

interface User {
  id: number;
  firstName: string;
  lastName: string;
  employeeID: string;
  title: string;
  officeName: string;
}

interface WorkTask {
  id: number;
  taskTitle: string;
  description: string;
  assignedByUserName: string;
  dueDate: string;
  priority: string;
  currentStatus: string;
}

interface TaskAssignment {
  id: number;
  workTaskId: number;
  workTaskTitle: string;
  assignedTo: number;
  assignedToUserName: string;
  assignedPart: string;
  status: string;
  dueDate?: string;
  completedAt?: string;
  remarks?: string;
}

interface TaskHistory {
  id: number;
  workTaskId: number;
  workTaskTitle: string;
  changedByUserId: number;
  changedByUserName: string;
  oldStatus?: string;
  newStatus: string;
  changeDescription?: string;
}

interface Office {
  id: number;
  officeName: string;
}

const Dashboard: React.FC = () => {
  const [users, setUsers] = useState<User[]>([]);
  const [workTasks, setWorkTasks] = useState<WorkTask[]>([]);
  const [taskAssignments, setTaskAssignments] = useState<TaskAssignment[]>([]);
  const [taskHistories, setTaskHistories] = useState<TaskHistory[]>([]);
  const [offices, setOffices] = useState<Office[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  const API_BASE_URL = import.meta.env.VITE_API_BASE_URL;

  useEffect(() => {
    const fetchData = async () => {
      try {
        // Fetch Users
        const usersResponse = await fetch(`${API_BASE_URL}/api/users`);
        if (!usersResponse.ok) {
          throw new Error(`HTTP error! status: ${usersResponse.status} for users`);
        }
        const usersData: User[] = await usersResponse.json();
        setUsers(usersData);

        // Fetch Work Tasks
        const workTasksResponse = await fetch(`${API_BASE_URL}/api/worktasks`);
        if (!workTasksResponse.ok) {
          throw new Error(`HTTP error! status: ${workTasksResponse.status} for work tasks`);
        }
        const workTasksData: WorkTask[] = await workTasksResponse.json();
        setWorkTasks(workTasksData);

        // Fetch Task Assignments
        const taskAssignmentsResponse = await fetch(`${API_BASE_URL}/api/taskassignments`);
        if (!taskAssignmentsResponse.ok) {
          throw new Error(`HTTP error! status: ${taskAssignmentsResponse.status} for task assignments`);
        }
        const taskAssignmentsData: TaskAssignment[] = await taskAssignmentsResponse.json();
        setTaskAssignments(taskAssignmentsData);

        // Fetch Task Histories
        const taskHistoriesResponse = await fetch(`${API_BASE_URL}/api/taskhistories`);
        if (!taskHistoriesResponse.ok) {
          throw new Error(`HTTP error! status: ${taskHistoriesResponse.status} for task histories`);
        }
        const taskHistoriesData: TaskHistory[] = await taskHistoriesResponse.json();
        setTaskHistories(taskHistoriesData);

        // Fetch Offices
        const officesResponse = await fetch(`${API_BASE_URL}/api/offices`);
        if (!officesResponse.ok) {
          throw new Error(`HTTP error! status: ${officesResponse.status} for offices`);
        }
        const officesData: Office[] = await officesResponse.json();
        setOffices(officesData);

      } catch (err: any) {
        setError(err.message);
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, [API_BASE_URL]);

  if (loading) {
    return <div className="dashboard-container">Loading dashboard data...</div>;
  }

  if (error) {
    return <div className="dashboard-container" style={{ color: 'red' }}>Error: {error}</div>;
  }

  return (
    <div className="dashboard-container">
      <h1>TaskFlow Dashboard</h1>

      <h2>Users</h2>
      {users.length === 0 ? (
        <p>No users found.</p>
      ) : (
        <table style={{ width: '100%' }}>
          <thead>
            <tr>
              <th>ID</th>
              <th>First Name</th>
              <th>Last Name</th>
              <th>Employee ID</th>
              <th>Title</th>
              <th>Office</th>
            </tr>
          </thead>
          <tbody>
            {users.map((user) => (
              <tr key={user.id}>
                <td>{user.id}</td>
                <td>{user.firstName}</td>
                <td>{user.lastName}</td>
                <td>{user.employeeID}</td>
                <td>{user.title}</td>
                <td>{user.officeName}</td>
              </tr>
            ))}
          </tbody>
        </table>
      )}

      <h2>Work Tasks</h2>
      {workTasks.length === 0 ? (
        <p>No work tasks found.</p>
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
            {workTasks.map((task) => (
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

      <h2>Task Assignments</h2>
      {taskAssignments.length === 0 ? (
        <p>No task assignments found.</p>
      ) : (
        <table style={{ width: '100%' }}>
          <thead>
            <tr>
              <th>ID</th>
              <th>Work Task</th>
              <th>Assigned To</th>
              <th>Assigned Part</th>
              <th>Status</th>
              <th>Due Date</th>
            </tr>
          </thead>
          <tbody>
            {taskAssignments.map((assignment) => (
              <tr key={assignment.id}>
                <td>{assignment.id}</td>
                <td>{assignment.workTaskTitle}</td>
                <td>{assignment.assignedToUserName}</td>
                <td>{assignment.assignedPart}</td>
                <td>{assignment.status}</td>
                <td>{assignment.dueDate ? new Date(assignment.dueDate).toLocaleDateString() : 'N/A'}</td>
              </tr>
            ))}
          </tbody>
        </table>
      )}

      <h2>Task Histories</h2>
      {taskHistories.length === 0 ? (
        <p>No task histories found.</p>
      ) : (
        <table style={{ width: '100%' }}>
          <thead>
            <tr>
              <th>ID</th>
              <th>Work Task</th>
              <th>Changed By</th>
              <th>Old Status</th>
              <th>New Status</th>
              <th>Description</th>
            </tr>
          </thead>
          <tbody>
            {taskHistories.map((history) => (
              <tr key={history.id}>
                <td>{history.id}</td>
                <td>{history.workTaskTitle}</td>
                <td>{history.changedByUserName}</td>
                <td>{history.oldStatus || 'N/A'}</td>
                <td>{history.newStatus}</td>
                <td>{history.changeDescription || 'N/A'}</td>
              </tr>
            ))}
          </tbody>
        </table>
      )}

      <h2>Offices</h2>
      {offices.length === 0 ? (
        <p>No offices found.</p>
      ) : (
        <table style={{ width: '100%' }}>
          <thead>
            <tr>
              <th>ID</th>
              <th>Office Name</th>
            </tr>
          </thead>
          <tbody>
            {offices.map((office) => (
              <tr key={office.id}>
                <td>{office.id}</td>
                <td>{office.officeName}</td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
};

export default Dashboard;
