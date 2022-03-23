# Golang - React Todo App

The project is designed as an api-client application. The api side is built with .Net6. MSSql is selected as the database. The client side is created with React.

You can build the pojects with Dockerfiles (Please be sure that command running in same directory of Dockerfile).

```bash
DOTNET-REACT-EXAPMLE/UI> docker build -t case-ui .
DOTNET-REACT-EXAPMLE/UI> docker run -p 3000:3000 case-ui
```

```bash
DOTNET-REACT-EXAPMLE/API> docker build -t case-api .
DOTNET-REACT-EXAPMLE/API> docker run -p 7295:7295 case-api
```

## Usage

You can also run and use the projects separately.
For the api project, it is necessary to delete the migration file inside and migrate again in an environment with sql server installed on it.

```bash
Remove-migration
add-migration <your_migration_name>
Update-datase
```

For the React project, it will be sufficient to install npm packages in the same directory as the package.json file.

```bash
npm install
```

Then you can open the project with the `npm start` command and access the application from `http://localhost:3000`.

Please contact the repository owner for any necessary questions.
