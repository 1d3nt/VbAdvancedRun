﻿Namespace CoreServices.ServiceDeployment.VbWorkerServiceDeployer.ServiceDeployerCoreServices.ServiceManagement

    ''' <summary>
    ''' Handles deleting a service.
    ''' </summary>
    ''' <remarks>
    ''' This class implements the <see cref="IServiceDeleter"/> interface and provides the method
    ''' for deleting services using the Service Control Manager (SCM). It uses the <see cref="IErrorHandlingService"/>
    ''' to handle any errors that occur during the deletion process.
    ''' </remarks>
    ''' <seealso cref="IServiceDeleter"/>
    Friend Class ServiceDeleter
        Implements IServiceDeleter

        ''' <summary>
        ''' The error handling service.
        ''' </summary>
        Private ReadOnly _errorHandlingService As IErrorHandlingService

        ''' <summary>
        ''' Initializes a new instance of the <see cref="ServiceDeleter"/> class.
        ''' </summary>
        ''' <param name="errorHandlingService">
        ''' An instance of <see cref="IErrorHandlingService"/> used for handling errors.
        ''' </param>
        ''' <remarks>
        ''' The constructor initializes the <see cref="_errorHandlingService"/> field, which is used to handle any errors
        ''' encountered during the service deletion process.
        ''' </remarks>
        Public Sub New(errorHandlingService As IErrorHandlingService)
            _errorHandlingService = errorHandlingService
        End Sub

        ''' <summary>
        ''' Deletes the specified service.
        ''' </summary>
        ''' <param name="serviceHandle">
        ''' The handle to the service. This handle is returned by the <see cref="NativeMethods.OpenService"/> function and must have the DELETE access right.
        ''' </param>
        ''' <remarks>
        ''' This method wraps the <see cref="NativeMethods.DeleteService"/> function from the Windows API.
        ''' <see href="https://learn.microsoft.com/en-us/windows/win32/api/winsvc/nf-winsvc-deleteservice"/>
        ''' During investigation, it was found that the <see cref="NativeMethods.DeleteService"/> function did not throw an error even when encountering 
        ''' error code 5 (ACCESS_DENIED). This issue occurred specifically when the service handle was opened with access rights other than <see cref="DesiredAccess.All"/>, 
        ''' such as <see cref="DesiredAccess.Stop"/> or <see cref="DesiredAccess.Delete"/>. However, using <see cref="DesiredAccess.All"/> allowed the 
        ''' deletion to proceed successfully, indicating that full access rights are necessary for the deletion operation. This behavior suggests that 
        ''' <see cref="DeleteService"/> does not throw an exception on ACCESS_DENIED but instead returns a failure result that must be handled explicitly.
        ''' </remarks>
        Friend Sub DeleteService(serviceHandle As IntPtr) Implements IServiceDeleter.DeleteService
            If Not NativeMethods.DeleteService(serviceHandle) Then
                _errorHandlingService.HandleWin32Error(serviceHandle)
            End If
        End Sub
    End Class
End Namespace
