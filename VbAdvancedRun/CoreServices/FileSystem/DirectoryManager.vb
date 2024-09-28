Namespace CoreServices.FileSystem

    ''' <summary>
    ''' Represents a directory on the filesystem.
    ''' </summary>
    Friend Class DirectoryManager
        Implements IDirectoryManager

        ''' <summary>
        ''' Gets or sets the path to the directory.
        ''' </summary>
        ''' <value>The path to the directory.</value>
        Friend Property Path As String Implements IDirectoryManager.Path

        ''' <summary>
        ''' Creates the directory.
        ''' </summary>
        ''' <exception cref="InvalidOperationException">Thrown if the path is not set.</exception>
        Friend Sub Create() Implements IDirectoryManager.Create
            Try
                ThrowIfNoPath()
                Directory.CreateDirectory(Path)
            Catch ex As InvalidOperationException
                Throw
            End Try
        End Sub

        ''' <summary>
        ''' Throws an exception if the path is not set.
        ''' </summary>
        ''' <exception cref="InvalidOperationException">Thrown if the path is not set.</exception>
        Private Sub ThrowIfNoPath()
            If Path Is Nothing Then
                Throw New InvalidOperationException("Path must be set.")
            End If
        End Sub
    End Class
End Namespace
