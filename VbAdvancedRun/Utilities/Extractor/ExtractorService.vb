Namespace Utilities.Extractor

    ''' <summary>
    ''' Provides services for extracting resources and archives.
    ''' </summary>
    Friend Class ExtractorService
        Implements IExtractorService

        ''' <summary>
        ''' The resource extractor service.
        ''' </summary>
        Private ReadOnly _resourceExtractor As IResourceExtractor

        ''' <summary>
        ''' The archive extractor service.
        ''' </summary>
        Private ReadOnly _archiveExtractor As IArchiveExtractor

        ''' <summary>
        ''' The user prompter service.
        ''' </summary>
        Private ReadOnly _userPrompter As IUserPrompter

        ''' <summary>
        ''' The user input reader service.
        ''' </summary>
        Private ReadOnly _userInputReader As IUserInputReader

        ''' <summary>
        ''' The service provider for resolving services.
        ''' </summary>
        Private ReadOnly _serviceProvider As IServiceProvider

        ''' <summary>
        ''' The failure handler used to handle failures by prompting the user and exiting the application with an error.
        ''' </summary>
        Private ReadOnly _failureHandler As IFailureHandler

        ''' <summary>
        ''' Initializes a new instance of the <see cref="ExtractorService"/> class.
        ''' </summary>
        ''' <param name="resourceExtractor">The resource extractor service.</param>
        ''' <param name="archiveExtractor">The archive extractor service.</param>
        ''' <param name="userPrompter">The user prompter service.</param>
        ''' <param name="userInputReader">The user input reader service.</param>
        ''' <param name="serviceProvider">The service provider for resolving services.</param>
        ''' <param name="failureHandler">The failure handler service.</param>
        Public Sub New(resourceExtractor As IResourceExtractor, archiveExtractor As IArchiveExtractor, userPrompter As IUserPrompter, userInputReader As IUserInputReader, serviceProvider As IServiceProvider, failureHandler As IFailureHandler)
            _resourceExtractor = resourceExtractor
            _archiveExtractor = archiveExtractor
            _userPrompter = userPrompter
            _userInputReader = userInputReader
            _serviceProvider = serviceProvider
            _failureHandler = failureHandler
        End Sub

        ''' <summary>
        ''' Extracts the resource and the 7z archive to the service directory asynchronously.
        ''' </summary>
        ''' <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating success or failure.</returns>
        Private Async Function ExtractAllAsync() As Task(Of Boolean)
            Try
                Dim resourceSuccess As Boolean = Await ExtractResourceAsync()
                Dim archiveSuccess As Boolean = resourceSuccess AndAlso Await ExtractArchiveAsync()
                Return archiveSuccess
            Catch ex As Exception
                Throw
            End Try
        End Function

        ''' <summary>
        ''' Extracts the resource and the 7z archive to the service directory asynchronously with messaging and error handling.
        ''' </summary>
        ''' <returns>A task that represents the asynchronous operation.</returns>
        ''' <remarks>
        ''' If the extraction fails or an exception occurs, the method will prompt the user and exit the application gracefully by calling
        ''' <see cref="_failureHandler"/> to manage the error messaging and exit logic.
        ''' </remarks>
        Friend Async Function ExtractAllWithMessagingAsync() As Task Implements IExtractorService.ExtractAllWithMessagingAsync
            Try
                Dim success As Boolean = Await ExtractAllAsync()
                If Not success Then
                    _failureHandler.HandleFailure("Extraction was not successful, but no specific errors were reported.")
                End If
            Catch ex As Exception
                _failureHandler.HandleFailure("An error occurred during extraction.")
            End Try
        End Function

        ''' <summary>
        ''' Extracts the resource to the service directory asynchronously.
        ''' </summary>
        ''' <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating success or failure.</returns>
        Private Async Function ExtractResourceAsync() As Task(Of Boolean)
            Return Await _resourceExtractor.ExtractResourceToServiceDirectoryAsync()
        End Function

        ''' <summary>
        ''' Extracts the 7z archive to the service directory asynchronously.
        ''' </summary>
        ''' <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating success or failure.</returns>
        Private Async Function ExtractArchiveAsync() As Task(Of Boolean)
            Return Await _archiveExtractor.ExtractArchiveAsync()
        End Function
    End Class
End Namespace
