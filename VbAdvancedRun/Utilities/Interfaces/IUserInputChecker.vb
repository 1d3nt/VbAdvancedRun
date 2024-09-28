Namespace Utilities.Interfaces

    ''' <summary>
    ''' Defines methods for checking user input related to application setup decisions.
    ''' </summary>
    ''' <remarks>
    ''' The <see cref="IUserInputChecker"/> interface provides a method for prompting the user and obtaining the path
    ''' of the program they wish to launch. Implementations of this interface should include the logic for interacting
    ''' with the user to gather their input and make decisions based on their response.
    ''' 
    ''' The <see cref="IUserInputChecker.GetProgramPath"/> method is designed to solicit the path of the program from the user,
    ''' returning the path as a string. This allows the application to use the provided path to launch the specified program.
    ''' </remarks>
    ''' <seealso cref="UserInputChecker"/>
    Public Interface IUserInputChecker

        ''' <summary>
        ''' Prompts the user to enter the path of the program to launch.
        ''' </summary>
        ''' <returns>
        ''' The path of the program entered by the user as a string.
        ''' </returns>
        ''' <remarks>
        ''' Implementing classes should prompt the user to enter the path of the program they wish to launch.
        ''' The method reads the user's input from the console or other input sources, and returns the input as a string.
        ''' 
        ''' The <see cref="GetProgramPath"/> method helps in obtaining the necessary information from the user to proceed
        ''' with launching the specified program.
        ''' </remarks>
        Function GetProgramPath() As String
    End Interface
End Namespace
