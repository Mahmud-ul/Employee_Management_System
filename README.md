# Employee_Management_System

## **Setup Instructions**

1. **Clone the Repository**:
   - Clone the GitHub repository.

2. **Database Setup**:
   - Open SQL Server Management Studio (SSMS).
   - Create a new database for the project.
   - Locate and run the database schema or script file (e.g., `DatabaseSetup.sql`) (Emailed) to set up the tables and seed data.

3. **Configuration**:
   - Open the project in Visual Studio.
   - Update the connection string under `"ConnectionStrings"`:
     
4. **Install Dependencies**:
   - Open the **Package Manager Console** or use the terminal.
   - Restore all required NuGet packages:

---

## **Project Overview and Features**

### **Overview**
This project is a web-based **Employee Management System** built using ASP.NET Core MVC. It provides a comprehensive solution for managing employee data, department data, performance reviews, and departmental performance metrics.

### **Features**
1. **Employee Management**:
   - Add, update, and delete employee information.
   - View employee details, including position, department, and joining date.
   - Dynamic filtering and sorting of employee data.
   - Filter employees by name, department, email, phone, position, score range, and active status.

2. **Performance Reviews**:
   - Record and manage performance reviews for employees.
   - View the latest performance scores.

3. **Departmental Insights**:
   - Average performance scores calculated per department.

5. **Dynamic Table Updates**:
   - Use jQuery and AJAX for dynamic filtering and real-time employee data updates.

6. **Stored Procedure Integration**:
   - SQL Server stored procedure (`sp_AvgPerformanceScorePerDepartment`) for calculating departmental performance.

---

## **Additional Notes or Optimizations**

1. **Code Quality**:
   - Follow clean code practices.

2. **Optimization Tips**:
   - Index database columns used in `WHERE` and `JOIN` clauses to improve query performance.

3. **Future Enhancements**:
   - Add user authentication and role-based access control.
   - Implement an API for external integrations.
   - Add unit tests to ensure code reliability.
  
  
