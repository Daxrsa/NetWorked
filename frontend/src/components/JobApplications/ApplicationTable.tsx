import React from 'react';
import "./applications.css";

const JobApplicationsTable = () => {
    return (
        <table className="job-applications-table">
            <thead>
                <tr>
                    <th>Application ID</th>
                    <th>Applicant Name</th>
                    <th>Job Title</th>
                    <th>Matching Result</th>
                </tr>
            </thead>
            <tbody>
                {/* {applications.map(application => (
                    <tr key={application.id}>
                        <td>{application.id}</td>
                        <td>{application.applicantName}</td>
                        <td>{application.jobTitle}</td>
                        <td>{application.status}</td>
                    </tr>
                ))} */}
            </tbody>
        </table>
    );
};

export default JobApplicationsTable;
