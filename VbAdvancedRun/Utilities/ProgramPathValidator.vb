Namespace Utilities

    ''' <summary>
    ''' Provides methods for validating the program path by prompting the user for input and verifying that the provided path exists.
    ''' </summary>
    ''' <remarks>
    ''' This class implements the <see cref="IProgramPathValidator"/> interface and is responsible for prompting the user for a program path
    ''' and validating whether the path exists on the system.
    ''' </remarks>
    ''' <seealso cref="IProgramPathValidator"/>
    Friend Class ProgramPathValidator
        Implements IProgramPathValidator

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
        ''' Initializes a new instance of the <see cref="ProgramPathValidator"/> class.
        ''' </summary>
        ''' <param name="inputReader">The input reader to use for reading user input.</param>
        ''' <param name="prompter">The prompter to use for displaying messages to the user.</param>
        ''' <remarks>
        ''' The constructor initializes the fields necessary for reading user input and prompting the user.
        ''' Both dependencies are injected to decouple input handling and prompting from the path validation logic.
        ''' </remarks>
        Public Sub New(inputReader As IUserInputReader, prompter As IUserPrompter)
            _inputReader = inputReader
            _prompter = prompter
        End Sub

        ''' <summary>
        ''' Prompts the user to enter a valid program path.
        ''' </summary>
        ''' <returns>
        ''' The valid program path entered by the user.
        ''' </returns>
        ''' <remarks>
        ''' This method repeatedly prompts the user to provide the path of the program they wish to launch.
        ''' It ensures that the inputted path exists using <see cref="File.Exists"/>. If the path does not exist,
        ''' the user is prompted to provide a new path until a valid one is entered.
        ''' </remarks>
        ''' <seealso cref="IProgramPathValidator.GetValidProgramPath"/>
        Friend Function GetValidProgramPath() As String Implements IProgramPathValidator.GetValidProgramPath
            Dim programPath As String
            Do
                programPath = PromptForProgramPath()
            Loop Until IsValidPath(programPath)
            StoreProgramPath(programPath)
            Return programPath
        End Function

        ''' <summary>
        ''' Prompts the user to enter the program path.
        ''' </summary>
        ''' <returns>
        ''' The program path entered by the user.
        ''' </returns>
        Private Function PromptForProgramPath() As String
            _prompter.Prompt("Please enter the path of the program you would like to launch:")
            Return _inputReader.ReadInput()
        End Function

        ''' <summary>
        ''' Checks if the provided path is valid.
        ''' </summary>
        ''' <param name="path">The path to validate.</param>
        ''' <returns>
        ''' <c>True</c> if the path exists; otherwise, <c>False</c>.
        ''' </returns>
        Private Function IsValidPath(path As String) As Boolean
            If File.Exists(path) Then
                Return True
            Else
                _prompter.Prompt($"The path '{path}' does not exist. Please provide a valid path.")
                Return False
            End If
        End Function

        ''' <summary>
        ''' Stores the validated program path in the <see cref="PathStorage"/> singleton.
        ''' </summary>
        ''' <param name="path">The validated program path to store.</param>
        Private Shared Sub StoreProgramPath(path As String)
            PathStorage.Instance.ProgramPath = path
        End Sub
    End Class
End Namespace
