Namespace CoreServices.FileSystem

    ''' <summary>
    ''' Manages the creation and retrieval of application data directories, 
    ''' such as service directories within the 'VbAdvancedRun' folder.
    ''' </summary>
    Friend Class AppDataManager
        Implements IAppDataManager

        ''' <summary>
        ''' Stores the directory manager responsible for handling filesystem operations.
        ''' </summary>
        Private ReadOnly _directory As IDirectoryManager

        ''' <summary>
        ''' Initializes a new instance of the <see cref="AppDataManager"/> class, 
        ''' using a default <see cref="DirectoryManager"/> implementation.
        ''' </summary>
        ''' <param name="directory">An instance of <see cref="IDirectoryManager"/> used to handle filesystem operations.</param>
        ''' <exception cref="ArgumentNullException">Thrown if the assigned value is null.</exception>
        Public Sub New(directory As IDirectoryManager)
            ArgumentNullException.ThrowIfNull(directory, NameOf(directory))
            _directory = directory
        End Sub

        ''' <summary>
        ''' Creates the service directory under the 'VbAdvancedRun\Service' path.
        ''' </summary>
        Friend Sub CreateServiceDirectory() Implements IAppDataManager.CreateServiceDirectory
            _directory.Path = GetServiceDirectoryPath()
            Try
                _directory.Create()
            Catch ex As Exception
                Throw
            End Try
        End Sub

        ''' <summary>
        ''' Gets the path to the 'Service' directory within the 'VbAdvancedRun' folder.
        ''' </summary>
        ''' <returns>The full path to the 'Service' directory.</returns>
        Friend Function GetServiceDirectoryPath() As String Implements IAppDataManager.GetServiceDirectoryPath
            Const directoryName = "Service"
            Return GetAppDataPath(directoryName)
        End Function

        ''' <summary>
        ''' Builds a path to the specified directory within the 'VbAdvancedRun' folder on the main drive.
        ''' </summary>
        ''' <param name="directoryName">The name of the directory (e.g., 'Service').</param>
        ''' <returns>The full path to the specified directory within 'VbAdvancedRun'.</returns>
        Private Shared Function GetAppDataPath(directoryName As String) As String
            Dim mainDrive As String = Path.GetPathRoot(Environment.SystemDirectory)
            Dim baseDirectory As String = Path.Combine(mainDrive, Assembly.GetExecutingAssembly().GetName().Name)
            Return Path.Combine(baseDirectory, directoryName)
        End Function
    End Class
End Namespace
