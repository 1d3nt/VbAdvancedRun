# VbAdvancedRun

**VbAdvancedRun** is a .NET 8.0 application that combines the functionality of two projects: [VbWorkerServicePinvokeLauncher](https://github.com/1d3nt/VbWorkerServicePinvokeLauncher) and [VbWorkerServiceDeployer](https://github.com/1d3nt/VbWorkerServiceDeployer). It provides a streamlined solution for launching processes under the SYSTEM account, leveraging P/Invoke to interact with Windows APIs for elevated privilege management and service installation.

## Features

### Service Compilation & Installation
[VbWorkerServicePinvokeLauncher](https://github.com/1d3nt/VbWorkerServicePinvokeLauncher) is compiled into a Windows service, and packaged as a 7z file for easy extraction and deployment. The installation process is powered by the code from [VbWorkerServiceDeployer](https://github.com/1d3nt/VbWorkerServiceDeployer).

### Process Launching as SYSTEM
Once installed, VbAdvancedRun writes the process to be launched to a configuration file. Upon starting the service, the application launches the specified process with SYSTEM-level privileges.

### Seamless Service Management
VbAdvancedRun simplifies service installation and management by utilizing dependency injection and adhering to the Single Responsibility Principle, making the codebase easy to maintain and extend.
