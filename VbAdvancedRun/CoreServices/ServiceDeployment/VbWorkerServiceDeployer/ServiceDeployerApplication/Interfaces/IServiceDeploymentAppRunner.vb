Namespace CoreServices.ServiceDeployment.VbWorkerServiceDeployer.ServiceDeployerApplication.Interfaces

    ''' <summary>
    ''' Defines the contract for running the service deployment application.
    ''' </summary>
    ''' <remarks>
    ''' This interface includes the <see cref="IServiceDeploymentAppRunner.RunAsync"/> method for running the service deployment application asynchronously.
    ''' Implementations of this interface should encapsulate the logic required to start and manage the service deployment application's execution.
    ''' </remarks>
    Public Interface IServiceDeploymentAppRunner

        ''' <summary>
        ''' Runs the service deployment application asynchronously.
        ''' </summary>
        ''' <returns>
        ''' A <see cref="Task(Of Boolean)"/> representing the asynchronous operation. The result indicates success or failure of the operation.
        ''' </returns>
        ''' <remarks>
        ''' This method contains the main logic for running the service deployment application. It uses the <see cref="IServiceInstaller"/> to install services,
        ''' retrieves the <see cref="IServiceUninstaller"/> to uninstall services, and displays the result to the user.
        ''' It is expected to be called at the application's entry point to start the execution process.
        ''' </remarks>
        ''' <see cref="IServiceDeploymentAppRunner.RunAsync"/> method for executing the service deployment logic.
        Function RunAsync() As Task(Of Boolean)
    End Interface
End Namespace
