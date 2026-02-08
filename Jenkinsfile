pipeline {
    agent any

    stages {

        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Restore MVC (NuGet)') {
            steps {
                bat '''
                if not exist nuget.exe (
                    powershell -Command "Invoke-WebRequest https://dist.nuget.org/win-x86-commandline/latest/nuget.exe -OutFile nuget.exe"
                )
                nuget.exe restore ferre2.csproj -SolutionDirectory .
                '''
            }
        }

        stage('Build MVC') {
            steps {
                bat '''
                "C:\\Program Files (x86)\\Microsoft Visual Studio\\18\\BuildTools\\MSBuild\\Current\\Bin\\MSBuild.exe" ferre2.csproj /p:Configuration=Debug
                '''
            }
        }

        stage('Run xUnit Tests') {
            steps {
                bat '''
                dotnet test Ferre2.Tests/Ferre2.Tests.csproj --configuration Debug
                '''
            }
        }
    }

    post {
        success {
            echo '✅ Build y pruebas ejecutadas correctamente'
        }
        failure {
            echo '❌ Falló la compilación o las pruebas'
        }
    }
}




