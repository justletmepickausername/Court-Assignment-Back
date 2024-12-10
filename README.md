
# .NET 8 Minimal API - Contact Form Backend

This is the backend of the Contact Form application, built using .NET 8 Minimal API. It provides CRUD operations for managing contact form submissions from the Angular frontend.

## Table of Contents

- [Features](#features)
- [Requirements](#requirements)
- [Installation](#installation)
- [Usage](#usage)
- [API Endpoints](#api-endpoints)
- [Deployment](#deployment)
- [Versioning](#versioning)
- [Contributing](#contributing)
- [License](#license)

---

## Features

- CRUD operations (Create, Read, Update, Delete) for managing contact form submissions
- Built using .NET 8 Minimal API
- Lightweight and fast API implementation
- Supports easy integration with an Angular frontend

---

## Requirements

Before you begin, ensure you have met the following requirements:

- **.NET 8 SDK**: [Download .NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- **SQL Database** (e.g., SQL Server, PostgreSQL, MySQL) for storing contact form submissions
- **IDE**: Visual Studio, Visual Studio Code, or any other text editor of your choice

---

## Installation

Follow these steps to set up the project locally:

1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/your-dotnet-api.git
   ```

2. Navigate to the project directory:
   ```bash
   cd your-dotnet-api
   ```

3. Restore the project dependencies:
   ```bash
   dotnet restore
   ```

4. Configure your database connection (if applicable) in `appsettings.json`.

---

## Usage

To run the application locally:

```bash
dotnet run
```

By default, the API will be accessible at `https://localhost:5001` (for HTTPS) or `http://localhost:5000` (for HTTP). You can test it using Postman, Swagger, or any HTTP client of your choice.

---

## API Endpoints

- **GET** `/api/contact`  
  Fetches all contact form submissions.

- **GET** `/api/contact/{id}`  
  Fetches a contact form submission by its ID.

- **POST** `/api/contact`  
  Creates a new contact form submission.

- **PUT** `/api/contact/{id}`  
  Updates an existing contact form submission.

- **DELETE** `/api/contact/{id}`  
  Deletes a contact form submission by its ID.

---

## Deployment

To deploy the API to production:

1. Build the project for release:
   ```bash
   dotnet publish -c Release -o ./publish
   ```

2. Deploy the contents of the `publish/` folder to your server or cloud service (e.g., Azure, AWS, etc.).

For more details on deploying .NET APIs, refer to the [official documentation](https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/?view=aspnetcore-8.0).

---

## Versioning

We use [SemVer](https://semver.org/) for versioning. For the list of versions, see the [releases](https://github.com/your-username/your-dotnet-api/releases) page.

- **1.0.0**: Initial release
- **1.1.0**: Added support for contact form update

---

## Contributing

We welcome contributions to this project! If you'd like to contribute:

1. Fork the repository.
2. Create a new branch (`git checkout -b feature-branch`).
3. Make your changes and commit them (`git commit -m 'Add new feature'`).
4. Push to your branch (`git push origin feature-branch`).
5. Create a pull request.

---

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
