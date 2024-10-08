﻿Namespace CoreServices.ServiceDeployment.VbWorkerServiceDeployer.ServiceDeployerApplication

    ''' <summary>
    ''' Represents an application that uses dependency injection to obtain services and perform operations related to service deployment.
    ''' </summary>
    ''' <remarks>
    ''' The <see cref="ServiceDeploymentAppRunner"/> class relies on dependency injection to obtain an <see cref="IServiceProvider"/> 
    ''' which is used to retrieve services throughout the application. The main functionality of the class is to
    ''' run the application's core logic, including installing and uninstalling services.
    ''' </remarks>
    Friend Class ServiceDeploymentAppRunner
        Implements IServiceDeploymentAppRunner

        ''' <summary>
        ''' The service provider used for retrieving services.
        ''' </summary>
        Private ReadOnly _serviceProvider As IServiceProvider

        ''' <summary>
        ''' The user prompter used for displaying messages to the user.
        ''' </summary>
        Private ReadOnly _userPrompter As IUserPrompter

        ''' <summary>
        ''' The failure handler used for managing failures in service operations.
        ''' </summary>
        Private ReadOnly _failureHandler As IFailureHandler

        ''' <summary>
        ''' Initializes a new instance of the <see cref="ServiceDeploymentAppRunner"/> class.
        ''' </summary>
        ''' <param name="serviceProvider">
        ''' An instance of <see cref="IServiceProvider"/> used to resolve dependencies and obtain services.
        ''' </param>
        ''' <param name="userPrompter">
        ''' An instance of <see cref="IUserPrompter"/> used to display messages to the user.
        ''' </param>
        ''' <param name="failureHandler">
        ''' An instance of <see cref="IFailureHandler"/> used to handle failures.
        ''' </param>
        ''' <remarks>
        ''' The constructor takes an <see cref="IServiceProvider"/>, an <see cref="IUserPrompter"/>, an <see cref="IUserInputReader"/> 
        ''' and an <see cref="IFailureHandler"/> as parameters and assigns them to the corresponding fields.
        ''' </remarks>
        Public Sub New(serviceProvider As IServiceProvider, userPrompter As IUserPrompter, failureHandler As IFailureHandler)
            _serviceProvider = serviceProvider
            _userPrompter = userPrompter
            _failureHandler = failureHandler
        End Sub

        ''' <summary>
        ''' Runs the application.
        ''' </summary>
        ''' <remarks>
        ''' The <see cref="RunAsync"/> method installs the service, awaits a delay before uninstalling it, and then proceeds to uninstall the service.
        ''' It returns a <see cref="Boolean"/> indicating whether the operation was successful.
        ''' </remarks>
        Friend Async Function RunAsync() As Task(Of Boolean) Implements IServiceDeploymentAppRunner.RunAsync
            Try
                Await InstallServiceAsync()
                Await DelayBeforeAction()
                Await UninstallService()
                PromptServiceLaunch()
                Return True
            Catch ex As Exception
                _failureHandler.HandleFailure("An error occurred while running the application.")
            End Try
            Return False
        End Function

        ''' <summary>
        ''' Installs the service by using the <see cref="IServiceInstaller"/> retrieved from the service provider.
        ''' </summary>
        ''' <remarks>
        ''' This method attempts to install the service and handles any exceptions that occur during the installation process.
        ''' </remarks>
        Private Async Function InstallServiceAsync() As Task
            Dim serviceInstaller = _serviceProvider.GetService(Of IServiceInstaller)()
            Try
                Dim installationSuccess = Await serviceInstaller.InstallServiceAsync()
                _userPrompter.Prompt($"Service installation success: {installationSuccess}")
            Catch ex As Exception
                _failureHandler.HandleFailure($"Service installation failed: {ex.Message}")
            End Try
        End Function

        ''' <summary>
        ''' Introduces a delay before proceeding with an action.
        ''' </summary>
        ''' <returns>
        ''' A task that represents the asynchronous operation.
        ''' </returns>
        ''' <remarks>
        ''' The <see cref="DelayBeforeAction"/> method prompts the user about the delay duration and then simulates a delay
        ''' before proceeding with the specified action.
        ''' </remarks>
        Private Async Function DelayBeforeAction() As Task
            Const delayMilliseconds = 10000
            PromptUserAboutDelay(delayMilliseconds)
            Await AsynchronousProcessor.SimulateDelayedResponse(delayMilliseconds)
        End Function

        ''' <summary>
        ''' Prompts the user about the delay duration.
        ''' </summary>
        ''' <param name="delayMilliseconds">The delay duration in milliseconds.</param>
        ''' <remarks>
        ''' The <see cref="PromptUserAboutDelay"/> method uses the <see cref="IUserPrompter"/> service to notify the user about
        ''' the delay before uninstalling the service.
        ''' </remarks>
        Private Sub PromptUserAboutDelay(delayMilliseconds As Integer)
            _userPrompter.Prompt($"The service will wait for {delayMilliseconds / 1000} seconds before proceeding to uninstall.")
        End Sub

        ''' <summary>
        ''' Uninstalls the service by using the <see cref="IServiceUninstaller"/> retrieved from the service provider.
        ''' </summary>
        ''' <remarks>
        ''' This method attempts to uninstall the service and handles any exceptions that occur during the uninstallation process.
        ''' </remarks>
        Private Async Function UninstallService() As Task
            Dim serviceUninstaller = _serviceProvider.GetService(Of IServiceUninstaller)()
            Try
                Dim uninstallationSuccess = Await serviceUninstaller.UninstallServiceAsync()
                _userPrompter.Prompt($"Service uninstallation success: {uninstallationSuccess}")
            Catch ex As Exception
                _failureHandler.HandleFailure($"Service uninstallation failed: {ex.Message}")
            End Try
        End Function

        ''' <summary>
        ''' Prompts the user with a message indicating that the service has been successfully launched.
        ''' </summary>
        ''' <remarks>
        ''' This method retrieves the service path from the <see cref="PathStorage"/> singleton instance,
        ''' extracts the service name from the path, and uses the <see cref="IUserPrompter"/> to display
        ''' a message to the user indicating the successful launch of the service. 
        ''' Note that this message arrives a little late after the process has launched, 
        ''' but we really don't care lol. Instructs the user to press any key to exit.
        ''' </remarks>
        Private Sub PromptServiceLaunch()
            Dim processPath As String = PathStorage.Instance.ProgramPath
            Dim processName As String = Path.GetFileName(processPath)
            _userPrompter.Prompt($"The process '{processName}' has been successfully launched. Press any key to exit.")
        End Sub
    End Class
End Namespace
