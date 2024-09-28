Namespace Utilities.Interfaces

    ''' <summary>
    ''' Defines methods for validating the program path.
    ''' </summary>
    Public Interface IProgramPathValidator

        ''' <summary>
        ''' Prompts the user to enter a valid program path.
        ''' </summary>
        ''' <returns>The valid program path entered by the user.</returns>
        Function GetValidProgramPath() As String
    End Interface
End Namespace
