# Product Catalog Web Application

## Overview
This is a .NET Core MVC web application designed to manage and display products with specific properties and functionalities. The application includes features for product listing, creation, editing, and deletion, along with additional capabilities such as categorization, error logging, and user authentication.

## Features

### Product Management
- **Listing Products:**
  - The application displays products based on their scheduled appearance time.
  - Products have attributes including name, creation date, creator's userId, start date, duration, price, and an associated image.
  - The system returns products that are supposed to be shown at the current time.

- **Product Categories:**
  - Products are categorized into predefined categories, created as seed data in the database.
  - Products can be filtered by category.

- **Product Details:**
  - Users can view detailed information for each product.

- **Admin Privileges:**
  - Product management functionalities, such as adding, editing, and deleting, are restricted to users with the "Admin" role.

- **Product Updates Logging:**
  - Updates to products are logged, capturing update date and the responsible userId.

### Security
- **User Authentication:**
  - Identity is used for user authentication and role management.
  - Only users with the "Admin" role can perform product management operations.

### Error Handling
- **Logging Mechanism:**
  - The application implements an error logging mechanism to capture and log errors for debugging and troubleshooting.

## Architecture
- The application follows the [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html) pattern, separating concerns into layers like Domain, Application, Infrastructure, and Presentation.

## Repository Submission
- The project is hosted on a version control system such as GitHub, providing a clear history of changes and collaboration.

## Deadline
- The deadline for completing this project is set to 2 days from receiving the task description.

## Additional Considerations
- The solution prioritizes security, proper error handling, and adherence to architectural best practices.
- The use of an Object-Relational Mapper (ORM) is recommended, with Entity Framework (EF) as the preferred choice for working with MSSQL databases.
