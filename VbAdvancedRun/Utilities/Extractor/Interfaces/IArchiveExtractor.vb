Namespace Utilities.Extractor.Interfaces

    ''' <summary>
    ''' Defines the contract for extracting archives.
    ''' </summary>
    ''' <remarks>
    ''' This interface includes the <see cref="IArchiveExtractor.ExtractArchiveAsync"/> method for asynchronous extraction.
    ''' </remarks>
    Public Interface IArchiveExtractor

        ''' <summary>
        ''' Asynchronously extracts the archive to the application service directory.
        ''' </summary>
        ''' <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating success or failure.</returns>
        Function ExtractArchiveAsync() As Task(Of Boolean)
    End Interface
End Namespace
