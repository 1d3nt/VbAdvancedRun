Namespace Application.Interfaces

    ''' <summary>
    ''' Defines the contract for running the application.
    ''' </summary>
    ''' <remarks>
    ''' This interface includes the <see cref="IAppRunner.RunAsync"/> method for running the application asynchronously.
    ''' Implementations of this interface should encapsulate the logic required to start and manage the application's execution.
    ''' </remarks>
    Public Interface IAppRunner

        ''' <summary>
        ''' Runs the application asynchronously.
        ''' </summary>
        ''' <returns>
        ''' A <see cref="Task"/> representing the asynchronous operation.
        ''' </returns>
        ''' <remarks>
        ''' This method contains the main logic for running the application. It uses the <see cref="IAppDataManager"/> to create necessary directories,
        ''' retrieves the <see cref="IProgramPathValidator"/> to validate the program path, and displays the result to the user.
        ''' It is expected to be called at the application's entry point to start the execution process.
        ''' </remarks>
        ''' <see cref="IAppRunner.RunAsync"/> method for executing the application logic.
        Function RunAsync() As Task
    End Interface
End Namespace
