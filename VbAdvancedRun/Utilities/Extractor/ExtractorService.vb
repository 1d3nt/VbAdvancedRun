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
        ''' Initializes a new instance of the <see cref="ExtractorService"/> class.
        ''' </summary>
        ''' <param name="resourceExtractor">The resource extractor service.</param>
        ''' <param name="archiveExtractor">The archive extractor service.</param>
        ''' <param name="userPrompter">The user prompter service.</param>
        ''' <param name="userInputReader">The user input reader service.</param>
        ''' <param name="serviceProvider">The service provider for resolving services.</param>
        Public Sub New(resourceExtractor As IResourceExtractor, archiveExtractor As IArchiveExtractor, userPrompter As IUserPrompter, userInputReader As IUserInputReader, serviceProvider As IServiceProvider)
            _resourceExtractor = resourceExtractor
            _archiveExtractor = archiveExtractor
            _userPrompter = userPrompter
            _userInputReader = userInputReader
            _serviceProvider = serviceProvider
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
        Friend Async Function ExtractAllWithMessagingAsync() As Task Implements IExtractorService.ExtractAllWithMessagingAsync
            Try
                Dim success As Boolean = Await ExtractAllAsync()
                If Not success Then
                    _userPrompter.Prompt("Extraction failed without any errors.")
                    _userInputReader.ReadInput()
                    Dim exitUtility = _serviceProvider.GetService(Of IExitUtility)()
                    exitUtility.ExitWithError()
                End If
            Catch ex As Exception
                _userPrompter.Prompt($"An error occurred during extraction: {ex.Message}")
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
