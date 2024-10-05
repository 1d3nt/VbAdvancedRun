﻿Namespace CoreServices.ServiceDeployment.VbWorkerServiceDeployer.ServiceDeployerCoreServices.ServiceManagement.Interfaces

    ''' <summary>
    ''' Defines the contract for obtaining service paths and names.
    ''' </summary>
    ''' <remarks>
    ''' The <see cref="IServicePathProvider"/> interface provides methods for retrieving 
    ''' the paths and names of services to be installed. Implementing classes should provide the actual logic for obtaining these details.
    ''' 
    ''' The <see cref="IServicePathProvider.GetServicePath"/> method is used to retrieve 
    ''' the full path to the service executable, while the <see cref="IServicePathProvider.GetServiceName"/> 
    ''' method returns the service's name.
    ''' </remarks>
    ''' <seealso cref="ServicePathProvider"/>
    Public Interface IServicePathProvider

        ''' <summary>
        ''' Gets the full path to the service executable.
        ''' </summary>
        ''' <returns>The full path to the service executable.</returns>
        Function GetServicePath() As String

        ''' <summary>
        ''' Gets the name of the service.
        ''' </summary>
        ''' <returns>The name of the service.</returns>
        Function GetServiceName() As String
    End Interface
End Namespace
