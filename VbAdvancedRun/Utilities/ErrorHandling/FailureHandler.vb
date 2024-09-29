Namespace Utilities.ErrorHandling

    ''' <summary>
    ''' Implements methods to handle failures by prompting the user and exiting the application with an error.
    ''' </summary>
    Friend Class FailureHandler
        Implements IFailureHandler

        ''' <summary>
        ''' The user prompter used for displaying messages to the user.
        ''' </summary>
        Private ReadOnly _userPrompter As IUserPrompter

        ''' <summary>
        ''' The user input reader used for reading user input.
        ''' </summary>
        Private ReadOnly _userInputReader As IUserInputReader

        ''' <summary>
        ''' The service provider used to resolve services.
        ''' </summary>
        Private ReadOnly _serviceProvider As IServiceProvider

        ''' <summary>
        ''' Initializes a new instance of the <see cref="FailureHandler"/> class.
        ''' </summary>
        ''' <param name="userPrompter">An instance of <see cref="IUserPrompter"/> used to display messages to the user.</param>
        ''' <param name="userInputReader">An instance of <see cref="IUserInputReader"/> used to read user input.</param>
        ''' <param name="serviceProvider">An instance of <see cref="IServiceProvider"/> used to resolve services.</param>
        Public Sub New(userPrompter As IUserPrompter, userInputReader As IUserInputReader, serviceProvider As IServiceProvider)
            _userPrompter = userPrompter
            _userInputReader = userInputReader
            _serviceProvider = serviceProvider
        End Sub

        ''' <summary>
        ''' Handles the failure by prompting the user and exiting the application with an error.
        ''' </summary>
        ''' <param name="message">The message to display to the user regarding the failure.</param>
        Friend Sub HandleFailure(message As String) Implements IFailureHandler.HandleFailure
            _userPrompter.Prompt(message)
            _userInputReader.ReadInput()
            Dim exitUtility = _serviceProvider.GetService(Of IExitUtility)()
            exitUtility.ExitWithError()
        End Sub
    End Class
End Namespace
