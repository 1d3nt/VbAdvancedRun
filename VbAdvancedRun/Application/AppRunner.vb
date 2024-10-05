Namespace Application

    ''' <summary>
    ''' Represents an application that uses dependency injection to obtain services and perform operations.
    ''' </summary>
    ''' <remarks>
    ''' The <see cref="AppRunner"/> class relies on dependency injection to obtain an <see cref="IServiceProvider"/> 
    ''' which is used to retrieve services throughout the application. The main functionality of the class is to
    ''' run the application's core logic, including installing and uninstalling services.
    ''' </remarks>
    Friend Class AppRunner
        Implements IAppRunner

        ''' <summary>
        ''' The user prompter used for displaying messages to the user.
        ''' </summary>
        Private ReadOnly _userPrompter As IUserPrompter

        ''' <summary>
        ''' The user input reader used for reading user input.
        ''' </summary>
        Private ReadOnly _userInputReader As IUserInputReader

        ''' <summary>
        ''' The program path validator used for validating program paths.
        ''' </summary>
        Private ReadOnly _programPathValidator As IProgramPathValidator

        ''' <summary>
        ''' The app data manager used for managing application data directories.
        ''' </summary>
        Private ReadOnly _appDataManager As IAppDataManager

        ''' <summary>
        ''' The service provider used to resolve services.
        ''' </summary>
        Private ReadOnly _serviceProvider As IServiceProvider

        ''' <summary>
        ''' The extractor service used for extracting resources and archives.
        ''' </summary>
        Private ReadOnly _extractorService As IExtractorService

        ''' <summary>
        ''' The app settings manager used to manage application settings.
        ''' </summary>
        Private ReadOnly _appSettingsManager As IAppSettingsManager

        ''' <summary>
        ''' The failure handler used to handle failures by prompting the user and exiting the application with an error.
        ''' </summary>
        Private ReadOnly _failureHandler As IFailureHandler

        ''' <summary>
        ''' The service deployment application runner used for managing service deployment operations.
        ''' </summary>
        Private ReadOnly _serviceDeploymentAppRunner As IServiceDeploymentAppRunner

        ''' <summary>
        ''' Initializes a new instance of the <see cref="AppRunner"/> class.
        ''' </summary>
        ''' <param name="serviceProvider">
        ''' An instance of <see cref="IServiceProvider"/> used to resolve services.
        ''' </param>
        ''' <param name="userPrompter">
        ''' An instance of <see cref="IUserPrompter"/> used to display messages to the user.
        ''' </param>
        ''' <param name="userInputReader">
        ''' An instance of <see cref="IUserInputReader"/> used to read user input.
        ''' </param>
        ''' <param name="programPathValidator">
        ''' An instance of <see cref="IProgramPathValidator"/> used to validate program paths.
        ''' </param>
        ''' <param name="appDataManager">
        ''' An instance of <see cref="IAppDataManager"/> used to manage application data directories.
        ''' </param>
        ''' <param name="extractorService">
        ''' An instance of <see cref="IExtractorService"/> used to extract resources and archives.
        ''' </param>
        ''' <param name="appSettingsManager">
        ''' An instance of <see cref="IAppSettingsManager"/> used to manage application settings.
        ''' </param>
        ''' <param name="failureHandler">
        ''' An instance of <see cref="IFailureHandler"/> used to handle failures.
        ''' </param>
        ''' <param name="serviceDeploymentAppRunner">
        ''' An instance of <see cref="IServiceDeploymentAppRunner"/> used to manage service deployment.
        ''' </param>
        ''' <remarks>
        ''' The constructor takes an <see cref="IServiceProvider"/>, an <see cref="IUserPrompter"/>, 
        ''' an <see cref="IUserInputReader"/>, an <see cref="IProgramPathValidator"/>, an <see cref="IAppDataManager"/>, 
        ''' an <see cref="IExtractorService"/>, an <see cref="IAppSettingsManager"/>, an <see cref="IFailureHandler"/>, 
        ''' and an <see cref="IServiceDeploymentAppRunner"/> as parameters and assigns them to the corresponding fields.
        ''' </remarks>
        Public Sub New(serviceProvider As IServiceProvider, userPrompter As IUserPrompter, userInputReader As IUserInputReader, 
                       programPathValidator As IProgramPathValidator, appDataManager As IAppDataManager, extractorService As IExtractorService, 
                       appSettingsManager As IAppSettingsManager, failureHandler As IFailureHandler, serviceDeploymentAppRunner As IServiceDeploymentAppRunner)
            _serviceProvider = serviceProvider
            _userPrompter = userPrompter
            _userInputReader = userInputReader
            _programPathValidator = programPathValidator
            _appDataManager = appDataManager
            _extractorService = extractorService
            _appSettingsManager = appSettingsManager
            _failureHandler = failureHandler
            _serviceDeploymentAppRunner = serviceDeploymentAppRunner
        End Sub

        ''' <summary>
        ''' Runs the application asynchronously.
        ''' </summary>
        ''' <returns>
        ''' A <see cref="Task"/> representing the asynchronous operation.
        ''' </returns>
        ''' <remarks>
        ''' This method contains the main logic for running the application. It uses the <see cref="IAppDataManager"/> to create necessary directories,
        ''' retrieves the <see cref="IProgramPathValidator"/> to validate the program path, and displays the result to the user.
        ''' It is expected to be called at the application's entry point to start the execution process.
        ''' The <see cref="IServiceDeploymentAppRunner"/> is used to handle service deployment tasks.
        ''' </remarks>
        ''' <see cref="IAppRunner.RunAsync"/> method for executing the application logic.
        Friend Async Function RunAsync() As Task Implements IAppRunner.RunAsync
            Dim programPath = GetValidProgramPath()
            PromptUser(programPath)
            Await EnsureDirectoriesAndFilesExistAsync()
            Await WriteSettingsAsync()
            Await ExecuteServiceDeploymentAsync()
            ReadUserInput()
        End Function

        ''' <summary>
        ''' Gets the valid program path.
        ''' </summary>
        ''' <returns>The valid program path.</returns>
        Private Function GetValidProgramPath() As String
            Return _programPathValidator.GetValidProgramPath()
        End Function

        ''' <summary>
        ''' Prompts the user with the program path.
        ''' </summary>
        ''' <param name="programPath">The program path.</param>
        Private Sub PromptUser(programPath As String)
            _userPrompter.Prompt($"Attempting to launch the program at path '{programPath}'.")
        End Sub

        ''' <summary>
        ''' Ensures that the service directory and necessary files exist.
        ''' This method delegates the directory creation to the <see cref="_appDataManager"/> and uses the <see cref="_extractorService"/> to extract resources and archives.
        ''' </summary>
        Friend Async Function EnsureDirectoriesAndFilesExistAsync() As Task
            _appDataManager.CreateServiceDirectory()
            Await _extractorService.ExtractAllWithMessagingAsync()
        End Function

        ''' <summary>
        ''' Asynchronously writes the application settings using the <see cref=".WriteSettings"/> method.
        ''' </summary>
        ''' <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        ''' <remarks>
        ''' This method attempts to write settings using the <see cref="IAppSettingsManager.WriteSettings"/> method. If the 
        ''' operation fails, it calls the <see cref="IFailureHandler.HandleFailure"/> method with an appropriate message. 
        ''' Exceptions are also caught and handled by reporting a failure through the same mechanism.
        ''' </remarks>
        Private Async Function WriteSettingsAsync() As Task
            Try
                Dim writeSuccess As Boolean = Await _appSettingsManager.WriteSettings()
                If Not writeSuccess Then
                    _failureHandler.HandleFailure("Writing settings was not successful, but no specific errors were reported.")
                End If
            Catch ex As Exception
                _failureHandler.HandleFailure("An error occurred while writing settings.")
            End Try
        End Function

        ''' <summary>
        ''' Executes the service deployment process asynchronously.
        ''' </summary>
        ''' <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Private Async Function ExecuteServiceDeploymentAsync() As Task
            Dim deploySuccess = Await _serviceDeploymentAppRunner.RunAsync()
            If Not deploySuccess Then
                _failureHandler.HandleFailure("Service deployment failed.")
            End If
        End Function

        ''' <summary>
        ''' Reads the user input.
        ''' </summary>
        Private Sub ReadUserInput()
            _userInputReader.ReadInput()
        End Sub
    End Class
End Namespace
