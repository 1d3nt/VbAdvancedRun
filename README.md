# VbAdvancedRun

**VbAdvancedRun** is a .NET 8.0 application that combines the functionality of two projects: [VbWorkerServicePinvokeLauncher](https://github.com/1d3nt/VbWorkerServicePinvokeLauncher) and [VbWorkerServiceDeployer](https://github.com/1d3nt/VbWorkerServiceDeployer). It provides a streamlined solution for launching processes under the SYSTEM account, leveraging P/Invoke to interact with Windows APIs for elevated privilege management and service installation.

## Features

### Service Compilation & Installation
[VbWorkerServicePinvokeLauncher](https://github.com/1d3nt/VbWorkerServicePinvokeLauncher) is compiled into a Windows service, and packaged as a 7z file for easy extraction and deployment. The installation process is powered by the code from [VbWorkerServiceDeployer](https://github.com/1d3nt/VbWorkerServiceDeployer).

### Process Launching as SYSTEM
Once installed, VbAdvancedRun writes the process to be launched to a configuration file. Upon starting the service, the application launches the specified process with SYSTEM-level privileges.

### Seamless Service Management
VbAdvancedRun simplifies service installation and management by utilizing dependency injection and adhering to the Single Responsibility Principle, making the codebase easy to maintain and extend.

## Key Components

### P/Invoke for Elevated Privileges
Utilizes P/Invoke to interact with Windows APIs, allowing processes to be launched with elevated privileges or under different user contexts.

### Dependency Injection
Implements a flexible, testable architecture through dependency injection.

### Single Responsibility Principle
Adheres to clean code practices to ensure that each class has a distinct responsibility, enhancing maintainability.

### Configuration
All settings, including the process to be launched, are configured via `appsettings.json`. The configuration is read by the service, which launches the specified process with the necessary privileges.

# Directory Structure

```
|-- Application
|   |-- Interfaces
|   |   |-- IAppRunner.vb
|   |   +-- IServiceConfigurator.vb
|   |-- AppRunner.vb
|   +-- ServiceConfigurator.vb
|-- CoreServices
|   |-- Configuration
|   |   |-- Interfaces
|   |   |   |-- IAppsettingsManager.vb
|   |   |   |-- IAppSettingsReader.vb
|   |   |   +-- IAppsettingsWriter.vb
|   |   |-- AppSettingsManager.vb
|   |   |-- AppSettingsReader.vb
|   |   +-- AppsettingsWriter.vb
|   |-- FileSystem
|   |   |-- Interfaces
|   |   |   |-- IAppDataManager.vb
|   |   |   +-- IDirectoryManager.vb
|   |   |-- AppDataManager.vb
|   |   +-- DirectoryManager.vb
|   |-- ProcessManagement
|   |   +-- Interfaces
|   +-- ServiceDeployment
|       +-- VbWorkerServiceDeployer
|           |-- ServiceDeployerApplication
|           |   |-- Interfaces
|           |   |   +-- IServiceDeploymentAppRunner.vb
|           |   +-- ServiceDeploymentAppRunner.vb
|           |-- ServiceDeployerCoreServices
|           |   |-- ServiceManagement
|           |   |   |-- Interfaces
|           |   |   |   |-- IServiceControlManager.vb
|           |   |   |   |-- IServiceCreator.vb
|           |   |   |   |-- IServiceDeleter.vb
|           |   |   |   |-- IServiceInstaller.vb
|           |   |   |   |-- IServicePathProvider.vb
|           |   |   |   |-- IServiceStarter.vb
|           |   |   |   |-- IServiceStatusChecker.vb
|           |   |   |   |-- IServiceStopper.vb
|           |   |   |   +-- IServiceUninstaller.vb
|           |   |   |-- ServiceControlManager.vb
|           |   |   |-- ServiceCreator.vb
|           |   |   |-- ServiceDeleter.vb
|           |   |   |-- ServiceInstaller.vb
|           |   |   |-- ServicePathProvider.vb
|           |   |   |-- ServiceStarter.vb
|           |   |   |-- ServiceStatusChecker.vb
|           |   |   |-- ServiceStopper.vb
|           |   |   +-- ServiceUninstaller.vb
|           |   +-- WindowsApiInterop
|           |       |-- Enums
|           |       |   |-- DesiredAccess.vb
|           |       |   |-- ServiceControlCodes.vb
|           |       |   |-- ServiceErrorControlFlags.vb
|           |       |   |-- ServiceManagerAccessFlags.vb
|           |       |   |-- ServiceType.vb
|           |       |   |-- StartType.vb
|           |       |   +-- Win32ErrorCodes.vb
|           |       |-- Methods
|           |       |   |-- HandleManager.vb
|           |       |   |-- MemoryManager.vb
|           |       |   +-- NativeMethods.vb
|           |       |-- Structs
|           |       |   +-- ServiceStatus.vb
|           |       +-- ExternDll.vb
|           +-- ServiceDeployerUtilities
|               |-- ServiceDeployerErrorHandling
|               |   |-- Interfaces
|               |   |   |-- IErrorHandlingService.vb
|               |   |   |-- IWin32ErrorHelper.vb
|               |   |   +-- IWin32ErrorUtility.vb
|               |   |-- ErrorHandlingService.vb
|               |   |-- Win32ErrorHelper.vb
|               |   +-- Win32ErrorUtility.vb
|               +-- AsynchronousProcessor.vb
|-- My Project
|   +-- Resources.Designer.vb
|-- Resources
|   +-- CompiledServices.7z
|-- Utilities
|   |-- ErrorHandling
|   |   |-- Interfaces
|   |   |   |-- IExitUtility.vb
|   |   |   +-- IFailureHandler.vb
|   |   |-- ExitUtility.vb
|   |   +-- FailureHandler.vb
|   |-- Extractor
|   |   |-- Interfaces
|   |   |   |-- IArchiveExtractor.vb
|   |   |   |-- IExtractorService.vb
|   |   |   +-- IResourceExtractor.vb
|   |   |-- ArchiveExtractor.vb
|   |   |-- ExtractorService.vb
|   |   |-- ResourceExtractor.vb
|   |   +-- ResourceHelper.vb
|   |-- Interfaces
|   |   |-- IProgramPathValidator.vb
|   |   |-- IUserInputChecker.vb
|   |   |-- IUserInputReader.vb
|   |   +-- IUserPrompter.vb
|   |-- PathStorage.vb
|   |-- ProgramPathValidator.vb
|   |-- UserInputChecker.vb
|   |-- UserInputReader.vb
|   +-- UserPrompter.vb
|-- GlobalAttributes.vb
+-- Program.vb
