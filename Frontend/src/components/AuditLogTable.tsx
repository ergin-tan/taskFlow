import React from 'react';

interface AuditLog {
  id: number;
  tableName: string;
  action: string;
  timestamp: string;
  userId: number | null;
  oldValues: string | null;
  newValues: string | null;
  primaryKey: string;
}

interface AuditLogTableProps {
  auditLogs: AuditLog[];
}

const AuditLogTable: React.FC<AuditLogTableProps> = ({ auditLogs }) => {
  return (
    <div className="audit-log-table-container">
      <h2>Audit Logs</h2>
      {auditLogs.length === 0 ? (
        <p>No audit logs found.</p>
      ) : (
        <table className="audit-log-table">
          <thead>
            <tr>
              <th>ID</th>
              <th>Table Name</th>
              <th>Action</th>
              <th>Timestamp</th>
              <th>User ID</th>
              <th>Primary Key</th>
              <th>Old Values</th>
              <th>New Values</th>
            </tr>
          </thead>
          <tbody>
            {auditLogs.map((log) => (
              <tr key={log.id}>
                <td>{log.id}</td>
                <td>{log.tableName}</td>
                <td>{log.action}</td>
                <td>{new Date(log.timestamp).toLocaleString()}</td>
                <td>{log.userId ?? 'N/A'}</td>
                <td>{log.primaryKey}</td>
                <td>{log.oldValues ? JSON.stringify(JSON.parse(log.oldValues), null, 2) : 'N/A'}</td>
                <td>{log.newValues ? JSON.stringify(JSON.parse(log.newValues), null, 2) : 'N/A'}</td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
};

export default AuditLogTable;
