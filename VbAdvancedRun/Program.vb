''' <author>
''' Sam (ident)
''' Twitter: <see href="https://twitter.com/1d3nt">https://twitter.com/1d3nt</see>
''' GitHub: <see href="https://github.com/1d3nt">https://github.com/1d3nt</see>
''' Email: <see href="mailto:ident@simplecoders.com">ident@simplecoders.com</see>
''' VBForums: <see href="https://www.vbforums.com/member.php?113656-ident">https://www.vbforums.com/member.php?113656-ident</see>
''' ORCID: <see href="https://orcid.org/0009-0007-1363-3308">https://orcid.org/0009-0007-1363-3308</see>
''' </author>
''' <date>24/09/2024</date>
''' <version>1.0.0</version>
''' <license>Creative Commons Attribution 4.0 International (CC BY 4.0) - See LICENSE.md for details</license>
''' <summary>
''' The entry point for the application.
''' </summary>
''' <remarks>
''' This module contains the <see cref="Main"/> method, which sets up and starts the application host.
''' 
''' This project includes P/Invoke declarations and methods for interacting with Windows API functions.
''' Contributions and enhancements are welcomed to further extend the functionality and improve the integration.
''' 
''' Just a hobby programmer that enjoys P/Invoke and exploring complex interactions with system-level APIs.
''' My mallory x
''' </remarks>
Module Program

    ''' <summary>
    ''' Entry point for the console application. This application manages user interactions to configure and set up services. 
    ''' It prompts users for input to determine whether to proceed with various setup tasks and performs actions based on their responses.
    ''' </summary>
    ''' <param name="args">Command-line arguments (not used in this implementation).</param>
    ''' <remarks>
    ''' The <paramref name="args"/> parameter is included to adhere to the standard signature for a console application's
    ''' <see cref="Main"/> method. While this parameter is not utilized in the current implementation, including it follows 
    ''' best practices and ensures consistency with the conventional entry point signature.
    ''' 
    ''' This method initializes the <see cref="IServiceProvider"/> by calling <see cref="ServiceConfigurator.ConfigureServices"/> 
    ''' to configure and provide the required services for the application. The services are then passed into the <see cref="AppRunner"/> 
    ''' class, which handles the execution of the core logic for the setup tasks.
    ''' 
    ''' The method calls <see cref="AppRunner.RunAsync"/> and waits for its completion using 
    ''' <see cref="M:System.Threading.Tasks.Task.GetAwaiter().GetResult"/>. This approach ensures that the asynchronous operations 
    ''' in <see cref="AppRunner.RunAsync"/> complete before the application exits. Unlike <see cref="M:System.Threading.Tasks.Task.Wait()"/>, 
    ''' which can cause deadlocks in some cases, the use of <see cref="M:System.Threading.Tasks.Task.GetAwaiter().GetResult"/> 
    ''' is a safer alternative in a console application's synchronous entry point.
    ''' 
    ''' <para>
    ''' The variable <c>serviceProvider</c> service management tasks. 
    ''' </para>
    ''' <para>
    ''' The <c>appRunner</c> variable is responsible for orchestrating the setup process by executing the application's core logic.
    ''' </para>
    ''' </remarks>
    <SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification:="Standard Main method parameter signature.")>
    Sub Main(args As String())
        Dim serviceProvider As IServiceProvider = ConfigureServices()
        Dim userPrompter = serviceProvider.GetService(Of IUserPrompter)()
        Dim userInputReader = serviceProvider.GetService(Of IUserInputReader)()
        Dim appRunner = serviceProvider.GetService(Of IAppRunner)()
        If appRunner Is Nothing Then
            HandleAppRunnerResolutionFailure(serviceProvider, userPrompter, userInputReader)
        End If
        RunApplication(appRunner, userPrompter)
    End Sub

    ''' <summary>
    ''' Configures services for dependency injection.
    ''' </summary>
    ''' <returns>
    ''' An instance of <see cref="IServiceProvider"/> configured with the necessary services.
    ''' </returns>
    Private Function ConfigureServices() As IServiceProvider
        Dim serviceConfigurator As New ServiceConfigurator()
        Return serviceConfigurator.ConfigureServices()
    End Function

    ''' <summary>
    ''' Handles the failure to resolve the <see cref="IAppRunner"/> instance.
    ''' Prompts the user with an error message, reads input, and exits the application with an error code.
    ''' </summary>
    ''' <param name="serviceProvider">The service provider used to resolve dependencies.</param>
    ''' <param name="userPrompter">The user prompter used to display messages to the user.</param>
    ''' <param name="userInputReader">The user input reader used to read user input.</param>
    Private Sub HandleAppRunnerResolutionFailure(serviceProvider As IServiceProvider, userPrompter As IUserPrompter, userInputReader As IUserInputReader)
        userPrompter.Prompt("Failed to resolve IAppRunner.")
        userInputReader.ReadInput()
        Dim exitUtility = serviceProvider.GetService(Of IExitUtility)()
        exitUtility.ExitWithError()
    End Sub

    ''' <summary>
    ''' Runs the application and handles any exceptions that occur during execution.
    ''' </summary>
    ''' <param name="appRunner">The application runner used to run the application.</param>
    ''' <param name="userPrompter">The user prompter used to display messages to the user.</param>
    Private Sub RunApplication(appRunner As IAppRunner, userPrompter As IUserPrompter)
        Try
            appRunner.RunAsync().GetAwaiter().GetResult()
        Catch ex As Exception
            userPrompter.Prompt($"An error occurred: {ex.Message}")
        End Try
    End Sub
End Module
