Namespace Utilities

    ''' <summary>
    ''' Represents a singleton class that stores the program path.
    ''' This class ensures that only one instance exists and provides global access to the program path.
    ''' </summary>
    Friend Class PathStorage

        ''' <summary>
        ''' Private constructor to prevent external instantiation.
        ''' This ensures that the class remains a singleton.
        ''' </summary>
        Private Sub New()
        End Sub

        ''' <summary>
        ''' Gets the single instance of the <see cref="PathStorage"/> class.
        ''' This property provides a global point of access to the instance.
        ''' </summary>
        Friend Shared ReadOnly Property Instance As New PathStorage()

        ''' <summary>
        ''' Gets or sets the program path.
        ''' This property holds the path to the program, which can be accessed and modified globally.
        ''' </summary>
        Friend Property ProgramPath As String
    End Class
End Namespace
