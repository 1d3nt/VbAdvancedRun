Namespace Utilities.Extractor.Interfaces

    ''' <summary>
    ''' Defines the contract for extracting embedded resources.
    ''' </summary>
    Public Interface IResourceExtractor

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
        Function ExtractResourceToServiceDirectoryAsync() As Task(Of Boolean)
    End Interface
End Namespace
