Imports SharpCompress.Archives
Imports SharpCompress.Common

Namespace Utilities.Extractor

    ''' <summary>
    ''' Provides functionality to extract 7z archives using the SharpCompress library.
    ''' </summary>
    Friend Class ArchiveExtractor
        Implements IArchiveExtractor

        ''' <summary>
        ''' The app data manager used for managing application data directories.
        ''' </summary>
        Private ReadOnly _appDataManager As IAppDataManager

        ''' <summary>
        ''' Initializes a new instance of the <see cref="ArchiveExtractor"/> class.
        ''' </summary>
        ''' <param name="appDataManager">The application data manager to manage paths.</param>
        Public Sub New(appDataManager As IAppDataManager)
            _appDataManager = appDataManager
        End Sub

        ''' <summary>
        ''' Extracts the specified 7z archive to the application service directory.
        ''' </summary>
        ''' <exception cref="IOException">Thrown when an I/O error occurs during extraction.</exception>
        ''' <exception cref="ArchiveException">Thrown when an error occurs while processing the archive.</exception>
        Friend Async Function ExtractArchiveAsync() As Task(Of Boolean) Implements IArchiveExtractor.ExtractArchiveAsync
            Dim outputDirectory As String = _appDataManager.GetServiceDirectoryPath()
            Dim compiledServicesResourceName As String = ResourceHelper.CompiledServicesResourceName
            Dim archivePath As String = Path.Combine(outputDirectory, Path.GetFileName(compiledServicesResourceName))
            Using archive = Await Task.Run(Function() ArchiveFactory.Open(archivePath))
                For Each entry In archive.Entries
                    Try
                        Await ExtractEntryAsync(entry, outputDirectory)
                    Catch ex As Exception
                        Throw
                    End Try
                Next
            End Using
            Return True
        End Function

        ''' <summary>
        ''' Extracts a single entry from the archive to the specified output directory.
        ''' </summary>
        ''' <param name="entry">The archive entry to extract.</param>
        ''' <param name="outputDirectory">The directory to extract the entry to.</param>
        Private shared Async Function ExtractEntryAsync(entry As IArchiveEntry, outputDirectory As String) As Task
            Await Task.Run(Sub()
                entry.WriteToDirectory(outputDirectory, New ExtractionOptions() With {
                                            .ExtractFullPath = True,
                                            .Overwrite = True
                                          })
            End Sub)
        End Function
    End Class
End Namespace
