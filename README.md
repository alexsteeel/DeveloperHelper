# DevBooster

DevBooster is a developer tool designed to streamline common development tasks. It provides functionalities such as project creation and type inspection, helping developers boost their productivity.

## Project Structure

DevBooster follows a modular architecture, consisting of a main application and several modules:

*   **DevBooster:** The main WPF application that hosts the different modules.
*   **ProjectManagementModule:** Provides functionality for creating new .NET projects.
*   **ReflectionModule:** Enables inspection and display of .NET types using reflection.
*   **BaseModule:** Contains base classes and common functionality used by other modules.

## Technical Stack

DevBooster is built using the following technologies and frameworks:

*   **.NET 5:** The core framework for the application.
*   **WPF (Windows Presentation Foundation):** Used for building the user interface.
*   **Prism:** A framework for building loosely coupled, maintainable, and testable XAML applications. It's used for modularity and dependency injection.
*   **MahApps.Metro:** A library for modern UI styling of WPF applications.
*   **Dapplo.Confluence:** For integration with Confluence.
*   **GitLabApiClient:** For integration with GitLab.
*   **Microsoft.Extensions.Configuration:** Used for managing application configuration.
*   **Microsoft.Extensions.DependencyInjection:** Used for dependency injection.

## Setup and Usage

### Prerequisites

*   .NET 5 SDK (or later)
*   Visual Studio 2019 (or later) with the .NET desktop development workload installed.

### Building the Project

1.  Clone the repository:
    ```bash
    git clone <repository-url>
    ```
2.  Open the `DevBooster.sln` solution file in Visual Studio.
3.  Build the solution (Build > Build Solution).

### Running the Application

After successfully building the project, you can run the application from Visual Studio by pressing F5 or by running the executable located in `src/DevBooster/bin/Debug/net5.0-windows/DevBooster.exe` (the exact path might vary based on your build configuration).

### Core Features

*   **Project Creation:**
    *   Navigate to the "Create Project" section (this might be a tab or a dedicated view).
    *   Fill in the required project details (e.g., project name, location, template).
    *   Click the "Create" button.
*   **Type Inspection:**
    *   Navigate to the "Reflection" or "Types" section.
    *   Load an assembly (e.g., by browsing for a DLL file).
    *   The view will display the types found in the assembly, allowing you to inspect their details.

## Testing

The project includes integration tests for the `ProjectManagementModule`. These tests verify the functionality of the .NET project creation feature.

To run the tests:

1.  Open the `DevBooster.sln` solution file in Visual Studio.
2.  Open the Test Explorer (Test > Test Explorer).
3.  Click "Run All Tests" in the Test Explorer.

## Contributing

Contributions are welcome! If you'd like to contribute to DevBooster, please follow these steps:

1.  Fork the repository.
2.  Create a new branch for your feature or bug fix:
    ```bash
    git checkout -b feature/your-feature-name
    ```
    or
    ```bash
    git checkout -b bugfix/your-bug-fix-name
    ```
3.  Make your changes and commit them with clear and descriptive messages.
4.  Push your changes to your forked repository.
5.  Create a pull request to the main repository's `main` branch (or the appropriate development branch).

Please ensure your code adheres to the existing coding style and includes tests where applicable.
