Namespace CoreServices.FileSystem.Interfaces

    ''' <summary>
    ''' Defines the contract for managing application data directories, 
    ''' such as service directories within the 'VbAdvancedRun' folder.
    ''' </summary>
    Public Interface IAppDataManager

        ''' <summary>
        ''' Creates the service directory under the 'VbAdvancedRun\Service' path.
        ''' </summary>
        Sub CreateServiceDirectory()

        ''' <summary>
        ''' Gets the path to the 'Service' directory within the 'VbAdvancedRun' folder.
        ''' </summary>
        ''' <returns>The full path to the 'Service' directory.</returns>
        Function GetServiceDirectoryPath() As String
    End Interface
End Namespace
