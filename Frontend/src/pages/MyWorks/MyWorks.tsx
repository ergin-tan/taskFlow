import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './MyWorks.css';

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

const taskStatusOptions = [
    "Assigned",
    "InProgress",
    "WaitingForApproval",
    "Completed",
    "Canceled",
    "OnHold",
];

const kanbanColumns = [
    { status: "Assigned", title: "To Do" },
    { status: "InProgress", title: "In Progress" },
    { status: "WaitingForApproval", title: "Waiting Approval" },
    { status: "Completed", title: "Completed" },
];

const priorityOptions = ["Low", "Medium", "High", "Urgent"];

type BoardData = {
    [key: string]: WorkTask[];
}

const MyWorks: React.FC = () => {
    const [boardData, setBoardData] = useState<BoardData>({});
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);
    const navigate = useNavigate();

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

                const newBoardData: BoardData = {};
                kanbanColumns.forEach(column => {
                    newBoardData[column.status] = data.filter(task => task.currentStatus === column.status);
                });
                setBoardData(newBoardData);

            } catch (err: any) {
                setError(err.message);
            } finally {
                setLoading(false);
            }
        };

        fetchMyWorkTasks();
    }, [API_BASE_URL]);

    const handleDragStart = (e: React.DragEvent<HTMLDivElement>, taskId: number) => {
        e.dataTransfer.setData("taskId", taskId.toString());
    };

    const handleDrop = async (e: React.DragEvent<HTMLDivElement>, newStatus: string) => {
        e.preventDefault();
        const taskId = parseInt(e.dataTransfer.getData("taskId"));

        let taskToUpdate: WorkTask | undefined;
        let oldStatus: string | undefined;

        for (const status in boardData) {
            const task = boardData[status].find(t => t.id === taskId);
            if (task) {
                taskToUpdate = task;
                oldStatus = status;
                break;
            }
        }

        if (!taskToUpdate || !oldStatus || oldStatus === newStatus) {
            return;
        }

        try {
            const storedUser = localStorage.getItem("user");
            if (!storedUser) {
                throw new Error("User not logged in.");
            }
            const user: UserProfile = JSON.parse(storedUser);
            const userId = user.id;

            const dto = {
                taskTitle: taskToUpdate.taskTitle,
                description: taskToUpdate.description,
                assignedBy: userId,
                dueDate: taskToUpdate.dueDate,
                priority: priorityOptions.indexOf(taskToUpdate.priority),
                currentStatus: taskStatusOptions.indexOf(newStatus),
                completedAt: newStatus === 'Completed' ? new Date().toISOString() : null,
                isArchived: false
            };

            const response = await fetch(`${API_BASE_URL}/api/worktasks/${taskId}`, {
                method: "PUT",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(dto),
            });

            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }

            const newBoardData = { ...boardData };
            const taskIndex = newBoardData[oldStatus].findIndex(t => t.id === taskId);
            const updatedTask = { ...taskToUpdate, currentStatus: newStatus };
            newBoardData[oldStatus].splice(taskIndex, 1);
            newBoardData[newStatus] = [...newBoardData[newStatus], updatedTask];

            setBoardData(newBoardData);

        } catch (err: any) {
            setError(err.message);
        }
    };

    const handleDragOver = (e: React.DragEvent<HTMLDivElement>) => {
        e.preventDefault();
    };

    const handleCardClick = (taskId: number) => {
        navigate(`/task/${taskId}`);
    };

    const getPriorityClass = (priority: string) => {
        switch (priority) {
            case 'Urgent': return 'priority-urgent';
            case 'High': return 'priority-high';
            case 'Medium': return 'priority-medium';
            case 'Low': return 'priority-low';
            default: return '';
        }
    };

    const formatDate = (dateString: string) => {
        const date = new Date(dateString);
        const now = new Date();
        const isOverdue = date < now;
        return {
            text: new Date(dateString).toLocaleDateString(),
            isOverdue
        };
    };

    if (loading) {
        return <div className="my-works-container">Loading your work tasks...</div>;
    }

    if (error) {
        return <div className="my-works-container" style={{ color: 'red' }}>Error: {error}</div>;
    }

    return (
        <div className="my-works-container">
            <h1>My Works</h1>
            <div className="kanban-board">
                {kanbanColumns.map(column => (
                    <div
                        key={column.status}
                        className="kanban-column"
                        onDrop={(e) => handleDrop(e, column.status)}
                        onDragOver={handleDragOver}
                    >
                        <h2>{column.title}</h2>
                        <div className="kanban-column-content">
                            {boardData[column.status]?.map(task => {
                                const formattedDate = formatDate(task.dueDate);
                                return (
                                    <div
                                        key={task.id}
                                        className={`kanban-card ${getPriorityClass(task.priority)}`}
                                        draggable
                                        onDragStart={(e) => handleDragStart(e, task.id)}
                                        onClick={() => handleCardClick(task.id)}
                                    >
                                        <div className="card-title">{task.taskTitle}</div>
                                        <div className="card-details">
                                            <span className={`card-priority ${getPriorityClass(task.priority)}`}>
                                                {task.priority}
                                            </span>
                                            <span className={`card-due-date ${formattedDate.isOverdue ? 'overdue' : ''}`}>
                                                {formattedDate.text}
                                            </span>
                                        </div>
                                    </div>
                                );
                            })}
                        </div>
                    </div>
                ))}
            </div>
        </div>
    );
};

export default MyWorks;