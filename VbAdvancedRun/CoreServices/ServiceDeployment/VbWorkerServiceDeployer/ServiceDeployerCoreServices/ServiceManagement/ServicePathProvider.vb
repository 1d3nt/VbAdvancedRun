Namespace CoreServices.ServiceDeployment.VbWorkerServiceDeployer.ServiceDeployerCoreServices.ServiceManagement

    ''' <summary>
    ''' Provides methods to obtain service paths and names.
    ''' </summary>
    ''' <remarks>
    ''' The <see cref="ServicePathProvider"/> class implements the <see cref="IServicePathProvider"/> interface
    ''' and provides the actual logic for retrieving the paths and names of services to be installed.
    ''' </remarks>
    Friend Class ServicePathProvider
        Implements IServicePathProvider

        ''' <summary>
        ''' The app data manager used for managing application data directories.
        ''' </summary>
        Private ReadOnly _appDataManager As IAppDataManager

        ''' <summary>
        ''' Initializes a new instance of the <see cref="ServicePathProvider"/> class.
        ''' </summary>
        ''' <param name="appDataManager">An instance of <see cref="IAppDataManager"/> used to manage application data directories.</param>
        Public Sub New(appDataManager As IAppDataManager)
            _appDataManager = appDataManager
        End Sub

        ''' <summary>
        ''' Gets the full path to the service executable.
        ''' </summary>
        ''' <returns>The full path to the service executable.</returns>
        Friend Function GetServicePath() As String Implements IServicePathProvider.GetServicePath
            Return Path.Combine(_appDataManager.GetServiceDirectoryPath(), "VbWorkerServicePinvokeLauncher.exe")
        End Function

        ''' <summary>
        ''' Gets the name of the service.
        ''' </summary>
        ''' <returns>
        ''' A <see cref="String"/> representing the name of the service.
        ''' </returns>
        Friend Function GetServiceName() As String Implements IServicePathProvider.GetServiceName
            Dim servicePath As String = GetServicePath()
            Dim serviceName As String = Path.GetFileNameWithoutExtension(servicePath)
            Return serviceName
        End Function
    End Class
End Namespace
