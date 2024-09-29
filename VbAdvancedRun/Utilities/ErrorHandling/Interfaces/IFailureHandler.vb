Namespace Utilities.ErrorHandling.Interfaces

    ''' <summary>
    ''' Provides methods to handle failures by prompting the user and exiting the application with an error.
    ''' </summary>
    ''' <remarks>
    ''' This interface includes the <see cref="IFailureHandler.HandleFailure"/> method for handling failures in a user-friendly manner.
    ''' Implementations of this interface should encapsulate the logic required to manage user notifications and application exit.
    ''' </remarks>
    Public Interface IFailureHandler

        ''' <summary>
        ''' Handles the failure by prompting the user and exiting the application with an error.
        ''' </summary>
        ''' <param name="message">The message to display to the user regarding the failure.</param>
        ''' <remarks>
        ''' This method is responsible for presenting an error message to the user and terminating the application appropriately.
        ''' </remarks>
        ''' <see cref="IFailureHandler.HandleFailure"/> method for managing failure scenarios.
        Sub HandleFailure(message As String)
    End Interface
End Namespace
