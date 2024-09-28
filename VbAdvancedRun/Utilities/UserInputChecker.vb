Namespace Utilities

    ''' <summary>
    ''' Provides methods to check user input for application setup decisions.
    ''' </summary>
    ''' <remarks>
    ''' This class implements the <see cref="IUserInputChecker"/> interface and is used to determine whether the user wants to proceed
    ''' based on their response.
    ''' </remarks>
    ''' <seealso cref="IUserInputChecker"/>
    Friend Class UserInputChecker
        Implements IUserInputChecker

        ''' <summary>
        ''' The message displayed to the user when prompting them to enter the path of the program to launch.
        ''' </summary>
        ''' <remarks>
        ''' This constant holds the message that is shown to the user to prompt them to enter the path of the program to launch.
        ''' </remarks>
        Private Const PathPromptMessage As String = "Please enter the path of the program you would like to launch:"

        ''' <summary>
        ''' The input reader used to read user input.
        ''' </summary>
        ''' <remarks>
        ''' This field is initialized via dependency injection and is used to read user input from the console or other input sources.
        ''' </remarks>
        Private ReadOnly _inputReader As IUserInputReader

        ''' <summary>
        ''' The prompter used to display messages to the user.
        ''' </summary>
        ''' <remarks>
        ''' This field is initialized via dependency injection and is used to display messages to the user.
        ''' </remarks>
        Private ReadOnly _prompter As IUserPrompter

        ''' <summary>
        ''' Initializes a new instance of the <see cref="UserInputChecker"/> class.
        ''' </summary>
        ''' <param name="inputReader">The input reader to use for reading user input.</param>
        ''' <param name="prompter">The prompter to use for displaying messages to the user.</param>
        Public Sub New(inputReader As IUserInputReader, prompter As IUserPrompter)
            _inputReader = inputReader
            _prompter = prompter
        End Sub

        ''' <summary>
        ''' Prompts the user to enter the path of the program to launch.
        ''' </summary>
        ''' <returns>The path entered by the user.</returns>
        ''' <remarks>
        ''' This method prompts the user with a message to enter the path of the program they wish to launch and reads their input.
        ''' </remarks>
        ''' <seealso cref="IUserInputChecker.GetProgramPath"/>
        Friend Function GetProgramPath() As String Implements IUserInputChecker.GetProgramPath
            PromptUser()
            Return GetUserInput()
        End Function

        ''' <summary>
        ''' Prompts the user with a message to enter the path of the program to launch.
        ''' </summary>
        Private Sub PromptUser()
            _prompter.Prompt(PathPromptMessage)
        End Sub

        ''' <summary>
        ''' Reads the user's input.
        ''' </summary>
        ''' <returns>The input string from the user.</returns>
        Private Function GetUserInput() As String
            Return _inputReader.ReadInput()
        End Function
    End Class
End Namespace
