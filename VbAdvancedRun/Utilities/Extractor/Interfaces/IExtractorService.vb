Namespace Utilities.Extractor.Interfaces

    ''' <summary>
    ''' Defines the contract for extracting resources and archives.
    ''' </summary>
    ''' <remarks>
    ''' This interface includes the <see cref="IExtractorService.ExtractAllAsync"/> method for asynchronous extraction of resources and archives.
    ''' </remarks>
    Public Interface IExtractorService

        ''' <summary>
        ''' Extracts the resource and the 7z archive to the service directory asynchronously with messaging and error handling.
        ''' </summary>
        ''' <returns>A task that represents the asynchronous operation.</returns>
        Function ExtractAllWithMessagingAsync() As Task
    End Interface
End Namespace
