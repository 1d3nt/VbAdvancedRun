Namespace CoreServices.FileSystem.Interfaces

    ''' <summary>
    ''' Defines the contract for managing application data directories.
    ''' </summary>
    Public Interface IAppDataManager

        ''' <summary>
        ''' Creates the service directory.
        ''' </summary>
        Sub CreateServiceDirectory()

        ''' <summary>
        ''' Gets the path to the service directory.
        ''' </summary>
        ''' <returns>The full path to the service directory.</returns>
        Function GetServiceDirectoryPath() As String

        ''' <summary>
        ''' Gets the full path to the app settings file.
        ''' </summary>
        ''' <returns>The full path to the app settings file.</returns>
        Function GetAppSettingsFilePath() As String
    End Interface
End Namespace
