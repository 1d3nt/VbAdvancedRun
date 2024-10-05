﻿Namespace CoreServices.ServiceDeployment.VbWorkerServiceDeployer.ServiceDeployerCoreServices.ServiceManagement

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
        ''' Gets the full path to the service executable using the current user's Desktop directory.
        ''' </summary>
        ''' <returns>The full path to the service executable located on the user's Desktop.</returns>
        Friend Function GetServicePath() As String Implements IServicePathProvider.GetServicePath
            Dim desktopPath As String = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            Return io.Path.Combine(desktopPath, "ServiceTest", "WorkerService", "VbWorkerServicePinvokeLauncher.exe")
        End Function

        ''' <summary>
        ''' Gets the name of the service.
        ''' </summary>
        ''' <returns>
        ''' A <see cref="String"/> representing the name of the service.
        ''' </returns>
        Friend Function GetServiceName() As String Implements IServicePathProvider.GetServiceName
            Dim servicePath As String = GetServicePath()
            Dim serviceName As String = IO.Path.GetFileNameWithoutExtension(servicePath)
            Return serviceName
        End Function
    End Class
End Namespace
