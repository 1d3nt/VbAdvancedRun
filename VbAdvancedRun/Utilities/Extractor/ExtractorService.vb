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
        ''' Initializes a new instance of the <see cref="ExtractorService"/> class.
        ''' </summary>
        ''' <param name="resourceExtractor">The resource extractor service.</param>
        ''' <param name="archiveExtractor">The archive extractor service.</param>
        Public Sub New(resourceExtractor As IResourceExtractor, archiveExtractor As IArchiveExtractor)
            _resourceExtractor = resourceExtractor
            _archiveExtractor = archiveExtractor
        End Sub

        ''' <summary>
        ''' Extracts the resource and the 7z archive to the service directory asynchronously.
        ''' </summary>
        ''' <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating success or failure.</returns>
        Friend Async Function ExtractAllAsync() As Task(Of Boolean) Implements IExtractorService.ExtractAllAsync
            Try
                Dim resourceSuccess As Boolean = Await ExtractResourceAsync()
                Dim archiveSuccess As Boolean = resourceSuccess AndAlso Await ExtractArchiveAsync()
                Return archiveSuccess
            Catch ex As Exception
                Throw
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
