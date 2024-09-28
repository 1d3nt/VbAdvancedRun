namespace CoreServices.FileSystem.Interfaces

    ''' <summary>
    ''' Represents a directory in a file system abstraction.
    ''' </summary>
    Public Interface IDirectoryManager

        ''' <summary>
        ''' Gets or sets the path to the directory.
        ''' </summary>
        ''' <value>The path to the directory.</value>
        Property Path() As String

        ''' <summary>
        ''' Creates the directory if it does not exist.
        ''' </summary>
        Sub Create()
    End Interface
End Namespace