Namespace Utilities.Extractor

    ''' <summary>
    ''' Provides utility methods and properties for handling embedded resources.
    ''' </summary>
    Friend Class ResourceHelper

        ''' <summary>
        ''' The name of the embedded resource to extract.
        ''' </summary>
        ''' <remarks>
        ''' This must be qualified with the application's namespace to ensure 
        ''' the resource is correctly identified. Using the current namespace 
        ''' ensures it adapts if the project name changes.
        ''' </remarks>
        ''' <example>
        ''' The resource name should appear in the following format:
        ''' <code>
        ''' ProjectName.CompiledServices.7z
        ''' </code>
        ''' For example, if your project's namespace is 'VbAdvancedRun', the 
        ''' resource name will be:
        ''' <code>
        ''' VbAdvancedRun.CompiledServices.7z
        ''' </code>
        ''' </example>
        Friend Shared ReadOnly Property CompiledServicesResourceName As String = $"{Assembly.GetExecutingAssembly().GetName().Name}.{NameOf(My.Resources.CompiledServices)}.7z"
    End Class
End Namespace
