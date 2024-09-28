Namespace Utilities.Extractor

    ''' <summary>
    ''' Provides functionality to extract embedded resources from the assembly.
    ''' </summary>
    Friend Class ResourceExtractor
        Implements IResourceExtractor

        ''' <summary>
        ''' The application data manager used to manage application directories.
        ''' </summary>
        Private ReadOnly _appDataManager As IAppDataManager

        ''' <summary>
        ''' Initializes a new instance of the <see cref="ResourceExtractor"/> class.
        ''' </summary>
        ''' <param name="appDataManager">
        ''' An instance of <see cref="IAppDataManager"/> used to manage application data directories.
        ''' </param>
        Public Sub New(appDataManager As IAppDataManager)
            _appDataManager = appDataManager
        End Sub

        ''' <summary>
        ''' Extracts the embedded resource to the service directory asynchronously.
        ''' </summary>
        ''' <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating success or failure.</returns>
        ''' <exception cref="FileNotFoundException">
        ''' Thrown when the specified resource cannot be found in the assembly.
        ''' </exception>
        ''' <remarks>
        ''' Another possible approach to access the compiled services is to write 
        ''' the resource directly to the user's drive using the following code:
        ''' 
        ''' <code>
        ''' File.WriteAllBytes(Path.Combine(path, $"{NameOf(My.Resources.CompiledServices)}.7z"), My.Resources.CompiledServices)
        ''' </code>
        ''' 
        ''' This requires that the 'CompiledServices.7z' file is manually added as an 
        ''' embedded resource in the project.
        ''' </remarks>
        Friend Async Function ExtractResourceToServiceDirectoryAsync() As Task(Of Boolean) Implements IResourceExtractor.ExtractResourceToServiceDirectoryAsync
            Dim compiledServicesResourceName = ResourceHelper.CompiledServicesResourceName
            Dim outputDirectory As String = _appDataManager.GetServiceDirectoryPath()
            Dim resourceStream As Stream = GetResourceStream(compiledServicesResourceName)
            If resourceStream Is Nothing Then
                Throw New FileNotFoundException("Resource not found.", compiledServicesResourceName)
            End If
            Dim outputPath As String = Path.Combine(outputDirectory, Path.GetFileName(compiledServicesResourceName))
            Try
                Await WriteResourceToFileAsync(resourceStream, outputPath)
                Return True
            Catch ex As Exception
                Throw
            End Try
        End Function

        ''' <summary>
        ''' Gets the resource stream from the assembly.
        ''' </summary>
        ''' <param name="resourceName">The name of the resource.</param>
        ''' <returns>The resource stream.</returns>
        Private Shared Function GetResourceStream(resourceName As String) As Stream
            Dim assembly As Assembly = Assembly.GetExecutingAssembly()
            Return assembly.GetManifestResourceStream(resourceName)
        End Function

        ''' <summary>
        ''' Writes the resource stream to a file asynchronously.
        ''' </summary>
        ''' <param name="resourceStream">The resource stream.</param>
        ''' <param name="outputPath">The output file path.</param>
        ''' <returns>A task representing the asynchronous operation.</returns>
        Private Shared Async Function WriteResourceToFileAsync(resourceStream As Stream, outputPath As String) As Task
            Try
                Using fileStream As New FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None, 4096, FileOptions.Asynchronous)
                    Await resourceStream.CopyToAsync(fileStream)
                End Using
            Catch ex As Exception
                Throw
            End Try
        End Function
    End Class
End Namespace
