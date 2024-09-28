Namespace Utilities.Extractor.Interfaces

    ''' <summary>
    ''' Defines the contract for extracting resources and archives.
    ''' </summary>
    ''' <remarks>
    ''' This interface includes the <see cref="IExtractorService.ExtractAllAsync"/> method for asynchronous extraction of resources and archives.
    ''' </remarks>
    Public Interface IExtractorService

        ''' <summary>
        ''' Extracts the resource and the 7z archive to the service directory asynchronously.
        ''' </summary>
        ''' <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating success or failure.</returns>
        Function ExtractAllAsync() As Task(Of Boolean)
    End Interface
End Namespace
